using System;
using System.Collections;
using System.ComponentModel;
using GSS.Combat;
using GSS.Control;
using UnityEngine;

namespace GSS.Core
{
    public abstract class Action : MonoBehaviour
    {
        public CombatManager combatManager;
        public abstract  void Shopping();

        
        // these object will appear depending on the day
       public GameObject dayShop;
       public GameObject nightShop;
       public GameObject combatBoard;
       public GameObject defaultCharacter;
       
       
       // this is for character to start fighting
        public abstract void Combat();

        
        
        // We set objects to be active or not in order to start combat
        public IEnumerator CombatTransition()
        {
            Reference.transitor.Fade();
            defaultCharacter.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            combatBoard.SetActive(true);
            yield return new WaitForSeconds(1f);
            Combat();
        }
        
        
        // We set objects to be active or not in order to start shopping
        public IEnumerator ShoppingTransition()
        {
            Reference.transitor.Fade();
            yield return new WaitForSeconds(0.3f);
            CharacterGenerator.instance.SpawnCharacters();
            combatBoard.SetActive(false);
            defaultCharacter.SetActive(true);
            yield return new WaitForSeconds(.2f);
            Shopping();
        }
    }
}