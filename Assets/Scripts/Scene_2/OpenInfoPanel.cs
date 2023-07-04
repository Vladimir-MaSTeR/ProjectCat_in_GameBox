using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class OpenInfoPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    [Tooltip("������������� �������� 1 = �����, 2 = ������, 3 = �����")]
    [SerializeField]
    private IdObjectsHome _id = IdObjectsHome.FIREPLACE;

    private float _timeToOpen;
    private bool _startTime = false;

    private void Start() {
        _timeToOpen = (float) HomeEvents.onGetTimeToOpenPanel?.Invoke();
    }
    private void FixedUpdate() {
        if(_startTime) {
            GoTimeToOpen();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        _startTime = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        _startTime = false;
        _timeToOpen = (float) HomeEvents.onGetTimeToOpenPanel?.Invoke();
    }

    private void GoTimeToOpen() {
        if(_timeToOpen <= 0) {
            HomeEvents.onOpenInfoPanels?.Invoke(_id);
        } else {
            _timeToOpen -= Time.deltaTime;
        }
    }
}
