using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject canvas; // 캔버스

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F)) // 태그가 플레이어이고 F를 누른다면
        {
            canvas.SetActive(true); // 캔버스 활성화
        }
    }
}
