using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackEvent : MonoBehaviour
{
    // Attack ������Ʈ�� Collider ������Ʈ ����
    public Collider attackcol;

    // �ִϸ��̼� �̺�Ʈ �Լ� ȣ���� ���� �Լ�
    public void AttackEvent()
    {
        // �ڷ�ƾ�� ȣ��
        StartCoroutine(AttackOn());
    }

    // kinematic ��� ���� �ڷ�ƾ �Լ�
    IEnumerator AttackOn()
    {
        // �ִϸ��̼� �̺�Ʈ�� �߻��ϸ� �ݶ��̴� ������Ʈ�� �״ٰ�
        attackcol.enabled = true;

        yield return new WaitForSeconds(0.01f); // 0.01�� �ڿ�

        // �ݶ��̴� ������Ʈ�� �ٽ� ���ش�.
        attackcol.enabled = false;
    }
}
