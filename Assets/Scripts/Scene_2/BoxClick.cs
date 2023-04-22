using UnityEngine;
using UnityEngine.EventSystems;
public class BoxClick : MonoBehaviour, IPointerClickHandler {
    
    public void OnPointerClick(PointerEventData eventData) {
        HomeEvents.onOpenResourcesPanel?.Invoke();
    }
}
