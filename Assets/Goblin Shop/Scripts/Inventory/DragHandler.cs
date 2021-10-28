using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GSS.Combat;
using GSS.Control;
using GSS.Inventory;
using UnityEngine.EventSystems;

namespace GSS.Inventory
{

	public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public static GameObject Item;
		
     	public  bool      canDrag = true;
          public Vector3   startPosition;
          public Transform startParent;

          private void Start()
          {
	          startPosition = transform.position;
	          startParent   = transform.parent;
          }

          public void OnBeginDrag(PointerEventData eventData)
          {
	          Item                                       = gameObject;
	          GetComponent<CanvasGroup>().blocksRaycasts = false;
     		transform.SetParent(transform.root);
     		
     		var generic = eventData.pointerDrag.GetComponent<GenericItem>();
     		if (!generic.inCounter) return;
     		
     		generic.itemController.gold         -= generic.price;
     		generic.itemController.goldTxt.text =  generic.itemController.gold.ToString();
     		generic.itemController.counterItems.Remove(generic);
     		generic.inCounter = false;
     	}
     	public void OnDrag(PointerEventData eventData)
     	{
     		if (!canDrag) return;
     		Reference.ui.crosshair.ChangeCrosshair("Drag");
     		transform.position = Input.mousePosition;
     	}
      
     	public void OnEndDrag(PointerEventData eventData)
          {
     		Item = null;

               if (transform.parent == startParent || transform.parent == transform.root) 
     		{
	               transform.position = startPosition;
     			transform.SetParent (startParent);
     		}
               Reference.ui.crosshair.ChangeCrosshair("Select");
     		GetComponent<CanvasGroup>().blocksRaycasts = true;
     	}
          
     	public void OnPointerEnter(PointerEventData eventData)
     	{
     		var item      = eventData.pointerEnter.GetComponent<GenericItem>().item;
     		var character = CharacterGenerator.instance.GetSelected();
      
     		var crossHair = character.equipableItems.Contains(item) ? "Selected" : "Illegal";
     		canDrag = crossHair == "Selected";
     		Reference.ui.crosshair.ChangeCrosshair(crossHair);
     	}
     	public void OnPointerExit(PointerEventData eventData) => 
     		Reference.ui.crosshair.ChangeCrosshair("Select");
          
	}

}
