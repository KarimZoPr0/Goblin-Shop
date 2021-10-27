using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GSS.Inventory
{
    
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Create Item", order = 2)]


    public class Item : ScriptableObject
    {
        [Header("Stats")]
        public string id;
        public int atk;
        public int def;
        public int hp;
        public float price;
        public int buyLimit;
        public bool sideEffects;
        public Sprite sprite;
        public Vector2 scale;


        
        
        // this is for the bomb > 50/50 to get -3 def
        private void Start()
        {
            if (!sideEffects) return;
            var r = Random.Range(1, 2);
            if (r == 1)
                def -= 3;
        }
    }
}
