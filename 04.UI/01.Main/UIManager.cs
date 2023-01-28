using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : Singletone<UIManager>
{
    Canvas[] UI; //UI ������Ʈ���� ĵ���� ������Ʈ�� ���� �迭

    void Start()
    {
        UI = GetComponentsInChildren<Canvas>();
    }

    void Update()
    {
        switch (SceneLoadManager.Instance.CurrentScene) //���� ���� ����
        {
            default:
                for (int i = 0; i < UI.Length; i++)
                {
                    UI[i].enabled = true ; //�⺻�����δ� UI�� ���̴� ����
                }
                break;

            case SceneLoadManager.SceneStart.LOADING: //�ε����̶��
                for (int i = 0; i < UI.Length; i++)
                {
                    UI[i].enabled = false; //UI�� ������ �ʰ� �Ѵ�.
                }             
                break;
        }
    }

    public void Back_FirstSceneFunc()
    {
        SceneLoadManager.Instance.SceneTransOK(true); //�� ��ȯ ���� ���·� ����
        SceneManager.LoadScene(1);                    //�ε� ������ ��ȯ

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Debug.Log("ȣ��");
            Player_Data.Instance.transform.position = new Vector3(-40, 0, 0); //�÷��̾� ��ġ �ʱ�ȭ
            GameObject.Find("GameOver_Text_Button").gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("GameOver_Text_Button").gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Player_Data.Instance.playerData.isDie = false;
            Player_Data.Instance.playerData.Hp = Player_Data.Instance.playerData.MaxHp;
            Player_Data.Instance.GetComponent<CapsuleCollider>().enabled = true;
            Player_Data.Instance.GetComponent<Player_Combat>().ani.SetTrigger("gameOver");
        }
    }
}