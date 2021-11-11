using System.Collections.Generic;
using GSS.Combat;
using GSS.Core;
using GSS.Resources;
using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    DayCombat,
    NightCombat,
    DayShopping,
    NightShopping
}

namespace GSS.Control
{
    public class CombatManager : MonoBehaviour
    {
        [Header("Combat settings")] public List<Fighter> heroes;
        public List<Fighter> monsters;

         public List<Fighter> deadFighters;

        [Header("Action settings")] public GameState gameState;
        [SerializeField] private Day day;
        [SerializeField] private Night night;

        [Header("UI settings")] public UnityAction Events;

        private int fighterIndex;
        private bool canChangeState = true;
        private void Start()
        {
            CharacterGenerator.instance.SpawnCharacters();
            StartCoroutine(day.ShoppingTransition());
        }


        public void BeginCombat(List<Fighter> attackers)
        {
            var fighter = attackers[fighterIndex];
            fighter.move.MoveToTarget(fighter, fighter.target, attackers);
            fighterIndex++; 
        }

        public void SetTarget(List<Fighter> attackers, List<Fighter> targets)
        {
            foreach (var attacker in attackers)
            {
                attacker.target = FindTarget(targets);
                attacker.character.target = attacker.target.character;
            }
        }

        private Fighter FindTarget(List<Fighter> targetsFromList)
        {
            var target = targetsFromList[Random.Range(0, targetsFromList.Count)];
            if (target.IsDead)
                target = targetsFromList[Random.Range(0, targetsFromList.Count)];

            return target;
        }
        
        public void Attack(Fighter fighter, Fighter target)
        {
            const int damage = 1;
            if (fighter.character.baseAttack > target.character.baseDefense)
                target.TakeDamage(damage);

            else if(fighter.character.baseAttack < target.character.baseDefense) 
                fighter.TakeDamage(damage);
        }
        

        int characterCount;
        public void CheckGameState(List<Fighter> fighters)
        {
            characterCount++;
            if (characterCount >= fighters.Count )
            {
                RemoveDeadFighter();
                if(!canChangeState) return;;
                
                fighterIndex = 0;
                characterCount = 0;

                if (gameState == GameState.DayCombat)
                {
                    gameState = GameState.NightShopping;
                }
                else if (gameState == GameState.NightCombat)
                {
                    gameState = GameState.DayShopping;
                }
            }
            ChangeState();
        }

        private void RemoveDeadFighter()
        {
            foreach (var deadFighter in deadFighters)
            {
                if (heroes.Contains(deadFighter))
                {
                    heroes.Remove(deadFighter);
                    CheckSurvivors(heroes);
                }
                else if (monsters.Contains(deadFighter))
                {
                    monsters.Remove(deadFighter);
                    CheckSurvivors(monsters);
                }
            }
        }

        private void CheckSurvivors(List<Fighter> fightersOrTargets)
        {
            if (fightersOrTargets.Count <= 0)
            {
                print("Load gameover scene");
                canChangeState = false;
            }
        }

        private void ChangeState()
        {
            switch (gameState)
            {
                case GameState.DayCombat: 
                    day.Combat();
                    break;
                case GameState.NightCombat:
                   night.Combat();
                    break;
                case GameState.DayShopping:
                    StartCoroutine(day.ShoppingTransition());
                    break;
                case GameState.NightShopping:
                    StartCoroutine(night.ShoppingTransition());
                    break;
                default:
                    Debug.Log("GameState was not found");
                    break;
            }
        }
    }
}
