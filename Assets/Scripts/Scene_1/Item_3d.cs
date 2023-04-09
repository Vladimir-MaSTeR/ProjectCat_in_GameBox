using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Item_3d : MonoBehaviour {//, IPointerDownHandler {
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

    //public void OnPointerDown(PointerEventData eventData) {
    //    //throw new System.NotImplementedException();
    //}


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
