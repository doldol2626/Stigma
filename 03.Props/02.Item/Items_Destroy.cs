using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // �ε��� ��ü�� �±װ� Player���
        {
            if (!GetComponentInParent<Items_Farming>().IsNoSpace()) //�κ��丮�� ������ ���� ���
            {
                Player_Data.Instance.CheckItem(transform.parent.gameObject); //������ ȹ�� �Լ� ����
                Destroy(transform.parent.gameObject); //�θ�(�� ���� ������Ʈ) ����
            }
            else //�κ��丮�� ������ ���� ���
            {
                FindObjectOfType<UI_ChatLog>().SystemLog("�κ��丮�� �����մϴ�.");
                return; //�ý��� �޽��� ����, �Լ� ����������
            }
        }
    }
}
