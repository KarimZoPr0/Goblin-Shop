using System;
using GSS.Control;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum ItemType {
	Resources,
	Weapons
}

namespace GSS.Inventory {
	public class GenericItem : MonoBehaviour {
		[Header("Stats Settings")] public string id;

		public int   atk;
		public int   def;
		public int   hp;
		public float price;
		public int   buyLimit;
		public bool  sideEffects;
		public Image image;

		[Header("Item Settings")] public ItemType itemType;

		public Item           item;
		public ItemController itemController;
		public bool           inCounter;

		public DragHandler dragHandler;

		private Vector3 startP;

		public Vector3 ReturnToStartPosition => dragHandler.startPosition;

		private void Awake() {
			dragHandler = GetComponent<DragHandler>();
		}

		// updating the data every time we generate items
		public void LoadData() {
			id           = item.id;
			atk          = item.atk;
			def          = item.def;
			hp           = item.hp;
			price        = item.price;
			sideEffects  = item.sideEffects;
			buyLimit     = item.buyLimit;
			image.sprite = item.sprite;

			if (!sideEffects) return;
			(atk, def) = Random.Range(1,3) == 1 ? (0, -3) : (3, 0);
		}
	}
}