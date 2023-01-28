using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player") //�浹ü�� �÷��̾���
        {
            FindObjectOfType<NoticeText>().ChangeText("FŰ�� ���� �̵�"); //ȭ�� �߾ӿ� �̵� ��� ǥ��

            if (Input.GetKey(KeyCode.F)) //FŰ �Է� ��
            {
                if (gameObject.name == "NextEntrance") //�̱��� ������ ���
                {
                    FindObjectOfType<UI_ChatLog>().SystemLog("����� ������ �Ұ����� �����Դϴ�.");
                    return; //ä��â �α׿� ���� �޽��� ǥ�� �� ����������
                }

                SceneLoadManager.Instance.SceneTransOK(true); //�� ��ȯ ���� ���·� ����
                SceneManager.LoadScene(1);                    //�ε� ������ ��ȯ

                Player_Data.Instance.transform.position = new Vector3(-40, 0, 0); //�÷��̾� ��ġ �ʱ�ȭ
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        FindObjectOfType<NoticeText>().ChangeText(null);
        //ȭ�� �߾ӿ� ǥ�õƴ� �ؽ�Ʈ ����
    }
}
