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
using UnityEngine.WSA;
using UnityEngine.XR;
using Random = UnityEngine.Random;

namespace GSS.Combat
{
    public class CharacterGenerator : MonoBehaviour
    {
        public static CharacterGenerator Instance => instance;
        public static CharacterGenerator instance { get; private set; }
        

        public List<CharacterScriptableObject> heroes = null;
        public List<CharacterScriptableObject> monsters = null;
        
        
        public CombatManager combatManager;
        public ItemController itemController;
        
        public Day day;
        public Night night;
        
        public SpriteRenderer CharacterSprite;
        public CharacterScriptableObject characterStats;
        public Text CharacterDialog;

        
        string txt = "";
        public int id = 0;
        
        
        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this.gameObject);
            else
                instance = this;
        }
        
        
        // This updates the list of scriptableobject > patrons > shopping
        private void GetList()
        {
            heroes = new List<CharacterScriptableObject>();
            monsters = new List<CharacterScriptableObject>();

            foreach(Fighter h in combatManager.heroes)
                heroes.Add(h.GetComponent<Fighter>().character);

            foreach(Fighter m in combatManager.monsters)
                monsters.Add(m.GetComponent<Fighter>().character);
        }

        
        // Depending on the gamestate we let heroes or monsters shop
        public void SpawnCharacters()
        {
            itemController.GenerateItems(itemController.weaponsItems, ItemType.Weapons);
            itemController.GenerateItems(itemController.resourceItems, ItemType.Resources);
            GetList();
            
            if(combatManager.gameState == GameState.DayShopping) 
                SelectCharacters(combatManager.heroes, combatManager.monsters);
            else if(combatManager.gameState == GameState.NightShopping)
                SelectCharacters(combatManager.monsters, combatManager.heroes);
            
        }

        // this is calling the next character
        public IEnumerator NextCharacter()
        {
            id++;
            Reference.transitor.Fade();
            yield return new WaitForSeconds(0.5f);
            foreach (var counterItem in itemController.counterItems)
            {
                GetStats(counterItem);
                // TODO: Fix THIS
                //counterItem.gameObject.GetComponent<RectTransform>().anchoredPosition = counterItem.objectSettings.HomePos;//homeTransform.position;//genericItem.objectSettings.HomePos;
                counterItem.inCounter = false;//DEACTIVATE TO AVOID STACKING 
            }
            itemController.counterItems.Clear();
            
            if(combatManager.gameState == GameState.DayShopping) 
                SelectCharacters(combatManager.heroes, combatManager.monsters);
            else if(combatManager.gameState == GameState.NightShopping)
                SelectCharacters(combatManager.monsters, combatManager.heroes);
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


        // this is checking if if all character has shopped and getting the characaters
        public void SelectCharacters(List<Fighter> attackers, List<Fighter> targets)
        {
            // id of selected character
            
            if (id >= attackers.Count)
            {
                id = 0;
                if (combatManager.gameState == GameState.DayShopping)
                {
                    StartCoroutine(day.CombatTransition());
                }
                else if (combatManager.gameState == GameState.NightShopping)
                {
                    StartCoroutine(night.CombatTransition());
                }
                
            }
            CharacterScriptableObject selected = null; // selected character
            selected = attackers[id].character;

            characterStats = selected;
            CharacterSprite.sprite = selected.sprite;


        }
        
        
        // this is for getting the current character stats > can be used for items
        public CharacterScriptableObject GetSelected()
        {
            return characterStats;
        }
    }
   
}