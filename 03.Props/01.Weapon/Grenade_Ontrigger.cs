using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade_Ontrigger : MonoBehaviour
{
    //M67프리펩에 이 스크립트붙어있음
    Collider col;
   void Start()
    {
        col = GetComponent<Collider>(); 
    }
    private void OnTriggerEnter(Collider other)
    {
        //만약 Ground태그를 가진 오브젝트랑 충돌 또는 Cube태그를 가진 오브젝트랑 충돌 하면
        //이 스크립트가 붙어있는 M67프리펩 오브젝트의 트리커를 false시킨다.
        if (other.gameObject.tag=="Ground"||other.gameObject.tag=="Enemy")
        {
            col.isTrigger = false;
        }
    }
}
