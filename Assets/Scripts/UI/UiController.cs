using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
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

    [Header("Звук")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clickButtonClip;


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

    private const int ONE_SCENE_INDEX = 0;
    private const int TWO_SCENE_INDEX = 1;


    private void Start()
    {
        StartResourcesText(); // пока без сохранения

        _menuPanel.SetActive(false);
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

    }


    public void ClickMeargSceneButton()
    {
        SaveResources();
        if (SceneManager.GetActiveScene().buildIndex != ONE_SCENE_INDEX)
        {
            SceneManager.LoadScene(ONE_SCENE_INDEX);
        }
    }

    public void ClickHomeSceneButton()
    {
        SaveResources();
        if (SceneManager.GetActiveScene().buildIndex != TWO_SCENE_INDEX)
        {
            SceneManager.LoadScene(TWO_SCENE_INDEX);
        }
    }

    public void ClickMenuButton()
    {
        // Возможно стоит ставить паузу
        _menuPanel.SetActive(true);
    }

    
    public void ClickBackButtonInMenuPanel()
    {
        _menuPanel.SetActive(false);
    }

    public void ClickResoucesButton()
    {
        // Возможно стоит ставить паузу
        _resorcesPanel.SetActive(true);
        ReloadResourcesText();
    }

    public void ClickBackButtonResoucesPanel()
    {
        _resorcesPanel.SetActive(false);
    }
    
    public void ClickButtonsSoundClic()
    {
        _source.PlayOneShot(_clickButtonClip);
    }

    private void SaveResources()
    {
        PlayerPrefs.SetInt("currentCloath1", _currentCloath_1);
        PlayerPrefs.SetInt("currentCloath2", _currentCloath_2);
        PlayerPrefs.SetInt("currentCloath3", _currentCloath_3);

        PlayerPrefs.SetInt("currentLog1", _currentLog_1);
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
}
