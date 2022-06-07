using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GSS.com;
using GSS.Control;
using GSS.Core;
using GSS.Inventory;
using UnityEngine;
using UnityEngine.UI;
using Action = System.Action;

namespace GSS.Combat {
	public class CharacterGenerator : MonoBehaviour {
		public List<CharacterScriptableObject> heroes;
		public List<CharacterScriptableObject> monsters;

		public CombatManager  combatManager;


		public SpriteRenderer            characterSprite;
		public CharacterScriptableObject selectedCharacter;
		public Text                      characterDialogue;

		private       int                characterCount;
		public       ItemController             itemController;
		public static CharacterGenerator Instance { get; private set; }

		private void Awake() {
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			else
				Instance = this;
		}
		
		public delegate IEnumerator CombatDelegate();
		public event CombatDelegate CombatEvent;
		public event Action         OnGenereteItems;
		
		// Depending on the gamestate we let heroes or monsters shop
		public void SpawnCharacters() {
			GetList();
			SelectCharacters();
			
		}

		// This updates the list of scriptableobject > patrons > shopping

		private void GetList() {
			heroes = new List<CharacterScriptableObject>(); monsters = new List<CharacterScriptableObject>();
			foreach (var hero in combatManager.heroes) heroes.Add(hero.GetComponent<Fighter>().character);
			foreach (var monster in combatManager.monsters) monsters.Add(monster.GetComponent<Fighter>().character);
		}

		public void SpawnItems() {
			itemController.GenerateItems(itemController.weaponsItems, ItemType.Weapons);
			itemController.GenerateItems(itemController.resourceItems, ItemType.Resources);
		}


		public IEnumerator NextCharacter() {
			characterCount++;
			ReferenceUI.transitor.Fade();
			yield return new WaitForSeconds(0.5f);

			foreach (var counterItem in itemController.counterItems)
			{
				GetStats(counterItem);
				SellItem(counterItem);
			}

			itemController.counterItems.Clear();
			SelectCharacters();
		}

		private void SellItem(GenericItem counterItem)
		{
			counterItem.GetComponent<Transform>().position = counterItem.ReturnToStartPosition;
			counterItem.dragHandler.transform.SetParent(counterItem.dragHandler.startParent);
			counterItem.inCounter = false;
			counterItem.gameObject.SetActive(false);
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

				StartCoroutine(CombatEvent?.Invoke());
				return;
			}

			selectedCharacter = attackers[characterCount].character;
			
			characterSprite.sprite = selectedCharacter.sprite;
			characterDialogue.text = selectedCharacter.GetDialog();
			
			SpawnItems();
			
			var count = itemController.genericItems.Count(genericItem => selectedCharacter.equipableItems.Contains(genericItem.item));
			var output = count == 0 ? "Can't euqip any" : "Can equip";
			Debug.Log(output);

			if (count == 0)
			{
				SpawnItems();
			}

		}

		// this is for getting the current character stats > can be used for items
		public CharacterScriptableObject GetSelected() {
			return selectedCharacter;
		}
	}
}