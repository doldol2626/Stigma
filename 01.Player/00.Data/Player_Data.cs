using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : Singletone<Player_Data>
{
    public PlayerData playerData; //�÷��̾� ������

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public List<ItemData> potionInven = new(); //�Һ�ĭ
    public List<ItemData> equipInven = new();  //���ĭ
    public List<ItemData> extraInven = new();  //��Ÿĭ
                                               //ȹ���� �������� ItemData ������Ʈ�� ���� ����Ʈ(=�κ��丮)

    public NoticeText noticeText; // ������ ���� �� �߾ӿ� ǥ�õ� �ؽ�Ʈ ������Ʈ ��ũ��Ʈ

    public void LevelUp()
    {
        int extra = playerData.Exp - playerData.MaxExp; //���� ����ġ�� ���� ����ġ���� �ִ� ����ġ�� �� ��
        playerData.Exp = extra; //�÷��̾��� ����ġ�� ������ ����ġ�� ����

        playerData.Level += 1; //���� ������ 1�� ����
        playerData.MaxExp = playerData.Level * 800; //�� �ִ� ����ġ ���� ���� ���� 800�� ���� ��

        playerData.MaxHp = (int)(playerData.MaxHp * 1.2f); //�ִ� ü�°��� ������ 1.2��� ����
        playerData.Hp = playerData.MaxHp; //���� ü���� ����� �ִ� ü������ ����

        playerData.MaxMp = (int)(playerData.MaxMp * 1.2f); //�ִ� �������� ������ 1.2��� ����
        playerData.Mp = playerData.MaxMp; //���� ������ ����� �ִ� �������� ����

        playerData.oAttack = (int)(playerData.oAttack * 1.2f); //�ִ� ���ݷ��� ������ 1.2��� ����
        playerData.Attack = playerData.oAttack; //���� ���ݷ��� ����� �ִ� ���ݷ����� ����

        playerData.oDefence = (int)(playerData.oDefence * 1.2f); //�ִ� ������ ������ 1.2��� ����
        playerData.Defence = playerData.oDefence; //���� ������ ����� �ִ� �������� ����

        UI_StatusBar.Instance.LvTextUpdate(); // �÷��̾� ����ġ �� ���� ���� �ؽ�Ʈ�� �÷��̾� �����Ϳ� �°� ����

        UI_ChatLog.Instance.SystemLog("Level" + playerData.Level + "��(��) �Ǽ̽��ϴ�. �����մϴ�!"); // ä�� �α׿� �� �ؽ�Ʈ

        noticeText.InfoText("<color=#F3FF00>LEVEL UP</color>"); // �������� �˸��� �߾ӿ� �� �ؽ�Ʈ UI �Լ� ȣ��
    }

    public void CheckItem(GameObject item)
    {
        ItemData itemData = item.GetComponent<Items_Farming>().itemData;

        if (itemData.itemType == 0) { GotPotion(itemData); }     //�Һ� ������ ȹ��
        else if (itemData.itemType == 1) { GotEquip(itemData); } //��� ������ ȹ��
        else if (itemData.itemType == 2) { GotExtra(itemData); } //��Ÿ ������ ȹ��
        else { GotCoin(itemData); }                               //���� ȹ��

        //ĳ�� ������ ���δ� boolŸ�� ���� ItemData.cash�� Ȯ��
    }

    void GotPotion(ItemData itemData)
    {
        itemData.count++; //������ 1 Up

        if (!potionInven.Contains(itemData)) //����Ʈ�� �� �������� ���ٸ�
        {
            potionInven.Add(itemData); //����Ʈ�� �����۵����� �߰�
            potionInven.TrimExcess();  //���ʿ��� �޸� ��ȯ
        }

        //�ܼ�â�� ȹ�� ������ ���� ǥ��
        Debug.Log(itemData.itemName + " ������: " + itemData.count);
        Debug.Log(itemData.itemInfo);

        UI_ChatLog.Instance.SystemLog(itemData.itemName + " +1 ȹ��"); // �Һ������ ȹ��� �̸��� ���� �α׿� ǥ��
    }

    void GotEquip(ItemData itemData)
    {
        equipInven.Add(itemData); //����Ʈ�� �����۵����� �߰�
        equipInven.TrimExcess();  //���ʿ��� �޸� ��ȯ

        //�ܼ�â�� ȹ�� ������ ���� ǥ��
        Debug.Log(itemData.itemName);
        Debug.Log(itemData.itemInfo);

        UI_ChatLog.Instance.SystemLog(itemData.itemName + " +1 ȹ��"); // �������� ȹ��� �̸��� ���� �α׿� ǥ��
    }

    void GotExtra(ItemData itemData)
    {
        itemData.count++; //������ 1 Up

        if (!extraInven.Contains(itemData)) //����Ʈ�� �� �������� ���ٸ�
        {
            extraInven.Add(itemData); //����Ʈ�� �����۵����� �߰�
            extraInven.TrimExcess();  //���ʿ��� �޸� ��ȯ
        }

        Debug.Log(itemData.itemInfo);

        UI_ChatLog.Instance.SystemLog(itemData.itemName + " +1 ȹ��"); // ������ ȹ��� �̸��� ���� �α׿� ǥ��
    }

    void GotCoin(ItemData itemData)
    {
        //�������� ȹ�� ���� ����
        //�⺻��ġ * �÷��̾� ���� * 0.8 ~ 1.2�� ���������� ��ȯ�� ��
        //��) (int)(100(�⺻��ġ) * �÷��̾� ���� 10 * ������ 0.9) = 900
        int price = (int)(itemData.price * playerData.Level * Random.Range(0.8f, 1.2f));

        playerData.Money += price; //�����ݿ� ȹ�� ������ �����ش�.

        FindObjectOfType<InventoryUI>().UpdateMoney();

        Debug.Log("�� ������: " + playerData.Money); //�ܼ�â�� ������ ǥ��

        UI_ChatLog.Instance.SystemLog("+" + price + " Gold ȹ��"); // ���� ȹ��� �̸��� ���� �α׿� ǥ��
    }
}