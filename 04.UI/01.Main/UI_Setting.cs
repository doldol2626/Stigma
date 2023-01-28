using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Setting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rectT; //렉트 트랜스폼 컴포넌트

    public Vector2 savedPos;    //저장된 위치
    public Vector2 savedSize;   //저장된 크기

    public GameObject sizeBtnPanel; //버튼 패널

    bool isMouseOn = false; //마우스가 창 위로 올라온 상태인지 확인

    //추가하고 싶은 것
    //1. 이 오브젝트 위에서 우클릭을 하면 클릭한 위치에서 버튼 패널이 나타나게 하는 것...ㅠㅠ
    //2. 위치와 크기를 플레이어가 조절할 수 있게 하는 것

    void Start()
    {
        //RectTransform 컴포넌트 불러오기
        rectT = GetComponent<RectTransform>();

        //위치 초기화
        ResetPos();

        //길이 초기화
        ResetSize();
    }

    void Update()
    {
        if (isMouseOn && Input.GetMouseButtonDown(1)) //isMouseOn이 true인 상태에서 우클릭 시
        {
            sizeBtnPanel.SetActive(true); //버튼 패널 활성화

            //패널 생성 위치를 마우스포인터 위치로 지정
            sizeBtnPanel.GetComponent<RectTransform>().position = Input.mousePosition;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //Esc 입력 시
        {
            sizeBtnPanel.SetActive(false); //버튼 패널 비활성화
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        isMouseOn = true; //마우스포인터가 창 위로 올라오면 isMouseOn 변수를 true로 전환
    }

    public void OnPointerExit(PointerEventData e)
    {
        isMouseOn = false; //마우스포인터가 창 밖으로 벗어나면 isMouseOn 변수를 false로 전환
        //하고 싶었으나... 그냥 움직이기만 해도 false가 되는 듯ㅠㅠ 왜야!
    }

    public void ResetPos()
    {
        //렉트트랜스폼 포지션을 savedPos(저장된 Vector2)값으로 변경
        rectT.anchoredPosition = savedPos;
    }

    public void ResetSize()
    {
        //렉트트랜스폼 사이즈를 savedSize(저장된 Vector2)값으로 변경
        rectT.sizeDelta = savedSize;
    }

    public void SavePos()
    {
        //savedPos값에 현재 렉트트랜스폼 포지션 값 대입
        savedPos = rectT.anchoredPosition;
    }

    public void SaveSize()
    {
        //savedSize값에 현재 렉트트랜스폼 사이즈 값 대입
        savedSize = rectT.sizeDelta;
    }
}