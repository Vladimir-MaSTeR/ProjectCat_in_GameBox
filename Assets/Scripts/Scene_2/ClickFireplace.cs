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
    [SerializeField]
    private GameObject _objectLight;
    /// <summary>
    /// Стартовый Уровень обьекта 
    /// </summary>
    [SerializeField]
    private int _lvObject = 0;
    /// <summary>
    /// Максимальный Уровень обьекта 
    /// </summary>
    [SerializeField]
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
    [SerializeField]
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
    /// Анимация при клике на обьект
    /// </summary>
    private Animation _animClick;


    /// <summary>
    /// Уровень камина (читать)
    /// </summary>
    public int LvFireplaceNow
    {
        get { return _lvObjectNow; }
    }

    private void Start()
    {
        _animClick = GetComponent<Animation>();
        if (!loadResorces)
        { _lvObjectNow = _lvObject; }
        else
        { LoadResouces(); }

        _lvObjectMax = _objectModel.Length - 1;
        AddModel(_lvObjectNow);
        if (_lvObjectNow == 0)
        { _objectLight.SetActive(false); }
        else
        { _objectLight.SetActive(true); }

        // _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 5;
        // _needResourceBagNow = (int)EventsResources.onGetCurentLog?.Invoke(_lvObjectNow + 1);

        _amtClickGoLvUp = _clickGoLvUp();
    }


    private float _clickGoLvUp()
    {
        float _amtClickGoLvUp = this._amtClickGoLvUp;

        return _amtClickGoLvUp;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        var _checkResLvUp = _checkResourceLvUp();   /// true; проверка ресурсов

        if (_lvObjectNow < _lvObjectMax &&
            _checkResLvUp == true)  /// проверка ресерса
        {
            _amtAddResource += 1;
            _animClick.Play("AnimationFireplaceClick");

            if (_scaleProgress.activeSelf == false)
            {
                //Invoke("_timeScaleOff", _needTimeGoLvUp);
                _activTimeGoLvUp = true;
                ScaleProgress(true);
                _timeScaleOff();
            }
            else if (_amtAddResource >= _amtClickGoLvUp)
            {
                _animClick.Play("AnimationFireplaceLvUp");

                LvUp();
                _amtAddResource = 0;
                ScaleProgress(false);
            }



        } // русурса не хватает
        else
        {

        }



    }


    private IDictionary<string, int> _fireplaceDictionary_1lv;


    /// <summary>
    /// проверка на наличее нужных ресерсов в сумке
    /// </summary>
    /// <returns></returns>
    private bool _checkResourceLvUp()
    {
        bool _checkUp = false;
        bool _resUp = true;

        _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary(1);
        var dictionary = _fireplaceDictionary_1lv;


        var stone_1lv = dictionary[ResourcesTags.Stone_1.ToString()];
        var log_1lv = dictionary[ResourcesTags.Log_1.ToString()];
        var neil_1lv = dictionary[ResourcesTags.Neil_1.ToString()];
        var cloth_1lv = dictionary[ResourcesTags.Cloth_1.ToString()];

        if (stone_1lv > 0) // камень 1 ур
        {
            var currentStone = EventsResources.onGetCurentStone(1);
            if (stone_1lv <= currentStone)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("камень 1 ур " + stone_1lv + "<= " + currentStone);

        }
        if (log_1lv > 0) // Дерево 1ур
        {
            var currentLog = EventsResources.onGetCurentLog(1);
            if (log_1lv <= currentLog)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("Дерево 1 ур " + log_1lv + "<= " + currentLog);

        }
        if (neil_1lv > 0) // Гвозди 1 ур
        {
            var currentNeil = EventsResources.onGetCurentNeil(1);
            if (neil_1lv <= currentNeil)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("Гвозди 1 ур " + neil_1lv + "<= " + currentNeil);

        }
        if (cloth_1lv > 0) // Ткань 1 ур
        {
            var currentCloth = EventsResources.onGetCurentClouth(1);
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
        _objectLight.SetActive(true);
        _takeResourceLvUp();
        // EventsResources.onStoneInBucket?.Invoke(_lvObjectNow, _amtRequiredResourceGoLvUp, 0); // Списать русурс для LvUp ;
        //  _amtRequiredResourceGoLvUp = (int)(_amtRequiredResourceGoLvUp * 1.3f); // новое задание
        //  _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 5;

        _lvObjectNow += 1;
        AddModel(_lvObjectNow);
        _amtClickGoLvUp = _clickGoLvUp();
        _amtClickGoLvUp += _lvObjectNow * 2;
        SaveResources();


    }

    private void _takeResourceLvUp()
    {
        _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary(1);
        var dictionary = _fireplaceDictionary_1lv;


        var stone_1lv = dictionary[ResourcesTags.Stone_1.ToString()];
        var log_1lv = dictionary[ResourcesTags.Log_1.ToString()];
        var neil_1lv = dictionary[ResourcesTags.Neil_1.ToString()];
        var cloth_1lv = dictionary[ResourcesTags.Cloth_1.ToString()];

        if (stone_1lv > 0) // камень 1 ур
        {
            var currentStone = EventsResources.onGetCurentStone(1);
            EventsResources.onStoneInBucket(1, currentStone, 0);
        }
        if (log_1lv > 0) // Дерево 1ур
        {
            var currentLog = EventsResources.onGetCurentLog(1);
            EventsResources.onLogInBucket(1, currentLog, 0);
        }
        if (neil_1lv > 0) // Гвозди 1 ур
        {
            var currentNeil = EventsResources.onGetCurentNeil(1);
            EventsResources.onNeilInBucket(1, currentNeil, 0);
        }
        if (cloth_1lv > 0) // Ткань 1 ур
        {
            var currentCloth = EventsResources.onGetCurentClouth(1);
            EventsResources.onClouthInBucket(1, currentCloth, 0);
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

    private void _scaleActivePosi()
    {
        //if (Input.touchCount > 0)
        //{
        //Touch[] touchClick = new Touch[Input.touchCount];
        //for (int i = 0; i < Input.touchCount; i++)
        //{ 
        //    touchClick[i] = Input.GetTouch(i);
        //}

        var touch = Input.GetTouch(0);
        //if (touch.phase == TouchPhase.Began) /// первое нажание косанием
        {
            Vector3 _scalePosi = touch.position;
            _scalePosi = new(_scalePosi.x, _scalePosi.y, 0);
            Vector3 _scaleDelta = new Vector3(-100, -200, 0);
            _scaleProgress.transform.localPosition = _scalePosi + _scaleDelta;
            // Debug.Log(_scalePosi);
        }
        //}
    }

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
                _amtAddResource = _amtAddResource - 0.3f;
            }
            else if (_amtClickGoLvUp * 2f / 3f <= _amtAddResource)
            {
                _amtAddResource = _amtAddResource - 0.1f;
            }
            else
            {
                _amtAddResource = _amtAddResource - 0.2f;
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
        PlayerPrefs.SetInt("lvFireplace", _lvObjectNow);
        // PlayerPrefs.SetInt("needResourcFireplace", _amtRequiredResourceGoLvUp);

        PlayerPrefs.Save();
    }

    private void LoadResouces()
    {
        if (loadResorces)
        {
            if (PlayerPrefs.HasKey("lvFireplace"))
            {
                _lvObjectNow = PlayerPrefs.GetInt("lvFireplace");
            }
            if (PlayerPrefs.HasKey("needResourcFireplace"))
            {
                //       _amtRequiredResourceGoLvUp = PlayerPrefs.GetInt("needResourcFireplace");
            }


        }
    }

}



