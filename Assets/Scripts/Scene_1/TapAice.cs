using UnityEngine;
using UnityEngine.EventSystems;

public class TapAice : MonoBehaviour, IPointerDownHandler {

    [Tooltip("Идентификатор льда")]
    [SerializeField]
    private int _id;

    private int currentDefrostCount;

    private void Start() {
        currentDefrostCount = (int)(MeargGameEvents.onGetdefrostCount?.Invoke());
    }

   

    public void OnPointerDown(PointerEventData eventData) {     
        if(currentDefrostCount > 0) {
            currentDefrostCount--;
            Debug.Log($"Есть клик по льду с ID = {_id} для удаления кликните ещё {currentDefrostCount} раз");
            ActifeFalse();
            //проигрывать звук
        } 
    }

    private void ActifeFalse() {
        if(currentDefrostCount <= 0) {
            MeargGameEvents.onFalseHoldColumn?.Invoke(_id); // евент для скрытия льда
            currentDefrostCount = (int)(MeargGameEvents.onGetdefrostCount?.Invoke());
            //проигрывать звук 
        }
    }
}
