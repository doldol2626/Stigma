using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Attack : MonoBehaviour
{
    // BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        /*boxCollider = GetComponent<BoxCollider>();*/
    }


    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Fire1"))// ���콺 ����Ŭ���� Ʈ���� Ȱ��ȭ
        {
            boxCollider.isTrigger = true;
            StartCoroutine(EnemyHit()); // �ڷ�ƾ�� �̿��Ͽ� �ð� ����

        }*/

    }

    /*IEnumerator EnemyHit() //1�ʵ��� Ʈ���Ű� �����ǰ� �ٽ� ������
    {
        yield return new WaitForSeconds(1f);
        boxCollider.isTrigger = false;
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // ��ũ��Ʈ Enemy�� �ִ� TakeDamage�� �Լ� ȣ��
            other.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Player_Data>().playerData.Attack);

        }
    }
}