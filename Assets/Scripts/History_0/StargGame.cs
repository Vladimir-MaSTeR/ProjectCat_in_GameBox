using UnityEngine;
using UnityEngine.EventSystems;

public class StargGame : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _historyPanel;

    private void Start()
    {
        _mainPanel.SetActive(true);
        _historyPanel.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(" À»  œŒ —“¿–“Œ¬Œ…  ¿–“»Õ ≈");

        _historyPanel.SetActive(true);
       // _mainPanel.SetActive(false);
        
    }

  
}
