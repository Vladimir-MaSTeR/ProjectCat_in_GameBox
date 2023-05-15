using UnityEngine;
using UnityEngine.EventSystems;

public class Bucket : MonoBehaviour, IDropHandler
{

    private bool holdBucket;


    public void OnDrop(PointerEventData eventData)
    {
        var childrenTag = eventData.pointerDrag.tag;

        SoundsEvents.onBoxOpenSounds?.Invoke();

        CheckLogResouces(childrenTag, eventData);
        CheckNeilResources(childrenTag, eventData);
        CheckCloathResources(childrenTag, eventData);
        CheckStoneResources(childrenTag, eventData);   
    }


    private void OnEnable()
    {
        EventsResources.onHoldBucket += CheckHold;
    }

    private void OnDisable()
    {
        EventsResources.onHoldBucket -= CheckHold;
    }


    private void CheckLogResouces(string childrenTag, PointerEventData eventData)
    {
        if (childrenTag == ResourcesTags.Log_1.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onLogInBucket?.Invoke(1, 1, 1);
            EventsResources.onUpdateQuest?.Invoke();
            Debug.Log($"Дерево {1} уровня  в козине");
        }

        if (childrenTag == ResourcesTags.Log_2.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onLogInBucket?.Invoke(2, 1, 1);
            Debug.Log($"Дерево {2} уровня  в козине");
        }

        if (childrenTag == ResourcesTags.Log_3.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onLogInBucket?.Invoke(3, 1, 1);
            Debug.Log($"Дерево {3} уровня  в козине");
        }

    }

    private void CheckNeilResources(string childrenTag, PointerEventData eventData)
    {
        if (childrenTag == ResourcesTags.Neil_1.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onNeilInBucket?.Invoke(1, 1, 1);
            EventsResources.onUpdateQuest?.Invoke();
            Debug.Log($"Гвоздь {1} уровня  в козине");
        }

        if (childrenTag == ResourcesTags.Neil_2.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onNeilInBucket?.Invoke(2, 1, 1);
            Debug.Log($"Гвоздь {2} уровня  в козине");
        }

        if (childrenTag == ResourcesTags.Neil_3.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onNeilInBucket?.Invoke(3, 1, 1);
            Debug.Log($"Гвоздь {3} уровня  в козине");
        }
    }

    private void CheckCloathResources(string childrenTag, PointerEventData eventData)
    {
        if (childrenTag == ResourcesTags.Cloth_1.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onClouthInBucket?.Invoke(1, 1, 1);
            EventsResources.onUpdateQuest?.Invoke();
            Debug.Log($"Ткань {1} уровня  в козине");
        }

        if (childrenTag == ResourcesTags.Cloth_2.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onClouthInBucket?.Invoke(2, 1, 1);
            Debug.Log($"Ткань {2} уровня  в козине");
        }

        if (childrenTag == ResourcesTags.Cloth_3.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onClouthInBucket?.Invoke(3, 1, 1);
            Debug.Log($"Ткань {3} уровня  в козине");
        }
    }

    private void CheckStoneResources(string childrenTag, PointerEventData eventData)
    {
        if (childrenTag == ResourcesTags.Stone_1.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onStoneInBucket?.Invoke(1, 1, 1);
            EventsResources.onUpdateQuest?.Invoke();
            Debug.Log($"Камень {1} уровня  в козине");
        }

        if (childrenTag == ResourcesTags.Stone_2.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onStoneInBucket?.Invoke(2, 1, 1);
            Debug.Log($"Камень {2} уровня  в козине");
        }

        if (childrenTag == ResourcesTags.Stone_3.ToString())
        {
            Destroy(eventData.pointerDrag);
            EventsResources.onStoneInBucket?.Invoke(3, 1, 1);
            Debug.Log($"Камень {3} уровня  в козине");
        }
    }

    private void CheckHold(bool hold)
    {
        holdBucket = hold;
    }
}
