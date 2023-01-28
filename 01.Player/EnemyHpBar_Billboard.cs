using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar_Billboard : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)//target이 존재한다면 즉 플레이어가 존재한다면
        {
            transform.forward = Camera.main.transform.forward; // 자신의 방향을 카메라 방향과 일치 시킨다
        }
    }
}