using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class snowFrost : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler 

{
    private void OnCollisionEnter(Collision collision)
    {
         transform.GetComponent<Rigidbody>().isKinematic = true;
        
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        
        Destroy(transform.gameObject) ;
        Debug.Log(transform.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(transform.name + "Enter");
    }
}
