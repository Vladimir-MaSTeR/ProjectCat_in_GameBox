using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] private float _timeRespavnItem = 2f;
    [SerializeField] private float _timeMovingYellow = 15f;

    [Header("Slots")]
    [SerializeField] private Image[] _slots;

    [Header("Items")]
    [SerializeField] private GameObject[] _items;
    [SerializeField] private GameObject[] _items_2lv;
    [SerializeField] private GameObject[] _items_3lv;
    

    private int _currentSlotIndex;
    private int _indexJobSlot;

    private int _currentItemIndex;
    private float _currentMovingYellowTime;

    private float _currentTimeRessItem;

    private List<GameObject> _combinedList;

    private void Start()
    {
        _currentSlotIndex = Random.Range(0, _slots.Length);
        _currentItemIndex = Random.Range(0, _items.Length);
        _currentTimeRessItem = _timeRespavnItem;

        _indexJobSlot = Random.Range(0, _slots.Length);
        _currentMovingYellowTime = _timeMovingYellow;

        // _slots[_indexJobSlot].color = Color.yellow;

        _combinedList = new List<GameObject>();
        _combinedList.AddRange(_items);
        _combinedList.AddRange(_items_2lv);
        _combinedList.AddRange(_items_3lv);

        ReloadItems();
    }


    private void Update()
    {
        SpavnInZeroPoint();
      
    }

    private void OnEnable()
    {
        ButtonsEvents.onSaveResouces += SaveItems;
    }

    private void OnDisable()
    {
        ButtonsEvents.onSaveResouces -= SaveItems;
    }

    private void SpavnInZeroPoint()
    {
        if (_currentTimeRessItem <= 0)
        {
            if (_slots[_currentSlotIndex].GetComponentInChildren<CanvasGroup>() != null)
            {
                _currentSlotIndex = Random.Range(0, _slots.Length);
                _currentItemIndex = Random.Range(0, _items.Length);
            }
            else
            {
                Instantiate(_items[_currentItemIndex], _slots[_currentSlotIndex].rectTransform);

                _currentTimeRessItem = _timeRespavnItem;
                _currentSlotIndex = Random.Range(0, _slots.Length);
                _currentItemIndex = Random.Range(0, _items.Length);
            }

        } else
        {
            _currentTimeRessItem -= Time.deltaTime;
        }
    }

    private void JobBank()
    {
        if (_slots[_indexJobSlot].GetComponentInChildren<CanvasGroup>() != null)
        {
            var amount = _slots[_indexJobSlot].GetComponentInChildren<Item>().GetCurrentAmountForText();
        }
    }
   

    private void ChangeJobInSlot()
    {
        if (_currentMovingYellowTime < 0)
        {

            _slots[_indexJobSlot].color = Color.white;

            _indexJobSlot = Random.Range(0, _slots.Length);
            _slots[_indexJobSlot].color = Color.yellow;

            _currentMovingYellowTime = _timeMovingYellow;
        } else
        {
            _currentMovingYellowTime -= Time.deltaTime;
        }
    }

    private void SaveItems()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].GetComponentInChildren<CanvasGroup>() != null)
            {
                var level = _slots[i].GetComponentInChildren<Item>().GetCurrentAmountForText();
                var tag = _slots[i].GetComponentInChildren<CanvasGroup>().tag;

                PlayerPrefs.SetInt($"numberSlot_{i}", i);
                PlayerPrefs.SetInt($"numberSlot_{i}_level", level);
                PlayerPrefs.SetString($"numberSlot_{i}_tag", tag);

                Debug.Log($"сохранил предмет в слоте {i} с уровнем {level} и тегом {tag}");
            }
        }
    }

    private void ReloadItems()
    {
        var numberSlot = 0;
        var level = 0;
        var tag = "";

        //нужно использовать мапу, ключем будет numberSlot_{i}

        for (int i = 0; i < _slots.Length; i++)
        {
            if (PlayerPrefs.HasKey($"numberSlot_{i}"))
            {
                numberSlot = PlayerPrefs.GetInt($"numberSlot_{i}");
            }

            if (PlayerPrefs.HasKey($"numberSlot_{i}_level"))
            {
                level = PlayerPrefs.GetInt($"numberSlot_{i}_level");
            }

            if (PlayerPrefs.HasKey($"numberSlot_{i}_tag"))
            {
                tag = PlayerPrefs.GetString($"numberSlot_{i}_tag");
            }

            Debug.Log($"Загрузил предмет из памяти для слота {numberSlot} с уровнем {level} и тегом {tag}");

            foreach (var item in _combinedList)
            {
                var currentTag = item.GetComponentInChildren<CanvasGroup>().tag;

                if (tag == currentTag)
                {
                    Instantiate(item, _slots[i].rectTransform);
                    return;
                }
            }
        }
    }
}