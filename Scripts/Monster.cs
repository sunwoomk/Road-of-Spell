using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    public int moveDistance = 1; // �� �ϴ� �̵� �Ÿ�
    public int damage = 10; // ������ �ִ� ������
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    public void MoveForward()
    {
        targetPosition += new Vector3(moveDistance, 0, 0); // X�� ���� �̵�
        StartCoroutine(MoveSmoothly());
    }

    IEnumerator MoveSmoothly()
    {
        Vector3 start = transform.position;
        float time = 0;
        while (time < 0.5f) // �ε巴�� �̵�
        {
            transform.position = Vector3.Lerp(start, targetPosition, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    public bool ReachedWall()
    {
        return transform.position.x >= 10; // ���� ���� ���� üũ
    }
}
