using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundUI : MonoBehaviour
{
    //�����̴��� ���� ����
    public Slider BgmSoundBar;
    //�����̴��� ���� ����
    public Slider EffectSoundBar;
    //���� �����̴� ���� ���� ���尡 ǥ�õ� �ؽ�Ʈ ���� 
    public TextMeshProUGUI BgmValueText;
    //���� �����̴� ���� ���� ���尡 ǥ�õ� �ؽ�Ʈ ���� 
    public TextMeshProUGUI EffectValueText;

    #region ������� �����̴��� ����
    // BgmSoundBar�����̴��� �� ���� ü�����忡 �־��� �Լ� 
    public void Bgm_SliderControl(float volume)
    {
        //������� ���� ���
        SoundManager.Instance.audioSource.volume = volume;
    }
    // BgmSoundBar�����̴��� �� ���� ü�����忡 �־��� �Լ�
    public void Bgm_SliderText(float value)
    {
        //Mathf.RoundToInt   <-�ݿø� ���� �Լ�
        //�ݿø� �� ���� �ؽ�Ʈ�� �ٲ��ش�.
        BgmValueText.text = Mathf.RoundToInt(value * 10f).ToString();
    }
    #endregion

    #region ȿ���� �����̴��� ����
    // EffectSoundBar�����̴��� �� ���� ü�����忡 �־��� �Լ� 
    public void effect_SliderControl(float volume)
    {
        //ȿ���� ���� ���
        Player_Data.Instance.audioSource.volume = volume;
    }
    // EffectSoundBar�����̴��� �� ���� ü�����忡 �־��� �Լ� 
    public void effect_SliderText(float value)
    {
        //Mathf.RoundToInt   <-�ݿø� ���� �Լ�
        //�ݿø� �� ���� �ؽ�Ʈ�� �ٲ��ش�.
        EffectValueText.text = Mathf.RoundToInt(value * 10f).ToString();
    }
    #endregion

    #region ������� +  ,  - ��ư���� ����
    // BgmSoundBar�� �ڽ���     + Button�� ��Ŭ���� �־��� �Լ�
    public void BgmPlusBtn()
    {
        //��ư�� ������ ���� �����̴��� �������� 0.1�� �����ϴ� �ڵ�
        BgmSoundBar.value += 0.1f;
        //������� ���� ���
        SoundManager.Instance.audioSource.volume += 0.01f;
    }
    // BgmSoundBar�� �ڽ���     - Button�� ��Ŭ���� �־��� �Լ�
    public void BgmMinusBtn()
    {
        //��ư�� ������ �Ŵ� �����̴��� �������� 0.1�� �����ϴ� �ڵ�
        BgmSoundBar.value -= 0.1f;
        //������� ���� ���
        SoundManager.Instance.audioSource.volume -= 0.01f;
    }
    #endregion

    #region ȿ���� +  ,  - ��ư���� ����
    // EffectSoundBar�� �ڽ���     + Button�� ��Ŭ���� �־��� �Լ�
    public void EffectPlusBtn()
    {
        //��ư�� ������ ���� �����̴��� �������� 0.1�� �����ϴ� �ڵ�
        EffectSoundBar.value += 0.1f;
        //ȿ���� ���� ���
        Player_Data.Instance.audioSource.volume += 0.01f;
    }
    // EffectSoundBar�� �ڽ���     - Button�� ��Ŭ���� �־��� �Լ�
    public void EffectMinusBtn()
    {
        //��ư�� ������ �Ŵ� �����̴��� �������� 0.1�� �����ϴ� �ڵ�
        EffectSoundBar.value -= 0.1f;
        //ȿ���� ���� ���
        Player_Data.Instance.audioSource.volume -= 0.01f;
    }
    #endregion
}