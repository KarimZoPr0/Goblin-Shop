using System.Collections;
using UnityEditor;
using UnityEngine;

namespace GSS.Core
{
    public class Night : Action
    {
      
        [ContextMenu("Start night shopping")]
        public void StartShopping()
        {
            StartCoroutine(ShoppingTransition());
        }
        
        public override void Shopping()
        {
            combatManager.gameState = GameState.NightShopping;
            
            dayShop.SetActive(false);
            nightShop.SetActive(true);
            
            Debug.Log("Prepare Night Shopping");
            combatManager.SetTarget(combatManager.monsters, combatManager.heroes);    
        }

        [ContextMenu("Start night combat")]
        public void StartCombat()
        {
            StartCoroutine(CombatTransition());
        }



        public override void Combat()
        {
            combatManager.gameState = GameState.NightCombat;
            combatManager.BeginCombat(combatManager.monsters);
        }
    }
}