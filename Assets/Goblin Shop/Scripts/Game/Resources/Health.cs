using System;
using DG.Tweening;
using GSS.Combat;
using GSS.Control;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GSS.Resources {
	public class Health : MonoBehaviour {
		public int healthPoints;

		[SerializeField] public  CombatManager combatManager;
		[SerializeField] private UnityEvent    onDie;
		[SerializeField] public  UnityEvent    onDamage;

		[SerializeField] private Image image;

		public void OnHit ()
		{
			image.material.SetFloat("_HitEffectBlend", .25f);
			image.material.DOFloat(0f, "_HitEffectBlend", .3f);
		}

		public void OnDeath ()
		{
			image.material.SetFloat("_HitEffectBlend", 0f);
			image.material.SetFloat("_GreyscaleBlend", 1f);
		}

		private Fighter _fighter;
		private void Awake()
		{
			_fighter = GetComponent<Fighter>();
		}

		private string testname;
		public static void Test(string name)
		{
			Debug.Log(name);
		}
		public void ApplyDamage() {
			const int damage    = 1;
			const int minHealth = 0;
			healthPoints = Mathf.Max(healthPoints - damage, minHealth);

			_fighter.character.baseHealth = healthPoints;
			_fighter.LoadData();

			if (healthPoints <= 0)
				Die(_fighter);

			onDamage?.Invoke();
		}

		private void Die(Fighter fighter) {
			if (fighter.IsDead) return;
			fighter.IsDead = true;
			fighter.InStock();
			onDie.Invoke();
		}
	}
}