using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_StatusBar : Singletone<UI_StatusBar>
{
    PlayerData playerData;

    public Slider hpBar;  //체력바
    public Slider hpbar2; //실드바
    public Slider mpBar;  //마력바
    public Slider cpBar;  //낙인 게이지바
    public Slider expBar; //경험치바

    public TextMeshProUGUI lvText; //레벨 표시 텍스트

    void Start()
    {
        //PlayerData 컴포넌트 불러오기
        playerData = Player_Data.Instance.playerData;

        //게이지바 초기화
        HpBarUpdate();
        HpBar2Update();
        MpBarUpdate();
        CpBarUpdate();
        ExpBarUpdate();
        LvTextUpdate();
    }

    //이후 스테이터스바 변경이 필요할 때마다 아래 함수들을 호출하도록 한다.

    public void HpBarUpdate()
    {
        //체력바 변경
        hpBar.value = (float)playerData.Hp / playerData.MaxHp;
    }

    public void HpBar2Update()
    {
        hpbar2.value = (float)playerData.PlusHp / playerData.MaxHp;
    }

    public void MpBarUpdate()
    {
        //마력바 변경
        mpBar.value = (float)playerData.Mp / playerData.MaxMp;
    }

    public void CpBarUpdate()
    {
        //낙인바 변경
        cpBar.value = (float)playerData.Cp / playerData.MaxCp;
    }

    public void ExpBarUpdate()
    {
        //경험치바 변경
        expBar.value = (float)playerData.Exp / playerData.MaxExp;
    }

    public void LvTextUpdate()
    {
        lvText.text = "Lv. " + playerData.Level;
    }
}