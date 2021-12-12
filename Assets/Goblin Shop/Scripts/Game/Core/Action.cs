using System.Collections;
using GSS.Combat;
using GSS.Control;
using UnityEngine;

namespace GSS.Core {
	public abstract class Action : MonoBehaviour {
		public CombatManager combatManager;


		// these object will appear depending on the day
		public GameObject dayShop;
		public GameObject nightShop;
		public GameObject combatBoard;
		public GameObject defaultCharacter;
		public abstract void Shopping();


		// this is for character to start fighting
		public abstract void Combat();


		// We set objects to be active or not in order to start combat
		public IEnumerator CombatTransition() {
			ReferenceUI.transitor.Fade();
			defaultCharacter.SetActive(false);
			yield return new WaitForSeconds(0.1f);
			combatBoard.SetActive(true);
			yield return new WaitForSeconds(1f);
			Combat();
		}


		// We set objects to be active or not in order to start shopping
		public IEnumerator ShoppingTransition() {
			ReferenceUI.transitor.Fade();
			yield return new WaitForSeconds(0.5f);
			combatBoard.SetActive(false);
			defaultCharacter.SetActive(true);
			Shopping();
			CharacterGenerator.Instance.SpawnCharacters();
		}
	}
}