using GSS.Combat;
using GSS.Control;
using UnityEngine;
using UnityEngine.Events;

namespace GSS.Resources {
	public class Health : MonoBehaviour {
		public int healthPoints;

		[SerializeField] public  CombatManager combatManager;
		[SerializeField] private UnityEvent    onDie;
		[SerializeField] public  UnityEvent    onDamage;


		public void TakeDamage(Fighter fighter) {
			const int damage    = 1;
			const int minHealth = 0;
			healthPoints = Mathf.Max(healthPoints - damage, minHealth);

			fighter.character.baseHealth = healthPoints;
			fighter.LoadData();

			if (healthPoints <= 0)
				Die(fighter);

			onDamage?.Invoke();
		}

		public void Die(Fighter fighter) {
			if (fighter.IsDead) return;
			fighter.IsDead = true;
			fighter.InStock();
			onDie.Invoke();
		}
	}
}