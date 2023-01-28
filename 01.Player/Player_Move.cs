using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 네임스페이스 추가
using UnityEngine.EventSystems;// 네임 스페이스 추가

public class Player_Move : MonoBehaviour
{
    // 키 값
    float hdown;
    float vdown;
    bool roll; // 구르기에 대한 변수!
    bool isRoll; // 무한 구르기를 막기 위한 변수!

    // 기본 움직임
    public float moveSpeed = 10.0f;
    Vector3 dir;

    // 애니메이터
    public Animator ani;

    [HideInInspector] //<-Public인스펙터창에서 보여지지않게 하기
    //올려치기 스킬이 실행이 되면 플레이어의 공격,구르기,움직임을 막을 bool변수
    public bool AttackUp;


    // public InventoryStart inventory;
    void Awake()
    {
        // 자식의 애니메이터 컴포넌트를 불러온다.
        ani = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        // 키 값 입력
        hdown = Input.GetAxis("Horizontal");
        vdown = Input.GetAxis("Vertical"); // 방향키 키 값 입력
        roll = Input.GetButtonDown("Jump");// 스페이스바 클릭으로 구르기!

        if (!IsUnReady())
            //로딩 중이 아닐 때 + PlayerData에서 false면 이 함수 실행, true면 실행X
        {
            // 함수 정리
            Move(); // 움직임
            Roll(); // 구르기에 관한 클래스 추가!
        }
    }

    // 움직임
    void Move()
    {
        // AttackUp이 false라면
        if (!AttackUp)
        {
            // 방향 정규화
            dir = new Vector3(hdown, 0, vdown).normalized;

            // isWalk 파라미터 애니메이션
            ani.SetFloat("isWalk", dir.magnitude);

            // 플레이어 움직이기
            transform.position += dir * Player_Data.Instance.playerData.Speed * Time.deltaTime;

            // 가는 방향 벡터값으로 바라보기
            transform.LookAt(transform.position + dir);
        }
    }

    // 구르기
    void Roll()
    {
        // AttackUp이 false라면
        if (!AttackUp)
        {
            //구르기 실행
            // 구르기 버튼이 눌렸거나 구르기 가능 상태 변수가 false일 때
            if (roll && !isRoll)
            {
                // 구르기 가능 상태 변수는 true
                isRoll = true;

                Debug.Log("구르기");

                // 구르는 애니메이션 작동
                ani.SetTrigger("doRoll");

                Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[0];
                Player_Data.Instance.audioSource.Play();
            }
        }
    }

    // 충돌하고 있는 상태라면
    void OnCollisionStay(Collision collision)
    {
        // 부딫히는 물체의 태그가 바닥이면
        if (collision.gameObject.tag == "Ground")
        {
            // 구르기 가능 상태 변수가 false가 된다!
            isRoll = false;
        }
    }

    // 충돌 상태를 빠져나간 순간
    void OnCollisionExit(Collision collision)
    {
        // 충돌을 빠져나간 게임오브젝트 tag가 Floor라면
        if (collision.gameObject.tag == "Ground")
        {
            // 구르기 가능 상태 변수를 true로 바꾸어라.
            isRoll = true;
        }
    }

    public bool IsUnReady()
    {
        List<bool> isUnReady = new();

        isUnReady.Add(SceneLoadManager.Instance.isLoading);
        isUnReady.Add(UI_ChatLog.Instance.inputField.isFocused);
        isUnReady.Add(Player_Data.Instance.playerData.isDie);

        isUnReady.TrimExcess(); //불필요한 메모리 반환

        return isUnReady.Contains(true); //위 조건 중 하나라도 들어맞는지 판단
    }
}