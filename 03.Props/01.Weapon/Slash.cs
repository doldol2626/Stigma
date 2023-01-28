using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float speed = 1000f; // 날라갈 속도
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed); //리지드 바디 힘으로 날아감
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.7f); // 0.7초뒤에 사라져라
    }
}
