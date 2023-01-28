using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltime : MonoBehaviour
{
    public SkillData skillData; //스킬 데이터
    Image[] icon; //스킬 아이콘 및 쿨타임 표시용

    void Awake()
    {
        icon = GetComponentsInChildren<Image>(); //스킬 아이콘 컴포넌트 불러오기
        icon[0].sprite = skillData.SkillIcon;    //스킬 아이콘을 미리 지정된 이미지로 변경
    }

    void Start()
    {
        skillData.CurrentTime = 0; //남은 쿨타임 초기화
    }

    public void CoolTime()
    {
        if (skillData.CurrentTime == 0) //쿨타임이 남아있지 않다면
        {
            skillData.CurrentTime = skillData.CoolTime; //남은 쿨타임을 스킬 쿨타임으로 지정
            icon[1].fillAmount = 1;                     //마스크 표시
            StartCoroutine(Activation());               //타이머 실행
        }
    }

    IEnumerator Activation() //타이머 및 버프 지속 시간 표시
    {
        while (skillData.CurrentTime > 0) //currentTime이 남아있다면
        {
            skillData.CurrentTime -= 0.1f;
            icon[1].fillAmount = skillData.CurrentTime / skillData.CoolTime;
            yield return new WaitForSeconds(0.1f);
            //0.1초마다 currentTime, 마스크 fillAmount 0.1씩 감소
        }

        //이어서 currentTime이 0 이하로 내려가게 될 경우
        //fillAmount값과 currentTime을 0으로 변경
        icon[1].fillAmount = 0;
        skillData.CurrentTime = 0;
    }
}
