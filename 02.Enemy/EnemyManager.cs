using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : Enemy
{
    float distance;// 플레이어와 몬스터의 거리

    // 몬스터의 원래 위치를 저장할 변수
    Vector3 originPos;

    // 움직임
    Vector3 dir;// 방향 변수

    // 타이머 변수
    float currentTime = 0; // 타임 카운트
    float attackDelay = 2f; // 공격 지연 시간

    // 플레이어 컴포넌트 변수
    Transform playerTr; // 플레이어 트랜스폼

    // monster 액션을 위한 구조체 변수
    MonsterAction m_act;

    // wolf의 다채로운 idle동작을 위한 시간 변수
    float stayTime;

    // 몬스터가 죽은 뒤에 아이템 드롭을 위한 변수
    int randomItem; // 아이템 종류가 무작위로 선택될 랜덤 인덱스 변수
    float randomItemNum; // 랜덤으로 나올 아이템 개수를 결정하는 변수
    public int randomItemlimitNum = 3; // 랜덤으로 나올 아이템 개수의 한계 개수 변수
    float itemRangeOffset = 2f; // 몬스터의 위치에서 얼만큼 떨어져서 생성할 지의 범위를 결정하기 위한 변수
    float randomDropRangeX; // 드롭될 위치를 오프셋 한계거리 안에서 선택될 X좌표 랜덤 변수
    float randomDropRangeZ; // 드롭될 위치를 오프셋 한계거리 안에서 선택될 Z좌표 랜덤 변수
    public float coinNum = 1; // 코인 생성될 개수

    // Start is called before the first frame update
    void Start()
    {

        // Player의 트랜스폼 컴포넌트 불러오기
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        // 몬스터 상태를 Idle상태로 초기화
        m_State = MonsterState.Idle;

        // 몬스터의 처음 위치를 저장
        originPos = transform.position;

        hp = monsterData.MaxHp; //몬스터 체력 초기화

        // 구조체에선 애니메이터 특성을 직접 사용할 수 없기 때문에 변수에 한 번 더 변수를 담는다.
        m_act.ani = anim;

        // Player레벨보다 몬스터 레벨이 같거나 작으면 초록색으로 몬스터 이름 표시
        if (monsterData.Level <= Player_Data.Instance.playerData.Level)
        {
            // 몬스터 이름 TextUI가 몬스터 데이터 Pname으로 설정
            monsterName.text = "​<color=#98FFAB>" + monsterData.Pname + " Lv." + monsterData.Level + "</color>";
        }
        // Player레벨보다 몬스터 레벨이 5를 초과하면 빨간색으로 몬스터 이름 표시
        else if (monsterData.Level > Player_Data.Instance.playerData.Level + 5)
        {
            // 몬스터 이름 TextUI가 몬스터 데이터 Pname으로 설정
            monsterName.text = "​<color=#EA1E00>" + monsterData.Pname + " Lv." + monsterData.Level + "</color>";
        }
        // 그 외에는 노란색으로 몬스터 이름 표시 (Default)
        else
        {
            // 몬스터 이름 TextUI가 몬스터 데이터 Pname으로 설정
            monsterName.text = "​<color=#FFD89B>" + monsterData.Pname + " Lv." + monsterData.Level + "</color>";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Player가 죽었을 때 몬스터 상태 판단 모두 반환
        if (player.playerData.isDie)
        {
            return;
        }

        // 플레이어 방향 정규화
        dir = (playerTr.position - transform.position).normalized;

        // 플레이어와의 거리 측정
        distance = Vector3.SqrMagnitude(playerTr.position - transform.position);

        switch (m_State)
        {
            case MonsterState.Idle:
                Idle();
                m_act.Walk(false);
                break;

            case MonsterState.Move:
                m_act.Walk(true);
                m_act.Attack(false);
                Move();
                break;

            case MonsterState.Attack:
                Attack();
                m_act.Attack(true);
                break;

            case MonsterState.Return:
                Return();
                break;
        }

        hpSlider.value = (float)hp / monsterData.MaxHp; //현재 hp를 슬라이더의 value에 반영
    }

    #region 상태 변화 실행 함수
    // 몬스터 상태가 Idle 일 때 호출
    void Idle()
    {
        // 애니메이터 트랜지션 breathe -> sit (파라미터 : stayTime greater 20)
        // Wolf의 다채로운 Idle 모션을 위해
        stayTime += Time.deltaTime; // Idle로 머무는 시간을 더해줌

        // 만약 25이상이면
        if (stayTime >= 25)
        {
            // 0으로 초기화
            stayTime = 0;
        }

        // monsterData가 Wolf일때만 호출되는 액션 실행 함수
        m_act.StayIdle(monsterData, stayTime);

        // 몬스터와 플레이어 거리가 찾는 범위 반경안에 들어온다면
        if (distance <= monsterData.findDistance)
        {
            // 몬스터 상태를 Move로 변경
            m_State = MonsterState.Move;

            stayTime = 0;
        }
    }

    // 몬스터 상태가 Move일 때 호출
    void Move()
    {
        // 몬스터의 원래 위치와 현재 위치가 한계 범위를 벗어났을 때
        if (Vector3.SqrMagnitude(originPos - this.transform.position) >= monsterData.limitDistance)
        {
            // 몬스터 상태를 Return으로 변경
            m_State = MonsterState.Return;
        }
        // 그게 아니고, 플레이어와의 거리가 공격범위를 벗어난 상태일 때
        else if (distance >= monsterData.attackDistance)
        {
            // 플레이어를 계속 바라보고
            transform.LookAt(playerTr);

            // 몬스터 움직이기
            transform.position += dir * monsterData.moveSpeed * Time.deltaTime;
        }
        // 아니면
        else
        {
            // 몬스터 상태를 Attack으로 변경
            m_State = MonsterState.Attack;

            // 공격 지연시간과 카운트시간을 처음에 일치 시켜주고 바로 공격
            currentTime = attackDelay;
        }

    }

    // 몬스터 상태가 Attack 일 때 호출
    void Attack()
    {
        // 시간 카운트
        currentTime += Time.deltaTime;

        // 플레이어와 몬스터의 거리가 공격범위 안이라면
        if (distance <= monsterData.attackDistance)
        {
            // 공격 지연시간이 카운트 타이머에 도달했을 때
            if (currentTime >= attackDelay)
            {
                // 카운트 타임 초기화
                currentTime = 0;
            }
        }
        // 플레이어와 몬스터의 거리가 공격범위 밖이라면
        else
        {
            // 몬스터의 상태를 Move로 변경
            m_State = MonsterState.Move;

            // 카운트 타임 초기화
            currentTime = 0;
        }
    }

    // 몬스터 상태가 Return 일 때 호출
    void Return()
    {
        // 플레이어와 몬스터의 거리가 공격범위 안이라면
        if (distance <= monsterData.attackDistance)
        {
            // 몬스터 상태를 Move으로 변경
            m_State = MonsterState.Move;
        }
        // 몬스터의 원래 위치와 현재 위치가 0.1보다 크다면
        if (Vector3.Distance(originPos, this.transform.position) > 0.1f)
        {
            // 원래 위치를 향한 방향 정규화
            Vector3 retDirection = (originPos - transform.position).normalized;

            // originPos를 계속 바라보고
            transform.LookAt(originPos);

            // 정확한 위치 보정을 위해 원래 위치로 계속 움직인다.
            transform.position += retDirection * monsterData.moveSpeed * Time.deltaTime;
        }
        // 그게 아니라면
        else
        {
            // 현재 위치에 원래 위치를 대입
            transform.position = originPos;

            // 처음 저장했던 최대 체력으로 다시 회복
            hp = monsterData.MaxHp;

            // 몬스터 상태를 Idle 로 변경
            m_State = MonsterState.Idle;
        }

    }

    // 몬스터 상태가 Damaged 일 때
    void Damaged()
    {
        // Hit 액션 호출
        m_act.Hit();
        // DamageProcess 코루틴 함수 호출
        StartCoroutine(DamageProcess());
    }

    // DamageProcess 코루틴 함수
    IEnumerator DamageProcess()
    {
        // 0.5초 지연 후
        yield return new WaitForSeconds(0.5f);

        // 몬스터 상태를 Move로 변경
        m_State = MonsterState.Move;
    }

    // 몬스터가 죽었을 때
    void Die()
    {
        // Die 액션 호출
        m_act.Die();

        // 코루틴 함수 전부 멈춤
        StopAllCoroutines();

        // DieProcess 코루틴 함수 호출
        StartCoroutine(DieProcess());

        // 몬스터가 죽고 아이템을 드롭해 줄 함수를 호출
        ItemDrop();

        PlayerData playerData = Player_Data.Instance.playerData; // 플레이어 데이터 싱글톤을 지역변수로 불러

        if (playerData.Level >= 10) return; // 플레이어 레벨이 10이라면 경험치 획득 불가

        int getExp = (int)(GetComponent<Enemy>().monsterData.Exp * Random.Range(0.8f, 1.2f));

        Player_Data.Instance.playerData.Exp += getExp;
        // 경험치에 몬스터가 드랍하는 경험치(*0.8에서 1.2)를 더함

        UI_ChatLog.Instance.SystemLog("+" + getExp + " Exp 획득");

        Debug.Log("player 경험치 : " + playerData.Exp);

        UI_StatusBar.Instance.ExpBarUpdate(); // 경험치바 업데이트

        if (playerData.Exp >= playerData.MaxExp) { Player_Data.Instance.LevelUp(); }
        // 만약 경험치 양이 최대 경험치 양보다 클 경우 레벨업 함수 실행
    }

    // DieProcess 코루틴 함수
    IEnumerator DieProcess()
    {
        monsterBody.enabled = true;

        // 2초 지연 뒤에
        yield return new WaitForSeconds(2f);

        // 게임 오브젝트 삭제
        Destroy(gameObject);
    }

    // Player가 죽었을 때 Player_Combat에서 호출되는 함수
    public void PlayerDieAction()
    {
        // 몬스터 데이터 이름이 Golem이거나 Wolf일 때
        if (monsterData.Pname == "골렘" || monsterData.Pname == "스컬 킹" || monsterData.Pname == "늑대")
        {
            // 호출되는 승리 액션 실행 함수
            m_act.Victory();
        }
        // 그 외의 몬스터 데이터라면
        else if (monsterData != null)
        {
            // 몬스터의 상태를 Idle상태로 변환
            m_State = MonsterState.Idle;
        }

        // 몬스터 상태를 PlayerDie로 변경
        m_State = MonsterState.PlayerDie;
    }
    #endregion

    #region 데미지 적용 함수
    // 몬스터 피격 판정을 위한 함수
    public override void TakeDamage(int hitPower)
    {
        base.TakeDamage(hitPower); //부모 클래스의 TakeDamage 먼저 사용

        // 몬스터 상태가 맞았거나 돌아가거나, 죽은 상태라면
        if (m_State == MonsterState.Damaged || m_State == MonsterState.Return || m_State == MonsterState.Die)
        {
            // 모두 반환
            return;
        }

        // 체력이 0보다 크다면
        if (hp > 0)
        {
            // 몬스터 상태를 Damaged 로 변경
            m_State = MonsterState.Damaged;

            Damaged();
        }
        // 그렇지 않으면
        else
        {
            // 몬스터 상태를 Die로 변경
            m_State = MonsterState.Die;

            Die();
        }
    }
    #endregion

    #region 몬스터 액션 실행 구조체
    // 몬스터의 액션을 달리하기 위해
    public struct MonsterAction
    {
        // 불러올 애니메이터의 변수
        public Animator ani;

        // Wolf의 다채로운 idle동작을 위한 함수
        public void StayIdle(MonsterData monster, float stayTime)
        {
            // 만약 monsterData의 이름이 Wolf라면
            if (monster.Pname == "Wolf")
            {
                // stayTime이라는 파라미터의 매개변수값 전달
                ani.SetFloat("stayTime", stayTime);
            }
        }

        // 걷는 액션을 매개변수로 받아 조절
        public void Walk(bool tf)
        {
            ani.SetBool("isWalk", tf);
        }
        // 공격 액션을 매개변수로 받아 조절
        public void Attack(bool tf)
        {
            ani.SetBool("doAttack", tf);
        }
        // 맞는 액션
        public void Hit()
        {
            ani.SetTrigger("getHit");
        }
        // 죽는 액션
        public void Die()
        {
            ani.SetTrigger("isDie");
        }
        // Player가 죽었을 때 액션 (Golem만 해당 나머지는 Idle로 돌아가게 하기 위함)
        public void Victory()
        {
            ani.SetTrigger("victory");
        }
    }
    #endregion

    // 몬스터가 죽고나서 아이템을 드롭할 함수
    void ItemDrop()
    {
        // 드롭될 아이템개수를 랜덤으로 정해주기
        randomItemNum = Random.Range(0, randomItemlimitNum);

        // 10%확률 미만이면
        if (randomItemNum < 0.1)
        {
            // 드롭될 아이템 개수가 1개
            randomItemNum = 0;
        }
        // 그게 아니고, 10%확률 이상이고, 60%확률 미만이면
        else if (0.1 <= randomItemNum && randomItemNum < 0.6)
        {
            // 드롭될 아이템 개수가 2개
            randomItemNum = 1;
        }
        // 그게 아니고, 60%확률 이상이면
        else if (0.6 <= randomItemNum)
        {
            // 드롭될 아이템 개수가 3개
            randomItemNum = 2;
        }

        Debug.Log("randomItemNum : " + randomItemNum);

        // 드롭될 아이템 개수만큼 아이템을 생성시킬 반복문
        for (int i = 0; i < randomItemNum; i++)
        {
            // getItems 배열에서 아이템 종류를 index번호로 랜덤으로 추출
            randomItem = Random.Range(0, getItems.Length);

            // 몬스터의 센터포지션에서 오프셋 최소-최대거리중 랜덤좌표 X, Z를 추출
            randomDropRangeX = Random.Range(transform.position.x - itemRangeOffset, transform.position.x + itemRangeOffset);
            randomDropRangeZ = Random.Range(transform.position.z - itemRangeOffset, transform.position.z + itemRangeOffset);

            // 랜덤 인덱스번호 중 한 개의 아이템을 생성하여 새로운 게임오브젝트 변수에 담는다.
            GameObject newItem = Instantiate(getItems[randomItem]);

            // 새로 생성된 아이템의 좌표를 랜덤으로 추출한 X,Z좌표 몬스터의 원래 Y좌표 포지션으로 위치
            newItem.transform.position = new Vector3(randomDropRangeX, transform.position.y, randomDropRangeZ);
        }
        for (int i = 0; i < coinNum; i++)
        {
            Instantiate(getCoin, transform.position, transform.rotation);
        }
    }
}

