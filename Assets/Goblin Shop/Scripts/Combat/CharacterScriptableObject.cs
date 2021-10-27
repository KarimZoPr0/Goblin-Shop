using System;
using System.Collections.Generic;
using GSS.Inventory;
using UnityEngine;

namespace GSS.com
{
    [CreateAssetMenu(fileName = "CharacterName", menuName = "CharacterScriptableObjects/Create Character", order = 1)]
    public class CharacterScriptableObject : ScriptableObject
    {
        // Character name
        public string characterName;

        // Stats
        [Header("Stats")]
        public int attack;
        public int defense;
        public int health;
        
        public int baseAttack;
        public int baseDefense;
        public int baseHealth;

        private void OnEnable()
        {
            baseAttack = attack;
            baseDefense = defense;
            baseHealth = health;
            baseGold = gold;
        }
        

        // What is this character allowed to equip?
        [Header("Inventory Settings")]
        public List<Item> equipableItems;
    
        //the character's money
        [Header("For Patrons")]
        public float gold;

        public float baseGold;
        // Current Target
        [Header("Character Target")]
        public CharacterScriptableObject target;

        // Character UI
        [Header("UI")]
        public Sprite sprite;

        [Header("Warrior/Troll Dialog")]
        public string dialog1;

        [Header("Archer/Kobold Dialog")]
        public string dialog2;

        [Header("Wizard/Dragon Dialog")]
        public string dialog3;

        public List<GSS.Inventory.Item> equippableList = new List<Item>();


        // this is for the dialogue
        public string GetDialog()
        {
            switch (target.name)
            {
                case "C_Warrior":
                    return dialog1;

                case "C_Troll":
                    return dialog1;

                case "C_Archer":
                    return dialog2;

                case "C_Kobold":
                    return dialog2;

                case "C_Wizard":
                    return dialog3;

                case "C_Dragon":
                    return dialog3;
            }
            return "";
        }
        public bool isEquippable(Item thisItem){
            if (thisItem != null)
            {
                Debug.Log($"//ITEMCHECK: {thisItem.name}" + (equippableList.Contains(thisItem) ? "Item is equippable" : "Item is not equipabble"));
                return equippableList.Contains(thisItem);
            }
            else return false;
        }
    }
}

