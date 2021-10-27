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
    public class GenericItem : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [Header("Stats Settings")]

        public static GenericItem Nothing;
        public string id;
        public int atk;
        public int def;
        public int hp;
        public float price;
        public int buyLimit;
        public bool sideEffects;
        public Image image;
        [Header("Item Settings")]
        public ItemType itemType;
        public Item item;
        public ItemController itemController;
        public bool inCounter = false;
        
        // updating the data every time we generate items
        public void LoadData()
        {
            id = item.id;
            atk = item.atk;
            def = item.def;
            hp = item.hp;
            price = item.price;
            sideEffects = item.sideEffects;
            buyLimit = item.buyLimit;
            image.sprite = item.sprite;
        }


        #region Cursor Events
        public void OnPointerEnter(PointerEventData eventData)
        {
            var character = CharacterGenerator.instance.GetSelected();
            if (!character.equipableItems.Contains(item))
            {
                // TODO: Fix this
                //objectSettings.enabled = false;
                Reference.ui.crosshair.ChangeCrosshair("Illegal");
            }
            else
            {
                // TODO: Fix this
                //objectSettings.enabled = true;
                Reference.ui.crosshair.ChangeCrosshair("Select");
            }
        }

       
        public void OnPointerExit(PointerEventData eventData) => 
            Reference.ui.crosshair.ChangeCrosshair("Select");

        public void OnDrag(PointerEventData eventData) =>
            Reference.ui.crosshair.ChangeCrosshair("Drag");
        
        public void OnEndDrag(PointerEventData eventData) =>
            Reference.ui.crosshair.ChangeCrosshair("Select");
       
       

        public void OnBeginDrag(PointerEventData eventData)
        {
            var generic = eventData.pointerDrag.GetComponent<GenericItem>();

            if (generic.inCounter)
            { 
                generic.itemController.gold -= generic.price;
                generic.itemController.goldTxt.text = generic.itemController.gold.ToString();
                generic.itemController.counterItems.Remove(generic);
                inCounter = false;
            }
        }
        
        
        #endregion
        
    }
}
