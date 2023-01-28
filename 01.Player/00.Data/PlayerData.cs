using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성 메뉴안에 하위 메뉴를 만들어 줌
[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public string Pname; //이름

    [Header("체력")]
    public int Hp;       //기본 체력
    public int MaxHp;    //최대 체력
    public int PlusHp;   //보호막 체력

    [Header("마나")]
    public int Mp;       //기본 마나
    public int MaxMp;    //최대 마나

    [Header("클래스")]
    public int Cp;       //클래스 게이지
    public int MaxCp;    //최대 클래스 게이지

    [Header("능력치")]
    public int Attack;   //공격력
    public int Defence;  //방어력
    public float Speed;  //이동속도

    [Header("기본 능력치")]
    public int oAttack;  //기본 공격력
    public int oDefence; //기본 방어력
    public float oSpeed; //기본 이동속도

    [Header("플레이어 정보")]
    public int Level;    //레벨
    public int Exp;      //경험치
    public int MaxExp;   //최대 경험치

    [Header("소지금")]
    public int Money;    //소지금

    [Header("플레이어 상태")]
    public bool isDie = false;
}
