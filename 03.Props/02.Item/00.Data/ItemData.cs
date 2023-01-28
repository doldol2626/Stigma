using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성 메뉴안에 하위 메뉴를 만들어 줌
[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    public int itemType; //아이템 유형
    //0:소비, 1:장비, 2:기타, 3:코인

    public string itemName;  //아이템 이름
    public string itemInfo;  //아이템 설명
    public Sprite itemImage; //아이템 이미지

    public int Attack;  //공격력
    public int Defence; //방어력
    public int Hp;      //체력
    public int Mp;      //마나

    public int count; //아이템 보유량
    public int price; //판매가
    public bool cash; //캐시 아이템 여부. true라면 상점 판매 불가
}
