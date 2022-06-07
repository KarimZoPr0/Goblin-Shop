using System;
using System.Collections.Generic;
using System.Linq;
using GSS.Combat;
using GSS.Inventory;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GSS.Control {
	public class ItemController : MonoBehaviour {

		public List<Item> weaponsItems  = new List<Item>();
		public List<Item> resourceItems = new List<Item>();
		
		public List<GenericItem> genericItems = new List<GenericItem>();
		public List<GenericItem> counterItems = new List<GenericItem>();

		[Header("Gold")] public float gold;

		public TextMeshProUGUI goldTxt;

		public void GenerateItems(List<Item> items, ItemType itemType)
		{
			foreach (var genericItem in genericItems)
			{
				if (!genericItem.gameObject.activeSelf)
				{
					genericItem.gameObject.SetActive(true);
				}
				
				if (genericItem.itemType != itemType) continue;
				
				var randomWeapon = Random.Range(0, weaponsItems.Count);
				genericItem.item = items[randomWeapon];
				
				genericItem.LoadData();
			}
		}
	}
}