using System;
using System.Collections;
using System.Collections.Generic;
using GSS.Combat;
using GSS.Movement;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    [Header("Combat settings")] public List<Fighter> heroes;

    public List<Fighter> monsters;
    public List<Fighter> patrons;
    public List<Fighter> deadFighters;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip hitClip;

    public event Action CombatCompleteEvent;
    private bool gameOver;
    private int characterCount;
    private BoxCollider col;
    private List<Fighter> attackers;

    private void Start() => Move.AttackEvent += HandleAttack;
    private void OnDestroy() => Move.AttackEvent -= HandleAttack;

    public IEnumerator Co_ExecuteCombatSequence()
    {
        foreach (var attacker in attackers)
        {
            yield return attacker.MoveTowardsTarget();
        }

        RemoveDeadFighter(heroes);
        RemoveDeadFighter(monsters);
        RemoveDeadFighter(patrons);

        if (heroes.Count == 0 || monsters.Count == 0)
        {
            ReferenceUI.transitor.LoadScene(0);
            yield break;
        }

        CombatCompleteEvent?.Invoke();
    }


    public void SetTarget(List<Fighter> attackers, List<Fighter> targets)
    {
        this.attackers = attackers;
        foreach (var attacker in this.attackers)
        {
            attacker.target = FindTarget(targets);
            attacker.character.target = attacker.target.character;
        }
    }

    private Fighter FindTarget(List<Fighter> targetsFromList)
    {
        var aliveTargets = new Fighter[targetsFromList.Count];
        for (var i = 0; i < targetsFromList.Count; i++)
        {
            var target = targetsFromList[i];
            if (!target.IsDead)
            {
                aliveTargets[i] = target;
            }
        }

        return aliveTargets[Random.Range(0, aliveTargets.Length)];
    }

    private void HandleAttack(Fighter fighter, Fighter target)
    {
        if (fighter.character.baseAttack > target.character.baseDefense)
            target.Health.ApplyDamage();
        else if (fighter.character.baseAttack < target.character.baseDefense)
            fighter.Health.ApplyDamage();

        source.clip = hitClip;
        source.Play();
    }

    private void RemoveDeadFighter(List<Fighter> fighters) => fighters.RemoveAll(fighter => fighter.IsDead);

    public List<Fighter> GetAttackers() => attackers;
}