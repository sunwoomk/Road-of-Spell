using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // 몬스터 프리팹 (9개)
    public Transform[] spawnPoints; // 몬스터 스폰 위치
    public MonsterSpawnData spawnData; // 라운드별 확률 데이터
    public int round = 1; // 현재 라운드
    public int baseSpawnMin = 1;
    public int baseSpawnMax = 3;
    public int wallHealth = 100; // 성벽 체력

    private List<Monster> activeMonsters = new List<Monster>();

    void Start()
    {
        StartRound();
    }

    void StartRound()
    {
        round++; // 라운드 증가
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
