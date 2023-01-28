using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// 네임 스페이스 추가
using UnityEngine.UI; // 네임스페이스 추가
using UnityEngine.SceneManagement; // 네임스페이스 추가

public class Player_Combat : MonoBehaviour
{
    public PlayerData playerData; //플레이어 데이터
    public Animator ani;   //애니메이터

    //플레이어 조작(입력) 변수
    bool fdown; //공격
    bool sdown; //스킬

    // 공격 관련 변수
    //Rigidbody rid;
    public Collider sword; //public GameObject sword 에서 -> public Collider sword로 변경
    BoxCollider boxCollider;

    // 피격 관련 변수
    public Collider Punch;//Attack 오브젝트를 담아줄 Collider타입 변수 선언

    //다른 함수에서 접근이 가능 하도록 public선언 후 인스펙터창에서 보이지 않도록 숨겨준다.
    [HideInInspector]
    public Rigidbody rigid;
    //이 오브젝트의 리지드바디 컴포넌트

    // 플레이어가 죽었을 때 주변 몬스터들이 승리 액션을 취하게 할 변수
    Collider[] monsters; // 플레이어 주변 반경 안 몬스터의 콜라이더를 받을 배열 변수 
    public float limitDetectRange = 50f; // 콜라이더를 감지하기 위한 범위 제한 거리를 결정할 변수
    public int skulldeath = 0; // 스컬킹 잡는걸 셀 변수
    Text quickquestNo; // 퀵퀘스트 몬스터 숫자

    void Awake()
    {
        // 자식의 애니메이터 컴포넌트를 불러온다.
        ani = GetComponentInChildren<Animator>();

        // 무기 오브젝트의 박스콜라이더 컴포넌트를 불러온다.
        boxCollider = sword.GetComponent<BoxCollider>();

        // 리지드바디 컴포넌트를 불러온다.
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //플레이어 데이터 컴포넌트를 불러온다.
        playerData = Player_Data.Instance.playerData;

        playerData.Hp = playerData.MaxHp; //체력 초기화
        playerData.Mp = playerData.MaxMp; //마나 초기화

        UI_StatusBar.Instance.hpBar.value = playerData.MaxHp;  //체력바 초기화
        UI_StatusBar.Instance.mpBar.value = playerData.MaxMp;  //마력바 초기화

        quickquestNo = GameObject.Find("QuickQuestNo").GetComponent<Text>();
    }

    void Update()
    {
        // 키 값 입력
        fdown = Input.GetButtonDown("Fire1"); //마우스 클릭으로 공격!
        sdown = Input.GetButtonDown("Fire2"); //마우스 우클릭으로 스킬 사용!

        if (!GetComponent<Player_Move>().IsUnReady())
        //로딩 중이 아닐 때 + PlayerData에서 false면 이 함수 실행, true면 실행X
        {
            // 함수 정리
            Attack(); // 공격에 관한 클래스 추가!
            Skill();  // 스킬 사용에 관한 함수
        }

        // enemy는 숲에서 출몰하므로 Start에서 찾으면 null이 된다.
        // Enemy태그가 붙어있는 오브젝트를 변수에 담는다.
        GameObject enemyobjects = GameObject.FindGameObjectWithTag("Enemy");

        // enemyobjects가 null이 아니라면
        if (enemyobjects != null)
        {
            // PUNCH태그의 오브젝트 콜라이더 컴포넌트를 담는다.
            Punch = GameObject.FindGameObjectWithTag("PUNCH").GetComponent<Collider>();
        }
        SkullkingKill();
    }

    public void SkullkingKill()
    {
        //현재 씬이름이 포레스트라면
        if (SceneManager.GetActiveScene().name == "Forest")
        {
            skulldeath = 0;
            monsters = Physics.OverlapSphere(transform.position, limitDetectRange);

            foreach (Collider monster in monsters)
            {
                // 만약의 담긴 몬스터의 이름이 스컬킹이라면                  몬스터의 EnemyManager의 컴포넌트 안의 체력바 벨류가 0일때를 호출
                if (monster.gameObject.name == "Enemy_SkullKing(Clone)" && monster.GetComponent<EnemyManager>().hpSlider.value == 0)
                {
                    skulldeath = 1; // 1이 추가
                    print("skulldeath" + skulldeath.ToString());
                    quickquestNo.text = "스컬킹" + "1" + "/1";
                }
            }

        }
    }

    // 공격
    void Attack()
    {
        //만약 UI가 클릭이 되었다면
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //밑에 내용을 실행하지 말고 이 함수를 빠져 나가라
            return;
        }

