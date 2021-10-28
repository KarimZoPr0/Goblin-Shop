using System;
using System.Collections;
using System.Collections.Generic;
using GSS.Combat;
using GSS.Inventory;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GSS.Control
{
    public class ItemController : MonoBehaviour
    {
        
        [Header("Item settings")]
        public List<Item>        weaponsItems = new List<Item>();
        public List<Item>        resourceItems = new List<Item>();
        public List<GenericItem> genericItems  = new List<GenericItem>();
        public List<GenericItem> counterItems  = new List<GenericItem>();

        [Header("Gold")] 
        public float gold;
        public TextMeshProUGUI goldTxt;


        public Dictionary<string, RectTransform> homeDir = new Dictionary<string, RectTransform>();

        private void Start()
        {
            GenerateItems(weaponsItems, ItemType.Weapons);
            GenerateItems(resourceItems, ItemType.Resources);
        }

        public void GenerateItems(List<Item> items, ItemType itemType) 
        {
            homeDir.Clear();
            
            foreach (var genericItem in genericItems)
            {
                genericItem.gameObject.SetActive(true);
                var randomWeapon = Random.Range(0, weaponsItems.Count);
                if (genericItem.itemType == itemType)
                {
                    genericItem.item = items[randomWeapon];
                    genericItem.LoadData();
                }
               
                
            }
        }
    }
}
