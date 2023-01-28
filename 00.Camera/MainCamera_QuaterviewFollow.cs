using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_QuaterviewFollow : MonoBehaviour
{
    public Transform target; // Player

    public Quaternion rotation;
    public Vector3 offset; // ���� ����

    void Start()
    {
        target = Player_Data.Instance.gameObject.transform;
    }

    void Update()
    {
        if (target != null)//target�� �����Ѵٸ� �� �÷��̾ �����Ѵٸ�
        {
            transform.position = target.position + offset;
            transform.rotation = rotation;
        }
    }
}