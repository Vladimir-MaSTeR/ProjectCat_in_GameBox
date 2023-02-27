using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class Altar : MonoBehaviour, IPointerClickHandler
{
    [Header("Загружать сохранения или стартовые значения ресурсов")]
    [SerializeField] private bool loadResorces = true;
    /// <summary>
    /// Модель обьекта по уровням от 0 - стандартный до n- максимум
    /// </summary>
    [SerializeField]
    private GameObject[] _objectModel;
    /// <summary>
    /// Модель обьекта сейчас
    /// </summary>
    [SerializeField]
    private GameObject _objectNow;
    /// <summary>
    /// Стартовый Уровень обьекта (0 = 1) 
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
    ///      Шкала для заполнения может быть запушенны 
    /// </summary>
    [SerializeField]
    private bool _progressActivat;
    /// <summary>
    ///      Шкала для заполнения будет доспупен после  время.
    /// </summary>
    [SerializeField]
     private float _progressActivatTime;

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
    /// текушая прочность обьекта
    /// </summary>
    [SerializeField]
    private float _healthNow = 10;
    /// <summary>
    /// Максимальная прочность обьекта
    /// </summary>
    // [SerializeField]
    private float _healthMax = 10;


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
    private string _textLvObject; // = "lvFireplace";
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
    [Tooltip("Улучшение 1 алтаря круглая руна")]
    [SerializeField] private bool _altar_1;
    [Tooltip("Улучшение 2 алтаря квадратная руна")]
    [SerializeField] private bool _altar_2;
    [Tooltip("Улучшение 3 алтаря прямоугольная руна")]
    [SerializeField] private bool _altar_3;
    [Tooltip("Улучшение 4 алтаря треугольная руна")]
    [SerializeField] private bool _altar_4;
    private int _typeAltar;


    // Start is called before the first frame update
    void Start()
    {
        if (!loadResorces)
        { _lvObjectNow = _lvObject; }
        else
        { LoadResouces(); }

        _lvObjectMax = _objectModel.Length - 1;
        AddModel(_lvObjectNow);


        /// определяем тип алтаря
        if (_altar_1 == true)
            _typeAltar = 1;
        if (_altar_2 == true)
            _typeAltar = 2;
        if (_altar_3 == true)
            _typeAltar = 3;
        if (_altar_4 == true)
            _typeAltar = 4;
        _checkAltarActivate();

    }

    // Update is called once per frame
    void Update()
    {
        if (_progressActivat == false)
        {
            _checkAltarActivate();

        }

    }

    private void OnEnable()
    {
        AltarTimer.onTimerAltarOff += _altarActivate;
    }

    private void OnDisable()
    {
        AltarTimer.onTimerAltarOff -= _altarActivate;

    }

    /// <summary>
    /// проверка на активность алтаря (отправляет номер алтар получаем время до выдачи рун)
    /// </summary>
    public static Func<int,float> onCheckAltarActivate;



    /// <summary>
    /// добавить руны (int уровень алтаря,int тип алтаря)
    /// </summary>
    public static Action <int,int> onAddRunse;



        /// <summary>
        /// нажатие на обьект
        /// </summary>
        /// <param name="eventData"></param>
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {

        if (_progressActivat == true)
        {
            _amtAddResource += 1;

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

                _addRunes();
                //_animClick.Play("AnimationLvUp"); // 

                _amtAddResource = 0;
                ScaleProgress(false);
            }
            Debug.Log("КЛИК !! " + _amtAddResource);


        }
    }

    /// <summary>
    /// завершение накопления кликов, готовность выдать руны
    /// </summary>
    private void _addRunes()
    {
        _progressActivat = false;
        onAddRunse?.Invoke(_lvObjectNow,_typeAltar );  //(int Lv, int number);


    }

    /// <summary>
    /// Проверка на активность (руны с алтаря выданы или нет если нет то алтарь не активен)
    /// </summary>
    private void _checkAltarActivate ()
    {
        if (onCheckAltarActivate?.Invoke(_typeAltar) > 0)
        { 
            _progressActivat = false;
        }
        else
        {
            _progressActivat = true;
        }




    }



    private void _altarActivate (int typeAltar)
    {
        if(typeAltar == _typeAltar)
        {
            _progressActivat = true;

        }

    }




    #region lv obj повышение уровнвя - модели

    /// <summary>
    /// повышение уровнвя
    /// </summary>
    private void LvUp()
    {
       /// затраты на улучшения  _takeResourceLvUp();
       /// завершения отслеживания улучшения _completedQuests();
       /// увеличения уровня  _lvObjectNow += 1;

       // SourceHome.onSoundsСlickLvUpObj?.Invoke();
       /// замена модели  AddModel(_lvObjectNow);

        SaveResources();

    }

    /// <summary>
    /// истратить ресурсы из сумки для Lv Up
    /// </summary>
    private void _takeResourceLvUp()
    {

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

    #region Scale Шкала прогресса

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
           // Debug.Log(_progressResource);   
            _scaleProgressUp.GetComponent<Image>().fillAmount = _progressResource; 
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
    }

    #endregion

    #region Save - Load



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

    #endregion


}
