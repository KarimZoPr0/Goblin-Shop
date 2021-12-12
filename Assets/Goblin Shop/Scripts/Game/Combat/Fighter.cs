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

			foreach (var patron in Health.combatManager.patrons)
				if (patron.name == name)
					patron.gameObject.SetActive(false);
		}
	}
}