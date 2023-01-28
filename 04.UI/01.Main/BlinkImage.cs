using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    Image image; //이미지 컴포넌트
    
    [SerializeField]
    float blinkSpeed = 1f; //깜빡임 속도

    void Start()
    {
        image = GetComponent<Image>(); //이미지 컴포넌트 불러오기
        StartCoroutine(BI());          //깜빡임 코루틴 실행
    }

    IEnumerator BI()
    {
        while (true) //반복
        {
            image.CrossFadeAlpha(0.5f, blinkSpeed, true); //1초간 이미지 알파값을 50%로 변경
            yield return new WaitForSeconds(blinkSpeed);  //위 명령이 실행되는 시간동안 대기

            image.CrossFadeAlpha(1f, blinkSpeed, true);   //1초간 이미지 알파값을 100%로 변경
            yield return new WaitForSeconds(blinkSpeed);  //위 명령이 실행되는 시간동안 대기
        }
    }
}