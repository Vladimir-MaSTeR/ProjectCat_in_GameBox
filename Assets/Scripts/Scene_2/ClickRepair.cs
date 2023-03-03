using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ClickRepair : MonoBehaviour, IPointerClickHandler
{
    [Header("Загружать сохранения или стартовые значения ресурсов")]
    [SerializeField] private bool loadResorces = true;

    [Tooltip("Модель объекта по уровням от 0 -сломано до n- максимум")]
    [SerializeField] private GameObject[] _objectModel;
    [Tooltip("Модель объекта сейчас")]
    [SerializeField] private GameObject _objectNow;

    [Tooltip("Стартовый Уровень обьекта")]
    [SerializeField] private int _lvObject = 0;
    [Tooltip(" Максимальный Уровень обьекта ")]
    private int _lvObjectMax;
    [Tooltip("текушей уровень обьекта ")]
    private int _lvObjectNow;

    [Tooltip("Шкала для ремонта активация  (фон - канвас)")] 
    [SerializeField] private GameObject _scaleProgress;
    [Tooltip(" Шкала для ремонта заполнения (прогресса)")]     
    [SerializeField] private GameObject _scaleProgressUp;
    [Tooltip("Таймер (Прогрес заполнения шкалы) активирован")]
    [SerializeField] 
    private bool _activTimeGoLvUp = false;

    [Tooltip("Количество необходимых кликов для заполнения (Lv Up) (шкалы)")]
    [SerializeField] private float _amtClickGoLvUp = 10;
    [Tooltip("Количество произведенных кликов для Lv Up (Прогрес заполнения шкалы) ")]
    private float _amtAddResource = 0;

    [Tooltip("текушая прочность обьекта")]
    [SerializeField] private float _healthNow = 10;
    [Tooltip("Максимальная прочность обьекта")]
    private float _healthMax = 10;
    [Tooltip("Атакован пауками")]
    private bool _AtackSpider = false;

    [Tooltip("текуший уют обьекта")]
    [SerializeField]
    private float _comfortNow = 10;
    [Tooltip("Максимальный уют обьекта")]
    private float _comfortMax = 10;


    

   [Tooltip("Название ключа для сохранения объекта ")]
    [SerializeField]
    private string _textNameObject ; // = "lvFireplace";


    [Tooltip("Анимация при клике на обьект ")]
    [SerializeField]    private Animation _animClick;
    /// <summary>
    /// Анимация при клике на обьект
    /// </summary>
    //[SerializeField]
    //private Animation _animLvUp;

    [Header("Выбрать предмет улучшения")]
    [Tooltip("Улучшение камина")]
    [SerializeField] private bool _fireplaceActiv;
    [Tooltip("Улучшение кресло")]
    [SerializeField] private bool _armchairActiv;
    [Tooltip("Улучшение Кухни")]
    [SerializeField] private bool _kitchenActiv;
    [Tooltip("Улучшение Лестница на 2 этаж")]
    [SerializeField] private bool _ladderGoTo2Activ;
    [Tooltip("Улучшение кровати")]
    [SerializeField] private bool _bedActiv;
    [Tooltip("Улучшение Лестница на 3 этаж")]
    [SerializeField] private bool _ladderGoTo3Activ;
    [Tooltip("Улучшение Стол с картой")]
    [SerializeField] private bool _cupboardActiv;

    private void Start()
    {
        // _animClick = GetComponent<Animation>();
        if (!loadResorces)
        { _lvObjectNow = _lvObject; }
        else
        { LoadResouces(); }

        _lvObjectMax = _objectModel.Length - 1;
        AddModel(_lvObjectNow);
        _animClick = _objectNow.GetComponent<Animation>();
        //   _animLvUp = _objectNow.GetComponent<Animation>();

        // _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 5;
        // _needResourceBagNow = (int)EventsResources.onGetCurentLog?.Invoke(_lvObjectNow + 1);

        _amtClickGoLvUp = _clickGoLvUp();

        if (_checkResourceLvUp() == true) /// true; проверка ресурсов
        {
            Invoke("_animReadyUp", 7);
        }

        int typeStatus =0;
            if (_AtackSpider == false)
        { typeStatus = 1; }
        if (_AtackSpider == true)
        { typeStatus = 2; }
        onStatusSpiderHome?.Invoke(_textNameObject, typeStatus);
    }

    /// <summary>
    /// Количестко кликов для улучшение на след. уровень
    /// </summary>

    private float _clickGoLvUp()
    {
        float _amtClickGoLvUp = this._amtClickGoLvUp;
        return _amtClickGoLvUp;
    }

    #region  Данные для внешнего чтения ( уровень, имя, )
    /// <summary>
    /// Уровень обьекта 
    /// </summary>
    public int LvObj
    {        get
        {return _lvObjectNow;}
    }
    /// <summary>
    ///  имя сохранения объекта
    /// </summary>
    public string NameObject
    {
        get { return _textNameObject; }
    }
    #endregion

    [Tooltip("Статус объекта в доме")]
    public static Action<string, int> onStatusSpiderHome;


    private void OnEnable()
    {
        if (_fireplaceActiv == true) //"активания анимации  по апгрейду  камина
        {
            EventsResources.onAnimationReadyUpFireplace += _animReadyUp;
        }
        if (_armchairActiv == true) //активания анимации  по апгрейду  кресло
        {
            EventsResources.onAnimationReadyUpChair += _animReadyUp;
        }
        if (_kitchenActiv == true) //активания анимации  по апгрейду  Кухни
        {
            EventsResources.onAnimationReadyUpTable += _animReadyUp;
        }
        if (_ladderGoTo2Activ == true) //активания анимации  по апгрейду  Лестница на 2 этаж
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_bedActiv == true) //активания анимации  по апгрейду  кровати
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_ladderGoTo3Activ == true) //активания анимации  по апгрейду  Лестница на 3 этаж
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_cupboardActiv == true) //активания анимации  по апгрейду  Стол с картой
        {
            // EventsResources.onAnimationReadyUp
        }



    }

    private void OnDisable()
    {
        if (_fireplaceActiv == true) //"активания анимации  по апгрейду  камина
        {
            EventsResources.onAnimationReadyUpFireplace -= _animReadyUp;
        }
        if (_armchairActiv == true) //активания анимации  по апгрейду  кресло
        {
            EventsResources.onAnimationReadyUpChair -= _animReadyUp;
        }
        if (_kitchenActiv == true) //активания анимации  по апгрейду  Кухни
        {
            EventsResources.onAnimationReadyUpTable -= _animReadyUp;
        }
        if (_ladderGoTo2Activ == true) //активания анимации  по апгрейду  Лестница на 2 этаж
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_bedActiv == true) //активания анимации  по апгрейду  кровати
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_ladderGoTo3Activ == true) //активания анимации  по апгрейду  Лестница на 3 этаж
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_cupboardActiv == true) //активания анимации  по апгрейду  Стол с картой
        {
            // EventsResources.onAnimationReadyUp
        }


    }


    /// <summary>
    /// нажатие на обьект
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        //_animClick.
            var _checkResLvUp = _checkResourceLvUp();   /// true; проверка ресурсов
        _activQuests();
        if (_lvObjectNow < _lvObjectMax &&
            _checkResLvUp == true)  /// проверка ресерса
        {
            _amtAddResource += 1;
            if (_animClick.IsPlaying("AnimationLvUp") == false)
            {
                _animClick.Play("AnimationClick"); // "AnimationClick"
                _soundsСlick();

            }
            else
            {
                Debug.Log("AnimationLvUp +");
            }

            if (_scaleProgress.activeSelf == false)
            {
                //Invoke("_timeScaleOff", _needTimeGoLvUp);
                _activTimeGoLvUp = true;
                ScaleProgress(true);
                _timeScaleOff();
            }
            else if (_amtAddResource >= _amtClickGoLvUp)
            {
                //_animClick.Stop("AnimationClick");

                LvUp();
                _animClick.Play("AnimationLvUp"); // 

                _amtAddResource = 0;
                ScaleProgress(false);
            }

        } // ресурса не хватает
        else
        {
            _activQuests();
            if (_lvObjectNow != _lvObjectMax)
            {
                _animClick.Play("AnimationError");
                SourceHome.onSoundsСlickFalseRepair?.Invoke();
            }
        }


    }

    #region прочность и уют



    /// <summary>
    /// текущий уют в зависимости от количество прочности
    /// </summary>
    /// <returns></returns>
    private float _checkComfort()
    {
        _comfortNow = _comfortMax * (_healthNow / _healthMax);

        return _comfortNow;
    }


    /// <summary>
    /// Увеличение прочности и комфорт от уровня
    /// </summary>
    private void _lvUpComfort(int _lvlObjNow)
    {
        float _healthMaxWas = _healthMax;

        _healthMax += 15;
        _healthNow = _healthNow + (_healthMax - _healthMaxWas);

        /// + УЮТ
        {
            if (_fireplaceActiv == true) //  камин Дает Уют
            {
                switch (_lvlObjNow)
                {
                    case 1: _comfortMax = 80; break;
                    case 2: _comfortMax += 100; break;
                    case 3: _comfortMax += 120; break;

                    default:
                        break;
                }
            }
            if (_armchairActiv == true) //  кресло Дает Уют
            {
                switch (_lvlObjNow)
                {
                    case 1: _comfortMax = 40; break;
                    case 2: _comfortMax += 100; break;
                    case 3: _comfortMax += 120; break;

                    default:
                        break;
                }
            }
            if (_kitchenActiv == true) //  Кухня Дает Уют
            {
                switch (_lvlObjNow)
                {
                    case 1: _comfortMax = 60 + 20; break; ///с тол + буфет
                    case 2: _comfortMax += 100; break;
                    case 3: _comfortMax += 120; break;

                    default:
                        break;
                }
            }
            if (_ladderGoTo2Activ == true) //  Лестница на 2 этаж Дает Уют
            {
            }
            if (_bedActiv == true) //  кровать  Дает Уют
            {
            }
            if (_ladderGoTo3Activ == true) //  Лестница на 3 этаж Дает Уют
            {
            }
            if (_cupboardActiv == true) //  Стол с картой Дает Уют
            {
            }
        }




    }
