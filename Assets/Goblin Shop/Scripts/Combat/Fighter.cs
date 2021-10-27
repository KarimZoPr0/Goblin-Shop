using System;
using GSS.com;
using GSS.Movement;
using GSS.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GSS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [Header("Character Settings")]
        public CharacterScriptableObject character;

        [Header("Stats Settings")]
        [SerializeField] private TextMeshProUGUI attack;
        [SerializeField] private TextMeshProUGUI defense;
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI gold;

        public Health Health;
        public Move move;
        public Fighter target;

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

        public Fighter()
        {
            isDead = false;
        }

        [SerializeField] public bool isDead { get; set; }

       
    }
}