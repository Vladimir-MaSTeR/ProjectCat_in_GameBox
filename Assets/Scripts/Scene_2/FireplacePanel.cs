using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FireplacePanel : MonoBehaviour {
    #region ПЕРЕМЕННЫЕ ДВИЖКА

    [Tooltip("Информационная панель камина")]
    [SerializeField]
    private GameObject _fireplacePanel;

    [Tooltip("Скрипт в котором лежат словари с текстурами рун")]
    [SerializeField]
    private UpdateQuestsText _updateQuestsTextScript;
    
    [Tooltip("СкриптоблОбджект камина")]
    [SerializeField]
    private ObjectsHouseInfo _fireplaceInfo;


    [Space(20)]
    [Header("Текстовые поля внутри панели")]
    [Space(10)]
    [Header("Текущий текст")]
    [Tooltip("Текст текущего уровня камина")]
    [SerializeField]
    private TextMeshProUGUI _currentLevelText;

    [Tooltip("Текст текущей прочности")]
    [SerializeField]
    private TextMeshProUGUI _currentDurabilityValueText;

    [Tooltip("Текс текущего уюта")]
    [SerializeField]
    private TextMeshProUGUI _currentComfortValueText;

    [Space(10)]
    [Header("Следующий текст")]
    [Tooltip("Текст следующего уровня ")]
    [SerializeField]
    private TextMeshProUGUI _nextLevelText;

    [Space(10)]
    [Header("Текстовые поля рун")]
    [Tooltip("Текстовое поле для первой руны")]
    [SerializeField]
    private TextMeshProUGUI _oneRuneText;

    [Tooltip("Текстовое поле для второй руны")]
    [SerializeField]
    private TextMeshProUGUI _twoRuneText;

    [Tooltip("Текстовое поле третьей руны")]
    [SerializeField]
    private TextMeshProUGUI _threeRuneText;

    [Tooltip("Текстовое поле четвертой руны")]
    [SerializeField]
    private TextMeshProUGUI _fourRuneText;

    [Space(10)]
    [Header("Картинки рун")]
    [Tooltip("Первая картинка для отображения 3д руны")]
    [SerializeField]
    private RawImage _oneResoucesRawImage;

    [Tooltip("Первая картинка для отображения 3д руны")]
    [SerializeField]
    private RawImage _twoResoucesRawImage;

    [Tooltip("Первая картинка для отображения 3д руны")]
    [SerializeField]
    private RawImage _threeResoucesRawImage;

    [Tooltip("Первая картинка для отображения 3д руны")]
    [SerializeField]
    private RawImage _fourResoucesRawImage;

    [Space(10)]
    [Header("Текст будущих прочности и уюта")]
    [Tooltip("Текст будущего прочности")]
    [SerializeField]
    private TextMeshProUGUI _nextDurabilityValueText;

    [Tooltip("Текс будущего уюта")]
    [SerializeField]
    private TextMeshProUGUI _nextComfortValueText;

    #endregion

    #region ПРИВАТНЫЕ ПЕРЕМЕННЫЕ

    private Texture[] _logTextures;
    private Texture[] _neilTextures;
    private Texture[] _stoneTextures;
    private Texture[] _clothTextures;

    private IDictionary<string, int> _fireplaceDictionary_1lv;
    private IDictionary<string, int> _fireplaceDictionary_2lv;
    private IDictionary<string, int> _fireplaceDictionary_3lv;
    //private IDictionary<string, int> _fireplaceDictionary_4lv = EventsResources.onGetFireplaceDictionary?.Invoke(4);

    #endregion

    private void Start() {
        _fireplacePanel.SetActive(false);

        _logTextures = _updateQuestsTextScript.GetLogTextures();
        _neilTextures = _updateQuestsTextScript.GetNeilTexture();
        _stoneTextures = _updateQuestsTextScript.GetStoneTexture();
        _clothTextures = _updateQuestsTextScript.GetClothTextures();
    }

    private void OnEnable() {
        HomeEvents.onOpenInfoPanels += OpenFireplacePanel;
        HomeEvents.onClosedFireplacePanel += ClosedFireplacePanel;

        HomeEvents.onActiveFireplaceQuest += SaveActiveQuest;
    }

    private void OnDisable() {
        HomeEvents.onOpenInfoPanels -= OpenFireplacePanel;
        HomeEvents.onClosedFireplacePanel -= ClosedFireplacePanel;
        
        HomeEvents.onActiveFireplaceQuest -= SaveActiveQuest;
    }

    /**
     * Активирует панель камина со всеми данными - запускается эвентом 
     */
    private void OpenFireplacePanel(IdObjectsHome id) {
        if(IdObjectsHome.FIREPLACE == id) {
            _fireplacePanel.SetActive(true);

            // int currentLevel = (int)HomeEvents.onGetCurrentFireplaceLevel?.Invoke();
            int currentLevel = _fireplaceInfo.Level;
            // int currentLevel = Resources.Load<ObjectsHouseInfo>("Home").Level;
            SetCurrentText(currentLevel);
            SetNextTextAndRawImage(currentLevel);
            SetNextDurabilityValueAndComfortValueText(currentLevel);
        }
    }

    /**
     * Метода закрывает панель.
     * Вызывается эвентом, который вызывается нажатием кнопки.
     */
    private void ClosedFireplacePanel() {
        _fireplacePanel.SetActive(false);
    }

    /**
     * Метод сохраняет квест для отслеживания
     */
    private void SaveActiveQuest() {
        
        // int currentLevel = (int)HomeEvents.onGetCurrentFireplaceLevel?.Invoke();
        int currentLevel = _fireplaceInfo.Level;
        PlayerPrefs.DeleteKey(QuestConstants.key);
        
        if(currentLevel == 0) {
            PlayerPrefs.SetString(QuestConstants.key, QuestConstants.fireplaceQuest_1);
        }

        if(currentLevel == 1) {
            PlayerPrefs.SetString(QuestConstants.key, QuestConstants.fireplaceQuest_2);
        }

        if(currentLevel == 2) {
            PlayerPrefs.SetString(QuestConstants.key, QuestConstants.fireplaceQuest_3);
        }
        
        PlayerPrefs.Save();
    }

    //СКОРЕЙ ВСЕГО НИЖЕ ХАРДКОД

    /**
     * Выводит все текущие значения
     */
    private void SetCurrentText(int currentLevel) {

        _currentLevelText.text = $"Текущий уровень {currentLevel}";

        if(currentLevel == 0) {
            _currentDurabilityValueText.text = "0";
            _currentComfortValueText.text = "0";
        }

        if(currentLevel == 1) {
            _currentDurabilityValueText.text = "10";
            _currentComfortValueText.text = "15";
        }

        if(currentLevel == 2) {
            _currentDurabilityValueText.text = "15";
            _currentComfortValueText.text = "20";
        }

        if(currentLevel == 3) {
            _currentDurabilityValueText.text = "20";
            _currentComfortValueText.text = "25";
        }
    }

    //адский хардкод, при первой жевозможности разобрать
    /**
     * Выводит руны и их значения для будущего улутшения
     */
    private void SetNextTextAndRawImage(int currentLevel) {
        if(currentLevel == 0) {
            _nextLevelText.text = $"Следующий уровень {currentLevel + 1}";
            _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(currentLevel + 1);

            //LOG
            var targetLog = _fireplaceDictionary_1lv[ResourcesTags.Log_1.ToString()];
            var currentLog = EventsResources.onGetCurentLog?.Invoke(currentLevel + 1);
            _oneResoucesRawImage.texture = _logTextures[currentLevel];
            if(targetLog > 0) {
                _oneRuneText.text = $"{currentLog}/{targetLog}";
            } else {
                _oneRuneText.text = HomeConstants.defaultValue;
            }

            //NEIL
            var targetNeil = _fireplaceDictionary_1lv[ResourcesTags.Neil_1.ToString()];
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(currentLevel + 1);
            _twoResoucesRawImage.texture = _neilTextures[currentLevel];
            if(targetNeil > 0) {
                _twoRuneText.text = $"{currentNeil}/{targetNeil}";
            } else {
                _twoRuneText.text = HomeConstants.defaultValue;
            }

            //STONE
            var targetStone = _fireplaceDictionary_1lv[ResourcesTags.Stone_1.ToString()];
            var currentStone = EventsResources.onGetCurentStone?.Invoke(currentLevel + 1);
            _threeResoucesRawImage.texture = _stoneTextures[currentLevel];
            if(targetStone > 0) {
                _threeRuneText.text = $"{currentStone}/{targetStone}";
            } else {
                _threeRuneText.text = HomeConstants.defaultValue;
            }

            //CLOTH
            var targetCloth = _fireplaceDictionary_1lv[ResourcesTags.Cloth_1.ToString()];
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(currentLevel + 1);
            _fourResoucesRawImage.texture = _clothTextures[currentLevel];
            if(targetCloth > 0) {
                _fourRuneText.text = $"{currentCloth}/{targetCloth}";
            } else {
                _fourRuneText.text = HomeConstants.defaultValue;
            }
        }

        if(currentLevel == 1) {
            _nextLevelText.text = $"Следующий уровень {currentLevel + 1}";
            _fireplaceDictionary_2lv = EventsResources.onGetFireplaceDictionary?.Invoke(currentLevel + 1);

            //LOG
            var targetLog = _fireplaceDictionary_2lv[ResourcesTags.Log_2.ToString()];
            var currentLog = EventsResources.onGetCurentLog?.Invoke(currentLevel + 1);
            _oneResoucesRawImage.texture = _logTextures[currentLevel];
            if(targetLog > 0) {
                _oneRuneText.text = $"{currentLog}/{targetLog}";
            } else {
                _oneRuneText.text = HomeConstants.defaultValue;
            }

            //NEIL
            var targetNeil = _fireplaceDictionary_2lv[ResourcesTags.Neil_2.ToString()];
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(currentLevel + 1);
            _twoResoucesRawImage.texture = _neilTextures[currentLevel];
            if(targetNeil > 0) {
                _twoRuneText.text = $"{currentNeil}/{targetNeil}";
            } else {
                _twoRuneText.text = HomeConstants.defaultValue;
            }

            //STONE
            var targetStone = _fireplaceDictionary_2lv[ResourcesTags.Stone_2.ToString()];
            var currentStone = EventsResources.onGetCurentStone?.Invoke(currentLevel + 1);
            _threeResoucesRawImage.texture = _stoneTextures[currentLevel];
            if(targetStone > 0) {
                _threeRuneText.text = $"{currentStone}/{targetStone}";
            } else {
                _threeRuneText.text = HomeConstants.defaultValue;
            }

            //CLOTH
            var targetCloth = _fireplaceDictionary_2lv[ResourcesTags.Cloth_2.ToString()];
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(currentLevel + 1);
            _fourResoucesRawImage.texture = _clothTextures[currentLevel];
            if(targetCloth > 0) {
                _fourRuneText.text = $"{currentCloth}/{targetCloth}";
            } else {
                _fourRuneText.text = HomeConstants.defaultValue;
            }
        }

        if(currentLevel == 2) {
            _nextLevelText.text = $"Следующий уровень {currentLevel + 1}";
            _fireplaceDictionary_3lv = EventsResources.onGetFireplaceDictionary?.Invoke(currentLevel + 1);

            //LOG
            var targetLog = _fireplaceDictionary_3lv[ResourcesTags.Log_3.ToString()];
            var currentLog = EventsResources.onGetCurentLog?.Invoke(currentLevel + 1);
            _oneResoucesRawImage.texture = _logTextures[currentLevel];
            if(targetLog > 0) {
                _oneRuneText.text = $"{currentLog}/{targetLog}";
            } else {
                _oneRuneText.text = HomeConstants.defaultValue;
            }

            //NEIL
            var targetNeil = _fireplaceDictionary_3lv[ResourcesTags.Neil_3.ToString()];
            var currentNeil = EventsResources.onGetCurentNeil?.Invoke(currentLevel + 1);
            _twoResoucesRawImage.texture = _neilTextures[currentLevel];
            if(targetNeil > 0) {
                _twoRuneText.text = $"{currentNeil}/{targetNeil}";
            } else {
                _twoRuneText.text = HomeConstants.defaultValue;
            }

            //STONE
            var targetStone = _fireplaceDictionary_3lv[ResourcesTags.Stone_3.ToString()];
            var currentStone = EventsResources.onGetCurentStone?.Invoke(currentLevel + 1);
            _threeResoucesRawImage.texture = _stoneTextures[currentLevel];
            if(targetStone > 0) {
                _threeRuneText.text = $"{currentStone}/{targetStone}";
            } else {
                _threeRuneText.text = HomeConstants.defaultValue;
            }

            //CLOTH
            var targetCloth = _fireplaceDictionary_3lv[ResourcesTags.Cloth_3.ToString()];
            var currentCloth = EventsResources.onGetCurentClouth?.Invoke(currentLevel + 1);
            _fourResoucesRawImage.texture = _clothTextures[currentLevel];
            if(targetCloth > 0) {
                _fourRuneText.text = $"{currentCloth}/{targetCloth}";
            } else {
                _fourRuneText.text = HomeConstants.defaultValue;
            }
        }

        if(currentLevel == 3) {
            _nextLevelText.text = $"Достигнут максимальный уровень.";
            // _fireplaceDictionary_3lv = EventsResources.onGetFireplaceDictionary?.Invoke(currentLevel + 1);
            //
            // //LOG
            // var targetLog = _fireplaceDictionary_3lv[ResourcesTags.Log_3.ToString()];
            // var currentLog = EventsResources.onGetCurentLog?.Invoke(currentLevel + 1);
            // _oneResoucesRawImage.texture = _logTextures[currentLevel + 1];
            // if(targetLog > 0) {
            //     _oneRuneText.text = $"{currentLog}/{targetLog}";
            // } else {
            //     _oneRuneText.text = HomeConstants.defaultValue;
            // }
            //
            // //NEIL
            // var targetNeil = _fireplaceDictionary_3lv[ResourcesTags.Neil_3.ToString()];
            // var currentNeil = EventsResources.onGetCurentNeil?.Invoke(currentLevel + 1);
            // _twoResoucesRawImage.texture = _neilTextures[currentLevel + 1];
            // if(targetNeil > 0) {
            //     _twoRuneText.text = $"{currentNeil}/{targetNeil}";
            // } else {
            //     _twoRuneText.text = HomeConstants.defaultValue;
            // }
            //
            // //STONE
            // var targetStone = _fireplaceDictionary_3lv[ResourcesTags.Stone_3.ToString()];
            // var currentStone = EventsResources.onGetCurentStone?.Invoke(currentLevel + 1);
            // _threeResoucesRawImage.texture = _stoneTextures[currentLevel + 1];
            // if(targetStone > 0) {
            //     _threeRuneText.text =  $"{currentStone}/{targetStone}";
            // } else {
            //     _threeRuneText.text = HomeConstants.defaultValue;
            // }
            //
            // //CLOTH
            // var targetCloth = _fireplaceDictionary_3lv[ResourcesTags.Cloth_3.ToString()];
            // var currentCloth = EventsResources.onGetCurentClouth?.Invoke(currentLevel + 1);
            // _fourResoucesRawImage.texture = _clothTextures[currentLevel + 1];
            // if(targetCloth > 0) {
            //     _fourRuneText.text = $"{currentCloth}/{targetCloth}";
            // } else {
            //     _fourRuneText.text = HomeConstants.defaultValue;
            // }
        }
    }

    private void SetNextDurabilityValueAndComfortValueText(int currentValue) {
        if(currentValue == 0) {
            _nextDurabilityValueText.text = "10";
            _nextComfortValueText.text = "15";
        }

        if(currentValue == 1) {
            _nextDurabilityValueText.text = "15";
            _nextComfortValueText.text = "20";
        }

        if(currentValue == 2) {
            _nextDurabilityValueText.text = "20";
            _nextComfortValueText.text = "25";
        }

        if(currentValue == 3) {
            _nextDurabilityValueText.text = HomeConstants.defaultValue;
            _nextComfortValueText.text = HomeConstants.defaultValue;
        }
    }
}
