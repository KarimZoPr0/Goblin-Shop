using GSS.Resources;
using UnityEngine;

namespace GSS.Core
{
    public class Day : Action
    {
        [ContextMenu("Start day shopping")]
        public void StartShopping()
        {
            StartCoroutine(ShoppingTransition());
        }
        
        public override void Shopping()
        { 
            dayShop.SetActive(true);
            nightShop.SetActive(false);
            
            Debug.Log("Prepare Day Shopping");
            combatManager.SetTarget(combatManager.heroes, combatManager.monsters);
        }
        [ContextMenu("Start day combat")]
        public void StartCombat()
        {
            StartCoroutine(CombatTransition());
        }

        public override void Combat()
        {
            combatManager.gameState = GameState.DayCombat;
            combatManager.BeginCombat(combatManager.heroes);
        }
        
    }
}