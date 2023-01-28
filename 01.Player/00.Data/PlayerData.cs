using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �޴��ȿ� ���� �޴��� ����� ��
[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public string Pname; //�̸�

    [Header("ü��")]
    public int Hp;       //�⺻ ü��
    public int MaxHp;    //�ִ� ü��
    public int PlusHp;   //��ȣ�� ü��

    [Header("����")]
    public int Mp;       //�⺻ ����
    public int MaxMp;    //�ִ� ����

    [Header("Ŭ����")]
    public int Cp;       //Ŭ���� ������
    public int MaxCp;    //�ִ� Ŭ���� ������

    [Header("�ɷ�ġ")]
    public int Attack;   //���ݷ�
    public int Defence;  //����
    public float Speed;  //�̵��ӵ�

    [Header("�⺻ �ɷ�ġ")]
    public int oAttack;  //�⺻ ���ݷ�
    public int oDefence; //�⺻ ����
    public float oSpeed; //�⺻ �̵��ӵ�

    [Header("�÷��̾� ����")]
    public int Level;    //����
    public int Exp;      //����ġ
    public int MaxExp;   //�ִ� ����ġ

    [Header("������")]
    public int Money;    //������

    [Header("�÷��̾� ����")]
    public bool isDie = false;
}
