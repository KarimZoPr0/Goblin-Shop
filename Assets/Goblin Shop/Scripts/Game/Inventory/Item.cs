using UnityEngine;

namespace GSS.Inventory {
	[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Create Item", order = 2)]
	public class Item : ScriptableObject {
		[Header("Stats")] public string id;

		public int    atk;
		public int    def;
		public int    hp;
		public float  price;
		public int    buyLimit;
		public bool   sideEffects;
		public Sprite sprite;
	}
}