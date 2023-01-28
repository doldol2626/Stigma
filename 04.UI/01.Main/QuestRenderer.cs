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
    public GameObject canvas; // ĵ����
    public Button perviousPageButton, nextPageButton;
    public GameObject takeButton;
    public Image questImg;
    public TextMeshProUGUI desciptionTxt;
    public GameObject questPre; // ����ǥ ������

     Text quickquestTxt; // ������Ʈ ����
     Text quickquestNo; // ������Ʈ ���� ����

    public List<QuestDataProperty> questData;

    int currentPage;

    public GameObject player; //�÷��̾�
    // Start is called before the first frame update
    void Start()
    {

        currentPage = 0;
        desciptionTxt.text = questData[currentPage].description;
        questImg.sprite = questData[currentPage].sprite;

        // �� �ٸ� ���� �����ϱ� �̸����� ã���ֱ�
        quickquestTxt = GameObject.Find("QuickQuestTxt").GetComponent<Text>();
        quickquestNo = GameObject.Find("QuickQuestNo").GetComponent<Text>();
        player = GameObject.Find("Player");

    }


    public void nextQuest() // ���� �������� �Ѿ�� �Լ�
    {
        currentPage++;
        desciptionTxt.text = questData[currentPage].description;
        questImg.sprite = questData[currentPage].sprite;

    }

    public void PrevQuest() // �� �������� ���ư��� �Լ�
    {
        currentPage--;
        desciptionTxt.text = questData[currentPage].description;
        questImg.sprite = questData[currentPage].sprite;

    }

    public void TakeQuest() // ����Ʈ ���� �Լ�
    {
        quickquestTxt.text = "����ŷ�� ����";
        quickquestNo.text = "����ŷ" + player.GetComponent<Player_Combat>().skulldeath.ToString() + "/1";
        StartCoroutine("QuickQuest");
    }

    IEnumerator QuickQuest()
    {

        // �ϴ� ���븦 ���� �� �ִ��� ������ �ȵǹǷ� �� ��ũ��Ʈ�� �۵��ϴ����� �ľ��غ���
       // quickquestTxt.text = "����ŷ�� ��� ���� �������� ��������";
        //quickquestNo.text = "���� ������" + "0/" +"0";
        yield return new WaitForSeconds(0.1f);
        canvas.SetActive(false); // ĵ���� ��Ȱ��ȭ
        questPre.SetActive(false); // ����ǥ ��Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPage >= questData.Count - 1) // �� �������� �����ߴٸ�
        {
            nextPageButton.interactable = false; // ������ư ��Ȱ��ȭ �� ������ư Ȱ��ȭ
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
