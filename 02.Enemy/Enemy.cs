using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //체력바 네임스페이스 추가
using TMPro;

public class Enemy : MonoBehaviour
{
    // 몬스터 상태에 대한 enum 정의
    public enum MonsterState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die,
        PlayerDie
    }

    public MonsterState m_State; // 몬스터 상태에 대한 enum 변수

    public MonsterData monsterData;            //몬스터 데이터(public)

    protected Animator anim;                   //애니메이터 컴포넌트
    protected BoxCollider monsterBody; //적 콜라이더 컴포넌트

    protected Player_Combat player;            //플레이어
    protected bool Sword_check = false;        //플레이어의 Sword와 충돌 판정할 bool변수

    protected int hp;                          //체력
    public Slider hpSlider;                    //체력바(public)

    // 아이템 오브젝트 생성을 위한 프리팹 변수
    public GameObject[] getItems; // Item 프리팹 종류를 모두 담을 배열 변수
    public GameObject getCoin; // 코인 프리팹 변수

    public TextMeshProUGUI monsterName; // 몬스터 이름 UI

    void Awake()
    {
        //앞서 선언한 컴포넌트 불러오기
        anim = GetComponentInChildren<Animator>(); // 몬스터의 자식 컴포넌트인 애니메이터를 변수에 담는다.
        monsterBody = GetComponent<BoxCollider>(); // 몬스터 본체 콜라이더 컴포넌트를 변수에 담는다

        //플레이어 불러오기
        player = FindObjectOfType<Player_Combat>();
    }

    public virtual void TakeDamage(int damage) //적이 공격당하면 Player의 스크립트에서 호출되는 함수
    {
        hp -= damage;
        Debug.Log("Monster HP :"+hp);

        //플레이어 -> 유효타 시 클래스 게이지 획득 및 슬라이더에 반영
        if (Player_Data.Instance.playerData.Cp < Player_Data.Instance.playerData.MaxCp)
        {
            Player_Data.Instance.playerData.Cp += Random.Range(5, 10);

            if (Player_Data.Instance.playerData.Cp >= Player_Data.Instance.playerData.MaxCp)
            {
                Player_Data.Instance.playerData.Cp = Player_Data.Instance.playerData.MaxCp;
            }

            UI_StatusBar.Instance.CpBarUpdate();
        }

        //여기까진 모든 적이 공통되나, 각자 추가할 내용이 다름.
        //따라서 virtual로 선언, 자식들로 하여금 이 클래스를 상속받아 override하도록 함.
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword") && player.sword.isTrigger == true)
        //아래에 있는 Player_Move스크립트를 수정하면 오류
        //가 사라진다.
        // "Sword"태그를 가진 오브젝트와 충돌을 하고 플레이어의 sword의 트리거가 켜지면                             
        {
            TakeDamage(monsterData.Attack); //TakeDamage의 함수 호출

            Sword_check = true;//Sword_check를 false에서 true로 바꾼다.
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sword") && player.sword.isTrigger == false && Sword_check == true)
        {
            Sword_check = false;
        }
    }
}