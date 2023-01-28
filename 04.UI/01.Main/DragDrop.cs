using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Canvas canvas; //MainUI ĵ����

    RectTransform rectTransform; //UI�� Transform ��� RectTransform

    CanvasGroup canvasGroup;     //UI ��� �׷� ��ü�� Ư�� ������ ���������� �ٷ� �ʿ� ����
                                 //�� ������ �����ϱ� ���� ���

    void Awake()
    {
        //���������� ������ ������Ʈ �ҷ�����
        canvas = GameObject.Find("MainUI").GetComponent<Canvas>(); //���� ĵ������ �̸����� ã�� �ҷ�����
        rectTransform = GetComponent<RectTransform>();             //�ű���� ������Ʈ�� RectTransform
        canvasGroup = GetComponentInChildren<CanvasGroup>();       //���� �ڽ�(Image)�� CanvasGroup
    }

    public void OnPointerDown(PointerEventData eventData) //Ŭ�� ��
    {
        if (!name.Contains("Panel") || !transform.parent.name.Contains("BarSlot")) { Copy(); }
        //�ű���� ������Ʈ�� �г� ��ü�ų� ���� ���Թٿ� ���Ե� ��츦 ����, ���� ���� �Լ� ����
    }

    public void OnBeginDrag(PointerEventData eventData) //�巡�� ����
    {
        canvasGroup.alpha = 0.6f;           //�̹��� ���� �� �������ϰ� ����
        canvasGroup.blocksRaycasts = false; //����ĳ��Ʈ�� ���� �ݶ��̴� �۵� ����
    }

    public void OnDrag(PointerEventData eventData) //�巡�� ��
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //...������ ���� �� �𸣰ڴµ� �̷��� ���� �̵��� �ȴٳ׿�??
    }

    public void OnEndDrag(PointerEventData eventData) //�巡�� ��
    {
        canvasGroup.alpha = 1f;            //�̹��� ���� �� ����ȭ
        canvasGroup.blocksRaycasts = true; //�ݶ��̴� �ٽ� �۵�

        if (!name.Contains("Panel")) //�̵� ������Ʈ�� �г��� �ƴ� ���(=��ų, �������� ���)
        {
            if (!transform.parent.name.Contains("BarSlot")) //��ġ�� ���� ���Թٷ� �Ѿ�� �ʾҴٸ�
            {
                Destroy(gameObject); //����
            }
        }
    }

    void Copy() //���� ���� �Լ�
    {
        //�κ��丮, ��ų �гο��� ������Ʈ�� ���� ���Թٷ� ������ �ű�� ��� ������ ������ �ʿ�.
        //���� �ش� ������Ʈ�� �����Ͽ� ���� ��ġ�� ���� �� �ֵ��� �Ѵ�.

        GameObject copied = Instantiate(gameObject); //���纻 ������Ʈ ����

        copied.name = this.name; //���纻�� �̸��� �ű� ������Ʈ �̸����� ����

        copied.transform.SetParent(this.transform.parent); //���纻�� ���� �θ� ���

        copied.GetComponent<RectTransform>().anchoredPosition = this.rectTransform.anchoredPosition;
        //���纻�� ��ġ�� �ű� ������Ʈ�� ��ġ�� ����
    }
}