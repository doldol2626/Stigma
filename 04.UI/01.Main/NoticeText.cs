using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NoticeText : MonoBehaviour
{
    TextMeshProUGUI noticeText; // 텍스트 컴포넌트를 불러오기 위한 변수

    [SerializeField] GameObject infoTextPP; // 중앙에 생성시킬 텍스트 프리팹 오브젝트
    List<GameObject> infoTextObj = new();   // 게임오브젝트를 1개만 만들기 위한 리스트
    GameObject infoText;                    // 생성된 텍스트 오브젝트 

    [SerializeField] float fadeAlphaSeconds = 3f;    //페이드아웃 대기 시간
    [SerializeField] float textDeleteSeconds = 3f;   //텍스트 삭제 대기 시간

    [SerializeField] TextMeshProUGUI textMapName;    //맵 아래에 표시될 맵이름 텍스트 UI

    Dictionary<int, string> sceneNameKorean = new(); //한국어 맵 이름을 담은 딕셔너리

    int sceneIndex; //현재 씬 넘버

    bool isChange = true; //씬이 로딩중인지 확인할 bool변수

    void Start()
    {
        // 알림 텍스트 컴포넌트를 불러온다
        noticeText = GetComponentInChildren<TextMeshProUGUI>();

        noticeText.text = null; // 알림 텍스트의 내용을 초기화

        //딕셔너리에 한국어 맵 이름 저장
        sceneNameKorean.Add((int)SceneLoadManager.SceneStart.TITLE, "메인 화면");
        sceneNameKorean.Add((int)SceneLoadManager.SceneStart.LOADING, "로딩중");
        sceneNameKorean.Add((int)SceneLoadManager.SceneStart.START, "시작");
        sceneNameKorean.Add((int)SceneLoadManager.SceneStart.FIRSTTOWN, "피습당한 마을");
        sceneNameKorean.Add((int)SceneLoadManager.SceneStart.FOREST, "저주받은 숲");
    }

    private void FixedUpdate()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex; //현재 씬 넘버를 sceneIndex에 대입

        if (SceneLoadManager.Instance.isLoading) { isChange = true; }
        //씬이 로딩중이라면, isChange를 true로 변경

        if (sceneIndex >= 3 && isChange) //현재 씬 넘버가 3 이상(마을 이후), isChange가 true라면
        {
            noticeText.text = null;

            // 맵 이름을 현재 씬 이름으로 설정 (원하는 이름으로 설정)
            //미니맵 아래에 NewText를 현재 씬 이름으로 바꾼다.
            textMapName.text = sceneNameKorean[sceneIndex];

            InfoText(sceneNameKorean[sceneIndex]); // 맵이름을 화면 중앙에 텍스트 UI로 표시

            isChange = false; //isChange를 false로 전환
        }
    }

    // Entrance에서 호출할 알림 텍스트 변경 함수
    public void ChangeText(string notice)
    {
        // 매개변수를 받아 알림 텍스트를 변경
        noticeText.text = notice;
    }

    // 알림 텍스트를 3초뒤에 사라지게 할 함수
    public void InfoText(string notice)
    {

        if (infoTextObj.Count < 1) //infoTextObj의 수가 하나보다 적다면
        {
            infoText = Instantiate(infoTextPP, transform); //이 오브젝트의 자식으로 noticetxtPP 프리팹 생성
            infoTextObj.Add(infoText);                     //infoText를 infoTextObj에 추가 

            TextMeshProUGUI info = infoText.GetComponent<TextMeshProUGUI>();
            //infoText에서 TextMeshProUGUI 컴포넌트 불러오기
            
            info.text = notice; //noticetxt의 텍스트를 전달받은 텍스트로 변경
            info.CrossFadeAlpha(0f, fadeAlphaSeconds, true); // 3초간 이미지 알파값을 0%로 변경

            StartCoroutine(DestroyText()); //텍스트 오브젝트 삭제 함수 실행
        }
    }

    IEnumerator DestroyText()
    {
        yield return new WaitForSeconds(textDeleteSeconds); //지정된 삭제 대기 시간만큼 대기
        Destroy(infoText);   //infoText 삭제
        infoTextObj.Clear(); //infoTextObj 리스트 클리어
    }
}
