using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data : Singletone<Player_Data>
{
    public PlayerData playerData; //플레이어 데이터

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public List<ItemData> potionInven = new(); //소비칸
    public List<ItemData> equipInven = new();  //장비칸
    public List<ItemData> extraInven = new();  //기타칸
                                               //획득한 아이템의 ItemData 컴포넌트를 담을 리스트(=인벤토리)

    public NoticeText noticeText; // 레벨업 했을 때 중앙에 표시될 텍스트 컴포넌트 스크립트

    public void LevelUp()
    {
        int extra = playerData.Exp - playerData.MaxExp; //남은 경험치는 현재 경험치에서 최대 경험치를 뺀 것
        playerData.Exp = extra; //플레이어의 경험치를 나머지 경험치로 변경

        playerData.Level += 1; //기존 레벨에 1을 더함
        playerData.MaxExp = playerData.Level * 800; //새 최대 경험치 값은 레벨 값에 800을 곱한 값

        playerData.MaxHp = (int)(playerData.MaxHp * 1.2f); //최대 체력값을 기존의 1.2배로 변경
        playerData.Hp = playerData.MaxHp; //현재 체력을 변경된 최대 체력으로 변경

        playerData.MaxMp = (int)(playerData.MaxMp * 1.2f); //최대 마나값을 기존의 1.2배로 변경
        playerData.Mp = playerData.MaxMp; //현재 마나를 변경된 최대 마나으로 변경

        playerData.oAttack = (int)(playerData.oAttack * 1.2f); //최대 공격력을 기존의 1.2배로 변경
        playerData.Attack = playerData.oAttack; //현재 공격력을 변경된 최대 공격력으로 변경

        playerData.oDefence = (int)(playerData.oDefence * 1.2f); //최대 방어력을 기존의 1.2배로 변경
        playerData.Defence = playerData.oDefence; //현재 방어력을 변경된 최대 방어력으로 변경

        UI_StatusBar.Instance.LvTextUpdate(); // 플레이어 경험치 바 위에 레벨 텍스트를 플레이어 데이터에 맞게 수정

        UI_ChatLog.Instance.SystemLog("Level" + playerData.Level + "이(가) 되셨습니다. 축하합니다!"); // 채팅 로그에 뜰 텍스트

        noticeText.InfoText("<color=#F3FF00>LEVEL UP</color>"); // 레벨업을 알리며 중앙에 뜰 텍스트 UI 함수 호출
    }

    public void CheckItem(GameObject item)
    {
        ItemData itemData = item.GetComponent<Items_Farming>().itemData;

        if (itemData.itemType == 0) { GotPotion(itemData); }     //소비 아이템 획득
        else if (itemData.itemType == 1) { GotEquip(itemData); } //장비 아이템 획득
        else if (itemData.itemType == 2) { GotExtra(itemData); } //기타 아이템 획득
        else { GotCoin(itemData); }                               //코인 획득

        //캐시 아이템 여부는 bool타입 변수 ItemData.cash로 확인
    }

    void GotPotion(ItemData itemData)
    {
        itemData.count++; //소지량 1 Up

        if (!potionInven.Contains(itemData)) //리스트에 이 아이템이 없다면
        {
            potionInven.Add(itemData); //리스트에 아이템데이터 추가
            potionInven.TrimExcess();  //불필요한 메모리 반환
        }

        //콘솔창에 획득 아이템 설명 표시
        Debug.Log(itemData.itemName + " 보유량: " + itemData.count);
        Debug.Log(itemData.itemInfo);

        UI_ChatLog.Instance.SystemLog(itemData.itemName + " +1 획득"); // 소비아이템 획득시 이름과 수량 로그에 표시
    }

    void GotEquip(ItemData itemData)
    {
        equipInven.Add(itemData); //리스트에 아이템데이터 추가
        equipInven.TrimExcess();  //불필요한 메모리 반환

        //콘솔창에 획득 아이템 설명 표시
        Debug.Log(itemData.itemName);
        Debug.Log(itemData.itemInfo);

        UI_ChatLog.Instance.SystemLog(itemData.itemName + " +1 획득"); // 장비아이템 획득시 이름과 수량 로그에 표시
    }

    void GotExtra(ItemData itemData)
    {
        itemData.count++; //소지량 1 Up

        if (!extraInven.Contains(itemData)) //리스트에 이 아이템이 없다면
        {
            extraInven.Add(itemData); //리스트에 아이템데이터 추가
            extraInven.TrimExcess();  //불필요한 메모리 반환
        }

        Debug.Log(itemData.itemInfo);

        UI_ChatLog.Instance.SystemLog(itemData.itemName + " +1 획득"); // 아이템 획득시 이름과 수량 로그에 표시
    }

    void GotCoin(ItemData itemData)
    {
        //랜덤으로 획득 코인 지정
        //기본수치 * 플레이어 레벨 * 0.8 ~ 1.2를 정수값으로 변환한 값
        //예) (int)(100(기본수치) * 플레이어 레벨 10 * 랜덤값 0.9) = 900
        int price = (int)(itemData.price * playerData.Level * Random.Range(0.8f, 1.2f));

        playerData.Money += price; //소지금에 획득 코인을 더해준다.

        FindObjectOfType<InventoryUI>().UpdateMoney();

        Debug.Log("총 소지금: " + playerData.Money); //콘솔창에 소지금 표시

        UI_ChatLog.Instance.SystemLog("+" + price + " Gold 획득"); // 코인 획득시 이름과 수량 로그에 표시
    }
}