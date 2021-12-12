using System.Collections.Generic;
using System.Linq;
using GSS.Combat;
using GSS.Core;
using UnityEngine;

public enum GameState {
	DayCombat,
	NightCombat,
	DayShopping,
	NightShopping
}

namespace GSS.Control {
	public class CombatManager : MonoBehaviour {
		[Header("Combat settings")] public List<Fighter> heroes;

		public List<Fighter> monsters;
		public List<Fighter> patrons;
		public List<Fighter> deadFighters;

		[Header("Action settings")] public  GameState gameState;
		[SerializeField]            private Day       day;
		[SerializeField]            private Night     night;

		public  int  attackerIndex;
		private bool canChangeState = true;

		private int characterCount;


		public static CombatManager Instance { get; private set; }

		private void Awake() {
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			else
				Instance = this;
		}

		private void Start() {
			StartCoroutine(day.ShoppingTransition());
		}


		public void BeginCombat(List<Fighter> attackers) {
			var attacker = attackers[attackerIndex];
			attacker.move.MoveToTarget(attacker, attacker.target, attackers);
			attackerIndex++;
		}

		public void SetTarget(List<Fighter> attackers, List<Fighter> targets) {
			foreach (var attacker in attackers) {
				attacker.target           = FindTarget(targets);
				attacker.character.target = attacker.target.character;
			}
		}

		private Fighter FindTarget(List<Fighter> targetsFromList) {
			var target = targetsFromList[Random.Range(0, targetsFromList.Count)];
			if (target.IsDead)
				target = targetsFromList[Random.Range(0, targetsFromList.Count)];
			return target;
		}

		public void Attack(Fighter fighter, Fighter target) {
			if (fighter.character.baseAttack > target.character.baseDefense)
				target.Health.TakeDamage(target);

			else if (fighter.character.baseAttack < target.character.baseDefense)
				fighter.Health.TakeDamage(fighter);
		}

		public void CheckGameState(List<Fighter> fighters) {
			characterCount++;
			if (characterCount >= fighters.Count) {
				RemoveFighter(heroes);
				RemoveFighter(monsters);
				RemoveFighter(patrons);

				if (!canChangeState) return;
				;

				attackerIndex  = 0;
				characterCount = 0;

				if (gameState == GameState.DayCombat)
					gameState                                         = GameState.NightShopping;
				else if (gameState == GameState.NightCombat) gameState = GameState.DayShopping;
			}

			ChangeState();
		}

		private void RemoveFighter(List<Fighter> fighters) {
			foreach (var deadFighter in deadFighters.Where(fighters.Contains)) {
				fighters.Remove(deadFighter);
				CheckSurvivors(fighters);
			}
		}

		private void CheckSurvivors(List<Fighter> fighters) {
			if (fighters.Count <= 0) {
				print("Load game over scene");
				canChangeState = false;
			}
		}

		private void ChangeState() {
			switch (gameState) {
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