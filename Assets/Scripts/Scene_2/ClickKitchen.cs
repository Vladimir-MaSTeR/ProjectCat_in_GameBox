
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickKitchen : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// ������ ������� �� ������� �� 0 -������� �� n- ��������
    /// </summary>
    [SerializeField]
    private GameObject[] _objectModelShkaf;
    /// <summary>
    /// ������ ������� �� ������� �� 0 -������� �� n- ��������
    /// </summary>
    [SerializeField]
    private GameObject[] _objectModelTable;
    /// <summary>
    /// ������ ������� ����
    /// </summary>
    [SerializeField]
    private GameObject _objectShkaf;
    /// <summary>
    /// ������ ������� ����
    /// </summary>
    [SerializeField]
    private GameObject _objectTable;
    /// <summary>
    /// ������� ������� 
    /// </summary>
    [SerializeField] 
    private int _lvObject = 0;
    /// <summary>
    /// ������������ ������� ������� 
    /// </summary>
[SerializeField]
    private int _lvObjectMax;
    /// <summary>
    ///      ����� ��� ������� �������
    /// </summary>
    [SerializeField]
    private GameObject _scaleProgress;
    /// <summary>
    ///     ����� ��� ������� ���������� (���������)
    /// </summary>
    [SerializeField]
    private GameObject _scaleProgressUp;
    /// <summary>
    /// ����������� ����������� ������ (��������) ��� Lv Up
    /// </summary>
    [SerializeField]
    private int _amtRequiredResourceGoLvUp = 1;
    /// <summary>
    /// ���������� ������������� ������ ��� Lv Up
    /// </summary>
 [SerializeField]
    private int _amtAddResource = 0;
    /// <summary>
    /// �����  �� ������� ��� Up
    /// </summary>
    [SerializeField]
    private int _needTimeGoLvUp = 2;
    /// <summary>
    ///  ������ �����������
    /// </summary>
[SerializeField]
    private bool _activTimeGoLvUp = false;

    private void Start()
    {
        _lvObjectMax = _objectModelShkaf.Length -1;
        _lvObjectMax = _objectModelTable.Length - 1;
        AddModel(_lvObject);
        _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 5;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // �������� �������
        if (_lvObject < _lvObjectMax  &&
              EventsResources.onGetCurentLog?.Invoke(_lvObject + 1) >= _amtRequiredResourceGoLvUp)  /// �������� �������
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



        } // ������� �� �������
        else
        {
            
        }



    }


    /// <summary>
    /// ��������� ������
    /// </summary>
   private void LvUp()
    {
        _lvObject += 1;
         AddModel(_lvObject);
        EventsResources.onLogInBucket?.Invoke(_lvObject, _amtRequiredResourceGoLvUp, 0); // ������� ������ ��� LvUp ;
        _amtRequiredResourceGoLvUp *= 2;
        _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 3;

    }


    /// <summary>
    /// ������ ������� ���� �������
    /// </summary>
    /// <param name="_LvMod"></param>
    private void AddModel(int _LvMod)
    {
        // _objectModelShkaf _objectModelTable

        Destroy(_objectShkaf);
        if (_LvMod <= _objectModelShkaf.Length && _LvMod >= 0)
        {
            _objectShkaf = Instantiate(_objectModelShkaf[_LvMod], transform.position, Quaternion.Euler(0f, -30f, 0f));
            var _shkaf = _objectShkaf.transform.GetChild(0);  // �������� 
            _shkaf.transform.position = transform.position; // �������� 
            _shkaf = _shkaf.transform.GetChild(0); // �������� 
            _shkaf.transform.position = transform.position; // �������� 

            _objectShkaf.transform.localScale = new Vector3(3f, 3f, 3f);
            _objectShkaf.transform.SetParent(transform);
            _objectShkaf.transform.SetAsFirstSibling(); // ������ ������
        }

        Destroy(_objectTable);
        if (_LvMod <= _objectModelTable.Length && _LvMod >= 0)
        {
            _objectTable = Instantiate(_objectModelTable[_LvMod],  _objectTable.transform.position, Quaternion.Euler(0f, -30f, 0f));
            var _table = _objectTable.transform.GetChild(0);  // �������� 
            Debug.Log(_table.name);
            _table.transform.position = _objectTable.transform.position; // �������� 
            Debug.Log(_table.name);
            if (_LvMod >= 1)
            {_table = _table.transform.GetChild(0); // �������� 
                _table.transform.position = _objectTable.transform.position; // �������� 
            }

            _objectTable.transform.localScale = new Vector3(3f, 3f, 3f);
            _objectTable.transform.SetParent(transform);
            _objectTable.transform.SetAsFirstSibling(); // ������ ������
        }

    }


    /// <summary>
    /// ����� ���������
    /// </summary>
    /// <param name="OnOff">true= ���,false=����</param>
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
