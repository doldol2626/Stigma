using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_Farming : MonoBehaviour
{
    public ItemData itemData; // ������ ������
    public float damTrace = 3.0f; // ���󰡴� �ӵ� ����

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !IsNoSpace()) //�浹ü�� �÷��̾�, �κ��丮 ����
        {
            // �ݰ� �ȿ� ������ Player ��ġ�� �ε巴�� ����
            transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime * damTrace);
            //                   �ε巴�� �̵�     ������ġ              ��ǥ��ġ                �ӵ�
        }
    }

    public bool IsNoSpace() //�÷��̾��� �κ��丮�� ������ �����ִ��� Ȯ���ϴ� �Լ�
    {
        List<bool> isNoSpace = new(); //�κ��丮�� ���� ���� ������ ����� ����Ʈ ����

        //�Һ� ������: �Һ�ĭ�� �������� 56�� �̻�, ȹ���� ������ �̼���
        isNoSpace.Add(itemData.itemType == 0 && Player_Data.Instance.potionInven.Count >= 56
            && !Player_Data.Instance.potionInven.Contains(itemData));

        //��� ������: ���ĭ�� �������� 56�� �̻�
        isNoSpace.Add(itemData.itemType == 1 && Player_Data.Instance.equipInven.Count >= 56);

        //��Ÿ ������: ��Ÿĭ�� �������� 56�� �̻�, ȹ���� ������ �̼��� 
        isNoSpace.Add(itemData.itemType == 2 && Player_Data.Instance.extraInven.Count >= 56
            && !Player_Data.Instance.extraInven.Contains(itemData));

        isNoSpace.TrimExcess(); //���ʿ��� �޸� ��ȯ

        return isNoSpace.Contains(true); //�� ���� �� �ϳ��� ���´��� �Ǵ�
    }
}
