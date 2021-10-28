using System;
using System.Collections;
using System.Collections.Generic;
using GSS.com;
using GSS.Combat;
using GSS.Control;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.XR;

public enum ItemType
{
    Resources,
    Weapons
}

namespace GSS.Inventory
{
    public class GenericItem : MonoBehaviour
    {
        [Header("Stats Settings")]
        public static GenericItem Nothing;
        public string id;
        public int    atk;
        public int    def;
        public int    hp;
        public float  price;
        public int    buyLimit;
        public bool   sideEffects;
        public Image  image;

        [Header("Item Settings")] 
        public ItemType       itemType;
        public Item           item;
        public ItemController itemController;
        public bool           inCounter = false;

        private DragHandler dragHandler;

        // updating the data every time we generate items
        public void LoadData()
        {
            id           = item.id;
            atk          = item.atk;
            def          = item.def;
            hp           = item.hp;
            price        = item.price;
            sideEffects  = item.sideEffects;
            buyLimit     = item.buyLimit;
            image.sprite = item.sprite;
        }


        private void Awake()
        {
            dragHandler = GetComponent<DragHandler>();
        }


    }
}
