using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestRenderer : MonoBehaviour
{
    [System.Serializable]
    public class QuestDataProperty
    {
        public Sprite sprite;
        public string description;

    }
    public GameObject canvas; // 캔버스
    public Button perviousPageButton, nextPageButton;
    public GameObject takeButton;
    public Image questImg;
    public TextMeshProUGUI desciptionTxt;
    public GameObject questPre; // 느낌표 아이콘

     Text quickquestTxt; // 퀵퀘스트 내용
     Text quickquestNo; // 퀵퀘스트 몬스터 숫자

    public List<QuestDataProperty> questData;

    int currentPage;

    public GameObject player; //플레이어
    // Start is called before the first frame update
    void Start()
    {

        currentPage = 0;
        desciptionTxt.text = questData[currentPage].description;
        questImg.sprite = questData[currentPage].sprite;

        // 다 다른 씬에 있으니까 이름으로 찾아주기
        quickquestTxt = GameObject.Find("QuickQuestTxt").GetComponent<Text>();
        quickquestNo = GameObject.Find("QuickQuestNo").GetComponent<Text>();
        player = GameObject.Find("Player");

    }


    public void nextQuest() // 다음 페이지로 넘어가는 함수
    {
        currentPage++;
        desciptionTxt.text = questData[currentPage].description;
        questImg.sprite = questData[currentPage].sprite;

    }

    public void PrevQuest() // 전 페이지로 돌아가는 함수
    {
        currentPage--;
        desciptionTxt.text = questData[currentPage].description;
        questImg.sprite = questData[currentPage].sprite;

    }

    public void TakeQuest() // 퀘스트 수락 함수
    {
        quickquestTxt.text = "스컬킹을 잡자";
        quickquestNo.text = "스컬킹" + player.GetComponent<Player_Combat>().skulldeath.ToString() + "/1";
        StartCoroutine("QuickQuest");
    }

    IEnumerator QuickQuest()
    {

        // 일단 늑대를 잡을 수 있는지 판정이 안되므로 이 스크립트가 작동하는지만 파악해보자
       // quickquestTxt.text = "스컬킹을 잡고 마을 통행증을 가져오자";
        //quickquestNo.text = "마을 통행증" + "0/" +"0";
        yield return new WaitForSeconds(0.1f);
        canvas.SetActive(false); // 캔버스 비활성화
        questPre.SetActive(false); // 느낌표 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPage >= questData.Count - 1) // 끝 페이지에 도달했다면
        {
            nextPageButton.interactable = false; // 다음버튼 비활성화 및 수락버튼 활성화
            takeButton.SetActive(true);
        }
        else
        {
            nextPageButton.interactable = true;
            takeButton.SetActive(false);
        }

        if (currentPage == 0)
        {
            perviousPageButton.interactable = false;
        }
        else
        {
            perviousPageButton.interactable = true;
        }
    }

}
