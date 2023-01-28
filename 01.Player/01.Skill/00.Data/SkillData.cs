using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성 메뉴안에 하위 메뉴를 만들어 줌
[CreateAssetMenu(fileName = "SkillData", menuName = "Data/SkillData", order = 0)]
public class SkillData : ScriptableObject
{
    //공통
    public string SkillType; //스킬 유형
    public string SkillName; //스킬 이름
    public string SkillInfo; //스킬 설명
    public Sprite SkillIcon; //스킬 이미지

    public int Level;      //스킬 사용에 필요한 레벨

    public int Mp; //사용 마나
    public int Cp; //사용 클래스 게이지

    public float CoolTime;    //스킬 쿨타임
    public float CurrentTime; //현재 남은 쿨타임

    //공격스킬
    public float Attack; //위력  

    //버프스킬
    public float Percentage; //능력치 증가폭
    public float Duration;   //버프 지속 시간
    public float BuffRemain; //버프 남은 시간
}