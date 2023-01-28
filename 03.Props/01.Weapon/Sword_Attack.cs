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
        /*if (Input.GetButtonDown("Fire1"))// 마우스 왼쪽클릭시 트리거 활성화
        {
            boxCollider.isTrigger = true;
            StartCoroutine(EnemyHit()); // 코루틴을 이용하여 시간 유지

        }*/

    }

    /*IEnumerator EnemyHit() //1초동안 트리거가 유지되고 다시 꺼진다
    {
        yield return new WaitForSeconds(1f);
        boxCollider.isTrigger = false;
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 스크립트 Enemy가 있는 TakeDamage의 함수 호출
            other.GetComponent<Enemy>().TakeDamage(GetComponentInParent<Player_Data>().playerData.Attack);

        }
    }
}