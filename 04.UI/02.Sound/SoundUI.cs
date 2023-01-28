using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundUI : MonoBehaviour
{
    //슬라이더를 담을 변수
    public Slider BgmSoundBar;
    //슬라이더를 담을 변수
    public Slider EffectSoundBar;
    //사운드 슬라이더 옆에 현재 사운드가 표시될 텍스트 변수 
    public TextMeshProUGUI BgmValueText;
    //사운드 슬라이더 옆에 현재 사운드가 표시될 텍스트 변수 
    public TextMeshProUGUI EffectValueText;

    #region 배경음악 슬라이더로 조절
    // BgmSoundBar슬라이더의 온 벨류 체인지드에 넣어줄 함수 
    public void Bgm_SliderControl(float volume)
    {
        //배경음악 사운드 재생
        SoundManager.Instance.audioSource.volume = volume;
    }
    // BgmSoundBar슬라이더의 온 벨류 체인지드에 넣어줄 함수
    public void Bgm_SliderText(float value)
    {
        //Mathf.RoundToInt   <-반올림 관련 함수
        //반올림 한 값을 텍스트로 바꿔준다.
        BgmValueText.text = Mathf.RoundToInt(value * 10f).ToString();
    }
    #endregion

    #region 효과음 슬라이더로 조절
    // EffectSoundBar슬라이더의 온 벨류 체인지드에 넣어줄 함수 
    public void effect_SliderControl(float volume)
    {
        //효과음 사운드 재생
        Player_Data.Instance.audioSource.volume = volume;
    }
    // EffectSoundBar슬라이더의 온 벨류 체인지드에 넣어줄 함수 
    public void effect_SliderText(float value)
    {
        //Mathf.RoundToInt   <-반올림 관련 함수
        //반올림 한 값을 텍스트로 바꿔준다.
        EffectValueText.text = Mathf.RoundToInt(value * 10f).ToString();
    }
    #endregion

    #region 배경음악 +  ,  - 버튼으로 조절
    // BgmSoundBar의 자식의     + Button의 온클릭에 넣어줄 함수
    public void BgmPlusBtn()
    {
        //버튼을 누를때 마다 슬라이더의 벨류값이 0.1씩 증가하는 코드
        BgmSoundBar.value += 0.1f;
        //배경음악 사운드 재생
        SoundManager.Instance.audioSource.volume += 0.01f;
    }
    // BgmSoundBar의 자식의     - Button의 온클릭에 넣어줄 함수
    public void BgmMinusBtn()
    {
        //버튼을 누를때 매다 슬라이더의 벨류값이 0.1씩 감소하는 코드
        BgmSoundBar.value -= 0.1f;
        //배경음악 사운드 재생
        SoundManager.Instance.audioSource.volume -= 0.01f;
    }
    #endregion

    #region 효과음 +  ,  - 버튼으로 조절
    // EffectSoundBar의 자식의     + Button의 온클릭에 넣어줄 함수
    public void EffectPlusBtn()
    {
        //버튼을 누를때 마다 슬라이더의 벨류값이 0.1씩 증가하는 코드
        EffectSoundBar.value += 0.1f;
        //효과음 사운드 재생
        Player_Data.Instance.audioSource.volume += 0.01f;
    }
    // EffectSoundBar의 자식의     - Button의 온클릭에 넣어줄 함수
    public void EffectMinusBtn()
    {
        //버튼을 누를때 매다 슬라이더의 벨류값이 0.1씩 감소하는 코드
        EffectSoundBar.value -= 0.1f;
        //효과음 사운드 재생
        Player_Data.Instance.audioSource.volume -= 0.01f;
    }
    #endregion
}