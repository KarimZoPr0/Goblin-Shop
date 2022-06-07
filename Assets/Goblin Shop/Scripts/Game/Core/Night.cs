using GSS.Combat;
using UnityEngine;

namespace GSS.Core {
	public class Night : Action {
		public override void Shopping() {
			combatManager.gameState = GameState.NightShopping;

			shoppingEvent?.Invoke();

			Debug.Log("Prepare Night Shopping");
			combatManager.SetTarget(combatManager.monsters, combatManager.heroes);
			CharacterGenerator.Instance.CombatEvent += CombatTransition;
		}

		public override void Combat() {
			combatManager.gameState = GameState.NightCombat;
			combatManager.BeginCombat(combatManager.monsters);
			CharacterGenerator.Instance.CombatEvent -= CombatTransition;

		}
	}
}