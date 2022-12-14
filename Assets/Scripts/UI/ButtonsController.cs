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

    private const int ONE_SCENE_INDEX = 0;
    private const int TWO_SCENE_INDEX = 1;

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








    private void Start()
    {
        _menuPanel.SetActive(false);
        _resorcesPanel.SetActive(false);
        _questsPanel.SetActive(false);
        _mainLongTextPanel.SetActive(false);
        _secondLongTextPanel.SetActive(false);

        CheckStartCraftResouces();
        ReloadCurrentQuest();
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


    public void ClickMeargSceneButton()
    {
        ButtonsEvents.onSaveResouces?.Invoke();
        SaveCurrentQuest();

        if (SceneManager.GetActiveScene().buildIndex != ONE_SCENE_INDEX)
        {
            SceneManager.LoadScene(ONE_SCENE_INDEX);
        }
    }

    public void ClickHomeSceneButton()
    {
        ButtonsEvents.onSaveResouces?.Invoke(); // событие на сохранение
        SaveCurrentQuest();

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
        ButtonsEvents.onReloadResources?.Invoke(); // событие на загрузку сохранений
        UpdateShortQuestText();
    }

    public void ClickBackButtonResoucesPanel()
    {
        _resorcesPanel.SetActive(false);
    }

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
        if (_currentQuest == INDEX_QUEST_0)
        {
            var text = Quests.SECOND_QUEST_0_SHORT;
            var completeText = SecondQuestText(text, _fireplaceDictionary_1lv);
            _questShortText.text = completeText;
        }
        else if (_currentQuest == INDEX_QUEST_1)
        {
            var text = Quests.SECOND_QUEST_1_SHORT;
            var completeText = SecondQuestText(text, _chairDictionary_1lv);
            _questShortText.text = completeText;

        }
        else if (_currentQuest == INDEX_QUEST_2)
        {
            var text = Quests.SECOND_QUEST_2_SHORT;
            var completeText = SecondQuestText(text, _tableDictionary_1lv);
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
        Debug.Log($"_fireplaceDictionary_1lv = {_fireplaceDictionary_1lv.Count}");
        _chairDictionary_1lv = EventsResources.onGetChairDictionary?.Invoke(1);
        _chairDictionary_2lv = EventsResources.onGetChairDictionary?.Invoke(2);
        _chairDictionary_3lv = EventsResources.onGetChairDictionary?.Invoke(3);
        Debug.Log($"_chairDictionary_1lv = {_chairDictionary_1lv.Count}");
        _tableDictionary_1lv = EventsResources.onGetTableDictionary?.Invoke(1);
        _tableDictionary_2lv = EventsResources.onGetTableDictionary?.Invoke(2);
        _tableDictionary_3lv = EventsResources.onGetTableDictionary?.Invoke(3);
        Debug.Log($"_tableDictionary_1lv = {_tableDictionary_1lv.Count}");
    }

    private string SecondQuestText(string startText, IDictionary<string, int> dictionary)
    {
        var modifayText = startText;
        var completeText = "";

        var stone_1lv = dictionary[ResourcesTags.Stone_1.ToString()];
        var log_1lv = dictionary[ResourcesTags.Log_1.ToString()];
        var neil_1lv = dictionary[ResourcesTags.Neil_1.ToString()];
        var cloth_1lv = dictionary[ResourcesTags.Cloth_1.ToString()];

        if (stone_1lv > 0)
        {
            var currentStone = EventsResources.onGetCurentStone?.Invoke(1);
            modifayText = modifayText + "\n" + $"Камни 1ур {stone_1lv} ({currentStone})";
        }
        if (log_1lv > 0)
        {
            var currentLog = EventsResources.onGetCurentLog?.Invoke(1);
            modifayText = modifayText + "\n" + $"Дерево 1ур {log_1lv} ({currentLog})";
        }
        if (neil_1lv > 0)
        {
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(1);
            modifayText = modifayText + "\n" + $"Гвозди 1 ур {neil_1lv} ({currentNeil})";
        }
        if (cloth_1lv > 0)
        {
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(1);
            modifayText = modifayText + "\n" + $"Ткань 1 ур {cloth_1lv} ({currentCloth})";
        }

        completeText = modifayText;
        

        return completeText;
    }

    private void CompleteFireplaceQuest()
    {
        _fireplaceQuestCheckButton.interactable = false;
        _fireplaceQuestTextButton.text = Quests.SECOND_QUEST_COMPLETE;
        _questShortText.text = Quests.SECOND_QUEST_COMPLETE;
    }

    private void CompleteChairQuest()
    {
        _chairQuestCheckButton.interactable = false;
        _chairQuestTextButton.text = Quests.SECOND_QUEST_COMPLETE;
        _questShortText.text = Quests.SECOND_QUEST_COMPLETE;
    }

    private void CompleteTableQuest()
    {
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
        if (level == 1)
        {
            _currentQuest = 0;
            UpdateShortQuestText();
        }
       
    }

    private void ClickChair(int level)
    {
        if (level == 1)
        {
            _currentQuest = 1;
            UpdateShortQuestText();
        }
    }

    private void ClickTable(int level)
    {
        if (level == 1)
        {
            _currentQuest = 2;
            UpdateShortQuestText();
        }
       
    }

}
