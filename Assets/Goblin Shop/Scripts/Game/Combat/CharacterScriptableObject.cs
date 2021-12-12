using System.Collections.Generic;
using GSS.Inventory;
using UnityEngine;

namespace GSS.com {
	[CreateAssetMenu(fileName = "CharacterName", menuName = "CharacterScriptableObjects/Create Character", order = 1)]
	public class CharacterScriptableObject : ScriptableObject {
		// Character name
		public string characterName;

		// Stats
		[Header("Stats")] public int attack;

		public int defense;
		public int health;

		public int baseAttack;
		public int baseDefense;
		public int baseHealth;


		// What is this character allowed to equip?
		[Header("Inventory Settings")] public List<Item> equipableItems;

		//the character's money
		[Header("For Patrons")] public float gold;

		public float baseGold;

		// Current Target
		[Header("Character Target")] public CharacterScriptableObject target;

		// Character UI
		[Header("UI")] public Sprite sprite;

		[Header("Warrior/Troll Dialog")] public string dialog1;

		[Header("Archer/Kobold Dialog")] public string dialog2;

		[Header("Wizard/Dragon Dialog")] public string dialog3;

		private void OnEnable() {
			baseAttack  = attack;
			baseDefense = defense;
			baseHealth  = health;
			baseGold    = gold;
		}


		public string GetDialog() {
			return target.characterName switch {
				"Warrior" => dialog1,
				"Troll" => dialog1,
				"Archer" => dialog2,
				"Kobold" => dialog2,
				"Wizard" => dialog3,
				"Dragon" => dialog3,
				_ => ""
			};
		}

		public bool CanEquip(Item item) {
			return item != null && equipableItems.Contains(item);
		}
	}
}