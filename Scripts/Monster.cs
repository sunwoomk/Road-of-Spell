using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    public int moveDistance = 1; // 한 턴당 이동 거리
    public int damage = 10; // 성벽에 주는 데미지
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    public void MoveForward()
    {
        targetPosition += new Vector3(moveDistance, 0, 0); // X축 방향 이동
        StartCoroutine(MoveSmoothly());
    }

    IEnumerator MoveSmoothly()
    {
        Vector3 start = transform.position;
        float time = 0;
        while (time < 0.5f) // 부드럽게 이동
        {
            transform.position = Vector3.Lerp(start, targetPosition, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    public bool ReachedWall()
    {
        return transform.position.x >= 10; // 성벽 도달 여부 체크
    }
}
