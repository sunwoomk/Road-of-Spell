using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public enum TurnPhase { MonsterMove, SpellCast }
    public TurnPhase currentPhase;

    public Button endTurnButton;

    public MonsterSpawner monsterSpawner;

    void Start()
    {
        currentPhase = TurnPhase.MonsterMove;
        endTurnButton.onClick.AddListener(EndTurn);
        StartTurn(); // ���� ���� �� ù �� ����
    }

    void StartTurn()
    {
        switch (currentPhase)
        {
            case TurnPhase.MonsterMove:
                MonsterMove();
                break;
            case TurnPhase.SpellCast:
                SpellCast();
                break;
        }
    }

    void MonsterMove()
    {
        Debug.Log("���Ͱ� �̵��մϴ�.");
        // ���� �̵� ���� �߰� (��: ���� �̵� �ִϸ��̼�)
        //StartCoroutine(monsterSpawner.SpawnMonsters());
        StartCoroutine(HandleMonsterTurn());

        // ���� �ð��� ���� �� �ڵ����� SpellCast�� ��ȯ
        //Invoke(nameof(SwitchToSpellCast), 1.0f); // 1�� �� ���� �������� ��ȯ
    }

    IEnumerator HandleMonsterTurn()
    {
        // ���� ����
        yield return StartCoroutine(monsterSpawner.SpawnMonsters());

        // ������ + ���� ���� ����
        foreach (GameObject monster in monsterSpawner.activeMonsters)
        {
            if (monster != null)
            {
                MonsterMovement movement = monster.GetComponent<MonsterMovement>();
                if (movement != null)
                {
                    yield return StartCoroutine(movement.MoveOneStep());
                }
            }
        }

        // �̵� �� ���� ������ ��ȯ
        currentPhase = TurnPhase.SpellCast;
        StartTurn();
    }

    void SwitchToSpellCast()
    {
        currentPhase = TurnPhase.SpellCast;
        StartTurn(); // ���� ���� �� ����
    }

    void SpellCast()
    {
        Debug.Log("���� ���� �� ��ȭ �ܰ�");
        // ���� ���� �� ��ȭ ���� �߰�
    }

    public void EndTurn()
    {
        if (currentPhase == TurnPhase.SpellCast)
        {
            Debug.Log("�� ����");
            currentPhase = TurnPhase.MonsterMove; // ���� �ٽ� ���� �̵�����
            StartTurn();
        }
    }
}
