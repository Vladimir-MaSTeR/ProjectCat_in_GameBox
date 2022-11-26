
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickFireplace : MonoBehaviour, IPointerClickHandler
{
    //
    [SerializeField]
    private GameObject[] _�ctivate;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (_�ctivate[0].activeSelf)
        {
            for (int i = 0; i < _�ctivate.Length; i++)
            {
                _�ctivate[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < _�ctivate.Length; i++)
            {
                _�ctivate[i].SetActive(true);
            }
        }



    }





}
