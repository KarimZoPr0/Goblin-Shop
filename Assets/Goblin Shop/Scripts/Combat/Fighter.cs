using System;
using GSS.com;
using GSS.Control;
using GSS.Movement;
using GSS.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GSS.Combat
{
    public class Fighter : MonoBehaviour
    {
        public bool IsDead
        {
            get => isDead;
            set => isDead = value;
        }

        [Header("Character Settings")]
        public CharacterScriptableObject character;

        
        [Header("Stats Settings")]
        [SerializeField] private TextMeshProUGUI attack;
        [SerializeField] private TextMeshProUGUI defense;
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI gold;
        
        [Header("Combat Settings")] 
        public CombatManager combatManager;
        public  Health  Health;
        public  Move    move;
        public  Fighter target;
        private bool    isDead;

        private void OnEnable()
        {
            Health.healthPoints = character.baseHealth;
            LoadData();
        }

        public void LoadData()
        {
            // adds stats from scriptable object to card.
            attack.text = character.baseAttack.ToString();
            defense.text = character.baseDefense.ToString();
            health.text = character.baseHealth.ToString();
            gold.text = character.baseGold.ToString();
        }

        public void TakeDamage(int damage)
        {
            var healthPoints = Health.healthPoints;
            Health.onDamage?.Invoke(); 
            
            healthPoints         = Mathf.Max(healthPoints - damage, 0);
            character.baseHealth = healthPoints;
            LoadData();


            if (healthPoints <= 0)
                Health.Die(this);
        }
        public void InStock()
        {
            gameObject.SetActive(false);
            character = null;
            Health.combatManager.deadFighters.Add(this);
        }
    }
}