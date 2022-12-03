
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickKitchen : MonoBehaviour, IPointerClickHandler
{
    [Header("Загружать сохранения или стартовые значения ресурсов")]
    [SerializeField] private bool loadResorces = true;

    /// <summary>
    /// Модель обьекта по уровням от 0 -сломано до n- максимум
    /// </summary>
    [SerializeField]
    private GameObject[] _objectModelShkaf;
    /// <summary>
    /// Модель обьекта по уровням от 0 -сломано до n- максимум
    /// </summary>
    [SerializeField]
    private GameObject[] _objectModelTable;
    /// <summary>
    /// Модель обьекта шкаф
    /// </summary>
    [SerializeField]
    private GameObject _objectShkaf;
    /// <summary>
    /// Модель обьекта стол
    /// </summary>
    [SerializeField]
    private GameObject _objectTable;
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
    ///      Шкала для ремонта фоновая
    /// </summary>
    [SerializeField]
    private GameObject _scaleProgress;
    /// <summary>
    ///     Шкала для ремонта заполнения (прогресса)
    /// </summary>
    [SerializeField]
    private GameObject _scaleProgressUp;
    /// <summary>
    /// Колличество необходимых кликов (ресурсов) для Lv Up
    /// </summary>
    [SerializeField]
    private int _amtRequiredResourceGoLvUp = 1;
    /// <summary>
    /// Количество произведенных кликов для Lv Up
    /// </summary>
    [SerializeField]
    private int _amtAddResource = 0;
    /// <summary>
    /// Время  на починку для Up
    /// </summary>
    [SerializeField]
    private int _needTimeGoLvUp = 2;
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
    /// текушее колличество ресурса в сумке 
    /// </summary>
    private int _needResourceBagNow;
    /// <summary>
    /// требуемый уровень Ресурса для ремонта 
    /// </summary>
    private int _needLvResource;


    private void Start()
    {

        if (!loadResorces)
        {_lvObjectNow = 0;}
        else
        {LoadResouces();}

        _lvObjectMax = _objectModelShkaf.Length - 1;
        _lvObjectMax = _objectModelTable.Length - 1;

        AddModel(_lvObjectNow);
        _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 5;
        _needLvResource = _lvObjectNow + 1;
        _needResourceBagNow = (int)EventsResources.onGetCurentLog?.Invoke(_needLvResource);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // проверка ресурса
        _needLvResource = _lvObjectNow + 1;
        _needResourceBagNow = (int)EventsResources.onGetCurentLog?.Invoke(_needLvResource);

        if (_lvObjectNow < _lvObjectMax &&
              _needResourceBagNow >= _amtRequiredResourceGoLvUp)  /// проверка ресерса
        {
            _amtAddResource += 1;
            if (_activTimeGoLvUp == true)
            { ScaleProgress(true); }

            if (_scaleProgress.activeSelf == false)
            {
                Invoke("_timeScaleOff", _needTimeGoLvUp);
                _activTimeGoLvUp = true;
                ScaleProgress(true);
            }
            else if (_amtAddResource >= _amtRequiredResourceGoLvUp)
            {
                LvUp();
                _amtAddResource = 0;
                ScaleProgress(false);
            }



        } // русурса не хватает
        else
        {

        }



    }


    /// <summary>
    /// повышение уронвя
    /// </summary>
    private void LvUp()
    {
        _lvObjectNow += 1;
        AddModel(_lvObjectNow);
        EventsResources.onLogInBucket?.Invoke(_lvObjectNow, _amtRequiredResourceGoLvUp, 0); // Списать русурс для LvUp ;
        _amtRequiredResourceGoLvUp = (int)(_amtRequiredResourceGoLvUp* 1.3f);
        _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 3;
        _needLvResource = _lvObjectNow + 1;
        SaveResources();
    }


    /// <summary>
    /// Замена обьекта выше уровнем
    /// </summary>
    /// <param name="_LvMod"></param>
    private void AddModel(int _LvMod)
    {
        // _objectModelShkaf _objectModelTable

        Destroy(_objectShkaf);
        if (_LvMod <= _objectModelShkaf.Length && _LvMod >= 0)
        {
            _objectShkaf = Instantiate(_objectModelShkaf[_LvMod], transform.position, Quaternion.Euler(0f, -30f, 0f));
            var _shkaf = _objectShkaf.transform.GetChild(0);  // заглушка 
            _shkaf.transform.position = transform.position; // заглушка 
            _shkaf = _shkaf.transform.GetChild(0); // заглушка 
            _shkaf.transform.position = transform.position; // заглушка 

            _objectShkaf.transform.localScale = new Vector3(3f, 3f, 3f);
            _objectShkaf.transform.SetParent(transform);
            _objectShkaf.transform.SetAsFirstSibling(); // Ввеерх списка
        }

        Destroy(_objectTable);
        if (_LvMod <= _objectModelTable.Length && _LvMod >= 0)
        {
            _objectTable = Instantiate(_objectModelTable[_LvMod], _objectTable.transform.position, Quaternion.Euler(0f, -30f, 0f));
            var _table = _objectTable.transform.GetChild(0);  // заглушка 
            Debug.Log(_table.name);
            _table.transform.position = _objectTable.transform.position; // заглушка 
            Debug.Log(_table.name);
            if (_LvMod >= 1)
            {
                _table = _table.transform.GetChild(0); // заглушка 
                _table.transform.position = _objectTable.transform.position; // заглушка 
            }

            _objectTable.transform.localScale = new Vector3(3f, 3f, 3f);
            _objectTable.transform.SetParent(transform);
            _objectTable.transform.SetAsFirstSibling(); // Ввеерх списка
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
            float _progressResource = (float)_amtAddResource / (float)_amtRequiredResourceGoLvUp;
            { _scaleProgressUp.GetComponent<Image>().fillAmount = _progressResource; }
        }
        else
        {
            _scaleProgress.SetActive(false);
            _scaleProgressUp.SetActive(false);
        }

    }



    private void _timeScaleOff()
    {
        _scaleProgress.SetActive(false);
        _scaleProgressUp.SetActive(false);
        _activTimeGoLvUp = false;
        _amtAddResource = 0;

    }

    private void SaveResources()
    {
        PlayerPrefs.SetInt("lvKitchen", _lvObjectNow);
        PlayerPrefs.Save();
    }

    private void LoadResouces()
    {
        if (loadResorces)
        {
            if (PlayerPrefs.HasKey("lvKitchen"))
            {
                _lvObjectNow = PlayerPrefs.GetInt("lvKitchen");
            }
        }
    }
}
