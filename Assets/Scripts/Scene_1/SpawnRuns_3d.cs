using System.Collections.Generic;
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

        //ReloadItems();
    }

    private void Update() {
        SpawnOneColumnRuns();
        SpawnTwoColumnRuns();
        SpawnThreeColumnRuns();
        SpawnFourColumnRuns();
    }


    /// <summary>
    /// метод для спавна рун первого рядя и их движения
    /// </summary>
    private void SpawnOneColumnRuns() {
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

    private void SpawnTwoColumnRuns() {
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

    private void SpawnThreeColumnRuns() {
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

    private void SpawnFourColumnRuns() {
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
            returneObject = items[Random.Range(0, items.Length)];
        }

        return returneObject;
    }


}
