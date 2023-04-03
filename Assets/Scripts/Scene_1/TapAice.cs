using UnityEngine;
using UnityEngine.EventSystems;

public class TapAice : MonoBehaviour, IPointerDownHandler {

    [Tooltip("������������� ����")]
    [SerializeField]
    private int _id;

    private int currentDefrostCount;

    private void Start() {
        currentDefrostCount = (int)(MeargGameEvents.onGetdefrostCount?.Invoke());
    }

   

    public void OnPointerDown(PointerEventData eventData) {     
        if(currentDefrostCount > 0) {
            currentDefrostCount--;
            Debug.Log($"���� ���� �� ���� � ID = {_id} ��� �������� �������� ��� {currentDefrostCount} ���");
            ActifeFalse();
            //����������� ����
        } 
    }

    private void ActifeFalse() {
        if(currentDefrostCount <= 0) {
            MeargGameEvents.onFalseHoldColumn?.Invoke(_id); // ����� ��� ������� ����
            currentDefrostCount = (int)(MeargGameEvents.onGetdefrostCount?.Invoke());
            //����������� ���� 
        }
    }
}
