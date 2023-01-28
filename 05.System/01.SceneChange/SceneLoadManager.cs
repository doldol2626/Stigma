using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singletone<SceneLoadManager>
{
    public enum SceneStart
    {
        TITLE,
        LOADING,
        START,
        FIRSTTOWN,
        FOREST,
    };

    [SerializeField] SceneStart currentScene; //현재 씬
    [SerializeField] SceneStart nextScene;    //다음 씬

    bool sceneTransOK; //씬을 전환해도 되는 상황인지 판단

    AsyncOperation operation; //현재 불러오고 있는 씬
    Slider loadingSlider;     //로딩 슬라이더

    public bool isLoading;

    public NoticeText noticeText;

    public SceneStart CurrentScene => currentScene;
    //currentScene을 반환하는 프로퍼티(캡슐화)

    public void SceneTransOK(bool isOK) => sceneTransOK = isOK;
    //sceneTransOK를 입력값으로 설정하는 함수(캡슐화)

    void Start()
    {
        SceneTransOK(true);               //씬 전환 가능 상태
        nextScene = SceneStart.FIRSTTOWN; //다음 씬을 마을로 설정
    }

    void Update()
    {
        currentScene = (SceneStart)SceneManager.GetActiveScene().buildIndex; //현재 씬 정보 반영
        ChooseScene(); //씬 선택 함수 실행

        if (currentScene == SceneStart.LOADING) { isLoading = true; }
        else { isLoading = false; }
    }

    IEnumerator SceneLoad()
    {
        yield return null;

        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
        //로딩 씬에서 슬라이더 불러오기

        operation = SceneManager.LoadSceneAsync((int)nextScene);
        //비동기 방식으로 씬을 불러오는 도중에도 다른 작업을 할 수있는 LoadSceneAsync 함수
        //반대로 그냥 LoadScene은 씬을 불러오는 도중에 다른 작업을 할 수가 없다.
        //대체로 LoadSceneAsync을 쓸때에는 코루틴이랑 같이 쓰는게 좋다함.

        operation.allowSceneActivation = false; //로딩이 끝나면 씬을 바로 시작 못하게 한다

        //isDone이 false일 때 동안(while문으로 계속 반복), 즉 로딩이 끝나서 isDone이 true가 되기 전까지 계속 반복
        while (!operation.isDone)
        {
            yield return null;

            if (operation.progress < 0.9f) //로딩중
            {
                loadingSlider.value = operation.progress; //슬라이더에 씬을 불러온 정도 반영
            }
            else //로딩 완료
            {
                loadingSlider.value = Mathf.MoveTowards(loadingSlider.value, 1f, Time.deltaTime * 0.3f);
                //Time.deltaTime에 *0.3f 추가. 이 추가 부분을 전역변수 하나를 만든 후
                //인스펙터창에서 조절하거나 수치를 변경해서 테스트해보면서 값을 바꿔가면 될 듯하다.

                if (!SceneManager.GetSceneByBuildIndex(2).isLoaded)
                {
                    SceneManager.LoadScene(2, LoadSceneMode.Additive); //StartScene 추가
                }

                operation.allowSceneActivation = true; //로딩이 끝났으므로 씬을 시작시킨다.
            }
        }
    }

    void ChooseScene()
    {
        while (sceneTransOK) //씬 전환 가능 상태일 경우
        {
            switch (currentScene) //현재 씬에 따라 명령 실행
            {
                case SceneStart.TITLE:
                    if (Input.anyKeyDown) { SceneManager.LoadScene(1); }
                    //아무 키나 입력되면 로딩 씬으로 전환
                    break;

                case SceneStart.LOADING:
                    StartCoroutine(SceneLoad()); //씬 전환 코루틴 실행
                    SceneTransOK(false); //씬 전환이 완료되면 씬 전환 불가 상태로 변경

                    break;

                case SceneStart.FIRSTTOWN:
                    nextScene = SceneStart.FOREST; //다음 씬을 숲으로 설정
                    break;

                case SceneStart.FOREST:
                    nextScene = SceneStart.FIRSTTOWN; //다음 씬을 마을로 설정
                    break;
            }
            break;
        }
    }
}