using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buff : MonoBehaviour
{
    public SkillData skillData;

    Image icon; //UI ǥ�� ������
    TextMeshProUGUI buffRemain; //���� �ð� ǥ��

    void Awake()
    {
        icon = GetComponentInChildren<Image>(); //���� ������ ������Ʈ �ҷ�����
        buffRemain = GetComponentInChildren<TextMeshProUGUI>(); //���� �ð� ǥ�� �ؽ�Ʈ �ҷ�����
    }

    public void Execute(SkillData skillData) //���� �߰�
    {
        this.skillData = skillData;                //�Ѱܹ��� ���� �� ����(��ų)�� �����ͷ� ����
        skillData.BuffRemain = skillData.Duration; //���� ���ӽð� ����
        icon.fillAmount = 1;                       //�������� fillAmount(ä���� ����)���� 1�� ����

        BuffManager.Instance.onBuff.Add(this);                //onBuff ����Ʈ�� �� ���� �߰� 
        BuffManager.Instance.ChooseBuff(skillData.SkillName); //ChooseBuff(���� �߰� �Լ�)�� �� ������ �̸� ����

        StartCoroutine(Activation()); //Ÿ�̸� ����
    }

    IEnumerator Activation() //Ÿ�̸� �� ���� ���� �ð� ǥ��
    {
        while (skillData.BuffRemain > 0) //���� ���� �ð��� �����ִٸ�
        {
            buffRemain.text = skillData.BuffRemain.ToString("F0");
            //���� ���� �ð� ǥ��(�Ҽ��� ����)

            skillData.BuffRemain -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            //0.1�ʸ��� currentTime 0.1�� ����

            if (skillData.BuffRemain <= 5) //���� ���� �ð��� 5�� ���϶��
            {
                buffRemain.text = skillData.BuffRemain.ToString("F0");
                //���� ���� �ð� ǥ��(�Ҽ��� ����)

                skillData.BuffRemain -= 0.5f;
                icon.CrossFadeAlpha(0.5f, 0f, true);
                yield return new WaitForSeconds(0.5f);
                icon.CrossFadeAlpha(1f, 0f, true);
                //0.5�ʸ��� currentTime 0.5�� ����, ������
            }
        }

        //�̾ currentTime�� 0 �Ʒ��� �������� �� ���
        //fillAmount���� currentTime�� 0���� ����
        icon.fillAmount = 0;
        skillData.BuffRemain = 0;

        //���� ȿ�� ���� �Լ� ����
        DeActivation();
    }

    public void DeActivation() //���� ȿ�� ����
    {
        BuffManager.Instance.RemoveBuff(skillData.SkillName); //RemoveBuff(���� ���� �Լ�)�� �� ������ �̸� ����
        BuffManager.Instance.onBuff.Remove(this);             //onBuff ����Ʈ���� �� ���� ����
        Destroy(gameObject);                                  //�� ������ ���Ե� ���ӿ�����Ʈ ����
    }
}