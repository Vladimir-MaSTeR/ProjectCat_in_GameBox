
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickArmchair : MonoBehaviour, IPointerClickHandler
{
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
    /// Уровень обьекта 
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

    private void Start()
    {
        _lvObjectMax = _objectModel.Length -1;
        AddModel(_lvObject);
        _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 4;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // проверка ресурса
        if (_lvObject < _lvObjectMax  &&
              EventsResources.onGetCurentNeil?.Invoke(_lvObject + 1) >= _amtRequiredResourceGoLvUp)  /// проверка ресерса
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
            else if ( _amtAddResource >= _amtRequiredResourceGoLvUp )
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
        _lvObject += 1;
         AddModel(_lvObject);
        EventsResources.onNeilInBucket?.Invoke(_lvObject, _amtRequiredResourceGoLvUp, 0); // Списать русурс для LvUp ;
        _amtRequiredResourceGoLvUp *= 2;
        _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 4;


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
            _objectNow = Instantiate(_objectModel[_LvMod], transform.position, Quaternion.Euler(0f, 140f, 0f));
            var _tabyr = _objectNow.transform.GetChild(0);  // заглушка 
            _tabyr.transform.position = transform.position; // заглушка 
            _objectNow.transform.localScale = new Vector3(3f, 3f, 3f);
            _objectNow.transform.SetParent(transform);
            _objectNow.transform.SetAsFirstSibling(); // Ввеерх списка
        }
    }


    /// <summary>
    /// Шкала прогресса
    /// </summary>
    /// <param name="OnOff">true= вкл,false=выкл</param>
    private void ScaleProgress (bool OnOff)
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



}
