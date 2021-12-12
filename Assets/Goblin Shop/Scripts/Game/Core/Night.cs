using UnityEngine;

namespace GSS.Core {
	public class Night : Action {
		public override void Shopping() {
			combatManager.gameState = GameState.NightShopping;

			dayShop.SetActive(false);
			nightShop.SetActive(true);

			Debug.Log("Prepare Night Shopping");
			combatManager.SetTarget(combatManager.monsters, combatManager.heroes);
		}

		public override void Combat() {
			combatManager.gameState = GameState.NightCombat;
			combatManager.BeginCombat(combatManager.monsters);
		}
	}
}