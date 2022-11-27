
using UnityEngine;
using UnityEngine.EventSystems;


public class ClickFireplace : MonoBehaviour, IPointerClickHandler
{
    //
    [SerializeField]
    private GameObject[] _àctivate;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (_àctivate[0].activeSelf)
        {
            for (int i = 0; i < _àctivate.Length; i++)
            {
                _àctivate[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < _àctivate.Length; i++)
            {
                _àctivate[i].SetActive(true);
            }
        }



    }





}
