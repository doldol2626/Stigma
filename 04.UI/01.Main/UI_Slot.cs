using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Slot : MonoBehaviour, IDropHandler
{
    Transform frame; //������ ��ġ(���� �ڽ� ���� ������ ���)
    List<string> childrenNames = new();

    void Awake()
    {
        frame = transform.GetChild(transform.childCount - 1);
        //������(�� ������ �ڽ�) ��ġ �ҷ�����
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


            //���� �̸��� �̺�Ʈ������(�Ű��� ������Ʈ)�� �̸��� �����ϴ� ��� => ��) SkillBarSlot, Skill
            else if (name.Contains(eventData.pointerDrag.name))
            {
                eventData.pointerDrag.transform.SetParent(transform); //�ش� ������Ʈ�� �θ� ���� �������� ����
                frame.SetAsLastSibling(); //�������� ������Ʈ�� ������ �ʵ��� �������� ������ �� ���������� ����

                eventData.pointerDrag.GetComponent<RectTransform>().position
                    = GetComponent<RectTransform>().position; //������Ʈ ��ġ�� ���� ��ġ�� �̵�
            }
            //�� �ʿ��� ��: �̹� ���Ծȿ� ��ų�� ���� ��� ���� ��ü�ϴ� ���
        }
    }
}