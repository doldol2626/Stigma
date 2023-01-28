using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Canvas canvas; //MainUI 캔버스

    RectTransform rectTransform; //UI는 Transform 대신 RectTransform

    CanvasGroup canvasGroup;     //UI 요소 그룹 전체의 특정 측면을 개별적으로 다룰 필요 없이
                                 //한 곳에서 제어하기 위해 사용

    void Awake()
    {
        //전역변수로 선언한 컴포넌트 불러오기
        canvas = GameObject.Find("MainUI").GetComponent<Canvas>(); //메인 캔버스를 이름으로 찾아 불러오기
        rectTransform = GetComponent<RectTransform>();             //옮기려는 오브젝트의 RectTransform
        canvasGroup = GetComponentInChildren<CanvasGroup>();       //슬롯 자식(Image)의 CanvasGroup
    }

    public void OnPointerDown(PointerEventData eventData) //클릭 시
    {
        if (!name.Contains("Panel") || !transform.parent.name.Contains("BarSlot")) { Copy(); }
        //옮기려는 오브젝트가 패널 자체거나 메인 슬롯바에 포함된 경우를 제외, 슬롯 복사 함수 실행
    }

    public void OnBeginDrag(PointerEventData eventData) //드래그 시작
    {
        canvasGroup.alpha = 0.6f;           //이미지 알파 값 반투명하게 조절
        canvasGroup.blocksRaycasts = false; //레이캐스트를 위한 콜라이더 작동 해제
    }

    public void OnDrag(PointerEventData eventData) //드래그 중
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //...원리가 뭔진 잘 모르겠는데 이렇게 쓰면 이동이 된다네요??
    }

    public void OnEndDrag(PointerEventData eventData) //드래그 끝
    {
        canvasGroup.alpha = 1f;            //이미지 알파 값 정상화
        canvasGroup.blocksRaycasts = true; //콜라이더 다시 작동

        if (!name.Contains("Panel")) //이동 오브젝트가 패널이 아닌 경우(=스킬, 아이템일 경우)
        {
            if (!transform.parent.name.Contains("BarSlot")) //위치가 메인 슬롯바로 넘어가지 않았다면
            {
                Destroy(gameObject); //삭제
            }
        }
    }

    void Copy() //슬롯 복사 함수
    {
        //인벤토리, 스킬 패널에서 오브젝트를 메인 슬롯바로 슬롯을 옮기는 경우 원본의 유지가 필요.
        //따라서 해당 오브젝트를 복사하여 기존 위치에 남길 수 있도록 한다.

        GameObject copied = Instantiate(gameObject); //복사본 오브젝트 생성

        copied.name = this.name; //복사본의 이름을 옮길 오브젝트 이름으로 설정

        copied.transform.SetParent(this.transform.parent); //복사본을 현재 부모에 상속

        copied.GetComponent<RectTransform>().anchoredPosition = this.rectTransform.anchoredPosition;
        //복사본의 위치를 옮길 오브젝트의 위치로 설정
    }
}