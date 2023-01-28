using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_StatusBar : Singletone<UI_StatusBar>
{
    PlayerData playerData;

    public Slider hpBar;  //ü�¹�
    public Slider hpbar2; //�ǵ��
    public Slider mpBar;  //���¹�
    public Slider cpBar;  //���� ��������
    public Slider expBar; //����ġ��

    public TextMeshProUGUI lvText; //���� ǥ�� �ؽ�Ʈ

    void Start()
    {
        //PlayerData ������Ʈ �ҷ�����
        playerData = Player_Data.Instance.playerData;

        //�������� �ʱ�ȭ
        HpBarUpdate();
        HpBar2Update();
        MpBarUpdate();
        CpBarUpdate();
        ExpBarUpdate();
        LvTextUpdate();
    }

    //���� �������ͽ��� ������ �ʿ��� ������ �Ʒ� �Լ����� ȣ���ϵ��� �Ѵ�.

    public void HpBarUpdate()
    {
        //ü�¹� ����
        hpBar.value = (float)playerData.Hp / playerData.MaxHp;
    }

    public void HpBar2Update()
    {
        hpbar2.value = (float)playerData.PlusHp / playerData.MaxHp;
    }

    public void MpBarUpdate()
    {
        //���¹� ����
        mpBar.value = (float)playerData.Mp / playerData.MaxMp;
    }

    public void CpBarUpdate()
    {
        //���ι� ����
        cpBar.value = (float)playerData.Cp / playerData.MaxCp;
    }

    public void ExpBarUpdate()
    {
        //����ġ�� ����
        expBar.value = (float)playerData.Exp / playerData.MaxExp;
    }

    public void LvTextUpdate()
    {
        lvText.text = "Lv. " + playerData.Level;
    }
}