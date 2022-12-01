using UnityEngine;
using UnityEngine.EventSystems;

public class Bucket : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var childrenTag = eventData.pointerDrag.tag;

        if (childrenTag == ResourcesTags.Log_1.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onLogInBucket?.Invoke(1);
        }

        if (childrenTag == ResourcesTags.Log_2.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onLogInBucket?.Invoke(2);
        }

        if (childrenTag == ResourcesTags.Log_3.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onLogInBucket?.Invoke(3);
        }
    }
}
