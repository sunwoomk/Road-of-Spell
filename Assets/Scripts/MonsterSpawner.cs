using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;  // 9���� ���� ������ �迭
    public Transform[] spawnPoints;  // ���� ���� ��ġ �迭
    public MonsterSpawnData spawnData;  // ScriptableObject���� Ȯ�� ������ ������
    public int round = 1;  // ���� ����
    public int minMonsters = 1;  // �ּ� ���� ������
    public int maxMonsters = 3;  // �ִ� ���� ������ (���尡 �ö󰡸� ���� ����)

    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        if (spawnData == null)
        {
            Debug.LogError("MonsterSpawnData�� �������� �ʾҽ��ϴ�!");
            yield break;
        }

        // ���� ������ ���� Ȯ�� ������ ��������
        RoundSpawnData roundData = GetRoundSpawnData(round);
        if (roundData == null)
        {
            Debug.LogError($"���� {round}�� ���� Ȯ�� �����Ͱ� �����ϴ�!");
            yield break;
        }

        // �������� ������ ������ ����
        int monsterCount = Random.Range(minMonsters, maxMonsters + 1);

        // ������ ��ġ���� ���� ����
        for (int i = 0; i < monsterCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];  // ���� ��ġ ����
            GameObject selectedMonster = GetRandomMonster(roundData);  // Ȯ���� ���� ���� ����
            if (selectedMonster != null)
            {
                Instantiate(selectedMonster, spawnPoint.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.5f);  // ���� ���� ����
        }
    }

    // ���� ���忡 �ش��ϴ� Ȯ�� ������ ��������
    RoundSpawnData GetRoundSpawnData(int currentRound)
    {
        foreach (var data in spawnData.rounds)
        {
            if (data.round == currentRound)
                return data;
        }
        return null;
    }

    // Ȯ�� ������� ������ ���� ����
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
