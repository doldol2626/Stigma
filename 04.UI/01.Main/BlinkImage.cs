using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    Image image; //�̹��� ������Ʈ
    
    [SerializeField]
    float blinkSpeed = 1f; //������ �ӵ�

    void Start()
    {
        image = GetComponent<Image>(); //�̹��� ������Ʈ �ҷ�����
        StartCoroutine(BI());          //������ �ڷ�ƾ ����
    }

    IEnumerator BI()
    {
        while (true) //�ݺ�
        {
            image.CrossFadeAlpha(0.5f, blinkSpeed, true); //1�ʰ� �̹��� ���İ��� 50%�� ����
            yield return new WaitForSeconds(blinkSpeed);  //�� ����� ����Ǵ� �ð����� ���

            image.CrossFadeAlpha(1f, blinkSpeed, true);   //1�ʰ� �̹��� ���İ��� 100%�� ����
            yield return new WaitForSeconds(blinkSpeed);  //�� ����� ����Ǵ� �ð����� ���
        }
    }
}