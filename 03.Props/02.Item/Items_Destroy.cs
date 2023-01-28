using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items_Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") // 부딪힌 물체의 태그가 Player라면
        {
            if (!GetComponentInParent<Items_Farming>().IsNoSpace()) //인벤토리에 공간이 있을 경우
            {
                Player_Data.Instance.CheckItem(transform.parent.gameObject); //아이템 획득 함수 실행
                Destroy(transform.parent.gameObject); //부모(빈 게임 오브젝트) 삭제
            }
            else //인벤토리에 공간이 없을 경우
            {
                FindObjectOfType<UI_ChatLog>().SystemLog("인벤토리가 부족합니다.");
                return; //시스템 메시지 송출, 함수 빠져나오기
            }
        }
    }
}
