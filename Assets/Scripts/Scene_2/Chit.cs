using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Chit : MonoBehaviour, IPointerClickHandler
{



    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        _addResourc();


    }


    /// <summary>
    /// истратить ресурсы из сумки для Lv Up
    /// </summary>
    private void _addResourc()
    {


        EventsResources.onStoneInBucket?.Invoke(1, 1, 1);
        EventsResources.onLogInBucket?.Invoke(1, 1, 1);
        EventsResources.onNeilInBucket?.Invoke(1, 1, 1);
        EventsResources.onClouthInBucket?.Invoke(1, 1, 1);

        EventsResources.onStoneInBucket?.Invoke(2, 1, 1);
        EventsResources.onLogInBucket?.Invoke(2, 1, 1);
        EventsResources.onNeilInBucket?.Invoke(2, 1, 1);
        EventsResources.onClouthInBucket?.Invoke(2, 1, 1);

        EventsResources.onStoneInBucket?.Invoke(3, 1, 1);
        EventsResources.onLogInBucket?.Invoke(3, 1, 1);
        EventsResources.onNeilInBucket?.Invoke(3, 1, 1);
        EventsResources.onClouthInBucket?.Invoke(3, 1, 1);
        Debug.Log("Чит!!?");
    }
}



