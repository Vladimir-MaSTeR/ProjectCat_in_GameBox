using UnityEngine;
using UnityEngine.EventSystems;

public class Spark : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [Header("Economic Script")]
    [SerializeField]
    private Economics _economicsScript;

    [Header("Start amount count")]

    private int _id;
    
    private Canvas _mainCanvas;
    private CanvasGroup _canvasGroup;

    private RectTransform _rectTransform; // 2d

    private int _currentSparkValue;

    private void Start() {
        _currentSparkValue = (int)(EventsResources.onGetSparkCurrentValue?.Invoke());

        _id = GetInstanceID();

        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        _currentSparkValue = (int)(EventsResources.onGetSparkCurrentValue?.Invoke());

        if(_currentSparkValue > 0) {
            var slottransform = _rectTransform.parent;
            slottransform.SetAsLastSibling();

            _canvasGroup.blocksRaycasts = false;
        }      
    }

    public void OnDrag(PointerEventData eventData) {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor; //2d
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.localPosition = Vector3.zero;

        _canvasGroup.blocksRaycasts = true;

        // EventsForMearge.onEndDragSound?.Invoke();

    }


    public int GetItemId() {
        return _id;
    }

}
