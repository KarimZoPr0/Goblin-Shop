using GSS.com;
using GSS.Combat;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GSS.Inventory {
	public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,
		IPointerExitHandler {
		public static GameObject Item;

		public bool      canDrag = true;
		public Vector3   startPosition;
		public Transform startParent;

		private void Start() {
			startPosition = transform.position;
			startParent   = transform.parent;
		}


		public void OnBeginDrag(PointerEventData eventData) {
			Item = gameObject;

			GetComponent<CanvasGroup>().blocksRaycasts = false;
			transform.SetParent(transform.root);

			var generic = eventData.pointerDrag.GetComponent<GenericItem>();
			if (!generic.inCounter) return;

			generic.itemController.gold         -= generic.price;
			generic.itemController.goldTxt.text =  generic.itemController.gold.ToString();
			generic.itemController.counterItems.Remove(generic);
			generic.inCounter = false;
		}

		public void OnDrag(PointerEventData eventData) {
			if (!canDrag) return;
			ReferenceUI.ui.crosshair.ChangeCrosshair("Drag");
			transform.position = Input.mousePosition;
		}

		public void OnEndDrag(PointerEventData eventData) {
			Item = null;

			if (transform.parent == startParent || transform.parent == transform.root) {
				transform.position = startPosition;
				transform.SetParent(startParent);
			}

			ReferenceUI.ui.crosshair.ChangeCrosshair("Select");
			GetComponent<CanvasGroup>().blocksRaycasts = true;
		}

		public void OnPointerEnter(PointerEventData eventData) {
			var item      = eventData.pointerEnter.GetComponent<GenericItem>();
			var character = CharacterGenerator.Instance.GetSelected();

			if (character == null) return;
			canDrag = character.CanEquip(item);

			var crossHair = canDrag ? "Selected" : "Illegal";
			ReferenceUI.ui.crosshair.ChangeCrosshair(crossHair);
		}

		public void OnPointerExit(PointerEventData eventData) {
			ReferenceUI.ui.crosshair.ChangeCrosshair("Select");
		}
	}
}