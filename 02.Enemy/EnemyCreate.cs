using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreate : Enemy
{
    // ���� ���� ���� �ڽ� ������Ʈ Ʈ������ ������Ʈ�� ����ֱ� ���� ����
    Transform[] createArea;
    // public Bounds spawnArea;

    // �ڽĿ�����Ʈ�� ������ + ������ �����ȿ��� �������� ������ X, Z��ǥ�� ����� ����
    float randomInSpawnAreaX;
    float randomInSpawnAreaZ;


    // ������ ������ offset ����
    public float rangeOffset = 10f;

    //�ڽ� ������Ʈ Ʈ������ �迭�� �ε����� �������� ������ �� ����
    int randomSpawnPoint;

    // ������Ʈ Ǯ�� ���� ����Ʈ ���� (������ ���͸� ����� ����)
    List<GameObject> monsters = new List<GameObject>();

    // invokeRepeat�� �ֱ⸦ �������� ����
    float randomTime; // minRepeatTime ~ maxRepeatTime ���� ���� �������ִ� ����

    // Start is called before the first frame update
    void Start()
    {
        // ���� ������Ʈ�� �ڽ� ������Ʈ�� ������Ʈ�� �迭�� ����ش�
        createArea = GetComponentsInChildren<Transform>();

        // ���� �ֱ⸦ �������� �������ִ� �Լ�
        IntervalTime();

        // 5~10�� ������ ���� �ð� ���� CreateFunction�Լ��� �ݺ��Ѵ�.
        InvokeRepeating("CreateFunction", monsterData.startCreateTime, randomTime);
    }

    // ���� ���� �Լ�
    void CreateFunction()
    {
        // Player�� �׾��� �� ���� ���� ��� ��ȯ
        if (player.playerData.isDie)
        {
            return;
        }

        // �������� ��� ����Ʈ ������ ���͵����Ϳ� ������ ���� �Ѱ� �������� ũ�� �ʴٸ�
        if (monsters.Count < monsterData.createLimitNumber)
        {
            // ���� Zone�� �ڽ� ������Ʈ �� �ϳ��� �������� ����
            randomSpawnPoint = Random.Range(1, createArea.Length);

            // ���͵����Ϳ� ��ϵ� ���͸� �����ؼ� newPrefab ������ ��´�.
            GameObject newPrefab = Instantiate(monsterData.Prefab);

            // monsters ����Ʈ�� newPrefab�� �߰�
            monsters.Add(newPrefab);

            // �ݶ��̴� ���� �� �������� X, Z�� ����
            randomInSpawnAreaX = Random.Range(createArea[randomSpawnPoint].position.x - rangeOffset, createArea[randomSpawnPoint].position.x + rangeOffset);
            randomInSpawnAreaZ = Random.Range(createArea[randomSpawnPoint].position.z - rangeOffset, createArea[randomSpawnPoint].position.z + rangeOffset);

            // newPrefab ��ġ�� ���� Zone���� ������ �ڽ� ������Ʈ�� �����ǰ� Y, �� �ݶ��̴� ������Ʈ�� ���� ������ ������ �����ǰ�X, Z�� ��ġ
            newPrefab.transform.position = new Vector3(randomInSpawnAreaX, transform.position.y, randomInSpawnAreaZ);
        }
    }

    // �ݺ��ϴ� �ֱ⸦ �������� �������ִ� �Լ�
    void IntervalTime()
    {
        // Player�� �׾��� �� ���� �ֱ� ���� ��� ��ȯ
        if (player.playerData.isDie)
        {
            return;
        }

        // ���͵����Ϳ� ������ �Ѱ� ������ŭ�� �ݺ�
        for (int i = 0; i < monsterData.createLimitNumber; i++)
        {
            // �ּҹݺ��ð��� �ִ� �ݺ��ð� �� �����ð� ����
            randomTime = Random.Range(monsterData.createMinTime, monsterData.createMaxTime);
        }
    }
}
