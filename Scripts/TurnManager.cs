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
        StartTurn(); // 게임 시작 시 첫 턴 실행
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
        Debug.Log("몬스터가 이동합니다.");
        // 몬스터 이동 로직 추가 (예: 몬스터 이동 애니메이션)
        //StartCoroutine(monsterSpawner.SpawnMonsters());
        StartCoroutine(HandleMonsterTurn());

        // 일정 시간이 지난 후 자동으로 SpellCast로 전환
        //Invoke(nameof(SwitchToSpellCast), 1.0f); // 1초 후 스펠 시전으로 전환
    }

    IEnumerator HandleMonsterTurn()
    {
        // 몬스터 스폰
        yield return StartCoroutine(monsterSpawner.SpawnMonsters());

        // 스폰된 + 기존 몬스터 전진
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

        // 이동 후 스펠 턴으로 전환
        currentPhase = TurnPhase.SpellCast;
        StartTurn();
    }

    void SwitchToSpellCast()
    {
        currentPhase = TurnPhase.SpellCast;
        StartTurn(); // 스펠 시전 턴 시작
    }

    void SpellCast()
    {
        Debug.Log("스펠 시전 및 강화 단계");
        // 스펠 시전 및 강화 로직 추가
    }

    public void EndTurn()
    {
        if (currentPhase == TurnPhase.SpellCast)
        {
            Debug.Log("턴 종료");
            currentPhase = TurnPhase.MonsterMove; // 턴을 다시 몬스터 이동으로
            StartTurn();
        }
    }
}
