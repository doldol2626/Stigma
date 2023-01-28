using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_QuaterviewFollow : MonoBehaviour
{
    public Transform target; // Player

    public Quaternion rotation;
    public Vector3 offset; // 추후 조절

    void Start()
    {
        target = Player_Data.Instance.gameObject.transform;
    }

    void Update()
    {
        if (target != null)//target이 존재한다면 즉 플레이어가 존재한다면
        {
            transform.position = target.position + offset;
            transform.rotation = rotation;
        }
    }
}