using GSS.Combat;
using UnityEngine;

namespace GSS.Control {
	public class EconomyController : MonoBehaviour {
		public ItemController itemController;

		public void Checkout() {
			// storing CharacterGenerator into a variable
			var characterGenerator = CharacterGenerator.Instance;

			if (itemController.counterItems.Count <= 0) return;
			if (itemController.gold > characterGenerator.GetSelected().baseGold) return;

			// checking if it count is higher than item limit
			foreach (var counterItem in itemController.counterItems) {
				var count = 0;
				foreach (var item in itemController.counterItems)
					if (item.id == counterItem.id)
						count++;
				if (count > counterItem.buyLimit) return;
			}

			itemController.goldTxt.text = itemController.gold.ToString();

			// Calling next character
			characterGenerator.StartCoroutine(characterGenerator.NextCharacter());
		}
	}
}