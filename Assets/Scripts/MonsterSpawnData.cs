using UnityEngine;

[System.Serializable]
public class RoundSpawnData
{
    public int round; // 라운드 번호
    public float[] spawnChances = new float[9]; // 9마리 몬스터의 스폰 확률 (0~100%)
}

[CreateAssetMenu(fileName = "MonsterSpawnData", menuName = "Game Data/Monster Spawn Data")]
public class MonsterSpawnData : ScriptableObject
{
    public RoundSpawnData[] rounds; // 여러 개의 라운드 데이터 저장
}
