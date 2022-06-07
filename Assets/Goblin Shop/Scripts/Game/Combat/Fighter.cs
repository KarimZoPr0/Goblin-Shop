using System.Collections.Generic;
using System.Linq;
using GSS.com;
using GSS.Movement;
using GSS.Resources;
using TMPro;
using UnityEngine;

namespace GSS.Combat {
	public class Fighter : MonoBehaviour {
		[Header("Character Settings")] public CharacterScriptableObject character;


		[Header("Stats Settings")] [SerializeField]
		private TextMeshProUGUI attack;

		[SerializeField] private TextMeshProUGUI defense;
		[SerializeField] private TextMeshProUGUI health;
		[SerializeField] private TextMeshProUGUI gold;

		[Header("Combat Settings")] public Health Health;

		public Fighter target;
		public Move    move;


		public int GetBaseAttack => character.baseAttack;
		public int GetBaseDefense => character.defense;
		
		public bool IsDead { get; set; }

		private void OnEnable() {
			Health.healthPoints = character.baseHealth;
			LoadData();
		}

		public void LoadData() {
			// adds stats from scriptable object to card.
			attack.text  = character.baseAttack.ToString();
			defense.text = character.baseDefense.ToString();
			health.text  = character.baseHealth.ToString();
			gold.text    = character.baseGold.ToString();
		}

		public void InStock() {
			gameObject.SetActive(false);
			character = null;
			Health.combatManager.deadFighters.Add(this);

			foreach (var patron in Health.combatManager.patrons.Where(patron => patron.name == this.name))
				patron.gameObject.SetActive(false);
		}


		public void MoveToTarget()
		{
			move.StartMovement(this, target);
		}
		
	}
}