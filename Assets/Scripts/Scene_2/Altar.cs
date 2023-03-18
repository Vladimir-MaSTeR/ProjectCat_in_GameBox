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

    [Tooltip("������ ������� �� ������� �� 0 -������� �� n- ��������")]
    [SerializeField]    private GameObject[] _objectModel;
    [Tooltip("������ ������� ������")]
    [SerializeField]    private GameObject _objectNow;

    [Tooltip("��������� ������� ������� (0 = 1)")]
    [SerializeField]     private int _lvObject = 0;
    [Tooltip("������������ ������� ������� ")]
    private int _lvObjectMax;
    [Tooltip("������� ������� ������� ")]
    private int _lvObjectNow;

    [Tooltip("����� ��� ��������� ���������  (��� - ������)")]
    [SerializeField]    private GameObject _scaleProgress;
    [Tooltip(" ����� ��� ��������� ����������")]
    [SerializeField]    private GameObject _scaleProgressUp;
    [Tooltip("������ (������� ���������� �����) �����������")]
    [SerializeField] 
    private bool _activTimeGoLvUp = false;

    [Tooltip(" ����� ��� ���������� ����� ���� ��������� (������ ����� � ������)")]
    [SerializeField]    private bool _progressActivat;
    [Tooltip("����� ��� ���������� ����� �������� ����� �����. (������ ����� ����� � ������ �����:)")]
    [SerializeField]     private float _progressActivatTime;

    [Tooltip("���������� ����������� ������ ��� ���������� (�����)")]
    [SerializeField]    private float _amtClickGoLvUp = 10;
    [Tooltip("���������� ������������� ������ ��� (������� ���������� �����) ")]
    private float _amtAddResource = 0;

    [Tooltip("������� ��������� �������")]
    [SerializeField]    private float _healthNow = 10;
    [Tooltip("������������ ��������� �������")]  
    private float _healthMax = 10;
    [Tooltip("�������� �������")]
    private bool _AtackSpider = false;




    [Tooltip("�������� ����� ��� ���������� ������� ")]
    [SerializeField]    private string _textNameObject; // = "lvFireplace";

    [Tooltip("�������� ��� ����� �� ������ ")]
    [SerializeField]    private Animation _animClick;
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

        int typeStatus = 0;
        if (_AtackSpider == false)
        { typeStatus = 1; }
        if (_AtackSpider == true)
        { typeStatus = 2; }
        onStatusSpiderHome?.Invoke(_textNameObject, typeStatus);

    }

    // Update is called once per frame
    void Update()
    {
        if (_progressActivat == false)
        {
            _checkAltarActivate();

        }

      //  Debug.Log("������ " + _progressActivat + " ����� " + _typeAltar);

    }


    [Tooltip("�������� �� ���������� ������ (���������� ����� ����� �������� ����� �� ������ ���)")]
    public static Func<int, float> onCheckAltarActivate;


    [Tooltip("�������� ���� (int ������� ������,int ��� ������)")]
    public static Action<int, int> onAddRunse;

    [Tooltip("������ ������� � ���� ")]
    public static Action<string, int> onStatusSpiderHome;

    private void OnEnable()
    {
        AltarTimer.onTimerAltarOff += _altarActivate;
    }

    private void OnDisable()
    {
        AltarTimer.onTimerAltarOff -= _altarActivate;

    }



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
           // Debug.Log("���� !! " + _amtAddResource);


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
        // Debug.Log(onCheckAltarActivate?.Invoke(_typeAltar));
        if (onCheckAltarActivate?.Invoke(_typeAltar) > 0)
        { 
            _progressActivat = false;
        }
        else
        {
            _progressActivat = true;
        }

    }


    /// <summary>
    /// �������� ������ ��� ����������
    /// </summary>
    /// <param name="typeAltar"> �����(���) ������</param>
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
                _amtAddResource = _amtAddResource - 0.1f;
            }
            else if (_amtClickGoLvUp * 2f / 3f <= _amtAddResource)
            {
                _amtAddResource = _amtAddResource - 0.033f;
            }
            else
            {
                _amtAddResource = _amtAddResource - 0.066f;
            }
            Invoke("_timeScaleOff", 0.1f);
            ScaleProgress(true);
        }
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
                    {_AtackSpider = true;}
            }
        }
    }

    #endregion


}
