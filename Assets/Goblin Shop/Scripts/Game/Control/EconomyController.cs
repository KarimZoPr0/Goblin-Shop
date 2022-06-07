using GSS.Combat;
using TMPro;
using UnityEngine;

namespace GSS.Control {
	public class EconomyController : MonoBehaviour {
		[SerializeField] private ItemController itemController;
		[SerializeField] private TMP_Text myGold_label;
		[SerializeField] private float kingsGold;

		private float myGold;

		public void Checkout() {
			// storing CharacterGenerator into a variable
			var characterGenerator = CharacterGenerator.Instance;

			if (itemController.counterItems.Count <= 0) return;
			if (itemController.gold > characterGenerator.GetSelected().baseGold) return;
			
			// Set myGold to the gold that you recieved from the the shopper
			myGold += itemController.gold;
			myGold_label.text = myGold.ToString();
			
			// checking if it count is higher than item limit
			foreach (var counterItem in itemController.counterItems) {
				var count = 0;
				foreach (var item in itemController.counterItems)
					if (item.id == counterItem.id)
						count++;
				if (count > counterItem.buyLimit) return;
			}

			itemController.goldTxt.text = itemController.gold.ToString();

			if (myGold > kingsGold)
			{
				ReferenceUI.transitor.LoadScene(0);
				print("You Defeated the king");
				return;
			}
			
			// Calling next character
			characterGenerator.StartCoroutine(characterGenerator.NextCharacter());
		}
	}
}