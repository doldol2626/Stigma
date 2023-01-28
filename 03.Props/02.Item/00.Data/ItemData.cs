using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �޴��ȿ� ���� �޴��� ����� ��
[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    public int itemType; //������ ����
    //0:�Һ�, 1:���, 2:��Ÿ, 3:����

    public string itemName;  //������ �̸�
    public string itemInfo;  //������ ����
    public Sprite itemImage; //������ �̹���

    public int Attack;  //���ݷ�
    public int Defence; //����
    public int Hp;      //ü��
    public int Mp;      //����

    public int count; //������ ������
    public int price; //�ǸŰ�
    public bool cash; //ĳ�� ������ ����. true��� ���� �Ǹ� �Ұ�
}
