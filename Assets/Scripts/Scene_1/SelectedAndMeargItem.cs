using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedAndMeargItem : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] 
    private GameObject[] _slots;

    private Slot_3d oldSlot;
    private int oldSelectedSloId;
    private GameObject oldGameObject;
    private GameObject currentGameObject;

    private void OnEnable() {
        MeargGameEvents.onSelectedSlot += CheckSelectedInSlot;
        MeargGameEvents.onGetOldObject += GetOldGameObject;
        MeargGameEvents.onClearVariables += ClearVariables;
        MeargGameEvents.onGetOldSlot += GetOldSlot;
    }

    private void OnDisable() {
        MeargGameEvents.onSelectedSlot -= CheckSelectedInSlot;
        MeargGameEvents.onGetOldObject -= GetOldGameObject;
        MeargGameEvents.onClearVariables -= ClearVariables;
        MeargGameEvents.onGetOldSlot -= GetOldSlot;
    }

    private void CheckSelectedInSlot(int slotId, GameObject gameObject) {
        foreach(var item in _slots) {
            var slot = item.GetComponent<Slot_3d>();

            if(slotId == slot.GetSlotID()) {
                oldSelectedSloId = slotId;

                if(oldSlot != null) {
                    oldSlot.DeselectSlot();
                }

                oldSlot = slot;

                if(gameObject != currentGameObject) {
                    slot.SelectSlot();
                    currentGameObject = gameObject;
                } else {
                    slot.DeselectSlot();
                    currentGameObject = null;
                }

                //currentGameObject = gameObject;

                //slot.SelectSlot();

                //Debug.Log($"Идунтификатор выбранного слота = {oldSelectedSloId}");
                //Debug.Log($"Tag выбранного слота = {currentGameObject.tag}");

                return;
            }
        }
    }



    private GameObject GetOldGameObject() {

        if(oldSlot != null) {
            oldSlot.DeselectSlot();
        }

        return currentGameObject;
    }

    private Slot_3d GetOldSlot() {

        //if(oldSlot != null) {
        //    oldSlot.DeselectSlot();
        //}

        return oldSlot;
    }

    private void ClearVariables() {
        currentGameObject = null;
    }
}
