using System.Collections;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    public IEnumerator MoveOneStep()
    {
        Vector3 targetPos = transform.position + new Vector3(0, 0, -1);
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
    }
}
