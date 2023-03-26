using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Item_3d : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [Header("Amount Text")]
    //[SerializeField] private Text _text;

    [Header("Start amount count")]
    //[SerializeField] private int _minAmountText = 1;
    //[SerializeField] private int _maxAmountText = 10;

    private int _id;
    private int _currentAmountForText;

    private float currentTransLocalPosX = 0f;
    private float currentTransLocalPosY = 0f;

    private Canvas _mainCanvas;
    private CanvasGroup _canvasGroup;

    //private RectTransform _rectTransform;// 2d
    private Transform _transform;// 3d

    private void Start() {
        _id = GetInstanceID();

        _transform = GetComponent<Transform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();

    }

    public void OnBeginDrag(PointerEventData eventData) {

        //var slottransform = _transform.parent;
        //slottransform.SetAsLastSibling();

        //_canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {   

        //_transform.localPosition += new Vector3(eventData.delta.x / _mainCanvas.scaleFactor, 
        //                                        eventData.delta.y / _mainCanvas.scaleFactor, 
        //                                        0);
    }

    public void OnEndDrag(PointerEventData eventData) {

       
        //transform.localPosition = Vector3.zero;

        //_canvasGroup.blocksRaycasts = true;

        // EventsForMearge.onEndDragSound?.Invoke();

    }

    public int GetCurrentAmountForText() {
        return _currentAmountForText;
    }

    public void SetCurrentAmountForText(int currentAmount) {
        _currentAmountForText = currentAmount;
    }

    public int GetItemId() {
        return _id;
    }

    public float GetItemLocalPositionX() {
        return currentTransLocalPosX;
    }

    public float GetItemLocalPositionY() {
        return currentTransLocalPosY;
    }

}
