using System.Collections.Generic;
using System.Linq;
using GSS.Combat;
using GSS.Inventory;
using TMPro;
using UnityEngine;

namespace GSS.Control {
	public class ItemController : MonoBehaviour {
		[Header("Item settings")] public List<Item> weaponsItems = new List<Item>();

		public List<Item>        resourceItems = new List<Item>();
		public List<GenericItem> genericItems  = new List<GenericItem>();
		public List<GenericItem> counterItems  = new List<GenericItem>();

		[Header("Gold")] public float gold;

		public TextMeshProUGUI goldTxt;

		public void GenerateItems(List<Item> items, ItemType itemType) {
			foreach (var genericItem in genericItems) {
				genericItem.gameObject.SetActive(true);
				var randomWeapon = Random.Range(0, weaponsItems.Count);
				if (genericItem.itemType != itemType) continue;
				genericItem.item = items[randomWeapon];
				genericItem.LoadData();
			}
			
		}
	}
}