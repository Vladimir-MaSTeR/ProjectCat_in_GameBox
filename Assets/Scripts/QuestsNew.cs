using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestsNew : MonoBehaviour {
    [Tooltip("Время через которое откроется панель предмета с выбором квеста")]
    [SerializeField]
    private float _timeToOpenPanel = 1.5f;
    
    [SerializeField]
    private UpdateQuestsText _updateQuestsTextScript;

    private string _currentQuest;
    private bool _questNull = true;

    private IDictionary<string, int> _fireplaceDictionary_1lv;
    private IDictionary<string, int> _fireplaceDictionary_2lv;
    private IDictionary<string, int> _fireplaceDictionary_3lv;
    
    private IDictionary<string, int> _kitchenDictionary_1lv;
    private IDictionary<string, int> _kitchenDictionary_2lv;
    private IDictionary<string, int> _kitchenDictionary_3lv;
    
    private IDictionary<string, int> _armchairDictionary_1lv;
    private IDictionary<string, int> _armchairDictionary_2lv;
    private IDictionary<string, int> _armchairDictionary_3lv;
    
    //private IDictionary<string, int> _fireplaceDictionary_4lv = EventsResources.onGetFireplaceDictionary?.Invoke(4);


    private void Awake() {
        // TEST();
        //DELETE_TEST_VALUE();
    }
    private void Start() {
        // _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(1);
        // _fireplaceDictionary_2lv = EventsResources.onGetFireplaceDictionary?.Invoke(2);
        // _fireplaceDictionary_3lv = EventsResources.onGetFireplaceDictionary?.Invoke(3);
        //_fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(4);


        ReloadQuestInPlaerPrefs();
        TrackingQuestValue();

        // DELETE_TEST_VALUE();
    }

    private void Update() {
        if(_questNull == false) {
            UpdateQuestValue();
        }
    }

    #region ТЕСТОВЫЕ ДАННЫЕ
    private void TEST() {
        PlayerPrefs.SetString(QuestConstants.key, QuestConstants.fireplaceQuest_1);

        Debug.Log($"СОХРАНИЛ = {QuestConstants.fireplaceQuest_1}");

        PlayerPrefs.Save();
    }

    private void DELETE_TEST_VALUE() {
        PlayerPrefs.DeleteKey(QuestConstants.key);
    }
    
    #endregion

    private void OnEnable() {
        HomeEvents.onGetTimeToOpenPanel += GetTimeToOpenPanel;
    }

    private void OnDisable() {
        HomeEvents.onGetTimeToOpenPanel -= GetTimeToOpenPanel;
    }

    /**
     * Обновляет квест
     */
    private void UpdateQuestValue() {
        
        //Камин
        if(QuestConstants.fireplaceQuest_1 == _currentQuest) {
            _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(1);
            UpdateQuest(_fireplaceDictionary_1lv, 
                                 ResourcesTags.Log_1.ToString(), ResourcesTags.Neil_1.ToString(),
                                 ResourcesTags.Stone_1.ToString(), ResourcesTags.Cloth_1.ToString(), 
                                 QuestConstants.fireplaceQuest_1, 1);
        }

        if(QuestConstants.fireplaceQuest_2 == _currentQuest) {
            _fireplaceDictionary_2lv = EventsResources.onGetFireplaceDictionary?.Invoke(2);
            UpdateQuest(_fireplaceDictionary_2lv, 
            ResourcesTags.Log_2.ToString(), ResourcesTags.Neil_2.ToString(),
            ResourcesTags.Stone_2.ToString(), ResourcesTags.Cloth_2.ToString(), 
            QuestConstants.fireplaceQuest_2, 2);
        }

        if(QuestConstants.fireplaceQuest_3 == _currentQuest) {
            _fireplaceDictionary_3lv = EventsResources.onGetFireplaceDictionary?.Invoke(3);
            UpdateQuest(_fireplaceDictionary_3lv, 
            ResourcesTags.Log_3.ToString(), ResourcesTags.Neil_3.ToString(),
            ResourcesTags.Stone_3.ToString(), ResourcesTags.Cloth_3.ToString(), 
            QuestConstants.fireplaceQuest_3, 3);
        }

        //Кухня
        if(QuestConstants.kitchenQuest_1 == _currentQuest) {
            _kitchenDictionary_1lv = EventsResources.onGetTableDictionary?.Invoke(1);
            UpdateQuest(_kitchenDictionary_1lv, 
            ResourcesTags.Log_1.ToString(), ResourcesTags.Neil_1.ToString(), 
            ResourcesTags.Stone_1.ToString(), ResourcesTags.Cloth_1.ToString(),
            QuestConstants.kitchenQuest_1, 1);
        }
        
        if(QuestConstants.kitchenQuest_2 == _currentQuest) {
            _kitchenDictionary_2lv = EventsResources.onGetTableDictionary?.Invoke(2);
            UpdateQuest(_kitchenDictionary_2lv, 
            ResourcesTags.Log_2.ToString(), ResourcesTags.Neil_2.ToString(), 
            ResourcesTags.Stone_2.ToString(), ResourcesTags.Cloth_2.ToString(),
            QuestConstants.kitchenQuest_2, 2);
        }
        
        if(QuestConstants.kitchenQuest_3 == _currentQuest) {
            _kitchenDictionary_3lv = EventsResources.onGetTableDictionary?.Invoke(3);
            UpdateQuest(_kitchenDictionary_3lv, 
            ResourcesTags.Log_3.ToString(), ResourcesTags.Neil_3.ToString(), 
            ResourcesTags.Stone_3.ToString(), ResourcesTags.Cloth_3.ToString(),
            QuestConstants.kitchenQuest_3, 3);
        }
        
        //Кресло
        if(QuestConstants.armchairQuest_1 == _currentQuest) {
            _armchairDictionary_1lv = EventsResources.onGetChairDictionary?.Invoke(1);
            UpdateQuest(_armchairDictionary_1lv, 
            ResourcesTags.Log_1.ToString(), ResourcesTags.Neil_1.ToString(), 
            ResourcesTags.Stone_1.ToString(), ResourcesTags.Cloth_1.ToString(),
            QuestConstants.armchairQuest_1, 1);
        }
        
        if(QuestConstants.armchairQuest_2 == _currentQuest) {
            _armchairDictionary_2lv = EventsResources.onGetChairDictionary?.Invoke(2);
            UpdateQuest(_armchairDictionary_2lv, 
            ResourcesTags.Log_2.ToString(), ResourcesTags.Neil_2.ToString(), 
            ResourcesTags.Stone_2.ToString(), ResourcesTags.Cloth_2.ToString(),
            QuestConstants.armchairQuest_2, 2);
        }
        
        if(QuestConstants.armchairQuest_3 == _currentQuest) {
            _armchairDictionary_3lv = EventsResources.onGetChairDictionary?.Invoke(3);
            UpdateQuest(_armchairDictionary_3lv, 
            ResourcesTags.Log_3.ToString(), ResourcesTags.Neil_3.ToString(), 
            ResourcesTags.Stone_3.ToString(), ResourcesTags.Cloth_3.ToString(),
            QuestConstants.armchairQuest_3, 3);
        }
    }
    
    /**
     * Метод отправляет нужные значения для отслеживания квеста камина.
     * 
     * param name="fireplaceDictionary">Словарь с необходимыми значениями для квеста
     * param name="mainQuestText"> текст для главного сообщения
     * param name="level"> уровень руны 
     */
    private void UpdateQuest(IDictionary<string, int> dictionary,
                                      string logTag, string neilTag, 
                                      string stoneTag, string clothTag, 
                                      string mainQuestText, int level) {
        //Debug.Log("Метод работает");

        _updateQuestsTextScript.SetQuestText(mainQuestText);

        var log = dictionary[logTag];
        //Debug.Log($"Значение из словаря нужного кол-ва ресурса с тегом log = {log}");
        var currentLog = EventsResources.onGetCurentLog?.Invoke(level);
        //Debug.Log($"Текущее значение руны с тегом log = {currentLog}");

        _updateQuestsTextScript.SetOneRawImage(logTag, level);
        _updateQuestsTextScript.SetOneRuneText((int)currentLog, log);

        var neil = dictionary[neilTag];
        var currentNeil = EventsResources.onGetCurentNeil?.Invoke(level);

        _updateQuestsTextScript.SetTwoRawImage(neilTag, level);
        _updateQuestsTextScript.SetTwoRuneText((int)currentNeil, neil);

        var stone = dictionary[stoneTag];
        var currentStone = EventsResources.onGetCurentStone?.Invoke(level);

        _updateQuestsTextScript.SetThreeRawImage(stoneTag, level);
        _updateQuestsTextScript.SetThreeRuneText((int)currentStone, stone);

        var cloth = dictionary[clothTag];
        var currentCloth = EventsResources.onGetCurentClouth?.Invoke(level);

        _updateQuestsTextScript.SetFourRawImage(clothTag, level);
        _updateQuestsTextScript.SetFourRuneText((int)currentCloth, cloth);
    }
    
    /**
     * Отслеживает выбран ли квест
     */
    private void TrackingQuestValue() {
        if(QuestConstants.noneQuest == _currentQuest) {
            Debug.Log("Загружаем дефолтные значения для отображения квеста");
            _questNull = true;
            _updateQuestsTextScript.DefoultValue();
        } else {
            Debug.Log("Начинаю отслеживать квест");
            _questNull = false;
        }
    }

    /// <summary>
    /// Метод загружает  отслеживаемы квест из PlaerPrefs если сохранение не найдено то присваивает значения по умолчанию.
    /// </summary>
    private void ReloadQuestInPlaerPrefs() {
        if(PlayerPrefs.HasKey(QuestConstants.key)) {
            _currentQuest = PlayerPrefs.GetString(QuestConstants.key);
        } else {
            _currentQuest = QuestConstants.noneQuest;
        }

        // добавить суда остальные квесты.
    }

    private float GetTimeToOpenPanel() {
        return _timeToOpenPanel;
    }
}
