using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [Header("Загружать сохранения или стартовые значения ресурсов")]
    [SerializeField] private bool loadResorces = true;

    [Header("Стартовые настройки ресурсов")]
    [SerializeField] private int _startCloath_1;
    [SerializeField] private int _startCloath_2;
    [SerializeField] private int _startCloath_3;

    [SerializeField] private int _startLog_1;
    [SerializeField] private int _startLog_2;
    [SerializeField] private int _startLog_3;

    [SerializeField] private int _startNeil_1;
    [SerializeField] private int _startNeil_2;
    [SerializeField] private int _startNeil_3;

    [SerializeField] private int _startStone_1;
    [SerializeField] private int _startStone_2;
    [SerializeField] private int _startStone_3;

    [SerializeField] private int _startPrestige;

    [Header("Текстовые поля для отображения ресурсов")]
    [SerializeField] private Text _cloath_0_Text;
    [SerializeField] private Text _cloath_1_Text;
    [SerializeField] private Text _cloath_2_Text;

    [SerializeField] private Text _log_0_Text;
    [SerializeField] private Text _log_1_Text;
    [SerializeField] private Text _log_2_Text;
    
    [SerializeField] private Text _neil_0_Text;
    [SerializeField] private Text _neil_1_Text;
    [SerializeField] private Text _neil_2_Text;
    
    [SerializeField] private Text _stone_0_Text;
    [SerializeField] private Text _stone_1_Text;
    [SerializeField] private Text _stone_2_Text;

    [SerializeField] private Text _prestigeText;

    [Header("Панели")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _resorcesPanel;

    //[Header("Звук")]
    //[SerializeField] private AudioSource _source;
    //[SerializeField] private AudioClip _clickButtonClip;

    [Header("Таймеры для заморозки корзины")]
    [SerializeField] private float _holdTime = 10;
    [SerializeField] private float _noHoldTime = 5;

    [Header("Текст о Заморзке")]
    [SerializeField] private Text _infoText;
    [SerializeField] private Text _clockHoldText;


    private const string HOLD_BUCKET_TEXT = "Корзина заморожена пауком";
    private const string NO_HOLD_BUCKET_TEXT = "Корзина разможенна";

    private int _currentCloath_1;
    private int _currentCloath_2;
    private int _currentCloath_3;

    private int _currentLog_1;
    private int _currentLog_2;
    private int _currentLog_3;

    private int _currentNeil_1;
    private int _currentNeil_2;
    private int _currentNeil_3;

    private int _currentStone_1;
    private int _currentStone_2;
    private int _currentStone_3;

    private int _currentPrestige;

    private bool _holdBucket;
    private float _currentHoldTime;
    private float _currentNoHoldTime;

    private void Start()
    {

        if (!loadResorces)
        {
            StartResourcesText();
        }
        else
        {
            LoadResouces();
    }

    _menuPanel.SetActive(false);
        _resorcesPanel.SetActive(false);

        _currentHoldTime = _holdTime;
        _currentNoHoldTime = _noHoldTime;
    }

    private void Update()
    {
        HoldTimer();
    }

    private void OnEnable()
    {
        EventsResources.onLogInBucket += ReloadLogText;
        EventsResources.onNeilInBucket += ReloadNeilText;
        EventsResources.onClouthInBucket += ReloadClouthText;
        EventsResources.onStoneInBucket += ReloadStoneText;

        EventsResources.onGetCurentClouth += GetCurrentCountClouth;
        EventsResources.onGetCurentLog += GetCurrentCountLog;
        EventsResources.onGetCurentNeil += GetCurrentCountNeil;
        EventsResources.onGetCurentStone += GetCurrentCountStone;

        ButtonsEvents.onSaveResouces += SaveResources;
        ButtonsEvents.onReloadResources += ReloadResourcesText;
        ButtonsEvents.onStartResourcesText += StartResourcesText;
    }

    private void OnDisable()
    {
        EventsResources.onLogInBucket -= ReloadLogText;
        EventsResources.onNeilInBucket -= ReloadNeilText;
        EventsResources.onClouthInBucket -= ReloadClouthText;
        EventsResources.onStoneInBucket -= ReloadStoneText;

        EventsResources.onGetCurentClouth -= GetCurrentCountClouth;
        EventsResources.onGetCurentLog -= GetCurrentCountLog;
        EventsResources.onGetCurentNeil -= GetCurrentCountNeil;
        EventsResources.onGetCurentStone -= GetCurrentCountStone;

        ButtonsEvents.onSaveResouces -= SaveResources;
        ButtonsEvents.onReloadResources -= ReloadResourcesText;
        ButtonsEvents.onStartResourcesText -= StartResourcesText;
    }


   
    private void SaveResources()
    {
        PlayerPrefs.SetInt("currentCloath1", _currentCloath_1);
        PlayerPrefs.SetInt("currentCloath2", _currentCloath_2);
        PlayerPrefs.SetInt("currentCloath3", _currentCloath_3);

        PlayerPrefs.SetInt("currentLog1", _currentLog_1);
        PlayerPrefs.SetInt("currentLog2", _currentLog_2);
        PlayerPrefs.SetInt("currentLog3", _currentLog_3);

        PlayerPrefs.SetInt("currentNeil1", _currentNeil_1);
        PlayerPrefs.SetInt("currentNeil2", _currentNeil_2);
        PlayerPrefs.SetInt("currentNeil3", _currentNeil_3);

        PlayerPrefs.SetInt("currentStone1", _currentStone_1);
        PlayerPrefs.SetInt("currentStone2", _currentStone_2);
        PlayerPrefs.SetInt("currentStone3", _currentStone_3);

        PlayerPrefs.Save();
    }

    private void LoadResouces()
    {
        if (loadResorces)
        {
            if (PlayerPrefs.HasKey("currentCloath1"))
            {
                _currentCloath_1 = PlayerPrefs.GetInt("currentCloath1");
            }

            if (PlayerPrefs.HasKey("currentCloath2"))
            {
                _currentCloath_2 = PlayerPrefs.GetInt("currentCloath2");
            }

            if (PlayerPrefs.HasKey("currentCloath3"))
            {
                _currentCloath_3 = PlayerPrefs.GetInt("currentCloath3");
            }

            //------
            
            if (PlayerPrefs.HasKey("currentLog1"))
            {
                _currentLog_1 = PlayerPrefs.GetInt("currentLog1");
            }

            if (PlayerPrefs.HasKey("currentLog2"))
            {
                _currentLog_2 = PlayerPrefs.GetInt("currentLog2");
            }

            if (PlayerPrefs.HasKey("currentLog3"))
            {
                _currentLog_3 = PlayerPrefs.GetInt("currentLog3");
            }

            //-------
            
            if (PlayerPrefs.HasKey("currentNeil1"))
            {
                _currentNeil_1 = PlayerPrefs.GetInt("currentNeil1");
            }
            
            if (PlayerPrefs.HasKey("currentNeil2"))
            {
                _currentNeil_2 = PlayerPrefs.GetInt("currentNeil2");
            }
            
            if (PlayerPrefs.HasKey("currentNeil3"))
            {
                _currentNeil_3 = PlayerPrefs.GetInt("currentNeil3");
            }

            //-------
            
            if (PlayerPrefs.HasKey("currentStone1"))
            {
                _currentStone_1 = PlayerPrefs.GetInt("currentStone1");
            }
            
            if (PlayerPrefs.HasKey("currentStone2"))
            {
                _currentStone_2 = PlayerPrefs.GetInt("currentStone2");
            }
            
            if (PlayerPrefs.HasKey("currentStone3"))
            {
                _currentStone_3 = PlayerPrefs.GetInt("currentStone3");
            }
        }
    }

    private void StartResourcesText()
    {
        _currentCloath_1 = _startCloath_1;
        _currentCloath_2 = _startCloath_2;
        _currentCloath_3 = _startCloath_3;
        // _cloathText.text = _currentCloath.ToString();

        _currentLog_1 = _startLog_1;
        _currentLog_2 = _startLog_2;
        _currentLog_3 = _startLog_3;
        // _logText.text = _currentLog.ToString();

        _currentNeil_1 = _startNeil_1;
        _currentNeil_2 = _startNeil_2;
        _currentNeil_3 = _startNeil_3;
        // _neilText.text = _currentNeil.ToString();

        _currentStone_1 = _startStone_1;
        _currentStone_2 = _startStone_2;
        _currentStone_3 = _startStone_3;
       // _stoneText.text = _currentStone.ToString();

        _currentPrestige = _startPrestige;
        _prestigeText.text = _currentPrestige.ToString();
    }

    private void ReloadResourcesText()
    {
        //_currentCloath = _startCloath;
        _cloath_0_Text.text = _currentCloath_1.ToString();
        _cloath_1_Text.text = _currentCloath_2.ToString();
        _cloath_2_Text.text = _currentCloath_3.ToString();

        // _currentLog = _startLog;
        _log_0_Text.text = _currentLog_1.ToString();
        _log_1_Text.text = _currentLog_2.ToString();
        _log_2_Text.text = _currentLog_3.ToString();

        // _currentNeil = _startNeil;
        _neil_0_Text.text = _currentNeil_1.ToString();
        _neil_1_Text.text = _currentNeil_2.ToString();
        _neil_2_Text.text = _currentNeil_3.ToString();

        // _currentStone = _startStone;
        _stone_0_Text.text = _currentStone_1.ToString();
        _stone_1_Text.text = _currentStone_2.ToString();
        _stone_2_Text.text = _currentStone_3.ToString();

        //_currentPrestige = _startPrestige;
       // _prestigeText.text = _currentPrestige.ToString();
    }

    private void HoldTimer()
    {


        if (_currentNoHoldTime <= 0)
        {
           

            _holdBucket = true;
            EventsResources.onHoldBucket?.Invoke(_holdBucket);
            _infoText.text = HOLD_BUCKET_TEXT;

            UpdateTimerText(_currentHoldTime);
            _currentHoldTime -= Time.deltaTime;

            if (_currentHoldTime <= 0)
            {
                
                _holdBucket = false;
                EventsResources.onHoldBucket?.Invoke(_holdBucket);
                _currentNoHoldTime = _noHoldTime;
            }
        } else
        {
            _holdBucket = false;
            EventsResources.onHoldBucket?.Invoke(_holdBucket);
            _infoText.text = NO_HOLD_BUCKET_TEXT;

            UpdateTimerText(_currentNoHoldTime);
            _currentNoHoldTime -= Time.deltaTime;
            _currentHoldTime = _holdTime;
        }
    }

    private void ReloadLogText(int levelResouces, int count, int plusOrMinus)
    {
        if (levelResouces == 1)
        {
            _currentLog_1 = plusOrMinus == 1? _currentLog_1 += count : _currentLog_1 -= count;
            _log_0_Text.text = _currentLog_1.ToString();

        } else if (levelResouces == 2)
        {
            _currentLog_2 = plusOrMinus == 1 ? _currentLog_2 += count : _currentLog_2 -= count;
            _log_1_Text.text = _currentLog_2.ToString();
        
        } else if (levelResouces == 3)
        {
            _currentLog_3 = plusOrMinus == 1 ? _currentLog_3 += count : _currentLog_3 -= count;
            _log_2_Text.text = _currentLog_3.ToString();
        }
    }

    private void ReloadNeilText(int levelResouces, int count, int plusOrMinus)
    {
        if (levelResouces == 1)
        {
            _currentNeil_1 = plusOrMinus == 1 ? _currentNeil_1 += count : _currentNeil_1 -= count;
            _neil_0_Text.text = _currentNeil_1.ToString();

        } else if (levelResouces == 2)
        {
            _currentNeil_2 = plusOrMinus == 1 ? _currentNeil_2 += count : _currentNeil_2 -= count;
            _neil_1_Text.text = _currentNeil_2.ToString();

        } else if (levelResouces == 3)
        {
            _currentNeil_3 = plusOrMinus == 1 ? _currentNeil_3 += count : _currentNeil_3 -= count;
            _neil_2_Text.text = _currentNeil_3.ToString();
        }
    }

    private void ReloadClouthText(int levelResouces, int count, int plusOrMinus)
    {
        if (levelResouces == 1)
        {
            _currentCloath_1 = plusOrMinus == 1 ? _currentCloath_1 += count : _currentCloath_1 -= count;
            _cloath_0_Text.text = _currentCloath_1.ToString();

        }
        else if (levelResouces == 2)
        {
            _currentCloath_2 = plusOrMinus == 1 ? _currentCloath_2 += count : _currentCloath_2 -= count;
            _cloath_1_Text.text = _currentCloath_2.ToString();

        }
        else if (levelResouces == 3)
        {
            _currentCloath_3 = plusOrMinus == 1 ? _currentCloath_3 += count : _currentCloath_3 -= count;
            _cloath_2_Text.text = _currentCloath_3.ToString();
        }
    }

    private void ReloadStoneText(int levelResouces, int count, int plusOrMinus)
    {
        if (levelResouces == 1)
        {
            _currentStone_1 = plusOrMinus == 1 ? _currentStone_1 += count : _currentStone_1 -= count;
            _stone_0_Text.text = _currentStone_1.ToString();

        }
        else if (levelResouces == 2)
        {
            _currentStone_2 = plusOrMinus == 1 ? _currentStone_2 += count : _currentStone_2 -= count;
            _stone_1_Text.text = _currentStone_2.ToString();

        }
        else if (levelResouces == 3)
        {
            _currentStone_3 = plusOrMinus == 1 ? _currentStone_3 += count : _currentStone_3 -= count;
            _stone_2_Text.text = _currentStone_3.ToString();
        }
    }

    private int GetCurrentCountLog(int levelResorces) {
        if (levelResorces == 3)
        {
            return _currentLog_3;
        
        } else if (levelResorces == 2)
        {
            return _currentLog_2;
        }

        return _currentLog_1;
    }

    private int GetCurrentCountNeil(int levelResorces)
    {
        if (levelResorces == 3)
        {
            return _currentNeil_3;

        }
        else if (levelResorces == 2)
        {
            return _currentNeil_2;
        }

        return _currentNeil_1;
    }

    private int GetCurrentCountClouth(int levelResorces)
    {
        if (levelResorces == 3)
        {
            return _currentCloath_3;

        }
        else if (levelResorces == 2)
        {
            return _currentCloath_2;
        }

        return _currentCloath_1;
    }

    private int GetCurrentCountStone(int levelResorces)
    {
        if (levelResorces == 3)
        {
            return _currentStone_3;

        }
        else if (levelResorces == 2)
        {
            return _currentStone_2;
        }

        return _currentStone_1;
    }

    private void UpdateTimerText(float time)
    {
        if (time < 0)
        {
            time = 0;
        }

        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);
        _clockHoldText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }
}
