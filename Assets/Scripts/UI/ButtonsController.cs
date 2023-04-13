using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [Header("Панели")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _resorcesPanel;
    [SerializeField] private GameObject _questsPanel;
    [SerializeField] private GameObject _mainLongTextPanel;
    [SerializeField] private GameObject _secondLongTextPanel;
    [SerializeField] private GameObject _turnHomeButtonsPanel;
    [SerializeField] private GameObject _sparkSopPanel;

    [SerializeField] private GameObject _theGameTextPanel;
    [SerializeField] private GameObject _autorsTextPanel;

    [Header("Алтари")]

    [Tooltip("Первый алиарь")]
    [SerializeField] private GameObject _altar_1;

    [Tooltip("Второй алиарь")]
    [SerializeField] private GameObject _altar_2;

    [Header("Текстовый поля")]
    [SerializeField] private Text _mainLongText;
    [SerializeField] private Text _secondLongText;

    [SerializeField] private Text _questShortText;

    [SerializeField] private Text _fireplaceQuestTextButton;
    [SerializeField] private Text _chairQuestTextButton;
    [SerializeField] private Text _tableQuestTextButton;

    [Header("Картинки для отслеживания заданий")]
    [SerializeField] private GameObject _questCheckImage_0;
    [SerializeField] private GameObject _questCheckImage_1;
    [SerializeField] private GameObject _questCheckImage_2;

    [Header("Кнопки активирования квестов")]
    [SerializeField] private Button _fireplaceQuestCheckButton;
    [SerializeField] private Button _chairQuestCheckButton;
    [SerializeField] private Button _tableQuestCheckButton;

    [Header("Звук")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clickButtonClip;

    [Header("Картинки для кнопки смены сцен")]
    [SerializeField]
    private Image _imageButtonScene;
    [SerializeField]
    private Sprite _iconInButtonMeargScene;
    [SerializeField]
    private Sprite _iconInButtonHomeScene;

    private int _currentQuest = -1; // переменная для отслеживания активного квеста. Начинается с ноля. 

    private const int INDEX_QUEST_NO_QUEST = -1;
    private const int INDEX_QUEST_0 = 0;
    private const int INDEX_QUEST_1 = 1;
    private const int INDEX_QUEST_2 = 2;


    private IDictionary<string, int> _fireplaceDictionary_1lv;
    private IDictionary<string, int> _chairDictionary_1lv;
    private IDictionary<string, int> _tableDictionary_1lv;

    private IDictionary<string, int> _fireplaceDictionary_2lv;
    private IDictionary<string, int> _chairDictionary_2lv;
    private IDictionary<string, int> _tableDictionary_2lv;

    private IDictionary<string, int> _fireplaceDictionary_3lv;
    private IDictionary<string, int> _chairDictionary_3lv;
    private IDictionary<string, int> _tableDictionary_3lv;

    private int _currentFireplaceLevel = 0;
    private int _currentChairLevel = 0;
    private int _currentTableLevel = 0;

    private bool _fireplaceQuestCopmlete = false;
    private bool _chairQuestCopmlete = false;
    private bool _tableQuestCopmlete = false;

    private Sprite _currentIconInButtonScene;

   


    private void Start()
    {
        _menuPanel.SetActive(false);
        _resorcesPanel.SetActive(false);
        _questsPanel.SetActive(false);
        _mainLongTextPanel.SetActive(false);
        _secondLongTextPanel.SetActive(false);
        _sparkSopPanel.SetActive(false);

        _theGameTextPanel.SetActive(false);
        _autorsTextPanel.SetActive(false);

        _altar_1.SetActive(true);
        _altar_2.SetActive(true);

       // CheckSceneForTornHomeButtonsActive();

        CheckStartCraftResouces();
        ReloadCurrentQuest();

        ReloadsaveQuestComplete();
        ReloadSaveCurrentLEvelObjectInHome();
        UpdateShortQuestText();

        if (_fireplaceQuestCopmlete == true)
        {
            CompleteFireplaceQuest();
            UpdateShortQuestText();
        }

        if (_chairQuestCopmlete == true)
        {
            CompleteChairQuest();
            UpdateShortQuestText();
        }

        if (_tableQuestCopmlete == true)
        {
            CompleteTableQuest();
            UpdateShortQuestText();
        }

        IconInButtonSceneToStart();



    }

    private void Update()
    {

        if (_fireplaceQuestCopmlete)
        {
            //ущё нужно менять текст главного квеста.
            _currentFireplaceLevel++;
            _fireplaceQuestCopmlete = false;
        }

        if (_chairQuestCopmlete)
        {
            //ущё нужно менять текст главного квеста.
            _currentChairLevel++;
            _chairQuestCopmlete = false;
        }

        if (_tableQuestCopmlete)
        {
            //ущё нужно менять текст главного квеста.
            _currentTableLevel++;
            _tableQuestCopmlete = false;
        }

        UpdateShortQuestText();
    }


    private void OnEnable()
    {
        EventsResources.onUpdateQuest += UpdateShortQuestText;

        EventsResources.onEndFireplaceQuest += CompleteFireplaceQuest;
        EventsResources.onEndChairQuest += CompleteChairQuest;
        EventsResources.onEndTableQuest += CompleteTableQuest;

        EventsResources.onFireplaceQuest += ClickFireplace;
        EventsResources.onChairQuest += ClickChair;      
        EventsResources.onTableQuest += ClickTable;
       
    }

    private void OnDisable()
    {
        EventsResources.onUpdateQuest -= UpdateShortQuestText;

        EventsResources.onEndFireplaceQuest -= CompleteFireplaceQuest;
        EventsResources.onEndChairQuest -= CompleteChairQuest;
        EventsResources.onEndTableQuest -= CompleteTableQuest;

        EventsResources.onFireplaceQuest -= ClickFireplace;
        EventsResources.onChairQuest -= ClickChair;
        EventsResources.onTableQuest -= ClickTable;
    }


    private void SaveCurrentLEvelObjectInHome ()
    {
        PlayerPrefs.SetInt("currentFireplaceLevel", _currentFireplaceLevel);
        PlayerPrefs.SetInt("currentChairLevel", _currentChairLevel);
        PlayerPrefs.SetInt("currentTableLevel", _currentTableLevel);

        PlayerPrefs.Save();
    }

    private void ReloadSaveCurrentLEvelObjectInHome()
    {
        if (PlayerPrefs.HasKey("currentFireplaceLevel"))
        {
            _currentFireplaceLevel = PlayerPrefs.GetInt("currentFireplaceLevel");
        }

        if (PlayerPrefs.HasKey("currentChairLevel"))
        {
            _currentChairLevel = PlayerPrefs.GetInt("currentChairLevel");
        }

        if (PlayerPrefs.HasKey("currentTableLevel"))
        {
            _currentTableLevel = PlayerPrefs.GetInt("currentTableLevel");
        }
    }

    public void ClickMeargSceneButton()
    {
        ButtonsEvents.onSaveResouces?.Invoke();
        SaveCurrentQuest();
        SaveQuestsComplete();
        SaveCurrentLEvelObjectInHome();


        if (SceneManager.GetActiveScene().buildIndex != SceneIndexConstants.MEARG_SCENE_INDEX)
        {
            SceneManager.LoadScene(SceneIndexConstants.MEARG_SCENE_INDEX);
        }
    }

    private void IconInButtonSceneToStart() {
        if(SceneManager.GetActiveScene().buildIndex == SceneIndexConstants.MEARG_SCENE_INDEX) {
            _currentIconInButtonScene = _iconInButtonHomeScene;
            _imageButtonScene.sprite = _currentIconInButtonScene;
        } else {
            _currentIconInButtonScene = _iconInButtonMeargScene;
            _imageButtonScene.sprite = _currentIconInButtonScene;
        }
    }

    public void ClickHomeSceneButton()
    {
        ButtonsEvents.onSaveResouces?.Invoke(); // событие на сохранение
        SaveCurrentQuest();
        SaveQuestsComplete();
        SaveCurrentLEvelObjectInHome();

        if (SceneManager.GetActiveScene().buildIndex != SceneIndexConstants.HOME_SCENE_INDEX)
        {
            SceneManager.LoadScene(SceneIndexConstants.HOME_SCENE_INDEX);
        } 
    }

    public void ClickLoadNewSceneButton() {
        ButtonsEvents.onSaveResouces?.Invoke(); // событие на сохранение
        SaveCurrentQuest();
        SaveQuestsComplete();
        SaveCurrentLEvelObjectInHome();

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex == SceneIndexConstants.MEARG_SCENE_INDEX) {
            SceneManager.LoadScene(SceneIndexConstants.HOME_SCENE_INDEX);
        } 

        if(currentSceneIndex == SceneIndexConstants.HOME_SCENE_INDEX) {
            SceneManager.LoadScene(SceneIndexConstants.MEARG_SCENE_INDEX);
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

        MeargGameEvents.onSaveAltarTime?.Invoke();
        _resorcesPanel.SetActive(true);
        _altar_1.SetActive(false);
        _altar_2.SetActive(false);

        ButtonsEvents.onReloadResources?.Invoke(); // событие на загрузку сохранений
        UpdateShortQuestText();
    }

    public void ClickBackButtonResoucesPanel()
    {
        _resorcesPanel.SetActive(false);
        _altar_1.SetActive(true);
        _altar_2.SetActive(true);
        MeargGameEvents.onReloadAltarTime?.Invoke();
    }

    #region МАГАЗИН ИСКОРОК
    public void ClickSparkShop() {
        //возможно стоит ставить паузу
        MeargGameEvents.onSaveAltarTime?.Invoke();

        _sparkSopPanel.SetActive(true);
        MeargGameEvents.onActiveSparkShopPanel?.Invoke();

        _altar_1.SetActive(false);
        _altar_2.SetActive(false);
    }

    public void ClickBackButtonSparkShop() {
        //если будет решено ставить паузу то незабыть убрать с паузы.
        _sparkSopPanel.SetActive(false);

        _altar_1.SetActive(true);
        _altar_2.SetActive(true);
        MeargGameEvents.onReloadAltarTime?.Invoke();
    }
    #endregion

    public void ClickQuestButton()
    {
        _questsPanel.SetActive(true);
    }

    public void ClickBackButtonInQuest()
    {
        _questsPanel.SetActive(false);
        UpdateShortQuestText();
    }

    public void ClickMainLongTextButton()
    {

        // тут нужно ещё добавить вывод текста
        _mainLongTextPanel.SetActive(true);
    }

    public void ClickBackButtonInMainLongTextButton()
    {
        _mainLongTextPanel.SetActive(false);
    }

    public void ClickSecondLongTextButtonInSecondQuest_1()
    {
        _secondLongText.text = Quests.SECOND_QUEST_1_LONG;
        _secondLongTextPanel.SetActive(true);
    }

    public void ClickSecondLongTextButtonInSecondQuest_2()
    {
        _secondLongText.text = Quests.SECOND_QUEST_2_LONG;
        _secondLongTextPanel.SetActive(true);
    }

    public void ClickSecondLongTextButtonInSecondQuest_3()
    {
        _secondLongText.text = Quests.SECOND_QUEST_3_LONG;
        _secondLongTextPanel.SetActive(true);
    }

    public void ClickBackButtonInSecondLongTextButton()
    {
        _secondLongTextPanel.SetActive(false);
    }

    public void ClickCheckQuestButton_0()
    {
        bool activeObject = _questCheckImage_0.activeSelf;

        if (!activeObject)
        {
            _currentQuest = INDEX_QUEST_0;
            Debug.Log($"Значение текущего квеста равно {_currentQuest}");
        }
        else
        {
            _currentQuest = INDEX_QUEST_NO_QUEST;
            Debug.Log($"Значение текущего квеста равно {_currentQuest}");
        }

        _questCheckImage_0.SetActive(!activeObject);
        _questCheckImage_1.SetActive(false);
        _questCheckImage_2.SetActive(false);
    }

    public void ClickCheckQuestButton_1()
    {
        bool activeObject = _questCheckImage_1.activeSelf;

        if (!activeObject)
        {
            _currentQuest = INDEX_QUEST_1;
            Debug.Log($"Значение текущего квеста равно {_currentQuest}");
        }
        else
        {
            _currentQuest = INDEX_QUEST_NO_QUEST;
            Debug.Log($"Значение текущего квеста равно {_currentQuest}");
        }

        _questCheckImage_1.SetActive(!activeObject);
        _questCheckImage_0.SetActive(false);
        _questCheckImage_2.SetActive(false);
    }

    public void ClickCheckQuestButton_2()
    {
        bool activeObject = _questCheckImage_2.activeSelf;

        if (!activeObject)
        {
            _currentQuest = INDEX_QUEST_2;
            Debug.Log($"Значение текущего квеста равно {_currentQuest}");
        }
        else
        {
            _currentQuest = INDEX_QUEST_NO_QUEST;
            Debug.Log($"Значение текущего квеста равно {_currentQuest}");
        }

        _questCheckImage_2.SetActive(!activeObject);
        _questCheckImage_0.SetActive(false);
        _questCheckImage_1.SetActive(false);
    }

    public void ClickButtonsSoundClic()
    {
        _source.PlayOneShot(_clickButtonClip);
    }

    public void ClickReloadSaveButton()
    {
        PlayerPrefs.DeleteAll();
        ButtonsEvents.onStartResourcesText?.Invoke();
        UpdateShortQuestText();
    }

    private void UpdateShortQuestText()
    {
        if (_currentQuest == INDEX_QUEST_0 && !_fireplaceQuestCopmlete)
        {
            var text = Quests.SECOND_QUEST_0_SHORT;
            var completeText = "";

            if (_currentFireplaceLevel == 0)
            {
                completeText = SecondQuestText(text, 1, _fireplaceDictionary_1lv);
            }

            if (_currentFireplaceLevel == 1)
            {
                completeText = SecondQuestText(text,  2, _fireplaceDictionary_2lv);
            }

            if (_currentFireplaceLevel == 2)
            {
                completeText = SecondQuestText(text, 3, _fireplaceDictionary_3lv);
            }

            _questShortText.text = completeText;
        }
        else if (_currentQuest == INDEX_QUEST_1 && !_chairQuestCopmlete)
        {
            var text = Quests.SECOND_QUEST_1_SHORT;
            var completeText = "";

            if (_currentChairLevel == 0)
            {
                completeText = SecondQuestText(text, 1, _chairDictionary_1lv);
            }

            if (_currentChairLevel == 1)
            {
                completeText = SecondQuestText(text, 2, _chairDictionary_2lv);
            }

            if (_currentChairLevel == 2)
            {
                completeText = SecondQuestText(text, 3, _chairDictionary_3lv);
            }

            _questShortText.text = completeText;

        }
        else if (_currentQuest == INDEX_QUEST_2 && !_tableQuestCopmlete)
        {
            var text = Quests.SECOND_QUEST_2_SHORT;
            var completeText = "";

            if (_currentTableLevel == 0)
            {
                completeText = SecondQuestText(text, 1, _tableDictionary_1lv);
            }

            if (_currentTableLevel == 1)
            {
                completeText = SecondQuestText(text, 2, _tableDictionary_2lv);
            }

            if (_currentTableLevel == 2)
            {
                completeText = SecondQuestText(text, 3, _tableDictionary_3lv);
            }

            _questShortText.text = completeText;

        }
        else
        {
            var text = Quests.SECOND_QUEST_SHORT_DEFAULT;
            _questShortText.text = text;
        }

    }

    private void CheckStartCraftResouces()
    {
        _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(1);
        _fireplaceDictionary_2lv = EventsResources.onGetFireplaceDictionary?.Invoke(2);
        _fireplaceDictionary_3lv = EventsResources.onGetFireplaceDictionary?.Invoke(3);
       
        _chairDictionary_1lv = EventsResources.onGetChairDictionary?.Invoke(1);
        _chairDictionary_2lv = EventsResources.onGetChairDictionary?.Invoke(2);
        _chairDictionary_3lv = EventsResources.onGetChairDictionary?.Invoke(3);
      
        _tableDictionary_1lv = EventsResources.onGetTableDictionary?.Invoke(1);
        _tableDictionary_2lv = EventsResources.onGetTableDictionary?.Invoke(2);
        _tableDictionary_3lv = EventsResources.onGetTableDictionary?.Invoke(3);
        
    }

    private string SecondQuestText(string startText, int level, IDictionary<string, int> dictionary)
    {
        var stone_1lv = 0;
        var log_1lv = 0;
        var neil_1lv = 0;
        var cloth_1lv = 0;

        CheckStartCraftResouces();

        if (level == 1)
        {
            stone_1lv = dictionary[ResourcesTags.Stone_1.ToString()];
            log_1lv = dictionary[ResourcesTags.Log_1.ToString()];
            neil_1lv = dictionary[ResourcesTags.Neil_1.ToString()];
            cloth_1lv = dictionary[ResourcesTags.Cloth_1.ToString()];
        }

        if (level == 2)
        {
            stone_1lv = dictionary[ResourcesTags.Stone_2.ToString()];
            log_1lv = dictionary[ResourcesTags.Log_2.ToString()];
            neil_1lv = dictionary[ResourcesTags.Neil_2.ToString()];
            cloth_1lv = dictionary[ResourcesTags.Cloth_2.ToString()];
        }

        if (level == 3)
        {
            stone_1lv = dictionary[ResourcesTags.Stone_3.ToString()];
            log_1lv = dictionary[ResourcesTags.Log_3.ToString()];
            neil_1lv = dictionary[ResourcesTags.Neil_3.ToString()];
            cloth_1lv = dictionary[ResourcesTags.Cloth_3.ToString()];
        }


        var completeText = QuestText(stone_1lv, log_1lv, neil_1lv, cloth_1lv, startText, level);

        return completeText;
    }

    private string QuestText(int stone, int log, int neil, int cloat, string startText, int runsLevel)
    {
        var modifayText = startText;
        var completeText = "";

        if (stone > 0)
        {
            var currentStone = EventsResources.onGetCurentStone?.Invoke(runsLevel);
            modifayText = modifayText + "\n" + $"Треугольная руна {runsLevel}ур {stone}({currentStone})";
        }
        if (log > 0)
        {
            var currentLog = EventsResources.onGetCurentLog?.Invoke(runsLevel);
            modifayText = modifayText + "\n" + $"Прямоугольная руна {runsLevel}ур {log}({currentLog})";
        }
        if (neil > 0)
        {
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(runsLevel);
            modifayText = modifayText + "\n" + $"Квадратная руна {runsLevel}ур {neil}({currentNeil})";
        }
        if (cloat > 0)
        {
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(runsLevel);
            modifayText = modifayText + "\n" + $"Круглая руна {runsLevel}ур {cloat}({currentCloth})";
        }

        completeText = modifayText;

        return completeText;
    }

    private void CompleteFireplaceQuest()
    {
        _fireplaceQuestCopmlete = true;

        _fireplaceQuestCheckButton.interactable = false;
        _fireplaceQuestTextButton.text = Quests.SECOND_QUEST_COMPLETE;
        _questShortText.text = Quests.SECOND_QUEST_COMPLETE;
    }

    private void CompleteChairQuest()
    {
        _chairQuestCopmlete = true;

        _chairQuestCheckButton.interactable = false;
        _chairQuestTextButton.text = Quests.SECOND_QUEST_COMPLETE;
        _questShortText.text = Quests.SECOND_QUEST_COMPLETE;
    }

    private void CompleteTableQuest()
    {
        _tableQuestCopmlete = true;

        _tableQuestCheckButton.interactable = false;
        _tableQuestTextButton.text = Quests.SECOND_QUEST_COMPLETE;
        _questShortText.text = Quests.SECOND_QUEST_COMPLETE;
    }

    private void SaveCurrentQuest()
    {
        PlayerPrefs.SetInt("currentQuest", _currentQuest);

        PlayerPrefs.Save();
    }

    private void ReloadCurrentQuest()
    {
        if (PlayerPrefs.HasKey("currentQuest"))
        {
            _currentQuest = PlayerPrefs.GetInt("currentQuest");
            Debug.Log($"_currentQuest = {_currentQuest}");
            CheckStartCraftResouces();
            UpdateShortQuestText();
        }
    }

    private void ClickFireplace(int level)
    {
            _currentQuest = 0;

            if (!_fireplaceQuestCopmlete)
            {
                UpdateShortQuestText();
            }
    }

    private void ClickChair(int level)
    {
        _currentQuest = 1;

        if (!_chairQuestCopmlete)
        {
                UpdateShortQuestText();
        }
    }

    private void ClickTable(int level)
    {
        _currentQuest = 2;

        if (!_tableQuestCopmlete)
        {
                UpdateShortQuestText();
        }
    }

    private void CheckSceneForTornHomeButtonsActive()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneIndexConstants.MEARG_SCENE_INDEX)
        {
            _turnHomeButtonsPanel.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().buildIndex == SceneIndexConstants.HOME_SCENE_INDEX)
        {
            _turnHomeButtonsPanel.SetActive(true);
        }
    }

    private void SaveQuestsComplete()
    {
        if (_fireplaceQuestCopmlete)
        {
            PlayerPrefs.SetInt("fireplaceQuestCopmlete", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fireplaceQuestCopmlete", 0);
        }

        if (_chairQuestCopmlete)
        {
            PlayerPrefs.SetInt("chairQuestCopmlete", 1);
        }
        else
        {
            PlayerPrefs.SetInt("chairQuestCopmlete", 0);
        }

        if (_tableQuestCopmlete)
        {
            PlayerPrefs.SetInt("tableQuestCopmlete", 1);
        }
        else
        {
            PlayerPrefs.SetInt("tableQuestCopmlete", 0);
        }

        PlayerPrefs.Save();
    }

    private void ReloadsaveQuestComplete()
    {
        if (PlayerPrefs.HasKey("fireplaceQuestCopmlete"))
        {
            if (PlayerPrefs.GetInt("fireplaceQuestCopmlete") == 1)
            {
                _fireplaceQuestCopmlete = true;
            }
            else
            {
                _fireplaceQuestCopmlete = false;
            }
        }

        if (PlayerPrefs.HasKey("chairQuestCopmlete"))
        {
            if (PlayerPrefs.GetInt("chairQuestCopmlete") == 1)
            {
                _chairQuestCopmlete = true;
            }
            else
            {
                _chairQuestCopmlete = false;
            }
        }

        if (PlayerPrefs.HasKey("tableQuestCopmlete"))
        {
            if (PlayerPrefs.GetInt("tableQuestCopmlete") == 1)
            {
                _tableQuestCopmlete = true;
            }
            else
            {
                _tableQuestCopmlete = false;
            }
        }
    }

    public void ClickAutorButton()
    {
        
        _autorsTextPanel.SetActive(true);
    }

    public void ClickBackButtonInAutorPanel()
    {
        _autorsTextPanel.SetActive(false);
    }

    public void ClickTheGameButton()
    {

        _theGameTextPanel.SetActive(true);
    }

    public void ClickBackButtonInTheGamePanel()
    {
        _theGameTextPanel.SetActive(false);
    }

}
