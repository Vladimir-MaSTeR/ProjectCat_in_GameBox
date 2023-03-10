using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Spawner_3d : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] private float _timeRespavnItem = 2f;

    [Header("Slots")]
    [SerializeField] private GameObject[] _slots;

    [Header("Items")]
    [SerializeField] private GameObject[] _items;
    [SerializeField] private GameObject[] _items_2lv;
    [SerializeField] private GameObject[] _items_3lv;


    private int _currentSlotIndex;
    private int _currentItemIndex;
    private float _currentTimeRessItem;

    private List<GameObject> _combinedList;

    private void Start() {
        _currentSlotIndex = Random.Range(0, _slots.Length);
        _currentItemIndex = Random.Range(0, _items.Length);
        _currentTimeRessItem = _timeRespavnItem;

        _combinedList = new List<GameObject>();
        _combinedList.AddRange(_items);
        _combinedList.AddRange(_items_2lv);
        _combinedList.AddRange(_items_3lv);

        ReloadItems();
    }


    private void Update() {
        SpavnInZeroPoint();

    }

    private void OnEnable() {
        ButtonsEvents.onSaveResouces += SaveItems;
        EventsResources.onSpawnItem += SpawnItem;
        EventsResources.onSpawnItemToSlot += SpawnItemToSlot;
    }

    private void OnDisable() {
        ButtonsEvents.onSaveResouces -= SaveItems;
        EventsResources.onSpawnItem -= SpawnItem;
        EventsResources.onSpawnItemToSlot -= SpawnItemToSlot;
    }

    private void SpavnInZeroPoint() {
        if(_currentTimeRessItem <= 0) {
            if(_slots[_currentSlotIndex].GetComponentInChildren<CanvasGroup>() != null) {
                _currentSlotIndex = Random.Range(0, _slots.Length);
                _currentItemIndex = Random.Range(0, _items.Length);
            } else {
                Instantiate(_items[_currentItemIndex], _slots[_currentSlotIndex].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                _currentTimeRessItem = _timeRespavnItem;
                _currentSlotIndex = Random.Range(0, _slots.Length);
                _currentItemIndex = Random.Range(0, _items.Length);
            }

        } else {
            _currentTimeRessItem -= Time.deltaTime;
        }
    }

    /// <summary>
    /// ����� ��� ������ ����� ���� � ������ ����������, ��������� ������. (�������� ����� ���������� �� ��������� ����� ������)
    /// </summary>
    /// <param name="itemTag">��� ���� ������� ����� ����������</param>
    private void SpawnItem(string itemTag) {
        foreach(var slot in _slots) {
            if(slot.GetComponentInChildren<CanvasGroup>() == null) {
                foreach(var item in _combinedList) {
                    var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

                    if(itemTag == childrenTag) {
                        Instantiate(item, slot.transform);
                        SoundsEvents.onSpawnRuns?.Invoke();
                        return;
                    }
                }
                return;
            }
        }
    }

    /// <summary>
    /// ����� ������� ���� ���� � ���������� ������
    /// </summary>
    /// <param name="itemTag">��� ���� ������� ����� ����������</param>
    /// <param name="slotId">������������� ����� � ������� �������� ����</param>
    private void SpawnItemToSlot(string itemTag, int slotId) {
        foreach(var slot in _slots) {
            if(slot.GetComponentInChildren<Slot_3d>().GetSlotID() == slotId) {
                foreach(var item in _combinedList) {
                    var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

                    if(itemTag == childrenTag) {
                        Instantiate(item, slot.transform);
                        SoundsEvents.onSpawnRuns?.Invoke();
                        return;
                    }
                }
                return;
            }
        }
    }

    private void SaveItems() {
        for(int i = 0; i < _slots.Length; i++) {
            if(_slots[i].GetComponentInChildren<CanvasGroup>() != null) {
                var level = _slots[i].GetComponentInChildren<Item>().GetCurrentAmountForText();
                var tag = _slots[i].GetComponentInChildren<CanvasGroup>().tag;

                PlayerPrefs.SetInt($"numberSlot_{i}", i);
                PlayerPrefs.SetInt($"numberSlot_{i}_level", level);
                PlayerPrefs.SetString($"numberSlot_{i}_tag", tag);

                Debug.Log($"�������� ������� � ����� {i} � ������� {level} � ����� {tag}");
            }
        }
    }

    private void ReloadItems() {
        IDictionary<int, string> reloadDictionary = new Dictionary<int, string>();

        for(int i = 0; i < _slots.Length; i++) {
            if(PlayerPrefs.HasKey($"numberSlot_{i}") && PlayerPrefs.HasKey($"numberSlot_{i}_tag")) {
                var numberSlot = PlayerPrefs.GetInt($"numberSlot_{i}");
                var tag = PlayerPrefs.GetString($"numberSlot_{i}_tag");

                reloadDictionary.Add(numberSlot, tag);
                Debug.Log($"�������� ������� �� ������ ��� ����� {numberSlot} � ����� {tag}");
            }
        }

        SpawnReloadItems(reloadDictionary);
    }

    private void SpawnReloadItems(IDictionary<int, string> dictionary) {
        foreach(var dict in dictionary) {
            var slot = dict.Key;
            var tag = dict.Value;

            foreach(var item in _combinedList) {
                var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

                if(tag == childrenTag) {
                    Instantiate(item, _slots[slot].transform);

                    PlayerPrefs.DeleteKey($"numberSlot_{slot}");
                    PlayerPrefs.DeleteKey($"numberSlot_{slot}_tag");
                }
            }
        }
    }

}
