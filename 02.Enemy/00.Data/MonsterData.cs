using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �޴��ȿ� ���� �޴��� ����� ��
[CreateAssetMenu(fileName = "MonsterData", menuName = "Data/MonsterData", order = 0)]
public class MonsterData : ScriptableObject
{
    [Header("���� Ư��")]
    public string Pname;
    public int MaxHp;
    public int Attack;
    public int Level;
    public int Exp;
    public float moveSpeed = 5.0f;// �����̴� �ӵ� ����

    [Header("�Ÿ� ����")]
    // �Ÿ� �Ǵ��� ���� ���� (MonsterData�� �ٲ�� �� ��)
    public float findDistance; // Player�� ã�� �Ÿ�
    public float attackDistance; // �����ϴ� ����
    public float limitDistance; // ���Ͱ� �Ѿư��� �Ѱ� ����

    [Header("����")]
    public GameObject Prefab; // ������ ������ ������Ʈ
    public float startCreateTime; // ���� ���� �ð�
    public float createMinTime; // �ּ� �ݺ� �ֱ� ����
    public float createMaxTime; // �ִ� �ݺ� �ֱ� ����
    public int createLimitNumber; // �Ѱ� ���� ��
}
