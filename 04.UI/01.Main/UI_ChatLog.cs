using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ChatLog : Singletone<UI_ChatLog>
{
    public ScrollRect scrollRect;     //로그 창 스크롤뷰
    public TextMeshProUGUI logText;   //표시되는 로그 텍스트
    public TMP_InputField inputField; //플레이어 입력칸
    public Button sendBtn;            //전송 버튼

    void Start()
    {
        //접속 시 기본 멘트
        logText.text = "​<color=#A8CEFF>[System] 접속을 환영합니다!</color>";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //엔터 입력 시
        {
            SendLog();
        }
    }

    public void SendLog()
    {
        if (inputField.text.Length > 0) //입력칸이 비어있지 않다면
        {
            logText.text += ("\n[채팅] " + inputField.text); //텍스트 업데이트
            inputField.text = null;                          //입력칸 초기화
            inputField.ActivateInputField();                 //입력칸 활성 유지

            scrollRect.verticalScrollbar.value = 0; //스크롤바 위치 아래로(안됨)
        }
    }

    public void SystemLog(string log)
    {
        logText.text += ("​\n<color=#A8CEFF>" + log + "</color>"); //텍스트 업데이트

        scrollRect.verticalScrollbar.value = 0; //스크롤바 위치 아래로(안됨)
    }
}