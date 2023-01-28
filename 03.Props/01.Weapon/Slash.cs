using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float speed = 1000f; // ���� �ӵ�
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed); //������ �ٵ� ������ ���ư�
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.7f); // 0.7�ʵڿ� �������
    }
}
