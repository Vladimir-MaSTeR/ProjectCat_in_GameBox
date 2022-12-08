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

    [Header("Картинки для отслеживания заданий")]
    [SerializeField] private GameObject _questCheckImage_0;
    [SerializeField] private GameObject _questCheckImage_1;
    [SerializeField] private GameObject _questCheckImage_2;

    [Header("Звук")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clickButtonClip;

    private const int ONE_SCENE_INDEX = 0;
    private const int TWO_SCENE_INDEX = 1;

    private int _currentQuest = -1; // переменная для отслеживания активного квеста. начинается с ноля. 

    private const int INDEX_QUEST_NO_QUEST = -1;
    private const int INDEX_QUEST_0 = 0;
    private const int INDEX_QUEST_1 = 1;
    private const int INDEX_QUEST_2 = 2;



    private void Start()
    {
        _menuPanel.SetActive(false);
        _resorcesPanel.SetActive(false);
        _questsPanel.SetActive(false);
        _mainLongTextPanel.SetActive(false);
        _secondLongTextPanel.SetActive(false);

        UpdateShortQuestText();
    }


    private void OnEnable()
    {
        EventsResources.onUpdateQuest += UpdateShortQuestText;
    }

    private void OnDisable()
    {
        EventsResources.onUpdateQuest += UpdateShortQuestText;
    }


    public void ClickMeargSceneButton()
    {
        ButtonsEvents.onSaveResouces?.Invoke();
        if (SceneManager.GetActiveScene().buildIndex != ONE_SCENE_INDEX)
        {
            SceneManager.LoadScene(ONE_SCENE_INDEX);
            UpdateShortQuestText();
        }
    }

    public void ClickHomeSceneButton()
    {
        ButtonsEvents.onSaveResouces?.Invoke(); // событие на сохранение
        if (SceneManager.GetActiveScene().buildIndex != TWO_SCENE_INDEX)
        {
            SceneManager.LoadScene(TWO_SCENE_INDEX);
            UpdateShortQuestText();
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
            var currentLog = EventsResources.onGetCurentStone(1);
            var text = Quests.SECOND_QUEST_0_SHORT + $"({currentLog})";
            _questShortText.text = text;

        }
        else if (_currentQuest == INDEX_QUEST_1)
        {
            var currentLog = EventsResources.onGetCurentNeil(1);
            var text = Quests.SECOND_QUEST_1_SHORT + $"({currentLog})";
            _questShortText.text = text;

        }
        else if (_currentQuest == INDEX_QUEST_2)
        {
            var currentLog = EventsResources.onGetCurentLog(1);
            var text = Quests.SECOND_QUEST_1_SHORT + $"({currentLog})";
            _questShortText.text = text;

        }
        else
        {
            var text = Quests.SECOND_QUEST_SHORT_DEFAULT;
            _questShortText.text = text;
        }

    }
}
