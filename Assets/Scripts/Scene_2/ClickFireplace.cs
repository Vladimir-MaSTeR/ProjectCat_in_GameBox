using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    /// [SerializeField]     private GameObject _objectLight;
    /// <summary>
    /// ��������� ������� ������� 
    /// </summary>
    [SerializeField]
    private int _lvObject = 0;
    /// <summary>
    /// ������������ ������� ������� 
    /// </summary>
    // [SerializeField]
    private int _lvObjectMax;
    /// <summary>
    ///      ����� ��� ������� ��������� (������)
    /// </summary>
    [SerializeField]
    private GameObject _scaleProgress;
    /// <summary>
    ///     ����� ��� ������� ���������� (���������)
    /// </summary>
    [SerializeField]
    private GameObject _scaleProgressUp;


    /// <summary>
    /// ���������� ����������� ������ ��� Lv Up (�����)
    /// </summary>
    [SerializeField]
    private float _amtClickGoLvUp = 10;
    /// <summary>
    /// ���������� ������������� ������ ��� Lv Up (������� ���������� �����)
    /// </summary>
    // [SerializeField]
    private float _amtAddResource = 0;


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
    /// �������� ����� ��� ���������� ������ �������
    /// </summary>
    [SerializeField]
    private string _textLvObject ; // = "lvFireplace";
    /// <summary>
    /// �������� ��� ����� �� ������
    /// </summary>
    [SerializeField]
    private Animation _animClick;
    /// <summary>
    /// �������� ��� ����� �� ������
    /// </summary>
    //[SerializeField]
    //private Animation _animLvUp;

    [Header("������� ������� ���������")]
    [Tooltip("��������� ������")]
    [SerializeField] private bool _fireplaceActiv;
    [Tooltip("��������� ������")]
    [SerializeField] private bool _armchairActiv;
    [Tooltip("��������� �����")]
    [SerializeField] private bool _kitchenActiv;
    [Tooltip("��������� �������� �� 2 ����")]
    [SerializeField] private bool _ladderGoTo2Activ;
    [Tooltip("��������� �������")]
    [SerializeField] private bool _bedActiv;
    [Tooltip("��������� �������� �� 3 ����")]
    [SerializeField] private bool _ladderGoTo3Activ;
    [Tooltip("��������� ���� � ������")]
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
    /// ���������� ������ ��� ��������� �� ����. �������
    /// </summary>
    /// <returns></returns>
    private float _clickGoLvUp()
    {
        float _amtClickGoLvUp = this._amtClickGoLvUp;

        return _amtClickGoLvUp;
    }

    /// <summary>
    /// ������� ������� 
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
    /// ������� �� ������
    /// </summary>
    /// <param name="eventData"></param>
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        //_animClick.

                var _checkResLvUp = _checkResourceLvUp();   /// true; �������� ��������
        _activQuests();

        if (_lvObjectNow < _lvObjectMax &&
            _checkResLvUp == true)  /// �������� �������
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

        } // ������� �� �������
        else
        {
            _activQuests();
        }



    }


    /// <summary>
    /// ��������� ������ 
    /// </summary>
    private void _activQuests ()
    {
        int _lvResObjUp = _lvObjectNow + 1;

        if (_fireplaceActiv == true) //"��������� ������  �� ��������  ������
        {
            EventsResources.onFireplaceQuest?.Invoke(_lvResObjUp);
        }
        if (_armchairActiv == true) //��������� ������  �� ��������  ������
        {
            EventsResources.onChairQuest?.Invoke(_lvResObjUp);
        }
        if (_kitchenActiv == true) //��������� ������  �� ��������  �����
        {
            EventsResources.onTableQuest?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo2Activ == true) //��������� ������  �� ��������  �������� �� 2 ����
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_bedActiv == true) //��������� ������  �� ��������  �������
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo3Activ == true) //��������� ������  �� ��������  �������� �� 3 ����
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_cupboardActiv == true) //��������� ������  �� ��������  ���� � ������
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }

    }

    /// <summary>
    /// ���������� ������
    /// </summary>
    private void _completedQuests()
    {
        int _lvResObjUp = _lvObjectNow + 1;

        if (_fireplaceActiv == true) //���������� ������  �� ��������  ������
        {
            EventsResources.onEndFireplaceQuest?.Invoke();
        }
        if (_armchairActiv == true) //���������� ������  �� ��������  ������
        {
            EventsResources.onEndChairQuest?.Invoke();
        }
        if (_kitchenActiv == true) //���������� ������  �� ��������  �����
        {
            EventsResources.onEndTableQuest?.Invoke();
        }
        if (_ladderGoTo2Activ == true) //���������� ������  �� ��������  �������� �� 2 ����
        {
            //  EventsResources. ?.Invoke();
        }
        if (_bedActiv == true) //���������� ������  �� ��������  �������
        {
            //  EventsResources. ?.Invoke();
        }
        if (_ladderGoTo3Activ == true) //���������� ������  �� ��������  �������� �� 3 ����
        {
            //  EventsResources. ?.Invoke();
        }
        if (_cupboardActiv == true) //���������� ������  �� ��������  ���� � ������
        {
            //  EventsResources. ?.Invoke();
        }

    }






    /// <summary>
    /// ������� �� ����� ������� ��� �������� �������
    /// </summary>
    private IDictionary<string, int> _resourceDictionary ()
    {
        IDictionary<string, int> _resictionary = EventsResources.onGetFireplaceDictionary(1);
        // ������ � ����� ������� (���������)
        int _lvResObjUp = _lvObjectNow + 1;

        if (_fireplaceActiv == true) //"��������� ������
        {
            _resictionary.Clear();
            _resictionary =  EventsResources.onGetFireplaceDictionary?.Invoke(_lvResObjUp);
        }
        if (_armchairActiv == true) //��������� ������
        {
            _resictionary.Clear();
            _resictionary = EventsResources.onGetChairDictionary?.Invoke(_lvResObjUp);
        }
        if (_kitchenActiv == true) //��������� �����
        {
            _resictionary.Clear();
            _resictionary = EventsResources.onGetTableDictionary?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo2Activ == true) //��������� �������� �� 2 ����
        {
            _resictionary.Clear();
            //  _resictionary = EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_bedActiv == true) //��������� �������
        {
            _resictionary.Clear();
            //  _resictionary = EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo3Activ == true) //��������� �������� �� 3 ����
        {
            _resictionary.Clear();
            //  _resictionary = EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_cupboardActiv == true) //��������� ���� � ������
        {
            _resictionary.Clear();
            //  _resictionary = EventsResources. ?.Invoke(_lvResObjUp);
        }



        return _resictionary;

    }



    /// <summary>
    /// �������� �� ������� ������ �������� � �����
    /// </summary>
    /// <returns></returns>
    private bool _checkResourceLvUp()
    {
        bool _checkUp = false;
        bool _resUp = true;

        var dictionary = _resourceDictionary();

        /// ������ ������ �� ������ ������� (�������!!!)
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


        if (stone_1lv > 0) // ������ 1 ��
        {
            var currentStone = EventsResources.onGetCurentStone?.Invoke(1);
            if (stone_1lv <= currentStone)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("������ 1 �� " + stone_1lv + "<= " + currentStone);

        }
        if (log_1lv > 0) // ������ 1��
        {
            var currentLog = EventsResources.onGetCurentLog?.Invoke(1);
            if (log_1lv <= currentLog)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("������ 1 �� " + log_1lv + "<= " + currentLog);

        }
        if (neil_1lv > 0) // ������ 1 ��
        {
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(1);
            if (neil_1lv <= currentNeil)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("������ 1 �� " + neil_1lv + "<= " + currentNeil);

        }
        if (cloth_1lv > 0) // ����� 1 ��
        {
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(1);
            if (cloth_1lv <= currentCloth)
            { _resUp = true; }
            else
            { _resUp = false; }
            Debug.Log("����� 1 �� " + cloth_1lv + "<= " + currentCloth);

        }

        _checkUp = _resUp;

        return _checkUp;

    }



    /// <summary>
    /// ��������� �������
    /// </summary>
    private void LvUp()
    {
        _takeResourceLvUp();
        // EventsResources.onStoneInBucket?.Invoke(_lvObjectNow, _amtRequiredResourceGoLvUp, 0); // ������� ������ ��� LvUp ;
        //  _amtRequiredResourceGoLvUp = (int)(_amtRequiredResourceGoLvUp * 1.3f); // ����� �������
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
    /// ��������� ������� �� ����� ��� Lv Up
    /// </summary>
    private void _takeResourceLvUp()
    {
        // _resourceDictionary = EventsResources.onGetFireplaceDictionary(1);
        var dictionary = _resourceDictionary();

        /// ������ ������ �� ������ ������� (�������!!!)
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

        /// ������ � ����� �������
        var _lvRequiredResource = _lvObjectNow + 1;
        var currentStone = EventsResources.onGetCurentStone?.Invoke(_lvRequiredResource);
        var currentLog = EventsResources.onGetCurentLog?.Invoke(_lvRequiredResource);
        var currentNeil = EventsResources.onGetCurentNeil?.Invoke(_lvRequiredResource);
        var currentCloth = EventsResources.onGetCurentClouth?.Invoke(_lvRequiredResource);



        if (stone_1lv < currentStone && stone_1lv > 0 ) // ������  ��
        {
            EventsResources.onStoneInBucket?.Invoke(1, stone_1lv, 0);
        }
        if (log_1lv < currentLog && log_1lv > 0) // ������ ��
        {
            EventsResources.onLogInBucket?.Invoke(1, log_1lv, 0);
        }
        if (neil_1lv < currentNeil && neil_1lv > 0) // ������  ��
        {
            EventsResources.onNeilInBucket?.Invoke(1, neil_1lv, 0);
        }
        if (cloth_1lv < currentCloth && cloth_1lv > 0) // �����  ��
        {
            EventsResources.onClouthInBucket?.Invoke(1, cloth_1lv, 0);
        }
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

    /// <summary>
    /// ����� ����� ��������� �����
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



