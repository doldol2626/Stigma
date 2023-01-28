using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Setting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rectT; //��Ʈ Ʈ������ ������Ʈ

    public Vector2 savedPos;    //����� ��ġ
    public Vector2 savedSize;   //����� ũ��

    public GameObject sizeBtnPanel; //��ư �г�

    bool isMouseOn = false; //���콺�� â ���� �ö�� �������� Ȯ��

    //�߰��ϰ� ���� ��
    //1. �� ������Ʈ ������ ��Ŭ���� �ϸ� Ŭ���� ��ġ���� ��ư �г��� ��Ÿ���� �ϴ� ��...�Ф�
    //2. ��ġ�� ũ�⸦ �÷��̾ ������ �� �ְ� �ϴ� ��

    void Start()
    {
        //RectTransform ������Ʈ �ҷ�����
        rectT = GetComponent<RectTransform>();

        //��ġ �ʱ�ȭ
        ResetPos();

        //���� �ʱ�ȭ
        ResetSize();
    }

    void Update()
    {
        if (isMouseOn && Input.GetMouseButtonDown(1)) //isMouseOn�� true�� ���¿��� ��Ŭ�� ��
        {
            sizeBtnPanel.SetActive(true); //��ư �г� Ȱ��ȭ

            //�г� ���� ��ġ�� ���콺������ ��ġ�� ����
            sizeBtnPanel.GetComponent<RectTransform>().position = Input.mousePosition;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //Esc �Է� ��
        {
            sizeBtnPanel.SetActive(false); //��ư �г� ��Ȱ��ȭ
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        isMouseOn = true; //���콺�����Ͱ� â ���� �ö���� isMouseOn ������ true�� ��ȯ
    }

    public void OnPointerExit(PointerEventData e)
    {
        isMouseOn = false; //���콺�����Ͱ� â ������ ����� isMouseOn ������ false�� ��ȯ
        //�ϰ� �;�����... �׳� �����̱⸸ �ص� false�� �Ǵ� ��Ф� �־�!
    }

    public void ResetPos()
    {
        //��ƮƮ������ �������� savedPos(����� Vector2)������ ����
        rectT.anchoredPosition = savedPos;
    }

    public void ResetSize()
    {
        //��ƮƮ������ ����� savedSize(����� Vector2)������ ����
        rectT.sizeDelta = savedSize;
    }

    public void SavePos()
    {
        //savedPos���� ���� ��ƮƮ������ ������ �� ����
        savedPos = rectT.anchoredPosition;
    }

    public void SaveSize()
    {
        //savedSize���� ���� ��ƮƮ������ ������ �� ����
        savedSize = rectT.sizeDelta;
    }
}