using System;
using System.Text;
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
			(atk, def) = Random.Range(0, 2) == 0 ? (0, -3) : (3, 0);
		}
		
		
		public string GetHeader() => $"\n<color=\"black\">{id.ToUpper()}</color>";

		private const string Format = "+#.##;-#.##";
		public string GetContent()
		{
			var content = new StringBuilder();
			
			content.Append("<color=\"blue\">");
			if (atk != 0)
			{
				content.Append($"ATK: {atk.ToString(Format)}\n");
			}
			if (def != 0)
			{
				content.Append($"DEF: {def.ToString(Format)}\n");
			}
			if (hp != 0)
			{
				content.Append($"HP: {hp.ToString(Format)}\n");
			}
			content.Append("</color>");
			
			content.Append($"<color=\"orange\">GOLD: {price}</color>\n");
			content.Append($"<color=\"red\">MAX PURCHASE: {buyLimit}</color>\n\n");

			return content.ToString();
		}
	}
}