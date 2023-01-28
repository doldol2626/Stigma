using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Slot : MonoBehaviour, IDropHandler
{
    Transform frame; //프레임 위치(이후 자식 순서 지정에 사용)
    List<string> childrenNames = new();

    void Awake()
    {
        frame = transform.GetChild(transform.childCount - 1);
        //프레임(맨 마지막 자식) 위치 불러오기
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                childrenNames.Add(transform.GetChild(i).name);
            }

            if (childrenNames.Contains(eventData.pointerDrag.name)) { return; }


            //슬롯 이름이 이벤트데이터(옮겨진 오브젝트)의 이름을 포함하는 경우 => 예) SkillBarSlot, Skill
            else if (name.Contains(eventData.pointerDrag.name))
            {
                eventData.pointerDrag.transform.SetParent(transform); //해당 오브젝트의 부모를 현재 슬롯으로 지정
                frame.SetAsLastSibling(); //프레임이 오브젝트에 묻히지 않도록 계층구조 순서를 맨 마지막으로 지정

                eventData.pointerDrag.GetComponent<RectTransform>().position
                    = GetComponent<RectTransform>().position; //오브젝트 위치를 슬롯 위치로 이동
            }
            //또 필요한 것: 이미 슬롯안에 스킬이 있을 경우 둘이 교체하는 기능
        }
    }
}