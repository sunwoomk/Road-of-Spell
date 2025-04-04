using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;  // 9개의 몬스터 프리팹 배열
    public Transform[] spawnPoints;  // 몬스터 스폰 위치 배열
    public MonsterSpawnData spawnData;  // ScriptableObject에서 확률 데이터 가져옴
    public int round = 1;  // 현재 라운드
    public int minMonsters = 1;  // 최소 스폰 마릿수
    public int maxMonsters = 3;  // 최대 스폰 마릿수 (라운드가 올라가면 증가 가능)

    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        if (spawnData == null)
        {
            Debug.LogError("MonsterSpawnData가 설정되지 않았습니다!");
            yield break;
        }

        // 현재 라운드의 몬스터 확률 데이터 가져오기
        RoundSpawnData roundData = GetRoundSpawnData(round);
        if (roundData == null)
        {
            Debug.LogError($"라운드 {round}에 대한 확률 데이터가 없습니다!");
            yield break;
        }

        // 랜덤으로 스폰할 마릿수 결정
        int monsterCount = Random.Range(minMonsters, maxMonsters + 1);

        // 랜덤한 위치에서 몬스터 스폰
        for (int i = 0; i < monsterCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];  // 랜덤 위치 선택
            GameObject selectedMonster = GetRandomMonster(roundData);  // 확률에 따라 몬스터 선택
            if (selectedMonster != null)
            {
                Instantiate(selectedMonster, spawnPoint.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.5f);  // 몬스터 스폰 간격
        }
    }

    // 현재 라운드에 해당하는 확률 데이터 가져오기
    RoundSpawnData GetRoundSpawnData(int currentRound)
    {
        foreach (var data in spawnData.rounds)
        {
            if (data.round == currentRound)
                return data;
        }
        return null;
    }

    // 확률 기반으로 랜덤한 몬스터 선택
    GameObject GetRandomMonster(RoundSpawnData roundData)
    {
        float totalChance = 0;
        foreach (float chance in roundData.spawnChances)
        {
            totalChance += chance;
        }

        float randomValue = Random.Range(0, totalChance);
        float cumulative = 0;

        for (int i = 0; i < roundData.spawnChances.Length; i++)
        {
            cumulative += roundData.spawnChances[i];
            if (randomValue <= cumulative)
            {
                return monsterPrefabs[i];
            }
        }

        return null;
    }
}
