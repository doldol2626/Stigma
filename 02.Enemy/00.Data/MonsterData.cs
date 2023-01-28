using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성 메뉴안에 하위 메뉴를 만들어 줌
[CreateAssetMenu(fileName = "MonsterData", menuName = "Data/MonsterData", order = 0)]
public class MonsterData : ScriptableObject
{
    [Header("몬스터 특성")]
    public string Pname;
    public int MaxHp;
    public int Attack;
    public int Level;
    public int Exp;
    public float moveSpeed = 5.0f;// 움직이는 속도 변수

    [Header("거리 범위")]
    // 거리 판단을 위한 변수 (MonsterData로 바꿔야 할 듯)
    public float findDistance; // Player를 찾는 거리
    public float attackDistance; // 공격하는 범위
    public float limitDistance; // 몬스터가 쫓아가는 한계 범위

    [Header("생성")]
    public GameObject Prefab; // 생성할 프리팹 오브젝트
    public float startCreateTime; // 생성 시작 시간
    public float createMinTime; // 최소 반복 주기 변수
    public float createMaxTime; // 최대 반복 주기 변수
    public int createLimitNumber; // 한계 생성 수
}
