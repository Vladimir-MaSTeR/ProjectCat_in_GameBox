using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedAndMeargItem : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] 
    private GameObject[] _slots;

    private Slot_3d oldSlot;
    private Slot_3d currentSlot;
    private int oldSelectedSloId;
    private GameObject oldGameObject;
    private GameObject currentGameObject;

    private void Start() {
        oldGameObject = null;
        currentGameObject = null;
    }

    private void OnEnable() {
        MeargGameEvents.onSelectedSlot += CheckSelectedInSlot;
        MeargGameEvents.onGetOldObject += GetOldGameObject;
        MeargGameEvents.onClearVariables += ClearVariables;
        MeargGameEvents.onClearOldSlot += ClearSlot;
        MeargGameEvents.onGetOldSlot += GetOldSlot;
        MeargGameEvents.onGetCurrentSlot += GetCurrentSlot;
        MeargGameEvents.onGetCurrentObject += GetCurrentGameObject;

        MeargGameEvents.onSetOldObject += SetOldObject;
        MeargGameEvents.onSetOldSlot += SetOldSlot;
    }

    private void OnDisable() {
        MeargGameEvents.onSelectedSlot -= CheckSelectedInSlot;
        MeargGameEvents.onGetOldObject -= GetOldGameObject;
        MeargGameEvents.onClearVariables -= ClearVariables;
        MeargGameEvents.onClearOldSlot -= ClearSlot;
        MeargGameEvents.onGetOldSlot -= GetOldSlot;
        MeargGameEvents.onGetCurrentSlot -= GetCurrentSlot;
        MeargGameEvents.onGetCurrentObject -= GetCurrentGameObject;

        MeargGameEvents.onSetOldObject -= SetOldObject;
        MeargGameEvents.onSetOldSlot -= SetOldSlot;
    }

    public void CheckSelectedInSlot(int slotId, GameObject gameObject) {
        foreach(var item in _slots) {
            var slot = item.GetComponent<Slot_3d>();

            if(slotId == slot.GetSlotID()) {
                Debug.Log($"������-������ ���� = {oldSlot}");
                Debug.Log($"�������-������� ���� = {currentSlot}");
                oldSlot = currentSlot;
                currentSlot = slot;

                Debug.Log($"������-������ ������ = {oldGameObject}");
                Debug.Log($"�������-������� ������ = {currentGameObject}");
                oldGameObject = currentGameObject;
                currentGameObject = gameObject;

                Debug.Log($"������ ���� = {oldSlot}");
                Debug.Log($"����� ���� = {currentSlot}");

                Debug.Log($"������ ������ = {oldGameObject}");
                Debug.Log($"����� ������ = {currentGameObject}");
                //Debug.Log($"Tag ���������� ����� = {currentGameObject.tag}");

                break;
            }
        }
    }

    public GameObject GetOldGameObject() {
        return oldGameObject;
    }

    public void SetOldObject(GameObject gameObj) {
        oldGameObject = gameObj;
        //currentGameObject = gameObj;
    }

    public GameObject GetCurrentGameObject() {
        return currentGameObject;
    }

    public Slot_3d GetOldSlot() {
        return oldSlot;
    }

    public void SetOldSlot(Slot_3d slot) {
        oldSlot = slot;
    }

    public Slot_3d GetCurrentSlot() {
        return currentSlot;
    }

    public void ClearVariables() {
        currentGameObject = null;
        oldGameObject = null;
    }

    public void ClearSlot() {
        currentSlot = null;
        oldSlot = null;
    }
}
