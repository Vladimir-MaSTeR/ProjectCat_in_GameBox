
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickFireplace : MonoBehaviour, IPointerClickHandler
{
    [Header("��������� ���������� ��� ��������� �������� ��������")]
    [SerializeField] private bool loadResorces = true;

    /// <summary>
    /// ������ ������� �� ������� �� 0 -������� �� n- ��������
    /// </summary>
    [SerializeField]
    private GameObject[] _objectModel;
    /// <summary>
    /// ������ ������� ������
    /// </summary>
    [SerializeField]
    private GameObject _objectNow;
    /// <summary>
    /// ����� (����)
    /// </summary>
    [SerializeField]
    private GameObject _objectLight;
    /// <summary>
    /// ��������� ������� ������� 
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
    private int[] _amtRequiredResourceGoLvUp;//= 1;
    /// <summary>
    /// ��� ����������� (��������)(1-������, 2-������, 3-������, 4-�����) ��� Lv Up 
    /// </summary>
    [SerializeField]
    private int[] _typeRequiredResourceGoLvUp;// = 1;
    /// <summary>
    /// ������� ����������� ������ (��������) ��� Lv Up 
    /// </summary>
    [SerializeField]
    private int[] _lvRequiredResourceGoLvUp;// = 1;
    /// <summary>
    /// ���������� ������������� ������ ��� Lv Up (������� ���������� �����)
    /// </summary>
    [SerializeField]
    private float _amtAddResource = 0;
    /// <summary>
    /// ���������� ����������� ������ ��� Lv Up (�����)
    /// </summary>
    [SerializeField]
    private float _amtClickGoLvUp;
    /// <summary>
    /// �����  �� ������� ��� Up
    /// </summary>
    // [SerializeField]
    // private int _needTimeGoLvUp = 2;
    /// <summary>
    ///  ������ �����������
    /// </summary>
    [SerializeField]
    private bool _activTimeGoLvUp = false;

    /// <summary>
    /// ������� ������� ������� 
    /// </summary>
    private int _lvObjectNow;
    /// <summary>
    /// ������� ����������� ������� � ����� 
    /// </summary>
    private int _needResourceBagNow;
    /// <summary>
    /// ��������� ������� ������� ��� ������� 
    /// </summary>
    private int _needLvResource;

    /// <summary>
    /// ������� ������ (������)
    /// </summary>
    public int LvFireplaceNow
    {
        get {return _lvObjectNow;}
    }

    private void Start()
    {
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
        float _amtClickGoLvUp = 0;
        for (int i = 0; i < _amtRequiredResourceGoLvUp.Length; i++)
        {
            _amtClickGoLvUp += _amtRequiredResourceGoLvUp[i];
        }
        return _amtClickGoLvUp;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // �������� �������
        //_needLvResource = _lvObjectNow + 1;
        // _needResourceBagNow = (int)EventsResources.onGetCurentStone?.Invoke(_needLvResource);

        var _checkResLvUp = _checkResourceLvUp(); /// �������� ��������

             
        if (_lvObjectNow < _lvObjectMax &&
            _checkResLvUp == true )  /// �������� �������
        {
            _amtAddResource += 1;
            //if (_activTimeGoLvUp == true)
            //{ ScaleProgress(true); }

            if (_scaleProgress.activeSelf == false)
            {
                //Invoke("_timeScaleOff", _needTimeGoLvUp);
                _activTimeGoLvUp = true;
                ScaleProgress(true);
                _timeScaleOff();

            }
            else if (_amtAddResource >= _amtClickGoLvUp)
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


    private bool _checkResourceLvUp()
    {
        bool _checkUp = false;
        bool _resUp = true;

        for (int i = 0; i < _amtRequiredResourceGoLvUp.Length; i++)
        {
            if (_resUp == true)
            {
                _resUp = _checkResource(_typeRequiredResourceGoLvUp[i], _lvRequiredResourceGoLvUp[i], _amtRequiredResourceGoLvUp[i]);
            }
        }
        _checkUp = _resUp;

        return _checkUp;

    }


    /// <summary>
    /// ������� ������������ ������� � �����
    /// </summary>
    /// <param name="typeRes">��� ������� (1-������, 2-������, 3-������, 4-�����) </param>
    /// <param name="_lvRes">������� �������</param>
    /// <param name="_amtRes">����������� �������</param>
    /// <returns></returns>
    private bool _checkResource(int typeRes, int _lvRes, int _amtRes)
    {
        bool _check = false;

        switch (typeRes)
        {
            case 1:
                {
                    _needResourceBagNow = (int)EventsResources.onGetCurentStone?.Invoke(_lvRes);
                    if (_needResourceBagNow >= _amtRes)
                    { _check = true; }
                    break;
                }
            case 2:
                {
                    _needResourceBagNow = (int)EventsResources.onGetCurentNeil?.Invoke(_lvRes);
                    if (_needResourceBagNow >= _amtRes)
                    { _check = true; }
                    break;
                }
            case 3:
                {
                    _needResourceBagNow = (int)EventsResources.onGetCurentLog?.Invoke(_lvRes);
                    if (_needResourceBagNow >= _amtRes)
                    { _check = true; }
                    break;
                }
            case 4:
                {
                    _needResourceBagNow = (int)EventsResources.onGetCurentClouth?.Invoke(_lvRes);
                    if (_needResourceBagNow >= _amtRes)
                    { _check = true; }
                    break;
                }
        }
        
        return _check;
        
    }


    /// <summary>
    /// ��������� �������
    /// </summary>
    private void LvUp()
    {
        _objectLight.SetActive(true);
        _lvObjectNow += 1;
        AddModel(_lvObjectNow);
        // EventsResources.onStoneInBucket?.Invoke(_lvObjectNow, _amtRequiredResourceGoLvUp, 0); // ������� ������ ��� LvUp ;
        //  _amtRequiredResourceGoLvUp = (int)(_amtRequiredResourceGoLvUp * 1.3f); // ����� �������
        //  _needTimeGoLvUp = _amtRequiredResourceGoLvUp / 5;

        _amtClickGoLvUp = _clickGoLvUp();
        _needLvResource = _lvObjectNow + 1;
        SaveResources();


    }


    /// <summary>
    /// ������ ������� ���� �������
    /// </summary>
    /// <param name="_LvMod"></param>
    private void AddModel(int _LvMod)
    {
        Destroy(_objectNow);
        if (_LvMod <= _objectModel.Length && _LvMod >= 0)
        {
            _objectNow = Instantiate(_objectModel[_LvMod], transform.position, Quaternion.Euler(0f, 0f, 0f));

            _objectNow.transform.SetParent(transform);
            _objectNow.transform.SetAsFirstSibling(); // ������ ������
        }
    }


    /// <summary>
    /// ����� ���������
    /// </summary>
    /// <param name="OnOff">true= ���,false=����</param>
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
            //if (touch.phase == TouchPhase.Began) /// ������ ������� ��������
			{
                Vector3 _scalePosi = touch.position;
            _scalePosi = new(_scalePosi.x, _scalePosi.y, 0);
                Vector3 _scaleDelta = new Vector3 (-100,-200,0) ;
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
        if (_scaleProgress.activeSelf == true )
        {
            Invoke("_timeScaleOff", 0.1f);
            _amtAddResource = _amtAddResource - 0.1f;
            ScaleProgress(true);
        }
   

        //_scaleProgress.SetActive(false);
        //_scaleProgressUp.SetActive(false);
        //_activTimeGoLvUp = false;
        //_amtAddResource = 0;

    }


    private void SaveResources()
    {
        //PlayerPrefs.SetInt("lvFireplace", _lvObjectNow);
        //PlayerPrefs.SetInt("needResourcFireplace", _amtRequiredResourceGoLvUp);

        PlayerPrefs.Save();
    }

    private void LoadResouces()
    {
        if (loadResorces)
        {
            //if (PlayerPrefs.HasKey("lvFireplace"))
            //{
            //    _lvObjectNow = PlayerPrefs.GetInt("lvFireplace");
            //}
            //if (PlayerPrefs.HasKey("needResourcFireplace"))
            //{
            //    _amtRequiredResourceGoLvUp = PlayerPrefs.GetInt("needResourcFireplace");
            }


        }
    }



