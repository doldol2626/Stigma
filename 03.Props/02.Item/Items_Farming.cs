using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_Farming : MonoBehaviour
{
    public ItemData itemData; // 아이템 데이터
    public float damTrace = 3.0f; // 따라가는 속도 변수

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !IsNoSpace()) //충돌체가 플레이어, 인벤토리 여유
        {
            // 반경 안에 있으면 Player 위치로 부드럽게 따라감
            transform.position = Vector3.Lerp(transform.position, other.transform.position, Time.deltaTime * damTrace);
            //                   부드럽게 이동     현재위치              목표위치                속도
        }
    }

    public bool IsNoSpace() //플레이어의 인벤토리에 공간이 남아있는지 확인하는 함수
    {
        List<bool> isNoSpace = new(); //인벤토리가 가득 차는 조건을 담아줄 리스트 생성

        //소비 아이템: 소비칸의 아이템이 56개 이상, 획득할 아이템 미소지
        isNoSpace.Add(itemData.itemType == 0 && Player_Data.Instance.potionInven.Count >= 56
            && !Player_Data.Instance.potionInven.Contains(itemData));

        //장비 아이템: 장비칸의 아이템이 56개 이상
        isNoSpace.Add(itemData.itemType == 1 && Player_Data.Instance.equipInven.Count >= 56);

        //기타 아이템: 기타칸의 아이템이 56개 이상, 획득할 아이템 미소지 
        isNoSpace.Add(itemData.itemType == 2 && Player_Data.Instance.extraInven.Count >= 56
            && !Player_Data.Instance.extraInven.Contains(itemData));

        isNoSpace.TrimExcess(); //불필요한 메모리 반환

        return isNoSpace.Contains(true); //위 조건 중 하나라도 들어맞는지 판단
    }
}
