using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltime : MonoBehaviour
{
    public SkillData skillData; //��ų ������
    Image[] icon; //��ų ������ �� ��Ÿ�� ǥ�ÿ�

    void Awake()
    {
        icon = GetComponentsInChildren<Image>(); //��ų ������ ������Ʈ �ҷ�����
        icon[0].sprite = skillData.SkillIcon;    //��ų �������� �̸� ������ �̹����� ����
    }

    void Start()
    {
        skillData.CurrentTime = 0; //���� ��Ÿ�� �ʱ�ȭ
    }

    public void CoolTime()
    {
        if (skillData.CurrentTime == 0) //��Ÿ���� �������� �ʴٸ�
        {
            skillData.CurrentTime = skillData.CoolTime; //���� ��Ÿ���� ��ų ��Ÿ������ ����
            icon[1].fillAmount = 1;                     //����ũ ǥ��
            StartCoroutine(Activation());               //Ÿ�̸� ����
        }
    }

    IEnumerator Activation() //Ÿ�̸� �� ���� ���� �ð� ǥ��
    {
        while (skillData.CurrentTime > 0) //currentTime�� �����ִٸ�
        {
            skillData.CurrentTime -= 0.1f;
            icon[1].fillAmount = skillData.CurrentTime / skillData.CoolTime;
            yield return new WaitForSeconds(0.1f);
            //0.1�ʸ��� currentTime, ����ũ fillAmount 0.1�� ����
        }

        //�̾ currentTime�� 0 ���Ϸ� �������� �� ���
        //fillAmount���� currentTime�� 0���� ����
        icon[1].fillAmount = 0;
        skillData.CurrentTime = 0;
    }
}
