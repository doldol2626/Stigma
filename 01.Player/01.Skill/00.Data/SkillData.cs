using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �޴��ȿ� ���� �޴��� ����� ��
[CreateAssetMenu(fileName = "SkillData", menuName = "Data/SkillData", order = 0)]
public class SkillData : ScriptableObject
{
    //����
    public string SkillType; //��ų ����
    public string SkillName; //��ų �̸�
    public string SkillInfo; //��ų ����
    public Sprite SkillIcon; //��ų �̹���

    public int Level;      //��ų ��뿡 �ʿ��� ����

    public int Mp; //��� ����
    public int Cp; //��� Ŭ���� ������

    public float CoolTime;    //��ų ��Ÿ��
    public float CurrentTime; //���� ���� ��Ÿ��

    //���ݽ�ų
    public float Attack; //����  

    //������ų
    public float Percentage; //�ɷ�ġ ������
    public float Duration;   //���� ���� �ð�
    public float BuffRemain; //���� ���� �ð�
}