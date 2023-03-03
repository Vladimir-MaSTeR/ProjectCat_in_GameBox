using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ClickRepair : MonoBehaviour, IPointerClickHandler
{
    [Header("��������� ���������� ��� ��������� �������� ��������")]
    [SerializeField] private bool loadResorces = true;

    [Tooltip("������ ������� �� ������� �� 0 -������� �� n- ��������")]
    [SerializeField] private GameObject[] _objectModel;
    [Tooltip("������ ������� ������")]
    [SerializeField] private GameObject _objectNow;

    [Tooltip("��������� ������� �������")]
    [SerializeField] private int _lvObject = 0;
    [Tooltip(" ������������ ������� ������� ")]
    private int _lvObjectMax;
    [Tooltip("������� ������� ������� ")]
    private int _lvObjectNow;

    [Tooltip("����� ��� ������� ���������  (��� - ������)")] 
    [SerializeField] private GameObject _scaleProgress;
    [Tooltip(" ����� ��� ������� ���������� (���������)")]     
    [SerializeField] private GameObject _scaleProgressUp;
    [Tooltip("������ (������� ���������� �����) �����������")]
    [SerializeField] 
    private bool _activTimeGoLvUp = false;

    [Tooltip("���������� ����������� ������ ��� ���������� (Lv Up) (�����)")]
    [SerializeField] private float _amtClickGoLvUp = 10;
    [Tooltip("���������� ������������� ������ ��� Lv Up (������� ���������� �����) ")]
    private float _amtAddResource = 0;

    [Tooltip("������� ��������� �������")]
    [SerializeField] private float _healthNow = 10;
    [Tooltip("������������ ��������� �������")]
    private float _healthMax = 10;
    [Tooltip("�������� �������")]
    private bool _AtackSpider = false;

    [Tooltip("������� ��� �������")]
    [SerializeField]
    private float _comfortNow = 10;
    [Tooltip("������������ ��� �������")]
    private float _comfortMax = 10;


    

   [Tooltip("�������� ����� ��� ���������� ������� ")]
    [SerializeField]
    private string _textNameObject ; // = "lvFireplace";


    [Tooltip("�������� ��� ����� �� ������ ")]
    [SerializeField]    private Animation _animClick;
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

        if (_checkResourceLvUp() == true) /// true; �������� ��������
        {
            Invoke("_animReadyUp", 7);
        }

        int typeStatus =0;
            if (_AtackSpider == false)
        { typeStatus = 1; }
        if (_AtackSpider == true)
        { typeStatus = 2; }
        onStatusSpiderHome?.Invoke(_textNameObject, typeStatus);
    }

    /// <summary>
    /// ���������� ������ ��� ��������� �� ����. �������
    /// </summary>

    private float _clickGoLvUp()
    {
        float _amtClickGoLvUp = this._amtClickGoLvUp;
        return _amtClickGoLvUp;
    }

    #region  ������ ��� �������� ������ ( �������, ���, )
    /// <summary>
    /// ������� ������� 
    /// </summary>
    public int LvObj
    {        get
        {return _lvObjectNow;}
    }
    /// <summary>
    ///  ��� ���������� �������
    /// </summary>
    public string NameObject
    {
        get { return _textNameObject; }
    }
    #endregion

    [Tooltip("������ ������� � ����")]
    public static Action<string, int> onStatusSpiderHome;


    private void OnEnable()
    {
        if (_fireplaceActiv == true) //"��������� ��������  �� ��������  ������
        {
            EventsResources.onAnimationReadyUpFireplace += _animReadyUp;
        }
        if (_armchairActiv == true) //��������� ��������  �� ��������  ������
        {
            EventsResources.onAnimationReadyUpChair += _animReadyUp;
        }
        if (_kitchenActiv == true) //��������� ��������  �� ��������  �����
        {
            EventsResources.onAnimationReadyUpTable += _animReadyUp;
        }
        if (_ladderGoTo2Activ == true) //��������� ��������  �� ��������  �������� �� 2 ����
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_bedActiv == true) //��������� ��������  �� ��������  �������
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_ladderGoTo3Activ == true) //��������� ��������  �� ��������  �������� �� 3 ����
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_cupboardActiv == true) //��������� ��������  �� ��������  ���� � ������
        {
            // EventsResources.onAnimationReadyUp
        }



    }

    private void OnDisable()
    {
        if (_fireplaceActiv == true) //"��������� ��������  �� ��������  ������
        {
            EventsResources.onAnimationReadyUpFireplace -= _animReadyUp;
        }
        if (_armchairActiv == true) //��������� ��������  �� ��������  ������
        {
            EventsResources.onAnimationReadyUpChair -= _animReadyUp;
        }
        if (_kitchenActiv == true) //��������� ��������  �� ��������  �����
        {
            EventsResources.onAnimationReadyUpTable -= _animReadyUp;
        }
        if (_ladderGoTo2Activ == true) //��������� ��������  �� ��������  �������� �� 2 ����
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_bedActiv == true) //��������� ��������  �� ��������  �������
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_ladderGoTo3Activ == true) //��������� ��������  �� ��������  �������� �� 3 ����
        {
            // EventsResources.onAnimationReadyUp
        }
        if (_cupboardActiv == true) //��������� ��������  �� ��������  ���� � ������
        {
            // EventsResources.onAnimationReadyUp
        }


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
                _sounds�lick();

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
            if (_lvObjectNow != _lvObjectMax)
            {
                _animClick.Play("AnimationError");
                SourceHome.onSounds�lickFalseRepair?.Invoke();
            }
        }


    }

    #region ��������� � ���



    /// <summary>
    /// ������� ��� � ����������� �� ���������� ���������
    /// </summary>
    /// <returns></returns>
    private float _checkComfort()
    {
        _comfortNow = _comfortMax * (_healthNow / _healthMax);

        return _comfortNow;
    }


    /// <summary>
    /// ���������� ��������� � ������� �� ������
    /// </summary>
    private void _lvUpComfort(int _lvlObjNow)
    {
        float _healthMaxWas = _healthMax;

        _healthMax += 15;
        _healthNow = _healthNow + (_healthMax - _healthMaxWas);

        /// + ���
        {
            if (_fireplaceActiv == true) //  ����� ���� ���
            {
                switch (_lvlObjNow)
                {
                    case 1: _comfortMax = 80; break;
                    case 2: _comfortMax += 100; break;
                    case 3: _comfortMax += 120; break;

                    default:
                        break;
                }
            }
            if (_armchairActiv == true) //  ������ ���� ���
            {
                switch (_lvlObjNow)
                {
                    case 1: _comfortMax = 40; break;
                    case 2: _comfortMax += 100; break;
                    case 3: _comfortMax += 120; break;

                    default:
                        break;
                }
            }
            if (_kitchenActiv == true) //  ����� ���� ���
            {
                switch (_lvlObjNow)
                {
                    case 1: _comfortMax = 60 + 20; break; ///� ��� + �����
                    case 2: _comfortMax += 100; break;
                    case 3: _comfortMax += 120; break;

                    default:
                        break;
                }
            }
            if (_ladderGoTo2Activ == true) //  �������� �� 2 ���� ���� ���
            {
            }
            if (_bedActiv == true) //  �������  ���� ���
            {
            }
            if (_ladderGoTo3Activ == true) //  �������� �� 3 ���� ���� ���
            {
            }
            if (_cupboardActiv == true) //  ���� � ������ ���� ���
            {
            }
        }




    }
