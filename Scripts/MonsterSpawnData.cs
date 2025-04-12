using UnityEngine;

[System.Serializable]
public class RoundSpawnData
{
    public int round; // ���� ��ȣ
    public float[] spawnChances = new float[9]; // 9���� ������ ���� Ȯ�� (0~100%)
}

[CreateAssetMenu(fileName = "MonsterSpawnData", menuName = "Game Data/Monster Spawn Data")]
public class MonsterSpawnData : ScriptableObject
{
    public RoundSpawnData[] rounds; // ���� ���� ���� ������ ����
}
