using GSS.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GenericItem genericItem;

    private static LTDescr Delay;
    private static LTDescr FadeInTween;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!genericItem) return;
        Delay = LeanTween.delayedCall(0.5f, t =>
        {
            TooltipSystem.Show(genericItem.GetContent(), genericItem.GetHeader());
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(Delay.uniqueId);
        TooltipSystem.Hide();
    }
}
