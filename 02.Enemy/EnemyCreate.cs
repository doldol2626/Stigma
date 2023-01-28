using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : Enemy
{
    // 몬스터 생성 존의 자식 오브젝트 트랜스폼 컴포넌트를 담아주기 위한 변수
    Transform[] createArea;
    // public Bounds spawnArea;

    // 자식오브젝트의 포지션 + 오프셋 범위안에서 무작위로 추출한 X, Z좌표를 담아줄 변수
    float randomInSpawnAreaX;
    float randomInSpawnAreaZ;


    // 범위를 정해줄 offset 변수
    public float rangeOffset = 10f;

    //자식 오브젝트 트랜스폼 배열의 인덱스를 무작위로 결정해 줄 변수
    int randomSpawnPoint;

    // 오브젝트 풀을 위한 리스트 선언 (생성된 몬스터를 담아줄 변수)
    List<GameObject> monsters = new List<GameObject>();

    // invokeRepeat의 주기를 결정해줄 변수
    float randomTime; // minRepeatTime ~ maxRepeatTime 사이 값을 추출해주는 변수

    // Start is called before the first frame update
    void Start()
    {
        // 게임 오브젝트의 자식 오브젝트의 컴포넌트를 배열에 담아준다
        createArea = GetComponentsInChildren<Transform>();

        // 생성 주기를 랜덤으로 추출해주는 함수
        IntervalTime();

        // 5~10초 사이의 랜덤 시간 동안 CreateFunction함수를 반복한다.
        InvokeRepeating("CreateFunction", monsterData.startCreateTime, randomTime);
    }

    // 몬스터 생성 함수
    void CreateFunction()
    {
        // Player가 죽었을 때 몬스터 생성 모두 반환
        if (player.playerData.isDie)
        {
            return;
        }

        // 프리팹을 담는 리스트 개수가 몬스터데이터에 설정된 생성 한계 개수보다 크지 않다면
        if (monsters.Count < monsterData.createLimitNumber)
        {
            // 몬스터 Zone의 자식 오브젝트 중 하나를 랜덤으로 추출
            randomSpawnPoint = Random.Range(1, createArea.Length);

            // 몬스터데이터에 등록된 몬스터를 생성해서 newPrefab 변수에 담는다.
            GameObject newPrefab = Instantiate(monsterData.Prefab);

            // monsters 리스트에 newPrefab을 추가
            monsters.Add(newPrefab);

            // 콜라이더 범위 내 랜덤으로 X, Z값 추출
            randomInSpawnAreaX = Random.Range(createArea[randomSpawnPoint].position.x - rangeOffset, createArea[randomSpawnPoint].position.x + rangeOffset);
            randomInSpawnAreaZ = Random.Range(createArea[randomSpawnPoint].position.z - rangeOffset, createArea[randomSpawnPoint].position.z + rangeOffset);

            // newPrefab 위치를 몬스터 Zone에서 선택한 자식 오브젝트의 포지션값 Y, 그 콜라이더 컴포넌트의 랜덤 범위를 추출한 포지션값X, Z로 위치
            newPrefab.transform.position = new Vector3(randomInSpawnAreaX, transform.position.y, randomInSpawnAreaZ);
        }
    }

    // 반복하는 주기를 랜덤으로 추출해주는 함수
    void IntervalTime()
    {
        // Player가 죽었을 때 몬스터 주기 생성 모두 반환
        if (player.playerData.isDie)
        {
            return;
        }

        // 몬스터데이터에 설정된 한계 개수만큼만 반복
        for (int i = 0; i < monsterData.createLimitNumber; i++)
        {
            // 최소반복시간과 최대 반복시간 중 랜덤시간 추출
            randomTime = Random.Range(monsterData.createMinTime, monsterData.createMaxTime);
        }
    }
}
