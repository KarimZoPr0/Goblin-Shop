using System.Collections.Generic;
using System.Linq;
using GSS.Combat;
using GSS.Core;
using TMPro;
using UnityEngine;
using Action = System.Action;

public enum GameState {
	DayCombat,
	DayShopping,
	NightShopping,
	NightCombat
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
		public                             TMP_Text  Something;

		public  int  attackerIndex;
		private bool canChangeState = true;

		private int characterCount;

		private void Start() {
			StartCoroutine(day.ShoppingTransition());
		}


		public void BeginCombat(List<Fighter> attackers)
		{
			var attacker = attackers[attackerIndex];
			attacker.MoveToTarget();
			attackerIndex++;
		}
		

		public void SetTarget(List<Fighter> attackers, List<Fighter> targets) {
			foreach (var attacker in attackers)
			{
				attacker.target = FindTarget(targets);
				attacker.character.target = attacker.target.character;
			}
		}

		private Fighter FindTarget(List<Fighter> targetsFromList) {
			var aliveFighters = targetsFromList.Where(x => !x.IsDead).ToArray();
			var target = aliveFighters[Random.Range(0, aliveFighters.Length)];
			return target;
		}

		public void Attack(Fighter fighter, Fighter target) {
			if (fighter.character.baseAttack > target.character.baseDefense)
				target.Health.ApplyDamage();

			else if (fighter.character.baseAttack < target.character.baseDefense)
				fighter.Health.ApplyDamage();
			
		}

		public void CheckGameState()
		{
			var fighters = gameState == GameState.DayCombat ? heroes : monsters;
			characterCount++;
			
			if (characterCount >= fighters.Count) {
				RemoveFighter(heroes);
				RemoveFighter(monsters);
				RemoveFighter(patrons);

				if (!canChangeState) return;

				attackerIndex  = 0;
				characterCount = 0;

				gameState = gameState switch
				{
					GameState.DayCombat => GameState.NightShopping,
					GameState.NightCombat => GameState.DayShopping,
					_ => gameState
				};
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