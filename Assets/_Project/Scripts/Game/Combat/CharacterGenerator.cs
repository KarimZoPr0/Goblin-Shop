using System;
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

namespace GSS.Combat
{
    public class CharacterGenerator : MonoBehaviour
    {
        public List<CharacterScriptableObject> heroes = new List<CharacterScriptableObject>();
        public List<CharacterScriptableObject> monsters = new List<CharacterScriptableObject>();

        public CombatManager combatManager;

        public SpriteRenderer characterSprite;
        public CharacterScriptableObject selectedCharacter;
        public Text characterDialogue;

        private int characterCount;
        public ItemController itemController;

        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip ringBellClip;


        public static CharacterGenerator Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            GetList();
            foreach (var hero in heroes)
            {
                hero.ResetStats();
            }
            
            foreach (var monster in monsters)
            {
                monster.ResetStats();
            }
        }

        public delegate IEnumerator CombatDelegate();

        public event CombatDelegate CombatEvent;

        // Depending on the gamestate we let heroes or monsters shop
        public void SpawnCharacters()
        {
            GetList();
            SelectCharacters();
        }

        private void GetList()
        {
            heroes = combatManager.heroes.Select(x => x.character).ToList();
            monsters = combatManager.monsters.Select(x => x.character).ToList();
        }

        public void SpawnItems()
        {
            itemController.GenerateItems(itemController.weaponsItems, ItemType.Weapons);
            itemController.GenerateItems(itemController.resourceItems, ItemType.Resources);
        }


        public IEnumerator NextCharacter()
        {
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

            yield return new WaitForSeconds(.3f);

            source.clip = ringBellClip;
            source.Play();
        }

        private void SellItem(GenericItem counterItem)
        {
            counterItem.GetComponent<Transform>().position = counterItem.ReturnToStartPosition;
            counterItem.dragHandler.transform.SetParent(counterItem.dragHandler.startParent);
            counterItem.inCounter = false;
            counterItem.gameObject.SetActive(false);
        }

        private void GetStats(GenericItem genericItem)
        {
            GetSelected().baseAttack += genericItem.atk;
            GetSelected().baseDefense += genericItem.def;
            GetSelected().baseHealth += genericItem.hp;
            GetSelected().baseGold -= itemController.gold;

            itemController.gold = 0;
            itemController.goldTxt.text = itemController.gold.ToString();

            LoadStats(combatManager.heroes);
            LoadStats(combatManager.monsters);
        }

        private void LoadStats(List<Fighter> fighters)
        {
            foreach (var fighter in fighters)
                fighter.LoadData();
        }

        // this is checking if all character has shopped and getting next the character
        private void SelectCharacters()
        {
            var attackers = combatManager.GetAttackers();

            if (characterCount >= attackers.Count)
            {
                StartCoroutine(CombatEvent?.Invoke());
                characterCount = 0;
                return;
            }

            selectedCharacter = attackers[characterCount].character;

            characterSprite.sprite = selectedCharacter.sprite;
            characterDialogue.text = selectedCharacter.GetDialog();

            do
            {
                SpawnItems();
            } while (!HasEquipableItems);
        }

        public CharacterScriptableObject GetSelected() => selectedCharacter;

        private bool HasEquipableItems => itemController.genericItems.Any(item => selectedCharacter.CanEquip(item));
    }
}