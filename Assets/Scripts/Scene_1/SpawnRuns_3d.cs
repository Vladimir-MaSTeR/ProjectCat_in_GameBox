using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnRuns_3d : MonoBehaviour {


    #region Переменные таймеров
    [Header("Таймеры")]
    
    [Tooltip("Время через которое сдвигать первую колонку рун в низ")]
    [SerializeField] 
    private float _timeRespOneColumn = 10f;

    [Tooltip("Время через которое сдвигать вторую колонку рун в низ")]
    [SerializeField] 
    private float _timeRespTwoColumn = 10f;

    [Tooltip("Время через которое сдвигать третью колонку рун в низ")]
    [SerializeField] 
    private float _timeRespThreeColumn = 10f;

    [Tooltip("Время через которое сдвигать четвертую колонку рун в низ")]
    [SerializeField] 
    private float _timeRespFourColumn = 10f;

    [Space(20)] // отступ в инспекторе между полями
    #endregion

    #region Переменные для слотов
    [Header("Слоты")]

    [Tooltip("Все ячейки в которых появляются руны")]
    [SerializeField] 
    private GameObject[] _allSlots;

    [Tooltip("Ячейки первого ряда")]
    [SerializeField] 
    private GameObject[] _oneColumSlots;

    [Tooltip("Ячейки второго ряда")]
    [SerializeField] 
    private GameObject[] _twoColumSlots;

    [Tooltip("Ячейки третьего ряда")]
    [SerializeField] 
    private GameObject[] _threeColumSlots;

    [Tooltip("Ячейки четвертого ряда")]
    [SerializeField] 
    private GameObject[] _fourColumSlots;

    [Space(20)] // отступ в инспекторе между полями
    #endregion

    #region Переменные для РУН
    [Header("Items")]

    [Tooltip("Руны первого уровня")]
    [SerializeField] 
    private GameObject[] _items_1lv;

    [Tooltip("Руны первого уровня")]
    [SerializeField] 
    private GameObject[] _items_2lv;

    [Tooltip("Руны первого уровня")]
    [SerializeField] 
    private GameObject[] _items_3lv;

    [Space(20)] // отступ в инспекторе между полями
    #endregion

    private float _currentTimeRespOneColumn;
    private float _currentTimeRespTwoColumn;
    private float _currentTimeRespThreeColumn;
    private float _currentTimeRespFourColumn;

    private bool _startTimer = true;
    private bool _holdColumn = false;

    private bool _holdOneColumn = false;
    private bool _holdTwoColumn = false;
    private bool _holdThreeColumn = false;
    private bool _holdFourColumn = false;

    private int _currentSlotIndex;
    private int _currentItemIndex;
    private float _currentTimeRessItem;


    private List<GameObject> _combinedList;

    private void Start() {
        _combinedList = new List<GameObject>();
        _combinedList.AddRange(_items_1lv);
        _combinedList.AddRange(_items_2lv);
        _combinedList.AddRange(_items_3lv);

        _currentTimeRespOneColumn = _timeRespOneColumn;
        _currentTimeRespTwoColumn = _timeRespTwoColumn;
        _currentTimeRespThreeColumn = _timeRespThreeColumn;
        _currentTimeRespFourColumn = _timeRespFourColumn;

        _currentSlotIndex = Random.Range(0, _allSlots.Length);

        ReloadItems();
    }

    private void Update() {
        if(_startTimer) {
                SpawnOneColumnRuns();
                SpawnTwoColumnRuns();
                SpawnThreeColumnRuns();
                SpawnFourColumnRuns();
        }       
    }

    private void OnEnable() {
        ButtonsEvents.onSaveResouces += SaveItems;

        MeargGameEvents.onGetCurrentTimeSpawnOldColumn += GetCurrentTimer;
        MeargGameEvents.onSetTimeToSpawnRuns += SetCurrentTimeOldColumn;
        EventsResources.onSpawnItemToSlot += SpawnItemToSlot;

        MeargGameEvents.onTiefRuns += TiefRuns;
        MeargGameEvents.onRandomRuns += RandomRuns;
        MeargGameEvents.onAiceRuns += HoldColumn;
        MeargGameEvents.onFalseHoldColumn += SetFalseHoldColumn;
    }

    private void OnDisable() {
        ButtonsEvents.onSaveResouces -= SaveItems;

        MeargGameEvents.onGetCurrentTimeSpawnOldColumn -= GetCurrentTimer;
        MeargGameEvents.onSetTimeToSpawnRuns -= SetCurrentTimeOldColumn;
        EventsResources.onSpawnItemToSlot -= SpawnItemToSlot;

        MeargGameEvents.onTiefRuns -= TiefRuns;
        MeargGameEvents.onRandomRuns -= RandomRuns;
        MeargGameEvents.onAiceRuns -= HoldColumn;
        MeargGameEvents.onFalseHoldColumn -= SetFalseHoldColumn;
    }

    private void HoldColumn() {
        var currentSpawnPointHoldSpider = MeargGameEvents.onGetCurrentSpawnPointHoldSpider?.Invoke();

        if(currentSpawnPointHoldSpider == 0) {
            _holdOneColumn = true;
        } else if(currentSpawnPointHoldSpider == 1) {
            _holdTwoColumn = true;
        } else if(currentSpawnPointHoldSpider == 2) {
            _holdThreeColumn = true;
        } else if(currentSpawnPointHoldSpider == 3) {
            _holdFourColumn = true;
        }
    }

    private void SetFalseHoldColumn(int column) {
        if(column == 0) {
            _holdOneColumn = false;
            SetCurrentTimeOldColumn(_currentTimeRespTwoColumn);
        } else if(column == 1) {
            _holdTwoColumn = false;
            SetCurrentTimeOldColumn(_currentTimeRespOneColumn);
        } else if(column == 2) {
            _holdThreeColumn = false;
            SetCurrentTimeOldColumn(_currentTimeRespOneColumn);
        } else if(column == 3) {
            _holdFourColumn = false;
            SetCurrentTimeOldColumn(_currentTimeRespOneColumn);
        }
    }

    /// <summary>
    /// Метод для спавна руны в рандомной, свободной клетке.
    /// </summary>
    /// <param name="itemTag">тег руны которую нужно заспавнить</param>
    private void SpawnItemRandomSlotToTag(string itemTag) {
        if(_allSlots[_currentSlotIndex].GetComponentInChildren<CanvasGroup>() != null) {
            _currentSlotIndex = Random.Range(0, _allSlots.Length);
        } else {

            foreach(var item in _combinedList) {
                var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

                if(itemTag == childrenTag) {
                    Instantiate(item, _allSlots[_currentSlotIndex].transform);
                    SoundsEvents.onSpawnRuns?.Invoke();
                    _currentSlotIndex = Random.Range(0, _allSlots.Length);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Метод спавнит одну руну в КОНКРЕТНОЙ ячейке
    /// </summary>
    /// <param name="itemTag">тег руны которую нужно заспавнить</param>
    /// <param name="slotId">идентификатор слота в котором спавнить руну</param>
    private void SpawnItemToSlot(string itemTag, int slotId) {
        foreach(var slot in _allSlots) {
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

    /// <summary>
    /// метод для спавна рун первой колонки и их движения
    /// </summary>
    private void SpawnOneColumnRuns() {
        if(_holdOneColumn == false) {
            if(_currentTimeRespOneColumn <= 0) {
                if(_oneColumSlots[0].GetComponentInChildren<CanvasGroup>() == null) {
                    Instantiate(_items_1lv[Random.Range(0, _items_1lv.Length)], _oneColumSlots[0].transform);
                    SoundsEvents.onSpawnRuns?.Invoke();

                    _currentTimeRespOneColumn = _timeRespOneColumn;

                } else {
                    if(_oneColumSlots[1].GetComponentInChildren<CanvasGroup>() == null) {
                        OneCirculeSelected(_oneColumSlots, _items_1lv);
                        _currentTimeRespOneColumn = _timeRespOneColumn;

                    } else {
                        if(_oneColumSlots[2].GetComponentInChildren<CanvasGroup>() == null) {
                            TwoCirculeSelected(_oneColumSlots, _items_1lv);
                            _currentTimeRespOneColumn = _timeRespOneColumn;
                        } else {
                            if(_oneColumSlots[3].GetComponentInChildren<CanvasGroup>() == null) {
                                ThreeCirculeSelected(_oneColumSlots, _items_1lv);
                                _currentTimeRespOneColumn = _timeRespOneColumn;
                            } else {
                                if(_oneColumSlots[4].GetComponentInChildren<CanvasGroup>() == null) {
                                    FoureCirculeSelected(_oneColumSlots, _items_1lv);
                                    _currentTimeRespOneColumn = _timeRespOneColumn;
                                } else {
                                    FiveCirculeSelected(_oneColumSlots, _items_1lv);
                                    _currentTimeRespOneColumn = _timeRespOneColumn;
                                }
                            }
                        }
                    }
                }
            } else {
                _currentTimeRespOneColumn -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// метод для спавна рун второй колонки и их движения
    /// </summary>
    private void SpawnTwoColumnRuns() {
        if(_holdTwoColumn == false) {
            if(_currentTimeRespTwoColumn <= 0) {
                if(_twoColumSlots[0].GetComponentInChildren<CanvasGroup>() == null) {
                    Instantiate(_items_1lv[Random.Range(0, _items_1lv.Length)], _twoColumSlots[0].transform);
                    SoundsEvents.onSpawnRuns?.Invoke();

                    _currentTimeRespTwoColumn = _timeRespTwoColumn;

                } else {
                    if(_twoColumSlots[1].GetComponentInChildren<CanvasGroup>() == null) {
                        OneCirculeSelected(_twoColumSlots, _items_1lv);
                        _currentTimeRespTwoColumn = _timeRespTwoColumn;

                    } else {
                        if(_twoColumSlots[2].GetComponentInChildren<CanvasGroup>() == null) {
                            TwoCirculeSelected(_twoColumSlots, _items_1lv);
                            _currentTimeRespTwoColumn = _timeRespTwoColumn;
                        } else {
                            if(_twoColumSlots[3].GetComponentInChildren<CanvasGroup>() == null) {
                                ThreeCirculeSelected(_twoColumSlots, _items_1lv);
                                _currentTimeRespTwoColumn = _timeRespTwoColumn;
                            } else {
                                if(_twoColumSlots[4].GetComponentInChildren<CanvasGroup>() == null) {
                                    FoureCirculeSelected(_twoColumSlots, _items_1lv);
                                    _currentTimeRespTwoColumn = _timeRespTwoColumn;
                                } else {
                                    FiveCirculeSelected(_twoColumSlots, _items_1lv);
                                    _currentTimeRespTwoColumn = _timeRespTwoColumn;
                                }
                            }
                        }
                    }
                }


            } else {
                _currentTimeRespTwoColumn -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// метод для спавна рун третьей колонки и их движения
    /// </summary>
    private void SpawnThreeColumnRuns() {
        if(_holdThreeColumn == false) {
            if(_currentTimeRespThreeColumn <= 0) {
                if(_threeColumSlots[0].GetComponentInChildren<CanvasGroup>() == null) {
                    Instantiate(_items_1lv[Random.Range(0, _items_1lv.Length)], _threeColumSlots[0].transform);
                    SoundsEvents.onSpawnRuns?.Invoke();

                    _currentTimeRespThreeColumn = _timeRespThreeColumn;

                } else {
                    if(_threeColumSlots[1].GetComponentInChildren<CanvasGroup>() == null) {
                        OneCirculeSelected(_threeColumSlots, _items_1lv);
                        _currentTimeRespThreeColumn = _timeRespThreeColumn;

                    } else {
                        if(_threeColumSlots[2].GetComponentInChildren<CanvasGroup>() == null) {
                            TwoCirculeSelected(_threeColumSlots, _items_1lv);
                            _currentTimeRespThreeColumn = _timeRespThreeColumn;
                        } else {
                            if(_threeColumSlots[3].GetComponentInChildren<CanvasGroup>() == null) {
                                ThreeCirculeSelected(_threeColumSlots, _items_1lv);
                                _currentTimeRespThreeColumn = _timeRespThreeColumn;
                            } else {
                                if(_threeColumSlots[4].GetComponentInChildren<CanvasGroup>() == null) {
                                    FoureCirculeSelected(_threeColumSlots, _items_1lv);
                                    _currentTimeRespThreeColumn = _timeRespThreeColumn;
                                } else {
                                    FiveCirculeSelected(_threeColumSlots, _items_1lv);
                                    _currentTimeRespThreeColumn = _timeRespThreeColumn;
                                }
                            }
                        }
                    }
                }


            } else {
                _currentTimeRespThreeColumn -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// метод для спавна рун четвертой колонки и их движения
    /// </summary>
    private void SpawnFourColumnRuns() {
        if(_holdFourColumn == false) {
            if(_currentTimeRespFourColumn <= 0) {
                if(_fourColumSlots[0].GetComponentInChildren<CanvasGroup>() == null) {
                    Instantiate(_items_1lv[Random.Range(0, _items_1lv.Length)], _fourColumSlots[0].transform);
                    SoundsEvents.onSpawnRuns?.Invoke();

                    _currentTimeRespFourColumn = _timeRespFourColumn;

                } else {
                    if(_fourColumSlots[1].GetComponentInChildren<CanvasGroup>() == null) {
                        OneCirculeSelected(_fourColumSlots, _items_1lv);
                        _currentTimeRespFourColumn = _timeRespFourColumn;

                    } else {
                        if(_fourColumSlots[2].GetComponentInChildren<CanvasGroup>() == null) {
                            TwoCirculeSelected(_fourColumSlots, _items_1lv);
                            _currentTimeRespFourColumn = _timeRespFourColumn;
                        } else {
                            if(_fourColumSlots[3].GetComponentInChildren<CanvasGroup>() == null) {
                                ThreeCirculeSelected(_fourColumSlots, _items_1lv);
                                _currentTimeRespFourColumn = _timeRespFourColumn;
                            } else {
                                if(_fourColumSlots[4].GetComponentInChildren<CanvasGroup>() == null) {
                                    FoureCirculeSelected(_fourColumSlots, _items_1lv);
                                    _currentTimeRespFourColumn = _timeRespFourColumn;
                                } else {
                                    FiveCirculeSelected(_fourColumSlots, _items_1lv);
                                    _currentTimeRespFourColumn = _timeRespFourColumn;
                                }
                            }
                        }
                    }
                }


            } else {
                _currentTimeRespFourColumn -= Time.deltaTime;
            }
        }
    }

    private void OneCirculeSelected(GameObject[] columSlots, GameObject[] itemsLvl) {
        var oneSlotRuneTag = columSlots[0].GetComponentInChildren<CanvasGroup>().tag;

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(oneSlotRuneTag == childrenTag) {
                Destroy(columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);
                Instantiate(item, columSlots[1].transform);
                Instantiate(SelectRuns(childrenTag, itemsLvl), columSlots[0].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                //currentTime = startTime;
                break;
            }
        }
    }
    private void TwoCirculeSelected(GameObject[] columSlots, GameObject[] itemsLvl) {
        var oneSlotRuneTag = columSlots[0].GetComponentInChildren<CanvasGroup>().tag;
        var twoSlotRuneTag = columSlots[1].GetComponentInChildren<CanvasGroup>().tag;

        foreach(var combin in _combinedList) {
            var childrenTag = combin.GetComponentInChildren<CanvasGroup>().tag;

            if(twoSlotRuneTag == childrenTag) {
                Destroy(columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[1].GetComponentInChildren<CanvasGroup>().gameObject);
                Instantiate(combin, columSlots[2].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(oneSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[1].transform);
                Instantiate(SelectRuns(childrenTag, itemsLvl), columSlots[0].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                //currentTime = startTime;
                break;
            }
        }
    }
    private void ThreeCirculeSelected(GameObject[] columSlots, GameObject[] itemsLvl) {
        var oneSlotRuneTag = columSlots[0].GetComponentInChildren<CanvasGroup>().tag;
        var twoSlotRuneTag = columSlots[1].GetComponentInChildren<CanvasGroup>().tag;
        var threeSlotRuneTag = columSlots[2].GetComponentInChildren<CanvasGroup>().tag;

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(threeSlotRuneTag == childrenTag) {
                Destroy(columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[1].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[2].GetComponentInChildren<CanvasGroup>().gameObject);
                Instantiate(item, columSlots[3].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(twoSlotRuneTag == childrenTag) {              
                Instantiate(item, columSlots[2].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(oneSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[1].transform);
                Instantiate(SelectRuns(childrenTag, itemsLvl), columSlots[0].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                //currentTime = startTime;
                break;
            }
        }
    }
    private void FoureCirculeSelected(GameObject[] columSlots, GameObject[] itemsLvl) {
        var oneSlotRuneTag = columSlots[0].GetComponentInChildren<CanvasGroup>().tag;
        var twoSlotRuneTag = columSlots[1].GetComponentInChildren<CanvasGroup>().tag;
        var threeSlotRuneTag = columSlots[2].GetComponentInChildren<CanvasGroup>().tag;
        var foureSlotRuneTag = columSlots[3].GetComponentInChildren<CanvasGroup>().tag;

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(foureSlotRuneTag == childrenTag) {
                Destroy(columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[1].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[2].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[3].GetComponentInChildren<CanvasGroup>().gameObject);
                Instantiate(item, columSlots[4].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(threeSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[3].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(twoSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[2].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(oneSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[1].transform);
                Instantiate(SelectRuns(childrenTag, itemsLvl), columSlots[0].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                //currentTime = startTime;
                break;
            }
        }
    }
    private void FiveCirculeSelected(GameObject[] columSlots, GameObject[] itemsLvl) {
        var oneSlotRuneTag = columSlots[0].GetComponentInChildren<CanvasGroup>().tag;
        var twoSlotRuneTag = columSlots[1].GetComponentInChildren<CanvasGroup>().tag;
        var threeSlotRuneTag = columSlots[2].GetComponentInChildren<CanvasGroup>().tag;
        var foureSlotRuneTag = columSlots[3].GetComponentInChildren<CanvasGroup>().tag;
        var fiveSlotRuneTag = columSlots[4].GetComponentInChildren<CanvasGroup>().tag;

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(fiveSlotRuneTag == childrenTag) {
                Destroy(columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[1].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[2].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[3].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[4].GetComponentInChildren<CanvasGroup>().gameObject);
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(foureSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[4].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(threeSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[3].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(twoSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[2].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(oneSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[1].transform);
                Instantiate(SelectRuns(childrenTag, itemsLvl), columSlots[0].transform);
                SoundsEvents.onSpawnRuns?.Invoke();

                //currentTime = startTime;
                break;
            }
        }
    }
    private GameObject SelectRuns(string  tag, GameObject[] items) {
        GameObject returneObject = items[Random.Range(0, items.Length)];

        while(returneObject.tag == tag) {
            Debug.Log("Возможно зависаем из за этой падлюки");
            returneObject = items[Random.Range(0, items.Length)];
        }

        return returneObject;
    }

    /// <summary>
    /// Возвращает текущее время таймера певой колонки.
    /// </summary>
    /// <returns></returns>
    private float GetCurrentTimer() {
        //return _currentTimeRespOneColumn;
        return _timeRespOneColumn;
    }
    private void SetCurrentTimeOldColumn(float currentTime) {
        _currentTimeRespOneColumn = currentTime;
        _currentTimeRespTwoColumn = currentTime;
        _currentTimeRespThreeColumn = currentTime;
        _currentTimeRespFourColumn = currentTime;
    }

    /// <summary>
    /// Метод воровства рун
    /// </summary>
    private void TiefRuns() {
        var randomSelectedRuns = Random.Range(0, _allSlots.Length);

        if(_allSlots[randomSelectedRuns].GetComponentInChildren<CanvasGroup>() == null) {
            TiefRuns();
        } else {
            Destroy(_allSlots[randomSelectedRuns].GetComponentInChildren<CanvasGroup>().gameObject);
            Debug.Log("ПАУК УКРАЛ РУНЫ");
        }
    }

    /// <summary>
    /// Метод перемешивания рун на доске
    /// </summary>
    private void RandomRuns() {
        _startTimer = false;

        List<string> tagRuns = new List<string>();
        List<int> slotRunsList = new List<int>();
        HashSet<int> slotNUmbers = new HashSet<int>();

        foreach(var slot in _allSlots) {
            if(slot.GetComponentInChildren<CanvasGroup>() != null) {
                tagRuns.Add(slot.GetComponentInChildren<CanvasGroup>().tag);
                Destroy(slot.GetComponentInChildren<CanvasGroup>().gameObject);
            }
        }

        while(slotNUmbers.Count < tagRuns.Count) {
            slotNUmbers.Add(Random.Range(0, _allSlots.Length));
        }

        slotRunsList = slotNUmbers.ToList();
        for(int i = 0; i < tagRuns.Count; i++) {
            var tag = tagRuns[i];

            foreach(var runs in _combinedList) {
                var runTag = runs.GetComponentInChildren<CanvasGroup>().tag;

                if(tag == runTag) {
                    Instantiate(runs, _allSlots[slotRunsList[i]].transform);
                    SoundsEvents.onSpawnRuns?.Invoke();                
                    break;
                }
            }
        }

        Debug.Log("ПАУК ПЕРЕМЕШАЛ РУНЫ");

        _startTimer = true;
    }

    #region СОХРАНЕНИЕ И ЗАГРУЗКА РУН НА ДОСКЕ
    private void SaveItems() {
        for(int i = 0; i < _allSlots.Length; i++) {
            if(_allSlots[i].GetComponentInChildren<CanvasGroup>() != null) {
                //var level = _slots[i].GetComponentInChildren<Item>().GetCurrentAmountForText();
                var tag = _allSlots[i].GetComponentInChildren<CanvasGroup>().tag;

                PlayerPrefs.SetInt($"numberSlot_{i}", i);
                //PlayerPrefs.SetInt($"numberSlot_{i}_level", level);
                PlayerPrefs.SetString($"numberSlot_{i}_tag", tag);

                //Debug.Log($"сохранил предмет в слоте {i} с уровнем {level} и тегом {tag}");
            }
        }
    }

    private void ReloadItems() {
        IDictionary<int, string> reloadDictionary = new Dictionary<int, string>();

        for(int i = 0; i < _allSlots.Length; i++) {
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
                    Instantiate(item, _allSlots[slot].transform);

                    PlayerPrefs.DeleteKey($"numberSlot_{slot}");
                    PlayerPrefs.DeleteKey($"numberSlot_{slot}_tag");
                }
            }
        }
    }
    #endregion
}
