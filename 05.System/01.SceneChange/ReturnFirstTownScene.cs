using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReturnFirstTownScene : MonoBehaviour
{
    //타이머 변수 선언
    float Timer = 0;
    //타이머가 2.5초보다 크거나 같다면 마을로 돌아가는 버튼이 뜨게 하는 타이머변수
    float ReturnFirstTownSceneTimer = 2.5f;
    private void FixedUpdate()
    {
        //playerData의 isDie가 true라면
        if (Player_Data.Instance.playerData.isDie == true)
        {
            //이 오브젝트의 0번째 자식을 활성화 해 준다.
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //활성화 후 Timer한테 시간이 흐르도록 해 준다.
            Timer += Time.deltaTime;
            //Timer가 2.5f보다 크거나 같다면
            if (Timer >= ReturnFirstTownSceneTimer)
            {
                //이 오브젝트의 1번째 자식을 활성화 해 준다.
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }
    //Back_FirstScene_Button의 자식의 Text (TMP)의 Button컴포넌트의 들어갈 OnClick함수
    public void Back_FirstSceneFunc()
    {
        //버튼이 눌렸다면 타이머를 다시 0으로 만들어준다.
        Timer = 0f;

        SceneLoadManager.Instance.SceneTransOK(true); //씬 전환 가능 상태로 변경
        SceneManager.LoadScene(1);                    //로딩 씬으로 전환


        //플레이어 위치 초기화
        Player_Data.Instance.transform.position = new Vector3(-40, 0, 0);
        //이 오브젝트의 0번째 자식을 비활성화 해 준다.
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //이 오브젝트의 1번째 자식을 비활성화 해 준다.
        gameObject.transform.GetChild(1).gameObject.gameObject.SetActive(false);
        //playerData의 isDie를 다시 false로 만들어서 플레이어가 다시 움직일 수 있도록한다.
        Player_Data.Instance.playerData.isDie = false;
        //playerData의 playerData.Hp를 다시 playerData.MaxHp로 만들어서 플레이어의 체력을 다시 MaxHp로 만들어준다.
        Player_Data.Instance.playerData.Hp = Player_Data.Instance.playerData.MaxHp;
        //플레이어의 CapsuleCollider를 다시 활성화 시켜준다.
        Player_Data.Instance.GetComponent<CapsuleCollider>().enabled = true;
        //Player_Combat의 트리거타입의 Player_Live파라미터를 실행 시켜서 플레이어가 다시 일어나서 돌아다닐 수 있도록 해 준다.
        Player_Data.Instance.GetComponent<Player_Combat>().ani.SetTrigger("Player_Live");
        //Player_Combat의 rigid.isKinematic을 false로 바꿔줘서 플레이어가 다시 물리적인 충돌이 가능하도록 만들어준다.
        Player_Data.Instance.GetComponent<Player_Combat>().rigid.isKinematic = false;
        //Player_Combat의 rigid.useGravity를 true로 바꿔줘서 플레이어가 다시 물리적인 충돌이 가능하도록 만들어준다.
        Player_Data.Instance.GetComponent<Player_Combat>().rigid.useGravity = true;
        //Player_Combat의 sword.enabled을 true로 바꿔줘서 플레이어가 몬스터를 다시 공격 할 수 있게 만들어준다.
        Player_Data.Instance.GetComponent<Player_Combat>().sword.enabled = true;
        //UI_StatusBar의 hpBar.value를 maxValue로 바꿔서 다시 플레이어의 체력이 꽉 차도록 만들어 준다.
        UI_StatusBar.Instance.hpBar.value = UI_StatusBar.Instance.hpBar.maxValue;
    }
}