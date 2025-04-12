using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // ���� ������ (9��)
    public Transform[] spawnPoints; // ���� ���� ��ġ
    public MonsterSpawnData spawnData; // ���庰 Ȯ�� ������
    public int round = 1; // ���� ����
    public int baseSpawnMin = 1;
    public int baseSpawnMax = 3;
    public int wallHealth = 100; // ���� ü��

    private List<Monster> activeMonsters = new List<Monster>();

    void Start()
    {
        StartRound();
    }

    void StartRound()
    {
        round++; // ���� ����
        int spawnCount = Random.Range(baseSpawnMin + round / 2, baseSpawnMax + round / 2 + 1);
        SpawnMonsters(spawnCount);
        MoveMonsters();
    }

    void SpawnMonsters(int count)
    {
        RoundSpawnData currentRoundData = GetRoundData();
        if (currentRoundData == null) return;

        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject monsterPrefab = GetRandomMonster(currentRoundData);
            if (monsterPrefab != null)
            {
                GameObject monsterObj = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
                Monster monster = monsterObj.GetComponent<Monster>();
                if (monster != null)
                {
                    activeMonsters.Add(monster);
                }
            }
        }
    }

    GameObject GetRandomMonster(RoundSpawnData roundData)
    {
        float randomValue = Random.value * 100f;
        float cumulativeChance = 0f;

        for (int i = 0; i < monsterPrefabs.Length; i++)
        {
            cumulativeChance += roundData.spawnChances[i];
            if (randomValue <= cumulativeChance)
            {
                return monsterPrefabs[i];
            }
        }
        return null;
    }

    RoundSpawnData GetRoundData()
    {
        foreach (var data in spawnData.rounds)
        {
            if (data.round == round)
            {
                return data;
            }
        }
        return null;
    }

    void MoveMonsters()
    {
        for (int i = activeMonsters.Count - 1; i >= 0; i--)
        {
            Monster monster = activeMonsters[i];
            monster.MoveForward();

            if (monster.ReachedWall())
            {
                wallHealth -= monster.damage;
                Destroy(monster.gameObject);
                activeMonsters.RemoveAt(i);
            }
        }
    }
}
