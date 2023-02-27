using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class Altar : MonoBehaviour, IPointerClickHandler
{
    [Header("��������� ���������� ��� ��������� �������� ��������")]
    [SerializeField] private bool loadResorces = true;
    /// <summary>
    /// ������ ������� �� ������� �� 0 - ����������� �� n- ��������
    /// </summary>
    [SerializeField]
    private GameObject[] _objectModel;
    /// <summary>
    /// ������ ������� ������
    /// </summary>
    [SerializeField]
    private GameObject _objectNow;
    /// <summary>
    /// ��������� ������� ������� (0 = 1) 
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
    ///      ����� ��� ���������� ����� ���� ��������� 
    /// </summary>
    [SerializeField]
    private bool _progressActivat;
    /// <summary>
    ///      ����� ��� ���������� ����� �������� �����  �����.
    /// </summary>
    [SerializeField]
     private float _progressActivatTime;

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
    /// ������� ��������� �������
    /// </summary>
    [SerializeField]
    private float _healthNow = 10;
    /// <summary>
    /// ������������ ��������� �������
    /// </summary>
    // [SerializeField]
    private float _healthMax = 10;


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
    private string _textLvObject; // = "lvFireplace";
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
    [Tooltip("��������� 1 ������ ������� ����")]
    [SerializeField] private bool _altar_1;
    [Tooltip("��������� 2 ������ ���������� ����")]
    [SerializeField] private bool _altar_2;
    [Tooltip("��������� 3 ������ ������������� ����")]
    [SerializeField] private bool _altar_3;
    [Tooltip("��������� 4 ������ ����������� ����")]
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


        /// ���������� ��� ������
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
    /// �������� �� ���������� ������ (���������� ����� ����� �������� ����� �� ������ ���)
    /// </summary>
    public static Func<int,float> onCheckAltarActivate;



    /// <summary>
    /// �������� ���� (int ������� ������,int ��� ������)
    /// </summary>
    public static Action <int,int> onAddRunse;



        /// <summary>
        /// ������� �� ������
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
            Debug.Log("���� !! " + _amtAddResource);


        }
    }

    /// <summary>
    /// ���������� ���������� ������, ���������� ������ ����
    /// </summary>
    private void _addRunes()
    {
        _progressActivat = false;
        onAddRunse?.Invoke(_lvObjectNow,_typeAltar );  //(int Lv, int number);


    }

    /// <summary>
    /// �������� �� ���������� (���� � ������ ������ ��� ��� ���� ��� �� ������ �� �������)
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




    #region lv obj ��������� ������� - ������

    /// <summary>
    /// ��������� �������
    /// </summary>
    private void LvUp()
    {
       /// ������� �� ���������  _takeResourceLvUp();
       /// ���������� ������������ ��������� _completedQuests();
       /// ���������� ������  _lvObjectNow += 1;

       // SourceHome.onSounds�lickLvUpObj?.Invoke();
       /// ������ ������  AddModel(_lvObjectNow);

        SaveResources();

    }

    /// <summary>
    /// ��������� ������� �� ����� ��� Lv Up
    /// </summary>
    private void _takeResourceLvUp()
    {

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

    #endregion

    #region Scale ����� ���������

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