#endregion




    #region задание (Квест) [Активация - завершение -  проверка]
    /// <summary>
    /// Активания квеста 
    /// </summary>
    private void _activQuests ()
    {
        int _lvResObjUp = _lvObjectNow + 1;

        if (_fireplaceActiv == true) //"активания квеста  по апгрейду  камина
        {
            EventsResources.onFireplaceQuest?.Invoke(_lvResObjUp);
        }
        if (_armchairActiv == true) //активания квеста  по апгрейду  кресло
        {
            EventsResources.onChairQuest?.Invoke(_lvResObjUp);
        }
        if (_kitchenActiv == true) //активания квеста  по апгрейду  Кухни
        {
            EventsResources.onTableQuest?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo2Activ == true) //активания квеста  по апгрейду  Лестница на 2 этаж
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_bedActiv == true) //активания квеста  по апгрейду  кровати
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo3Activ == true) //активания квеста  по апгрейду  Лестница на 3 этаж
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_cupboardActiv == true) //активания квеста  по апгрейду  Стол с картой
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
       // Debug.Log(_lvResObjUp);

    }

    /// <summary>
    /// завершение квеста
    /// </summary>
    private void _completedQuests()
    {
        int _lvResObjUp = _lvObjectNow + 1;
       // Debug.Log("completed "+ _lvResObjUp);
        if (_fireplaceActiv == true) //завершение квеста  по апгрейду  камина
        {
            EventsResources.onEndFireplaceQuest?.Invoke();
        }
        if (_armchairActiv == true) //завершение квеста  по апгрейду  кресло
        {
            EventsResources.onEndChairQuest?.Invoke();
        }
        if (_kitchenActiv == true) //завершение квеста  по апгрейду  Кухни
        {
            EventsResources.onEndTableQuest?.Invoke();
        }
        if (_ladderGoTo2Activ == true) //завершение квеста  по апгрейду  Лестница на 2 этаж
        {
            //  EventsResources. ?.Invoke();
        }
        if (_bedActiv == true) //завершение квеста  по апгрейду  кровати
        {
            //  EventsResources. ?.Invoke();
        }
        if (_ladderGoTo3Activ == true) //завершение квеста  по апгрейду  Лестница на 3 этаж
        {
            //  EventsResources. ?.Invoke();
        }
        if (_cupboardActiv == true) //завершение квеста  по апгрейду  Стол с картой
        {
            //  EventsResources. ?.Invoke();
        }

    }

    /// <summary>
    /// Словарь из квест задания для текущего обьекта
    /// </summary>
    private IDictionary<string, int> _resourceDictionary ()
    {
        int _lvResObjUp = _lvObjectNow + 1;

        IDictionary<string, int> _resictionary = EventsResources.onGetFireplaceDictionary?.Invoke(_lvResObjUp);
        // Уровнь в квест журнале (слудующий)

        if (_fireplaceActiv == true) //"Улучшение камина
        {
            _resictionary.Clear();
            _resictionary =  EventsResources.onGetFireplaceDictionary?.Invoke(_lvResObjUp);
        }
        if (_armchairActiv == true) //Улучшение кресло
        {
            _resictionary.Clear();
            _resictionary = EventsResources.onGetChairDictionary?.Invoke(_lvResObjUp);
        }
        if (_kitchenActiv == true) //Улучшение Кухни
        {
            _resictionary.Clear();
            _resictionary = EventsResources.onGetTableDictionary?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo2Activ == true) //Улучшение Лестница на 2 этаж
        {
            _resictionary.Clear();
            //  _resictionary = EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_bedActiv == true) //Улучшение кровати
        {
            _resictionary.Clear();
            //  _resictionary = EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo3Activ == true) //Улучшение Лестница на 3 этаж
        {
            _resictionary.Clear();
            //  _resictionary = EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_cupboardActiv == true) //Улучшение Стол с картой
        {
            _resictionary.Clear();
            //  _resictionary = EventsResources. ?.Invoke(_lvResObjUp);
        }



        return _resictionary;

    }
    #endregion

    #region Анимацмя и звук
    /// <summary>
    /// Анимация готовности к повышению уровня  
    /// </summary>
    private void _animReadyUp()
    {
        if (_lvObjectNow != _lvObjectMax)
        {
            _animClick.Play("AnimationReadyUp");
            if (_checkResourceLvUp() == true) /// true; проверка ресурсов
            {
                float pTime = Random.Range(2f, 4f);
                Invoke("_animReadyUp", pTime);
            }

            // Debug.Log(_lvResObjUp);
        }
    }

    /// <summary>
    /// звук при нажатии на обьект
    /// </summary>
    private void _soundsСlick()
    {
        if (_fireplaceActiv == true) //звук при нажатии на камин
        {
            SourceHome.onSoundsСlickFireplace?.Invoke();
        }
        if (_armchairActiv == true) // звук при нажатии на кресло
        {
            SourceHome.onSoundsСlickRepairObj?.Invoke();
        }
        if (_kitchenActiv == true) //звук при нажатии на  Кухни
        {
            SourceHome.onSoundsСlickRepairObj?.Invoke();
        }
        if (_ladderGoTo2Activ == true) //звук при нажатии на  Лестница на 2 этаж
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_bedActiv == true) //звук при нажатии на  кровати
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo3Activ == true) //звук при нажатии на  Лестница на 3 этаж
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_cupboardActiv == true) //звук при нажатии на  Стол с картой
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }

    }
    #endregion

    #region повышение уровня [проверка ресерсов- повышение - затраты - смена модели]
    /// <summary>
    /// проверка на наличее нужных ресурсов в сумке
    /// </summary>
    /// <returns></returns>
    private bool _checkResourceLvUp()
    {
        bool _checkUp = false;
        bool _resUp = true;

        var dictionary = _resourceDictionary();

        /// Нужный ресурс от уровня обьекта (КОСТЫЛЬ!!!)
        var stone_1lv = 0;
        var log_1lv = 0;
        var neil_1lv = 0;
        var cloth_1lv = 0;

        if (_lvObjectNow == 0)
        {
             stone_1lv = dictionary[ResourcesTags.Stone_1.ToString()];
             log_1lv = dictionary[ResourcesTags.Log_1.ToString()];
             neil_1lv = dictionary[ResourcesTags.Neil_1.ToString()];
             cloth_1lv = dictionary[ResourcesTags.Cloth_1.ToString()];
        }
        else if (_lvObjectNow == 1)
        {
             stone_1lv = dictionary[ResourcesTags.Stone_2.ToString()];
             log_1lv = dictionary[ResourcesTags.Log_2.ToString()];
             neil_1lv = dictionary[ResourcesTags.Neil_2.ToString()];
             cloth_1lv = dictionary[ResourcesTags.Cloth_2.ToString()];
        }
        else if (_lvObjectNow == 2)
        {
             stone_1lv = dictionary[ResourcesTags.Stone_3.ToString()];
             log_1lv = dictionary[ResourcesTags.Log_3.ToString()];
             neil_1lv = dictionary[ResourcesTags.Neil_3.ToString()];
             cloth_1lv = dictionary[ResourcesTags.Cloth_3.ToString()];
        }

        int _lvResObjUp = _lvObjectNow + 1;

        if (stone_1lv > 0) // камень 1 ур
        {
            var currentStone = EventsResources.onGetCurentStone?.Invoke(_lvResObjUp);
            if (stone_1lv <= currentStone)
            { _resUp = true; }
            else
            { _resUp = false;
                return _checkUp;
            }
            Debug.Log("Треугольная руна  " + stone_1lv + "<= " + currentStone + " ур- " + _lvResObjUp); // камень

        }
        if (log_1lv > 0) // Дерево 1ур
        {
            var currentLog = EventsResources.onGetCurentLog?.Invoke(_lvResObjUp);
            if (log_1lv <= currentLog)
            { _resUp = true; }
            else
            { _resUp = false;
                return _checkUp;
            }
            Debug.Log("Прямоугольная руна   " + log_1lv + "<= " + currentLog + " ур- " + _lvResObjUp); // Дерево

        }
        if (neil_1lv > 0) // Гвозди 1 ур
        {
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(_lvResObjUp);
            if (neil_1lv <= currentNeil)
            { _resUp = true; }
            else
            { _resUp = false;
                return _checkUp;
            }
            Debug.Log("Квадратная руна     " + neil_1lv + "<= " + currentNeil + " ур- " + _lvResObjUp); //Гвозди

        }
        if (cloth_1lv > 0) // Ткань 1 ур
        {
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(_lvResObjUp);
            if (cloth_1lv <= currentCloth)
            { _resUp = true; }
            else
            { _resUp = false;
                return _checkUp;
            }
            Debug.Log("Круглая руна   " + cloth_1lv + "<= " + currentCloth + " ур- " + _lvResObjUp); //Ткань

        }

        _checkUp = _resUp;

        return _checkUp;

    }



    /// <summary>
    /// повышение уровнвя
    /// </summary>
    private void LvUp()
    {
        _takeResourceLvUp();
        // EventsResources.onStoneInBucket?.Invoke(_lvObjectNow, _amtRequiredResourceGoLvUp, 0); // Списать русурс для LvUp ;
        //  _amtRequiredResourceGoLvUp = (int)(_amtRequiredResourceGoLvUp * 1.3f); // новое задание
        //  _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 5;
        _completedQuests();
        _lvObjectNow += 1;
        _lvUpComfort(_lvObjectNow);
        SourceHome.onSoundsСlickLvUpObj?.Invoke();
        AddModel(_lvObjectNow);
        
        _animClick = _objectNow.GetComponent<Animation>();
      //  _animLvUp = _objectNow.GetComponent<Animation>();
        _amtClickGoLvUp = _clickGoLvUp();
        _amtClickGoLvUp += _lvObjectNow * 2;
        SaveResources();

    }

    /// <summary>
    /// истратить ресурсы из сумки для Lv Up
    /// </summary>
    private void _takeResourceLvUp()
    {
        // _resourceDictionary = EventsResources.onGetFireplaceDictionary(1);
        var dictionary = _resourceDictionary();

        /// Нужный ресурс от уровня обьекта (КОСТЫЛЬ!!!)
        var stone_1lv = 0;
        var log_1lv = 0;
        var neil_1lv = 0;
        var cloth_1lv = 0;

        if (_lvObjectNow == 0)
        {
            stone_1lv = dictionary[ResourcesTags.Stone_1.ToString()];
            log_1lv = dictionary[ResourcesTags.Log_1.ToString()];
            neil_1lv = dictionary[ResourcesTags.Neil_1.ToString()];
            cloth_1lv = dictionary[ResourcesTags.Cloth_1.ToString()];
        }
        else if (_lvObjectNow == 1)
        {
            stone_1lv = dictionary[ResourcesTags.Stone_2.ToString()];
            log_1lv = dictionary[ResourcesTags.Log_2.ToString()];
            neil_1lv = dictionary[ResourcesTags.Neil_2.ToString()];
            cloth_1lv = dictionary[ResourcesTags.Cloth_2.ToString()];
        }
        else if (_lvObjectNow == 2)
        {
            stone_1lv = dictionary[ResourcesTags.Stone_3.ToString()];
            log_1lv = dictionary[ResourcesTags.Log_3.ToString()];
            neil_1lv = dictionary[ResourcesTags.Neil_3.ToString()];
            cloth_1lv = dictionary[ResourcesTags.Cloth_3.ToString()];
        }

        /// ресурс в сумке рюкзаке
        var _lvRequiredResource = _lvObjectNow + 1;
        var currentStone = EventsResources.onGetCurentStone?.Invoke(_lvRequiredResource);
        var currentLog = EventsResources.onGetCurentLog?.Invoke(_lvRequiredResource);
        var currentNeil = EventsResources.onGetCurentNeil?.Invoke(_lvRequiredResource);
        var currentCloth = EventsResources.onGetCurentClouth?.Invoke(_lvRequiredResource);



        if (stone_1lv < currentStone && stone_1lv > 0 ) // камень  ур
        {
            EventsResources.onStoneInBucket?.Invoke(_lvRequiredResource, stone_1lv, 0);
            Debug.Log("stone lv " + _lvRequiredResource + " Kol-vo " + stone_1lv);
        }
        if (log_1lv < currentLog && log_1lv > 0) // Дерево ур
        {
            EventsResources.onLogInBucket?.Invoke(_lvRequiredResource, log_1lv, 0);
            Debug.Log("log lv " + _lvRequiredResource + " Kol-vo " + log_1lv);

        }
        if (neil_1lv < currentNeil && neil_1lv > 0) // Гвозди  ур
        {
            EventsResources.onNeilInBucket?.Invoke(_lvRequiredResource, neil_1lv, 0);
            Debug.Log("neil lv " + _lvRequiredResource + " Kol-vo " + neil_1lv);

        }
        if (cloth_1lv < currentCloth && cloth_1lv > 0) // Ткань  ур
        {
            EventsResources.onClouthInBucket?.Invoke(_lvRequiredResource, cloth_1lv, 0);
            Debug.Log("cloth lv " + _lvRequiredResource + " Kol-vo " + cloth_1lv);

        }
    }

    /// <summary>
    /// Замена обьекта выше уровнем
    /// </summary>
    /// <param name="_LvMod"></param>
    private void AddModel(int _LvMod)
    {
        Destroy(_objectNow);
        if (_LvMod <= _objectModel.Length && _LvMod >= 0)
        {
            _objectNow = Instantiate(_objectModel[_LvMod], transform.position, Quaternion.Euler(0f, 0f, 0f));

            _objectNow.transform.SetParent(transform);
            _objectNow.transform.SetAsFirstSibling(); // Ввеерх списка
        }
    }
    #endregion

    #region шкала прогресса + ее откат назад
    /// <summary>
    /// Шкала прогресса
    /// </summary>
    /// <param name="OnOff">true= вкл,false=выкл</param>
    private void ScaleProgress(bool OnOff)
    {
        if (OnOff == true)
        {
            _scaleProgress.SetActive(true);
            _scaleProgressUp.SetActive(true);
            float _progressResource = _amtAddResource / _amtClickGoLvUp;
            { _scaleProgressUp.GetComponent<UnityEngine.UI.Image>().fillAmount = _progressResource; }
        }
        else
        {
            _scaleProgress.SetActive(false);
            _scaleProgressUp.SetActive(false);
        }

    }

    /// <summary>
    /// откат шкалы прогресса назад
    /// </summary>
    private void _timeScaleOff()
    {
        if (_amtAddResource <= 0)
        {
            ScaleProgress(false);
        }
        if (_scaleProgress.activeSelf == true)
        {
            if (_amtClickGoLvUp / 3f <= _amtAddResource)
            {
                _amtAddResource = _amtAddResource - 0.25f;
            }
            else if (_amtClickGoLvUp * 2f / 3f <= _amtAddResource)
            {
                _amtAddResource = _amtAddResource - 0.05f;
            }
            else
            {
                _amtAddResource = _amtAddResource - 0.1f;
            }
            Invoke("_timeScaleOff", 0.1f);
            ScaleProgress(true);
        }


        //_scaleProgress.SetActive(false);
        //_scaleProgressUp.SetActive(false);
        //_activTimeGoLvUp = false;
        //_amtAddResource = 0;

    }
    #endregion

    #region Save - Load


    private void SaveResources()
    {
        string _textKey = _textNameObject + "LvNow"; // _textLvObject
        PlayerPrefs.SetInt(_textKey, _lvObjectNow);
        _textKey = _textNameObject + "ClickGoLvUp"; // _textObjectClickGoLvUp
        PlayerPrefs.SetFloat(_textKey, _amtClickGoLvUp);
        _textKey = _textNameObject + "HpNow"; // _textHpObject
        PlayerPrefs.SetFloat(_textKey, _healthNow);
        _textKey = _textNameObject + "AtackSpider";
        if (_AtackSpider == false)
        {
            PlayerPrefs.SetInt(_textKey, 0);
        }
        else
        {
            PlayerPrefs.SetInt(_textKey, 1);
        }

        //
        PlayerPrefs.Save();
    }

    private void LoadResouces()
    {
        if (loadResorces)
        {
            string _textKey = _textNameObject + "LvNow"; // _textLvObject
            if (PlayerPrefs.HasKey(_textKey))
            {
                _lvObjectNow = PlayerPrefs.GetInt(_textKey);
            }
            _textKey = _textNameObject + "ClickGoLvUp"; // _textObjectClickGoLvUp
            if (PlayerPrefs.HasKey(_textKey))
            {
                _amtClickGoLvUp = PlayerPrefs.GetFloat(_textKey);
            }
            _textKey = _textNameObject + "HpNow"; // _textHpObject
            if (PlayerPrefs.HasKey(_textKey))
            {
                _amtClickGoLvUp = PlayerPrefs.GetFloat(_textKey);
            }
            _textKey = _textNameObject + "AtackSpider"; // _textLvObject
            if (PlayerPrefs.HasKey(_textKey))
            {
                int _loadAtackSpider = PlayerPrefs.GetInt(_textKey);
                if (_loadAtackSpider == 0)
                    _AtackSpider = false;
                else
                { _AtackSpider = true; }
            }
        }
    }
    
    #endregion

}



