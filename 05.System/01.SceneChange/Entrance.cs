using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player") //충돌체가 플레이어라면
        {
            FindObjectOfType<NoticeText>().ChangeText("F키를 눌러 이동"); //화면 중앙에 이동 방법 표시

            if (Input.GetKey(KeyCode.F)) //F키 입력 시
            {
                if (gameObject.name == "NextEntrance") //미구현 지역일 경우
                {
                    FindObjectOfType<UI_ChatLog>().SystemLog("현재는 진입이 불가능한 지역입니다.");
                    return; //채팅창 로그에 오류 메시지 표시 및 빠져나오기
                }

                SceneLoadManager.Instance.SceneTransOK(true); //씬 전환 가능 상태로 변경
                SceneManager.LoadScene(1);                    //로딩 씬으로 전환

                Player_Data.Instance.transform.position = new Vector3(-40, 0, 0); //플레이어 위치 초기화
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        FindObjectOfType<NoticeText>().ChangeText(null);
        //화면 중앙에 표시됐던 텍스트 제거
    }
}
