using System;
using System.Collections.Generic;
using GSS.Combat;
using GSS.Control;
using UnityEngine;
using UnityEngine.Events;


namespace GSS.Resources
{
    public class Health : MonoBehaviour
    {
        public int healthPoints;

        [SerializeField] private CombatManager combatManager;
        [SerializeField] private UnityEvent onDie;
        [SerializeField] private UnityEvent onDamage;

        private bool _isDead;

        public bool IsDead() => _isDead;
        public void TakeDamage(int damage, Fighter deadFighter)
        {
            onDamage?.Invoke(); 
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            deadFighter.character.baseHealth = healthPoints;
            deadFighter.LoadData();
            

            if (healthPoints <= 0)
                Die(deadFighter);
        }

        private void Die(Fighter deadFighter)
        {
            if (_isDead) return;
            onDie.Invoke();
            _isDead = true;
            deadFighter.isDead = true;

            StoreDeadFighters(deadFighter);
        }

        private void StoreDeadFighters(Fighter target)
        {
            target.gameObject.SetActive(false);
            target.character = null;
            combatManager.deadFighters.Add(target);
        }

        
    }
}