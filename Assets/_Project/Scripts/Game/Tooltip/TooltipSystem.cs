using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem Current;
    
    public Tooltip tooltip;
    private void Awake() => Current = this;

    public static void Show(string content, string header = "")
    {
        Current.tooltip.SetText(content, header);
        Current.tooltip.gameObject.SetActive(true);
    }
    
    public static void Hide()
    {
        Current.tooltip.gameObject.SetActive(false);
    }
}
