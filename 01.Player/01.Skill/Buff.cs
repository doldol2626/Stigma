using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buff : MonoBehaviour
{
    public SkillData skillData;

    Image icon; //UI 표시 아이콘
    TextMeshProUGUI buffRemain; //남은 시간 표시

    void Awake()
    {
        icon = GetComponentInChildren<Image>(); //버프 아이콘 컴포넌트 불러오기
        buffRemain = GetComponentInChildren<TextMeshProUGUI>(); //남은 시간 표시 텍스트 불러오기
    }

    public void Execute(SkillData skillData) //버프 추가
    {
        this.skillData = skillData;                //넘겨받은 값을 이 버프(스킬)의 데이터로 지정
        skillData.BuffRemain = skillData.Duration; //버프 지속시간 적용
        icon.fillAmount = 1;                       //아이콘의 fillAmount(채워진 정도)값을 1로 변경

        BuffManager.Instance.onBuff.Add(this);                //onBuff 리스트에 이 버프 추가 
        BuffManager.Instance.ChooseBuff(skillData.SkillName); //ChooseBuff(버프 추가 함수)에 이 버프의 이름 전달

        StartCoroutine(Activation()); //타이머 실행
    }

    IEnumerator Activation() //타이머 및 버프 지속 시간 표시
    {
        while (skillData.BuffRemain > 0) //버프 지속 시간이 남아있다면
        {
            buffRemain.text = skillData.BuffRemain.ToString("F0");
            //남은 지속 시간 표시(소수점 제거)

            skillData.BuffRemain -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            //0.1초마다 currentTime 0.1씩 감소

            if (skillData.BuffRemain <= 5) //버프 지속 시간이 5초 이하라면
            {
                buffRemain.text = skillData.BuffRemain.ToString("F0");
                //남은 지속 시간 표시(소수점 제거)

                skillData.BuffRemain -= 0.5f;
                icon.CrossFadeAlpha(0.5f, 0f, true);
                yield return new WaitForSeconds(0.5f);
                icon.CrossFadeAlpha(1f, 0f, true);
                //0.5초마다 currentTime 0.5씩 감소, 깜빡임
            }
        }

        //이어서 currentTime이 0 아래로 내려가게 될 경우
        //fillAmount값과 currentTime을 0으로 변경
        icon.fillAmount = 0;
        skillData.BuffRemain = 0;

        //버프 효과 삭제 함수 실행
        DeActivation();
    }

    public void DeActivation() //버프 효과 삭제
    {
        BuffManager.Instance.RemoveBuff(skillData.SkillName); //RemoveBuff(버프 삭제 함수)에 이 버프의 이름 전달
        BuffManager.Instance.onBuff.Remove(this);             //onBuff 리스트에서 이 버프 삭제
        Destroy(gameObject);                                  //이 버프가 포함된 게임오브젝트 삭제
    }
}