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
        if (player != null)//target�� �����Ѵٸ� �� �÷��̾ �����Ѵٸ�
        {
            transform.forward = Camera.main.transform.forward; // �ڽ��� ������ ī�޶� ����� ��ġ ��Ų��
        }
    }
}