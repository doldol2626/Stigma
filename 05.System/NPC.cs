using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject canvas; // ĵ����

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F)) // �±װ� �÷��̾��̰� F�� �����ٸ�
        {
            canvas.SetActive(true); // ĵ���� Ȱ��ȭ
        }
    }
}
