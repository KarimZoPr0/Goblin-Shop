using System.Collections;
using GSS.Combat;
using GSS.Control;
using UnityEngine;
using UnityEngine.Events;

namespace GSS.Core {
	public abstract class Action : MonoBehaviour {
		public CombatManager combatManager;

		
	
		public GameObject combatBoard;
		public GameObject defaultCharacter;


		public UnityEvent shoppingEvent;
		public abstract void Shopping();
		public abstract void Combat();
		
		public IEnumerator CombatTransition() {
			ReferenceUI.transitor.Fade();
			defaultCharacter.SetActive(false);
			yield return new WaitForSeconds(0.1f);
			combatBoard.SetActive(true);
			yield return new WaitForSeconds(1f);
			Combat();
		}


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