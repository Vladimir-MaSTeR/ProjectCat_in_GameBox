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
    /// Метод для спавна одной руны в первой попавшейся, свободной клетке. (ВОЗМЛЖНО СТОИТ ПЕРЕДЕЛАТЬ НА РАНДОМНЫЙ ВЫБОР КЛЕТКИ)
    /// </summary>
    /// <param name="itemTag">тег руны которую нужно заспавнить</param>
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
    /// Метод спавнит одну руну в КОНКРЕТНОЙ ячейке
    /// </summary>
    /// <param name="itemTag">тег руны которую нужно заспавнить</param>
    /// <param name="slotId">идентификатор слота в котором спавнить руну</param>
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

                Debug.Log($"сохранил предмет в слоте {i} с уровнем {level} и тегом {tag}");
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
                Debug.Log($"Загрузил предмет из памяти для слота {numberSlot} и тегом {tag}");
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
