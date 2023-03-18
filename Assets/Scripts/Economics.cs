using System.Collections.Generic;
using UnityEngine;

public class Economics : MonoBehaviour {

    [Header("��������� ���-�� ����")]
    [SerializeField]
    private int _comfortStartValue = 0;

    [Header("��������� ���-�� �������")]
    [SerializeField]
    private int _sparkStartValue = 0;

    [Space(20)] // ������ � ���������� ����� ������

    private int _currentComfortValue;
    private int _currentSparkValue;

    private const string NAME_COMFORT_VALUE_FOR_SAVE = "comfortValue";
    private const string NAME_SPARK_VALUE_FOR_SAVE = "sparcValue";


    private IDictionary<string, int> saveDictionary;


    private void Start() {

        ReloadValue();



        TEST();
    }

    //����� ��� ��������.
    private void TEST() {
        var currentComfort = EventsResources.onGetComfortCurrentValue?.Invoke();
        Debug.Log($"������� �������� �������� = {currentComfort}");

        var currentSpark = EventsResources.onGetSparkCurrentValue?.Invoke();
        Debug.Log($"������� �������� ������� = {currentSpark}");
        //----
        var plusComfort = 15;
        EventsResources.onAddOrDeductComfortValue?.Invoke(plusComfort, true);
        currentComfort = EventsResources.onGetComfortCurrentValue?.Invoke();
        Debug.Log($"�������� ����� ��� ���������� �������� �� {plusComfort}. " +
                  $"������� �������� �������� ����� = {currentComfort}");

        var plusSparc = 18;
        EventsResources.onAddOrDeductSparkValue?.Invoke(plusSparc, true);
        currentSpark = EventsResources.onGetSparkCurrentValue?.Invoke();
        Debug.Log($"�������� ����� ��� ���������� ������� �� {plusSparc}. " +
                  $"������� �������� ������� ����� = {currentSpark}");
        
        //----
        var minusComfort = 12;
        EventsResources.onAddOrDeductComfortValue?.Invoke(minusComfort, false);
        currentComfort = EventsResources.onGetComfortCurrentValue?.Invoke();
        Debug.Log($"�������� ����� ��� ���������� �������� �� {minusComfort}.  " +
                  $"������� �������� �������� ����� = {currentComfort}");

        var minusSpark = 13;
        EventsResources.onAddOrDeductSparkValue?.Invoke(minusSpark, false);
        currentSpark = EventsResources.onGetSparkCurrentValue?.Invoke();
        Debug.Log($"�������� ����� ��� ���������� ������� �� {minusSpark}. " +
                  $"������� �������� ������� ����� = {currentSpark}");
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
    /// ����� ��������� �������� �������� ����
    /// </summary>
    /// <Returns>���������� _currentComfortValue - ������� �������� ����</Returns>
    private int GetComfortCurrentValue() {
        return _currentComfortValue;
    }

    /// <summary>
    /// ����� ���������� ��� �������� ���
    /// </summary>
    /// <param name="value"> ����������� ������� ����� ��������� ��� ������</param>
    /// <param name="addOrDeduct"> ������� ���������� �� ��������. | true = ��������� | false  = ������ </param>
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
    /// ����� ��������� �������� �������� �������
    /// </summary>
    /// <Returns>���������� _currentSparkValue - ������� �������� �������</Returns>
    private int GetSparkCurrentValue() {
        return _currentSparkValue;
    }

    /// <summary>
    /// ����� ���������� ��� �������� �������
    /// </summary>
    /// <param name="value"> ����������� ������� ����� ��������� ��� ������</param>
    /// <param name="addOrDeduct"> ������� ���������� �� ��������. | true = ��������� | false  = ������ </param>
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
    /// ����� ���������� ������� ���������� �������
    /// </summary>
    private void SaveValue() {
        saveDictionary = new Dictionary<string, int>();

        saveDictionary.Add(NAME_COMFORT_VALUE_FOR_SAVE, _currentComfortValue);
        saveDictionary.Add(NAME_SPARK_VALUE_FOR_SAVE, _currentSparkValue);

        AddSavePlayerPrefs(saveDictionary);
    }

    /// <summary>
    /// ����� ��� �������� ����������
    /// </summary>
    private void ReloadValue() {
        _currentComfortValue = ReloadPlayerPrefs(NAME_COMFORT_VALUE_FOR_SAVE, _comfortStartValue);
        _currentSparkValue = ReloadPlayerPrefs(NAME_SPARK_VALUE_FOR_SAVE, _sparkStartValue);

        EventsResources.onUpdateComfortValue?.Invoke(_currentComfortValue);
        EventsResources.onUpdateSparkValue?.Invoke(_currentSparkValue);

    }
      

    /// <summary>
    /// ����� ���������� ����� PlayerPrefs
    /// </summary>
    /// <param name="dictionary"> ��������� ����������������� �������. string = ��� ����������. int = �������� ���������</param>
    private void AddSavePlayerPrefs(IDictionary<string, int> dictionary) {
        foreach(var item in dictionary) {
            PlayerPrefs.SetInt(item.Key, item.Value);
        }

        PlayerPrefs.Save();
    }



    /// <summary>
    /// ����� ������ ���������� � PlayerPrefs �� �����
    /// </summary>
    /// <param name="name"> ��� ����������</param>
    /// <param name="startParametr">��������� ��������</param>
    /// <returns></returns>
    private int ReloadPlayerPrefs(string name, int startParametr) {

        if(PlayerPrefs.HasKey(name)) {
            startParametr = PlayerPrefs.GetInt(name);
        } 

        return startParametr;
    }


}
