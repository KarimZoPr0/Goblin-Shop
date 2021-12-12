using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GSS.com;
using GSS.Control;
using GSS.Core;
using GSS.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace GSS.Combat {
	public class CharacterGenerator : MonoBehaviour {
		public List<CharacterScriptableObject> heroes;
		public List<CharacterScriptableObject> monsters;

		public CombatManager  combatManager;
		public ItemController itemController;

		public Day   day;
		public Night night;

		public SpriteRenderer            characterSprite;
		public CharacterScriptableObject selectedCharacter;
		public Text                      characterDialogue;

		private       int                characterCount;
		public static CharacterGenerator Instance { get; private set; }

		private void Awake() {
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			else
				Instance = this;
		}
		
		
		// Depending on the gamestate we let heroes or monsters shop
		public void SpawnCharacters() {
			SpawnItems();
			GetList();
			SelectCharacters();
		}

		// This updates the list of scriptableobject > patrons > shopping

		private void GetList() {
			heroes   = new List<CharacterScriptableObject>();
			monsters = new List<CharacterScriptableObject>();

			foreach (var hero in combatManager.heroes) heroes.Add(hero.GetComponent<Fighter>().character);

			foreach (var monster in combatManager.monsters) monsters.Add(monster.GetComponent<Fighter>().character);
		}

		private void SpawnItems() {
			itemController.GenerateItems(itemController.weaponsItems, ItemType.Weapons);
			itemController.GenerateItems(itemController.resourceItems, ItemType.Resources);
		}


		public IEnumerator NextCharacter() {
			characterCount++;
			ReferenceUI.transitor.Fade();
			yield return new WaitForSeconds(0.5f);

			foreach (var counterItem in itemController.counterItems) {
				GetStats(counterItem);
				counterItem.GetComponent<Transform>().position = counterItem.ReturnToStartPosition;
				counterItem.dragHandler.transform.SetParent(counterItem.dragHandler.startParent);

				counterItem.inCounter = false;
			}

			itemController.counterItems.Clear();
			SelectCharacters();
		}

		private void GetStats(GenericItem genericItem) {
			GetSelected().baseAttack  += genericItem.atk;
			GetSelected().baseDefense += genericItem.def;
			GetSelected().baseHealth  += genericItem.hp;
			GetSelected().baseGold    -= itemController.gold;

			itemController.gold         = 0;
			itemController.goldTxt.text = itemController.gold.ToString();

			LoadStats(combatManager.heroes);
			LoadStats(combatManager.monsters);
		}

		private void LoadStats(List<Fighter> fighters) {
			foreach (var fighter in fighters)
				fighter.LoadData();
		}


		// this is checking if all character has shopped and getting next the character
		private void SelectCharacters() {
			var attackers = combatManager.gameState == GameState.DayShopping
				? combatManager.heroes
				: combatManager.monsters;

			if (characterCount >= attackers.Count) {
				characterCount = 0;
				
				if (combatManager.gameState == GameState.DayShopping)
					StartCoroutine(day.CombatTransition());
				else if (combatManager.gameState == GameState.NightShopping)
					StartCoroutine(night.CombatTransition());
			}

			selectedCharacter = attackers[characterCount].character;
			
			characterSprite.sprite = selectedCharacter.sprite;
			characterDialogue.text = selectedCharacter.GetDialog();

			var countEquipableItem = itemController.genericItems.Count(genericItem => selectedCharacter.CanEquip(genericItem.item));
			print(countEquipableItem);
			if (countEquipableItem == 0) {
				print("Generate Items again!");
			}
		}

		// this is for getting the current character stats > can be used for items
		public CharacterScriptableObject GetSelected() {
			return selectedCharacter;
		}
	}
}