        //공격 실행
        // 만약 클릭을 한다면 공격 모션이 나간다!
        if (fdown)
        {
            // 코루틴을 이용하여 시간 유지
            StartCoroutine(EnemyHit());

            Debug.Log("공격");
            // 애니메이션 실행
            ani.SetTrigger("doAttack");
            // 공격 중 움직임 멈춤!
            //moveSpeed = 0; 
        }
    }

    // 공격동안 충돌 지연 코루틴 함수
    IEnumerator EnemyHit()
    {
        // 무기의 박스 콜라이더의 isTrigger 체크
        boxCollider.isTrigger = true;

        Player_Data.Instance.audioSource.clip = Player_Data.Instance.audioClips[1];
        Player_Data.Instance.audioSource.Play();

        // 1초 지연 시킨 뒤
        yield return new WaitForSeconds(0.5f);

        // 무기 콜라이더의 isTrigger 체크 해제
        boxCollider.isTrigger = false;
    }

    // 플레이어가 공격당하면 호출되는 함수
    public void DamageAction(int damage)
    {
        if (playerData.PlusHp > 0) //보호막이 있을 경우
        {
            playerData.PlusHp -= damage;          //에너미의 공격력만큼 보호막 hp 감소
            UI_StatusBar.Instance.HpBar2Update(); //보호막 슬라이더에 반영

            if (damage > playerData.PlusHp)            //데미지가 보호막 hp보다 큰 경우
            {
                int temp = damage - playerData.PlusHp; //데미지에서 보호막 잔량을 제한 수치를 계산

                playerData.PlusHp = 0;                 //보호막 hp를 0으로 초기화
                UI_StatusBar.Instance.HpBar2Update();  //해당 수치를 슬라이더에 반영

                playerData.Hp -= temp;                 //앞서 계산한 temp값을 플레이어의 체력에서 차감
                UI_StatusBar.Instance.HpBarUpdate();   //현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영
            }
        }
        else //보호막이 없는 경우(일반적인 상태)
        {
            Debug.Log("Player의 HP : " + playerData.Hp);

            playerData.Hp -= damage;             //에너미의 공격력만큼 플레이어의 체력을 깎는다
            UI_StatusBar.Instance.HpBarUpdate(); //현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영
        }

        if (Player_Data.Instance.playerData.Cp < Player_Data.Instance.playerData.MaxCp)
        {
            Player_Data.Instance.playerData.Cp += Random.Range(1, 6); //클래스 게이지 획득

            if (Player_Data.Instance.playerData.Cp >= Player_Data.Instance.playerData.MaxCp)
            {
                Player_Data.Instance.playerData.Cp = Player_Data.Instance.playerData.MaxCp;
            }

            UI_StatusBar.Instance.CpBarUpdate(); //슬라이더에 반영
        }

        // 플레이어의 체력이 0보다 작다면
        if (playerData.Hp <= 0)
        {
            Debug.Log("죽음");//콘솔창에 "죽음"출력

            // 플레이어 상태를 죽음 상태로 변경
            playerData.isDie = true;

            ani.SetTrigger("isDie");

            // 플레이어 주변 limitDetectRange만큼의 콜라이더를 감지하여 배열에 담는다.
            monsters = Physics.OverlapSphere(transform.position, limitDetectRange);

            // 담긴 콜라이더 수만큼 반복할 반복문
            foreach(Collider monster in monsters)
            {
                // 만약의 담긴 몬스터의 태그가 Enemy라면
                if(monster.gameObject.tag == "Enemy")
                {
                    // 그 몬스터의 EnemyManager의 컴포넌트 안의 PlayerCheck()함수를 호출
                    monster.GetComponent<EnemyManager>().PlayerDieAction();
                }
            }

            sword.enabled = false;
            //플레이어가 죽었으므로 sword컴포넌트를 비활성화 해줘서 충돌판정이 일어나지않도록 해 준다.
            gameObject.GetComponent<CapsuleCollider>().enabled = false;

            //플레이어의 콜라이더가 비활성 되었으므로 플레이어가 바닥을 뚫고 계속 떨어지지 않도록 리지드바디기능을 제어해준다.
            rigid.useGravity = false;
            rigid.isKinematic = true;

            //플레이어의 CapsuleCollider를 비활성화 해줘서 "PUNCH"태그를 가진 오브젝트와 충돌처리가 되지 않도록 해준다.

            //Destroy(gameObject, 4.5f);
            //4.5초 후에 플레이어 제거
        }
    }


    // 몬스터의 공격을 판단하기 위한 충돌 판정
    private void OnTriggerEnter(Collider other)
    {
        // 만약 충돌한 오브젝트의 태그가 PUNCH라면
        if (other.gameObject.tag == "PUNCH")
        {
            Debug.Log("맞았다");

            // 충돌한 오브젝트의 부모인 컴포넌트 EnemyManager를 불러와 몬스터의 공격력을 담는다.
            int monsterAttack = other.gameObject.GetComponentInParent<EnemyManager>().monsterData.Attack;

            // 몬스터의 공격력만큼 플레이어의 체력을 깎는 함수를 호출한다.
            DamageAction(monsterAttack);
        }
    }

    //스킬
    void Skill()
    {
        //만약 UI가 클릭이 되었다면 밑에 내용을 실행하지 말고 이 함수를 빠져 나가라
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        //우클릭 시 스킬 실행
        if (sdown) { SkillManager.Instance.ChooseSkill(); }
    }
}