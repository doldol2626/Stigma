using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade_Ontrigger : MonoBehaviour
{
    //M67�����鿡 �� ��ũ��Ʈ�پ�����
    Collider col;
   void Start()
    {
        col = GetComponent<Collider>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        //���� Ground�±׸� ���� ������Ʈ�� �浹 �Ǵ� Cube�±׸� ���� ������Ʈ�� �浹 �ϸ�
        //�� ��ũ��Ʈ�� �پ��ִ� M67������ ������Ʈ�� Ʈ��Ŀ�� false��Ų��.
        if (other.gameObject.tag=="Ground"||other.gameObject.tag=="Enemy")
        {
            col.isTrigger = false;
        }
    }
}
