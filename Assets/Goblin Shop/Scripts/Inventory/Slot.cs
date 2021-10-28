using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using GSS.Control;

namespace GSS.Inventory
{
	public class Slot : MonoBehaviour, IDropHandler
	{

		private GameObject Item => transform.childCount > 0 ? transform.GetChild(0).gameObject : null;

		public void OnDrop(PointerEventData eventData)
		{
			if (Item) return;
			var droppedItem = eventData.pointerDrag.GetComponent<GenericItem>();
			var dragHandler = eventData.pointerDrag.GetComponent<DragHandler>();


			if (!dragHandler.canDrag) return;
			DragHandler.Item.transform.SetParent(transform);

			AssignToCounter(droppedItem.itemController, droppedItem);
		}

		private static void AssignToCounter(ItemController itemController, GenericItem droppedItem)
		{
			itemController.counterItems.Add(droppedItem);
			itemController.gold         += droppedItem.price;
			itemController.goldTxt.text =  itemController.gold.ToString();
			droppedItem.inCounter       =  true;
		}
	}
}
