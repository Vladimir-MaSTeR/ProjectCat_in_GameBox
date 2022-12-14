using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class ClickFireplace : MonoBehaviour, IPointerClickHandler
{
    [Header("Загружать сохранения или стартовые значения ресурсов")]
    [SerializeField] private bool loadResorces = true;

    /// <summary>
    /// Модель обьекта по уровням от 0 -сломано до n- максимум
    /// </summary>
    [SerializeField]
    private GameObject[] _objectModel;
    /// <summary>
    /// Модель обьекта сейчас
    /// </summary>
    [SerializeField]
    private GameObject _objectNow;
    /// <summary>
    /// Огонь (Свет)
    /// </summary>
    /// [SerializeField]     private GameObject _objectLight;
    /// <summary>
    /// Стартовый Уровень обьекта 
    /// </summary>
    [SerializeField]
    private int _lvObject = 0;
    /// <summary>
    /// Максимальный Уровень обьекта 
    /// </summary>
    // [SerializeField]
    private int _lvObjectMax;
    /// <summary>
    ///      Шкала для ремонта активация (канвас)
    /// </summary>
    [SerializeField]
    private GameObject _scaleProgress;
    /// <summary>
    ///     Шкала для ремонта заполнения (прогресса)
    /// </summary>
    [SerializeField]
    private GameObject _scaleProgressUp;


    /// <summary>
    /// Количество необходимых кликов для Lv Up (шкалы)
    /// </summary>
    [SerializeField]
    private float _amtClickGoLvUp = 10;
    /// <summary>
    /// Количество произведенных кликов для Lv Up (Прогрес заполнения шкалы)
    /// </summary>
    // [SerializeField]
    private float _amtAddResource = 0;


    /// <summary>
    ///  Таймер активирован
    /// </summary>
    [SerializeField]
    private bool _activTimeGoLvUp = false;

    /// <summary>
    /// текушей уровень обьекта 
    /// </summary>
    private int _lvObjectNow;
    /// <summary>
    /// Название ключа для Сохранения уровня обьекта
    /// </summary>
    [SerializeField]
    private string _textLvObject ; // = "lvFireplace";
    /// <summary>
    /// Анимация при клике на обьект
    /// </summary>
    [SerializeField]
    private Animation _animClick;
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
    }

    /// <summary>
    /// КОличестко кликов для улучшение на след. уровень
    /// </summary>
    /// <returns></returns>
    private float _clickGoLvUp()
    {
        float _amtClickGoLvUp = this._amtClickGoLvUp;

        return _amtClickGoLvUp;
    }

    /// <summary>
    /// Уровень обьекта 
    /// </summary>
    public int LvObj
    {        get
        {return _lvObjectNow;}
    }
    public string NameObject
    {
        get { return _textLvObject; }
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
        }



    }


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

    }

    /// <summary>
    /// завершение квеста
    /// </summary>
    private void _completedQuests()
    {
        int _lvResObjUp = _lvObjectNow + 1;

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
        IDictionary<string, int> _resictionary = EventsResources.onGetFireplaceDictionary(1);
        // Уровнь в квест журнале (слудующий)
        int _lvResObjUp = _lvObjectNow + 1;

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


        if (stone_1lv > 0) // камень 1 ур
        {
            var currentStone = EventsResources.onGetCurentStone?.Invoke(1);
            if (stone_1lv <= currentStone)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("камень 1 ур " + stone_1lv + "<= " + currentStone);

        }
        if (log_1lv > 0) // Дерево 1ур
        {
            var currentLog = EventsResources.onGetCurentLog?.Invoke(1);
            if (log_1lv <= currentLog)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("Дерево 1 ур " + log_1lv + "<= " + currentLog);

        }
        if (neil_1lv > 0) // Гвозди 1 ур
        {
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(1);
            if (neil_1lv <= currentNeil)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("Гвозди 1 ур " + neil_1lv + "<= " + currentNeil);

        }
        if (cloth_1lv > 0) // Ткань 1 ур
        {
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(1);
            if (cloth_1lv <= currentCloth)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("Ткань 1 ур " + cloth_1lv + "<= " + currentCloth);

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
            EventsResources.onStoneInBucket?.Invoke(1, stone_1lv, 0);
        }
        if (log_1lv < currentLog && log_1lv > 0) // Дерево ур
        {
            EventsResources.onLogInBucket?.Invoke(1, log_1lv, 0);
        }
        if (neil_1lv < currentNeil && neil_1lv > 0) // Гвозди  ур
        {
            EventsResources.onNeilInBucket?.Invoke(1, neil_1lv, 0);
        }
        if (cloth_1lv < currentCloth && cloth_1lv > 0) // Ткань  ур
        {
            EventsResources.onClouthInBucket?.Invoke(1, cloth_1lv, 0);
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
            { _scaleProgressUp.GetComponent<Image>().fillAmount = _progressResource; }
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


    private void SaveResources()
    {
        PlayerPrefs.SetInt(_textLvObject, _lvObjectNow);
        // PlayerPrefs.SetInt("needResourcFireplace", _amtRequiredResourceGoLvUp);
        string _textObjectClickGoLvUp = _textLvObject + "ClickGoLvUp";
         PlayerPrefs.SetFloat(_textObjectClickGoLvUp, _amtClickGoLvUp);

        // 
        PlayerPrefs.Save();
    }

    private void LoadResouces()
    {
        if (loadResorces)
        {
            if (PlayerPrefs.HasKey(_textLvObject))
            {
                _lvObjectNow = PlayerPrefs.GetInt(_textLvObject);
            }
            string _textObjectClickGoLvUp = _textLvObject + "ClickGoLvUp";
            if (PlayerPrefs.HasKey(_textObjectClickGoLvUp))
            {
                _amtClickGoLvUp = PlayerPrefs.GetFloat(_textObjectClickGoLvUp);
            }


        }
    }

}