#endregion




    #region ������� (�����) [��������� - ���������� -  ��������]
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
       // Debug.Log(_lvResObjUp);

    }

    /// <summary>
    /// ���������� ������
    /// </summary>
    private void _completedQuests()
    {
        int _lvResObjUp = _lvObjectNow + 1;
       // Debug.Log("completed "+ _lvResObjUp);
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
        int _lvResObjUp = _lvObjectNow + 1;

        IDictionary<string, int> _resictionary = EventsResources.onGetFireplaceDictionary?.Invoke(_lvResObjUp);
        // ������ � ����� ������� (���������)

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
    #endregion

    #region �������� � ����
    /// <summary>
    /// �������� ���������� � ��������� ������  
    /// </summary>
    private void _animReadyUp()
    {
        if (_lvObjectNow != _lvObjectMax)
        {
            _animClick.Play("AnimationReadyUp");
            if (_checkResourceLvUp() == true) /// true; �������� ��������
            {
                float pTime = Random.Range(2f, 4f);
                Invoke("_animReadyUp", pTime);
            }

            // Debug.Log(_lvResObjUp);
        }
    }

    /// <summary>
    /// ���� ��� ������� �� ������
    /// </summary>
    private void _sounds�lick()
    {
        if (_fireplaceActiv == true) //���� ��� ������� �� �����
        {
            SourceHome.onSounds�lickFireplace?.Invoke();
        }
        if (_armchairActiv == true) // ���� ��� ������� �� ������
        {
            SourceHome.onSounds�lickRepairObj?.Invoke();
        }
        if (_kitchenActiv == true) //���� ��� ������� ��  �����
        {
            SourceHome.onSounds�lickRepairObj?.Invoke();
        }
        if (_ladderGoTo2Activ == true) //���� ��� ������� ��  �������� �� 2 ����
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_bedActiv == true) //���� ��� ������� ��  �������
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_ladderGoTo3Activ == true) //���� ��� ������� ��  �������� �� 3 ����
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }
        if (_cupboardActiv == true) //���� ��� ������� ��  ���� � ������
        {
            //  EventsResources. ?.Invoke(_lvResObjUp);
        }

    }
    #endregion

    #region ��������� ������ [�������� ��������- ��������� - ������� - ����� ������]
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

        int _lvResObjUp = _lvObjectNow + 1;

        if (stone_1lv > 0) // ������ 1 ��
        {
            var currentStone = EventsResources.onGetCurentStone?.Invoke(_lvResObjUp);
            if (stone_1lv <= currentStone)
            { _resUp = true; }
            else
            { _resUp = false;
                return _checkUp;
            }
            Debug.Log("����������� ����  " + stone_1lv + "<= " + currentStone + " ��- " + _lvResObjUp); // ������

        }
        if (log_1lv > 0) // ������ 1��
        {
            var currentLog = EventsResources.onGetCurentLog?.Invoke(_lvResObjUp);
            if (log_1lv <= currentLog)
            { _resUp = true; }
            else
            { _resUp = false;
                return _checkUp;
            }
            Debug.Log("������������� ����   " + log_1lv + "<= " + currentLog + " ��- " + _lvResObjUp); // ������

        }
        if (neil_1lv > 0) // ������ 1 ��
        {
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(_lvResObjUp);
            if (neil_1lv <= currentNeil)
            { _resUp = true; }
            else
            { _resUp = false;
                return _checkUp;
            }
            Debug.Log("���������� ����     " + neil_1lv + "<= " + currentNeil + " ��- " + _lvResObjUp); //������

        }
        if (cloth_1lv > 0) // ����� 1 ��
        {
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(_lvResObjUp);
            if (cloth_1lv <= currentCloth)
            { _resUp = true; }
            else
            { _resUp = false;
                return _checkUp;
            }
            Debug.Log("������� ����   " + cloth_1lv + "<= " + currentCloth + " ��- " + _lvResObjUp); //�����

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
        _lvUpComfort(_lvObjectNow);
        SourceHome.onSounds�lickLvUpObj?.Invoke();
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
            EventsResources.onStoneInBucket?.Invoke(_lvRequiredResource, stone_1lv, 0);
            Debug.Log("stone lv " + _lvRequiredResource + " Kol-vo " + stone_1lv);
        }
        if (log_1lv < currentLog && log_1lv > 0) // ������ ��
        {
            EventsResources.onLogInBucket?.Invoke(_lvRequiredResource, log_1lv, 0);
            Debug.Log("log lv " + _lvRequiredResource + " Kol-vo " + log_1lv);

        }
        if (neil_1lv < currentNeil && neil_1lv > 0) // ������  ��
        {
            EventsResources.onNeilInBucket?.Invoke(_lvRequiredResource, neil_1lv, 0);
            Debug.Log("neil lv " + _lvRequiredResource + " Kol-vo " + neil_1lv);

        }
        if (cloth_1lv < currentCloth && cloth_1lv > 0) // �����  ��
        {
            EventsResources.onClouthInBucket?.Invoke(_lvRequiredResource, cloth_1lv, 0);
            Debug.Log("cloth lv " + _lvRequiredResource + " Kol-vo " + cloth_1lv);

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
    #endregion

    #region ����� ��������� + �� ����� �����
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
            { _scaleProgressUp.GetComponent<UnityEngine.UI.Image>().fillAmount = _progressResource; }
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
                { _AtackSpider = true; }
            }
        }
    }
    
    #endregion

}



