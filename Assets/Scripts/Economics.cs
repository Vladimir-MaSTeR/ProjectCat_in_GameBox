using System.Collections.Generic;
using UnityEngine;

public class Economics : MonoBehaviour {

    [Header("Стартовое кол-во уюта")]
    [SerializeField]
    private int _comfortStartValue = 0;

    [Header("Стартовое кол-во искорок")]
    [SerializeField]
    private int _sparkStartValue = 0;

    [Space(20)] // отступ в инспекторе между полями

    private int _currentComfortValue;
    private int _currentSparkValue;

    private const string NAME_COMFORT_VALUE_FOR_SAVE = "comfortValue";
    private const string NAME_SPARK_VALUE_FOR_SAVE = "sparcValue";


    private IDictionary<string, int> saveDictionary;


    private void Start() {

        ReloadValue();



        TEST();
    }

    //метод для проверки.
    private void TEST() {
        var currentComfort = EventsResources.onGetComfortCurrentValue?.Invoke();
        Debug.Log($"Текущее значение комфорта = {currentComfort}");

        var currentSpark = EventsResources.onGetSparkCurrentValue?.Invoke();
        Debug.Log($"Текущее значение искорок = {currentSpark}");
        //----
        var plusComfort = 15;
        EventsResources.onAddOrDeductComfortValue?.Invoke(plusComfort, true);
        currentComfort = EventsResources.onGetComfortCurrentValue?.Invoke();
        Debug.Log($"Отправил эвент для увеличения комфорта на {plusComfort}. " +
                  $"Текущее значение комфорта стало = {currentComfort}");

        var plusSparc = 18;
        EventsResources.onAddOrDeductSparkValue?.Invoke(plusSparc, true);
        currentSpark = EventsResources.onGetSparkCurrentValue?.Invoke();
        Debug.Log($"Отправил эвент для увеличения искорок на {plusSparc}. " +
                  $"Текущее значение искорок стало = {currentSpark}");
        
        //----
        var minusComfort = 12;
        EventsResources.onAddOrDeductComfortValue?.Invoke(minusComfort, false);
        currentComfort = EventsResources.onGetComfortCurrentValue?.Invoke();
        Debug.Log($"Отправил эвент для уменьшения комфорта на {minusComfort}.  " +
                  $"Текущее значение комфорта стало = {currentComfort}");

        var minusSpark = 13;
        EventsResources.onAddOrDeductSparkValue?.Invoke(minusSpark, false);
        currentSpark = EventsResources.onGetSparkCurrentValue?.Invoke();
        Debug.Log($"Отправил эвент для уменьшения искорок на {minusSpark}. " +
                  $"Текущее значение искорок стало = {currentSpark}");
    }

    private void OnEnable() {
        EventsResources.onGetComfortCurrentValue += GetComfortCurrentValue;
        EventsResources.onGetSparkCurrentValue += GetSparkCurrentValue;

        EventsResources.onAddOrDeductComfortValue += AddOrDeductComfortValue;
        EventsResources.onAddOrDeductSparkValue += AddOrDeductSparkValue;

        ButtonsEvents.onSaveResouces += SaveValue;
    }

    private void OnDisable() {
        EventsResources.onGetComfortCurrentValue -= GetComfortCurrentValue;
        EventsResources.onGetSparkCurrentValue -= GetSparkCurrentValue;

        EventsResources.onAddOrDeductComfortValue -= AddOrDeductComfortValue;
        EventsResources.onAddOrDeductSparkValue -= AddOrDeductSparkValue;

        ButtonsEvents.onSaveResouces -= SaveValue;
    }

    /// <summary>
    /// Метод получения текущего значения Уюта
    /// </summary>
    /// <Returns>возвращает _currentComfortValue - текущее значение Уюта</Returns>
    private int GetComfortCurrentValue() {
        return _currentComfortValue;
    }

    /// <summary>
    /// Метод прибавляет или отнимает Уют
    /// </summary>
    /// <param name="value"> Колличество которое нужно прибавить или отнять</param>
    /// <param name="addOrDeduct"> признак отвечающий за действие. | true = прибавить | false  = отнять </param>
    private void AddOrDeductComfortValue(int value, bool addOrDeduct ) {
        if(addOrDeduct) {
            _currentComfortValue += value;
        } else {
            _currentComfortValue -= value;

            if(_currentComfortValue <= 0) {
                _currentComfortValue = 0;
            }
        }

        EventsResources.onUpdateComfortValue?.Invoke(_currentComfortValue);
    }

    /// <summary>
    /// Метод получения текущего значения Искорки
    /// </summary>
    /// <Returns>возвращает _currentSparkValue - текущее значение Искорки</Returns>
    private int GetSparkCurrentValue() {
        return _currentSparkValue;
    }

    /// <summary>
    /// Метод прибавляет или отнимает Искорки
    /// </summary>
    /// <param name="value"> Колличество которое нужно прибавить или отнять</param>
    /// <param name="addOrDeduct"> признак отвечающий за действие. | true = прибавить | false  = отнять </param>
    private void AddOrDeductSparkValue(int value, bool addOrDeduct) {
        if(addOrDeduct) {
            _currentSparkValue += value;
        } else {
            _currentSparkValue -= value;

            if(_currentSparkValue <= 0) {
                _currentSparkValue = 0;
            }
        }

        EventsResources.onUpdateSparkValue?.Invoke(_currentSparkValue);
    }

    /// <summary>
    /// метод сохранения который вызывается эвентом
    /// </summary>
    private void SaveValue() {
        saveDictionary = new Dictionary<string, int>();

        saveDictionary.Add(NAME_COMFORT_VALUE_FOR_SAVE, _currentComfortValue);
        saveDictionary.Add(NAME_SPARK_VALUE_FOR_SAVE, _currentSparkValue);

        AddSavePlayerPrefs(saveDictionary);
    }

    /// <summary>
    /// метод для загрузки сохранений
    /// </summary>
    private void ReloadValue() {
        _currentComfortValue = ReloadPlayerPrefs(NAME_COMFORT_VALUE_FOR_SAVE, _comfortStartValue);
        _currentSparkValue = ReloadPlayerPrefs(NAME_SPARK_VALUE_FOR_SAVE, _sparkStartValue);

        EventsResources.onUpdateComfortValue?.Invoke(_currentComfortValue);
        EventsResources.onUpdateSparkValue?.Invoke(_currentSparkValue);

    }
      

    /// <summary>
    /// Метод сохранения через PlayerPrefs
    /// </summary>
    /// <param name="dictionary"> принимает параметризованный словарь. string = имя сохранения. int = значение параметра</param>
    private void AddSavePlayerPrefs(IDictionary<string, int> dictionary) {
        foreach(var item in dictionary) {
            PlayerPrefs.SetInt(item.Key, item.Value);
        }

        PlayerPrefs.Save();
    }



    /// <summary>
    /// Метод поиска сохранения в PlayerPrefs по имени
    /// </summary>
    /// <param name="name"> имя сохранения</param>
    /// <param name="startParametr">дефолтное значение</param>
    /// <returns></returns>
    private int ReloadPlayerPrefs(string name, int startParametr) {

        if(PlayerPrefs.HasKey(name)) {
            startParametr = PlayerPrefs.GetInt(name);
        } 

        return startParametr;
    }


}
