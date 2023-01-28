using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackEvent : MonoBehaviour
{
    // Attack 오브젝트의 Collider 컴포넌트 변수
    public Collider attackcol;

    // 애니메이션 이벤트 함수 호출을 위한 함수
    public void AttackEvent()
    {
        // 코루틴을 호출
        StartCoroutine(AttackOn());
    }

    // kinematic 제어를 위한 코루틴 함수
    IEnumerator AttackOn()
    {
        // 애니메이션 이벤트가 발생하면 콜라이더 컴포넌트를 켰다가
        attackcol.enabled = true;

        yield return new WaitForSeconds(0.01f); // 0.01초 뒤에

        // 콜라이더 컴포넌트를 다시 켜준다.
        attackcol.enabled = false;
    }
}
