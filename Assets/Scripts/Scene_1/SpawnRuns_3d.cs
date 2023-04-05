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

    private void FixedUpdate() {
        if(_startTimer) {
            SpawnOneColumnRuns();
            SpawnTwoColumnRuns();
            SpawnThreeColumnRuns();
            SpawnFourColumnRuns();
        }
    }

    private void Update() {
        if(_startTimer) {
            //новый мердж3
            Mearg3Column(_oneColumSlots);
            Mearg3Column(_twoColumSlots);
            Mearg3Column(_threeColumSlots);
            Mearg3Column(_fourColumSlots);

            Mearg3Row(0);
            Mearg3Row(1);
            Mearg3Row(2);
            Mearg3Row(3);
            Mearg3Row(4);

            //MoveSelecteSlot(_allSlots);
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

    /// <summary>
    /// метод заморозки колонки
    /// </summary>
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

    /// <summary>
    /// метод разморозки колонки
    /// </summary>
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
    /// новый 3 в ряд, какашка на ифах для колонн
    private void Mearg3Column(GameObject[] columSlots) {
        //вызываем эвент для поиска 3 в ряд

        GameObject object0 = null;
        GameObject object1 = null;
        GameObject object2 = null;
        GameObject object3 = null;
        GameObject object4 = null;

        if(columSlots[0].GetComponentInChildren<CanvasGroup>() != null) {
            //MeargGameEvents.onStartMeargThree?.Invoke(_oneColumSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);

            object0 = columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject;
        }

        if(columSlots[1].GetComponentInChildren<CanvasGroup>() != null) {
            //MeargGameEvents.onStartMeargThree?.Invoke(_oneColumSlots[1].GetComponentInChildren<CanvasGroup>().gameObject);
            object1 = columSlots[1].GetComponentInChildren<CanvasGroup>().gameObject;
        }

        if(columSlots[2].GetComponentInChildren<CanvasGroup>() != null) {
            //MeargGameEvents.onStartMeargThree?.Invoke(_oneColumSlots[2].GetComponentInChildren<CanvasGroup>().gameObject);
            object2 = columSlots[2].GetComponentInChildren<CanvasGroup>().gameObject;
        }

        if(columSlots[3].GetComponentInChildren<CanvasGroup>() != null) {
            //MeargGameEvents.onStartMeargThree?.Invoke(_oneColumSlots[3].GetComponentInChildren<CanvasGroup>().gameObject);
            object3 = columSlots[3].GetComponentInChildren<CanvasGroup>().gameObject;
        }

        if(columSlots[4].GetComponentInChildren<CanvasGroup>() != null) {
            //MeargGameEvents.onStartMeargThree?.Invoke(_oneColumSlots[4].GetComponentInChildren<CanvasGroup>().gameObject);
            object4 = columSlots[4].GetComponentInChildren<CanvasGroup>().gameObject;
        }

        // 5 в ряд
        if(object0 != null && object1 != null && object2 != null && object3 != null && object4 != null) {
            if(object0.tag == object1.tag && object0.tag == object2.tag && object0.tag == object3.tag && object0.tag == object4.tag) {
                Debug.Log("5 В РЯД ПО ВЕРТИКАЛИ");
                AddResouces(object0.tag);
                AddResouces(object1.tag);
                AddResouces(object2.tag);
                AddResouces(object3.tag);
                AddResouces(object4.tag);

                Destroy(object0);
                Destroy(object1);
                Destroy(object2);
                Destroy(object3);
                Destroy(object4);

                UpdateSparks(4);
            }
        }

        // 4 в ряд
        if(object0 != null && object1 != null && object2 != null && object3 != null) {
            if(object0.tag == object1.tag && object0.tag == object2.tag && object0.tag == object3.tag) {
                Debug.Log("4 В РЯД ПО ВЕРТИКАЛИ");
                AddResouces(object0.tag);
                AddResouces(object1.tag);
                AddResouces(object2.tag);
                AddResouces(object3.tag);

                Destroy(object0);
                Destroy(object1);
                Destroy(object2);
                Destroy(object3);

                UpdateSparks(3);
            }
        }

        if(object1 != null && object2 != null && object3 != null && object4 != null) {
            if(object1.tag == object2.tag && object1.tag == object3.tag && object1.tag == object4.tag) {
                Debug.Log("4 В РЯД ПО ВЕРТИКАЛИ");
                AddResouces(object1.tag);
                AddResouces(object2.tag);
                AddResouces(object3.tag);
                AddResouces(object4.tag);

                Destroy(object1);
                Destroy(object2);
                Destroy(object3);
                Destroy(object4);

                UpdateSparks(3);
            }
        }

        // 3 в ряд
        if(object0 != null && object1 != null && object2 != null) {
            if(object0.tag == object1.tag && object0.tag == object2.tag) { //3 в ряд
                Debug.Log("3 В РЯД ПО ВЕРТИКАЛИ");

                AddResouces(object0.tag);
                AddResouces(object1.tag);
                AddResouces(object2.tag);

                Destroy(object0);
                Destroy(object1);
                Destroy(object2);

                UpdateSparks(2);
            }
        }

        if(object1 != null && object2 != null && object3) {
            if(object1.tag == object2.tag && object1.tag == object3.tag) {
                Debug.Log("3 В РЯД ПО ВЕРТИКАЛИ");

                AddResouces(object1.tag);
                AddResouces(object2.tag);
                AddResouces(object3.tag);

                Destroy(object1);
                Destroy(object2);
                Destroy(object3);

                UpdateSparks(2);
            }
        }

        if(object2 != null && object3 != null && object4 != null) {
            if(object2.tag == object3.tag && object2.tag == object4.tag) {
                Debug.Log("3 В РЯД ПО ВЕРТИКАЛИ");

                AddResouces(object2.tag);
                AddResouces(object3.tag);
                AddResouces(object4.tag);

                Destroy(object2);
                Destroy(object3);
                Destroy(object4);

                UpdateSparks(2);
            }
        }

    }

    /// <summary>
    /// новый 3 в ряд, какашка на ифах для рядов
    /// </summary> 
    private void Mearg3Row(int index) {
        //вызываем эвент для поиска 3 в ряд

        GameObject object0Column0 = null;
        GameObject object0Column1 = null;
        GameObject object0Column2 = null;
        GameObject object0Column3 = null;


        if(_oneColumSlots[index].GetComponentInChildren<CanvasGroup>() != null) {
            object0Column0 = _oneColumSlots[index].GetComponentInChildren<CanvasGroup>().gameObject;
        }

        if(_twoColumSlots[index].GetComponentInChildren<CanvasGroup>() != null) {
            object0Column1 = _twoColumSlots[index].GetComponentInChildren<CanvasGroup>().gameObject;
        }

        if(_threeColumSlots[index].GetComponentInChildren<CanvasGroup>() != null) {
            object0Column2 = _threeColumSlots[index].GetComponentInChildren<CanvasGroup>().gameObject;
        }

        if(_fourColumSlots[index].GetComponentInChildren<CanvasGroup>() != null) {
            object0Column3 = _fourColumSlots[index].GetComponentInChildren<CanvasGroup>().gameObject;
        }
        // 4 в ряд
        if(object0Column0 != null && object0Column1 != null && object0Column2 != null && object0Column3 != null) {
            if(object0Column0.tag == object0Column1.tag && object0Column0.tag == object0Column2.tag && object0Column0.tag == object0Column3.tag) {
                Debug.Log("4 В РЯД ПО ГОРИЗОНТАЛИ");
                AddResouces(object0Column0.tag);
                AddResouces(object0Column1.tag);
                AddResouces(object0Column2.tag);
                AddResouces(object0Column3.tag);

                Destroy(object0Column0);
                Destroy(object0Column1);
                Destroy(object0Column2);
                Destroy(object0Column3);

                UpdateSparks(3);
            }
        }

        // 3 в ряд
        if(object0Column0 != null && object0Column1 != null && object0Column2 != null) {
            if(object0Column0.tag == object0Column1.tag && object0Column0.tag == object0Column2.tag) {
                Debug.Log("3 В РЯД ПО ГОРИЗОНТАЛИ");
                AddResouces(object0Column0.tag);
                AddResouces(object0Column1.tag);
                AddResouces(object0Column2.tag);

                Destroy(object0Column0);
                Destroy(object0Column1);
                Destroy(object0Column2);

                UpdateSparks(2);
            }
        }

        if(object0Column1 != null && object0Column2 != null && object0Column3 != null) {
            if(object0Column1.tag == object0Column2.tag && object0Column1.tag == object0Column3.tag) {
                Debug.Log("3 В РЯД ПО ГОРИЗОНТАЛИ");
              
                AddResouces(object0Column1.tag);
                AddResouces(object0Column2.tag);
                AddResouces(object0Column3.tag);

               
                Destroy(object0Column1);
                Destroy(object0Column2);
                Destroy(object0Column3);

                UpdateSparks(2);
            }
        }
    }

    private void UpdateSparks(int count) {
        if(count == 2) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, true);
            Debug.Log($"Увеличиваем искорки на {1}");
        }
        if(count == 3) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(3, true);
            Debug.Log($"Увеличиваем искорки на {3}");
        }
        if(count == 4) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(5, true);
            Debug.Log($"Увеличиваем искорки на {5}");
        }
    }

    private void AddResouces(string tag) {
        if(ResourcesTags.Cloth_1.ToString() == tag) {
            EventsResources.onClouthInBucket?.Invoke(1, 1, 1);
        }
        if(ResourcesTags.Cloth_2.ToString() == tag) {
            EventsResources.onClouthInBucket?.Invoke(2, 1, 1);
        }
        if(ResourcesTags.Cloth_3.ToString() == tag) {
            EventsResources.onClouthInBucket?.Invoke(3, 1, 1);
        }

        if(ResourcesTags.Log_1.ToString() == tag) {
            EventsResources.onLogInBucket?.Invoke(1, 1, 1);
        }
        if(ResourcesTags.Log_2.ToString() == tag) {
            EventsResources.onLogInBucket?.Invoke(2, 1, 1);
        }
        if(ResourcesTags.Log_3.ToString() == tag) {
            EventsResources.onLogInBucket?.Invoke(3, 1, 1);
        }

        if(ResourcesTags.Neil_1.ToString() == tag) {
            EventsResources.onNeilInBucket?.Invoke(1, 1, 1);
        }
        if(ResourcesTags.Neil_2.ToString() == tag) {
            EventsResources.onNeilInBucket?.Invoke(2, 1, 1);
        }
        if(ResourcesTags.Neil_3.ToString() == tag) {
            EventsResources.onNeilInBucket?.Invoke(3, 1, 1);
        }

        if(ResourcesTags.Stone_1.ToString() == tag) {
            EventsResources.onStoneInBucket?.Invoke(1, 1, 1);
        }
        if(ResourcesTags.Stone_2.ToString() == tag) {
            EventsResources.onStoneInBucket?.Invoke(2, 1, 1);
        }
        if(ResourcesTags.Stone_3.ToString() == tag) {
            EventsResources.onStoneInBucket?.Invoke(3, 1, 1);
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
                                    //MoveSelecteSlot(_oneColumSlots);
                                    _currentTimeRespOneColumn = _timeRespOneColumn;
                                }
                            }
                        }
                    }
                    //MoveSelecteSlot(_oneColumSlots);
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
                    //MoveSelecteSlot(_twoColumSlots);
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
                    //MoveSelecteSlot(_threeColumSlots);
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
                    //MoveSelecteSlot(_fourColumSlots);
                }
            } else {
                _currentTimeRespFourColumn -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Метод перемещает выделенный слот вместе с перемещение рун
    /// </summary>
    /// <param name="columSlots"></param>
    private void MoveSelecteSlot(GameObject[] columSlots) {
        for(int i = 0; i < columSlots.Length; i++) {
            var checkSelected = columSlots[i].GetComponentInChildren<Slot_3d>().GetSelected();
            //var slotId = columSlots[i].GetComponentInChildren<Slot_3d>().GetSlotID();

            if(checkSelected) {
                //columSlots[i].GetComponentInChildren<Slot_3d>().DeselectSlot();

                if(i + 1 < columSlots.Length) {
                    //columSlots[i + 1].GetComponentInChildren<Slot_3d>().SelectSlot();
                    var slotId = columSlots[i + 1].GetComponentInChildren<Slot_3d>().GetSlotID();
                    Debug.Log("МОЖЕТ БЫТЬ МЫ СЮДА НЕ ЗАХОДИМ");
                    GameObject pointerObject = columSlots[i + 1].GetComponentInChildren<CanvasGroup>().gameObject;
                    MeargGameEvents.onSelectedSlot?.Invoke(slotId, pointerObject);
                    break;
                }
                //else {
                //    columSlots[i].GetComponentInChildren<Slot_3d>().DeselectSlot();
                //    break;
                //}
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

        MoveSelecteSlot(columSlots);
    }
    private void TwoCirculeSelected(GameObject[] columSlots, GameObject[] itemsLvl) {
        var oneSlotRuneTag = columSlots[0].GetComponentInChildren<CanvasGroup>().tag;
        var twoSlotRuneTag = columSlots[1].GetComponentInChildren<CanvasGroup>().tag;

        //Debug.Log("ТУТ ЕСТЬ ПРОБЛЕМА");

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(twoSlotRuneTag == childrenTag) {
                Destroy(columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[1].GetComponentInChildren<CanvasGroup>().gameObject);
                //Debug.Log("УДАЛИЛ  ДВЕ РУНЫ");
                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(twoSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[2].transform);
                SoundsEvents.onSpawnRuns?.Invoke();
                //Debug.Log("ЗАСПАВНИЛ РУНУ В ТРЕТЬЕМ СЛОТЕ");
                break;
            }
        }

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(oneSlotRuneTag == childrenTag) {
                Instantiate(item, columSlots[1].transform);
                Instantiate(SelectRuns(childrenTag, itemsLvl), columSlots[0].transform);
                SoundsEvents.onSpawnRuns?.Invoke();
                //Debug.Log("ЗАСПАВНИЛ РУНЫ ВО ВТОРОМ И ПЕРВОМ СЛОТЕ");
                //currentTime = startTime;
                break;
            }
        }
        MoveSelecteSlot(columSlots);
    }
    private void ThreeCirculeSelected(GameObject[] columSlots, GameObject[] itemsLvl) {
        var oneSlotRuneTag = columSlots[0].GetComponentInChildren<CanvasGroup>().tag;
        var twoSlotRuneTag = columSlots[1].GetComponentInChildren<CanvasGroup>().tag;
        var threeSlotRuneTag = columSlots[2].GetComponentInChildren<CanvasGroup>().tag;

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(twoSlotRuneTag == childrenTag) {
                Destroy(columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[1].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[2].GetComponentInChildren<CanvasGroup>().gameObject);
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

        MoveSelecteSlot(columSlots);
    }
    private void FoureCirculeSelected(GameObject[] columSlots, GameObject[] itemsLvl) {
        var oneSlotRuneTag = columSlots[0].GetComponentInChildren<CanvasGroup>().tag;
        var twoSlotRuneTag = columSlots[1].GetComponentInChildren<CanvasGroup>().tag;
        var threeSlotRuneTag = columSlots[2].GetComponentInChildren<CanvasGroup>().tag;
        var foureSlotRuneTag = columSlots[3].GetComponentInChildren<CanvasGroup>().tag;

        foreach(var item in _combinedList) {
            var childrenTag = item.GetComponentInChildren<CanvasGroup>().tag;

            if(twoSlotRuneTag == childrenTag) {
                Destroy(columSlots[0].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[1].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[2].GetComponentInChildren<CanvasGroup>().gameObject);
                Destroy(columSlots[3].GetComponentInChildren<CanvasGroup>().gameObject);
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

        MoveSelecteSlot(columSlots);
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

                break;
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
               
                break;
            }
        }

        MoveSelecteSlot(columSlots);
    }
    private GameObject SelectRuns(string  tag, GameObject[] items) {
        GameObject returneObject = items[Random.Range(0, items.Length)];

        while(returneObject.tag == tag) {
            //Debug.Log("Возможно зависаем из за этой падлюки");
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
