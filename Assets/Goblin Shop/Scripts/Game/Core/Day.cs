using GSS.Combat;
using UnityEngine;

namespace GSS.Core {
	public class Day : Action {
		public override void Shopping()
		{
			combatManager.gameState = GameState.DayShopping;
			shoppingEvent.Invoke();
			Debug.Log("Prepare Day Shopping");
			combatManager.SetTarget(combatManager.heroes, combatManager.monsters);
			CharacterGenerator.Instance.CombatEvent += CombatTransition;
		}

		public override void Combat()
		{
			combatManager.gameState = GameState.DayCombat;
			combatManager.BeginCombat(combatManager.heroes);
			CharacterGenerator.Instance.CombatEvent -= CombatTransition;

		}
	}
}