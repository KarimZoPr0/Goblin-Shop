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

        [SerializeField] public  CombatManager combatManager;
        [SerializeField] private UnityEvent    onDie;
        [SerializeField] public  UnityEvent    onDamage;

        private bool isDead;

        public bool IsDead() => isDead;

        public void Die(Fighter fighter)
        {
            if (isDead) return;
            onDie.Invoke();
            isDead = true;
            fighter.IsDead = true;
            fighter.InStock();
        }

    }
}