using UnityEngine;

namespace GSS.Core {
	public class Day : Action {
		public override void Shopping() {
			dayShop.SetActive(true);
			nightShop.SetActive(false);

			Debug.Log("Prepare Day Shopping");
			combatManager.SetTarget(combatManager.heroes, combatManager.monsters);
		}

		public override void Combat() {
			combatManager.gameState = GameState.DayCombat;
			combatManager.BeginCombat(combatManager.heroes);
		}
	}
}