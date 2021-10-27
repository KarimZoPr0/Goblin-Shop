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
        public List<Item> weaponsItems = new List<Item>();
        public List<Item> resourceItems = new List<Item>();
        public List<GenericItem> genericItems = new List<GenericItem>();
        
      
        public List<GenericItem> counterItems = new List<GenericItem>();

        [Header("Gold")] 
        public float gold;
        public TextMeshProUGUI goldTxt;


        public Dictionary<string, RectTransform> homeDir = new Dictionary<string, RectTransform>();

        // Resource should only generated resource items and same for weapons
        private void Start()
        {

            GenerateItems(weaponsItems, ItemType.Weapons);
            GenerateItems(resourceItems, ItemType.Resources);
        }

        
        private int i;
        
        
        // this is randomly generating items by assigning these scriptableableobjects to different items
        public void GenerateItems(List<Item> items, ItemType itemType) 
        {
            homeDir.Clear();
            // Depending on the ItemType, it will add random items
            foreach (var genericItem in genericItems)
            {
// homeDir.Add(genericItem.objectSettings.Id, genericItem.objectSettings.homeTransform);
                genericItem.gameObject.SetActive(true);
                var randomWeapon = Random.Range(0, weaponsItems.Count);
                if (genericItem.itemType == itemType)
                {
                    genericItem.item = items[randomWeapon];
                    genericItem.LoadData();
                    //genericItem.gameObject.GetComponent<RectTransform>().anchoredPosition = genericItem.objectSettings.homeTransform.anchoredPosition;
                    // if we have same id name then we add the id name + a number that increases by 1 
                    // that way each item has unique id name

                    // if (genericItem.item.id == items[randomWeapon].id)
                    // {
                    //     i++;
                    //     genericItem.objectSettings.Id = genericItem.id + (i);
                    // }
                }
               
                
            }
        }
    }
}
