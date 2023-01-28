using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : Singletone<UIManager>
{
    Canvas[] UI; //UI 오브젝트들의 캔버스 컴포넌트를 담을 배열

    void Start()
    {
        UI = GetComponentsInChildren<Canvas>();
    }

    void Update()
    {
        switch (SceneLoadManager.Instance.CurrentScene) //현재 씬에 따라서
        {
            default:
                for (int i = 0; i < UI.Length; i++)
                {
                    UI[i].enabled = true ; //기본적으로는 UI가 보이는 상태
                }
                break;

            case SceneLoadManager.SceneStart.LOADING: //로딩중이라면
                for (int i = 0; i < UI.Length; i++)
                {
                    UI[i].enabled = false; //UI를 보이지 않게 한다.
                }             
                break;
        }
    }

    public void Back_FirstSceneFunc()
    {
        SceneLoadManager.Instance.SceneTransOK(true); //씬 전환 가능 상태로 변경
        SceneManager.LoadScene(1);                    //로딩 씬으로 전환

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Debug.Log("호출");
            Player_Data.Instance.transform.position = new Vector3(-40, 0, 0); //플레이어 위치 초기화
            GameObject.Find("GameOver_Text_Button").gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("GameOver_Text_Button").gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Player_Data.Instance.playerData.isDie = false;
            Player_Data.Instance.playerData.Hp = Player_Data.Instance.playerData.MaxHp;
            Player_Data.Instance.GetComponent<CapsuleCollider>().enabled = true;
            Player_Data.Instance.GetComponent<Player_Combat>().ani.SetTrigger("gameOver");
        }
    }
}