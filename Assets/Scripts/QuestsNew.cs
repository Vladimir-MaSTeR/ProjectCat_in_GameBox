using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestsNew : MonoBehaviour {
    [Tooltip("����� ����� ������� ��������� ������ �������� � ������� ������")]
    [SerializeField]
    private float _timeToOpenPanel = 1.5f;
    
    [SerializeField]
    private UpdateQuestsText _updateQuestsTextScript;

    private string _currentQuest;
    private bool _questNull = true;

    private IDictionary<string, int> _fireplaceDictionary_1lv;
    private IDictionary<string, int> _fireplaceDictionary_2lv;
    private IDictionary<string, int> _fireplaceDictionary_3lv;
    //private IDictionary<string, int> _fireplaceDictionary_4lv = EventsResources.onGetFireplaceDictionary?.Invoke(4);


    private void Awake() {
        // TEST();
        //DELETE_TEST_VALUE();
    }
    private void Start() {
        _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(1);
        _fireplaceDictionary_2lv = EventsResources.onGetFireplaceDictionary?.Invoke(2);
        _fireplaceDictionary_3lv = EventsResources.onGetFireplaceDictionary?.Invoke(3);
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

    #region �������� ������
    private void TEST() {
        PlayerPrefs.SetString(QuestConstants.key, QuestConstants.fireplaceQuest_1);

        Debug.Log($"�������� = {QuestConstants.fireplaceQuest_1}");

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


    /// <summary>
    /// ��������� �����
    /// </summary>
    private void UpdateQuestValue() {
        if(QuestConstants.fireplaceQuest_1 == _currentQuest) {
            _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(1);
            fireplaceUpdateQuest(_fireplaceDictionary_1lv, QuestConstants.fireplaceQuest_1, 1);
        }

        if(QuestConstants.fireplaceQuest_2 == _currentQuest) {
            _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(2);
            fireplaceUpdateQuest(_fireplaceDictionary_2lv, QuestConstants.fireplaceQuest_2, 2);
        }

        if(QuestConstants.fireplaceQuest_3 == _currentQuest) {
            _fireplaceDictionary_1lv = EventsResources.onGetFireplaceDictionary?.Invoke(3);
            fireplaceUpdateQuest(_fireplaceDictionary_3lv, QuestConstants.fireplaceQuest_3, 3);
        }
    }

    /// <summary>
    /// ����� ���������� ������ �������� ��� ������������ ������ ������. 
    /// </summary>
    /// <param name="fireplaceDictionary">������� � ������������ ���������� ��� ������</param>
    /// <param name="mainQuestText"> ����� ��� �������� ���������</param>
    /// <param name="level"> ������� ���� </param>
    private void fireplaceUpdateQuest(IDictionary<string, int> dictionary, string mainQuestText, int level) {
        //Debug.Log("����� ��������");

        _updateQuestsTextScript.SetQuestText(mainQuestText);

        var log = dictionary[ResourcesTags.Log_1.ToString()];
        //Debug.Log($"�������� �� ������� ������� ���-�� ������� � ����� log = {log}");
        var currentLog = EventsResources.onGetCurentLog?.Invoke(level);
        //Debug.Log($"������� �������� ���� � ����� log = {currentLog}");

        _updateQuestsTextScript.SetOneRawImage(ResourcesTags.Log_1.ToString(), level);
        _updateQuestsTextScript.SetOneRuneText((int)currentLog, log);

        var neil = dictionary[ResourcesTags.Neil_1.ToString()];
        var currentNeil = EventsResources.onGetCurentNeil?.Invoke(level);

        _updateQuestsTextScript.SetTwoRawImage(ResourcesTags.Neil_1.ToString(), level);
        _updateQuestsTextScript.SetTwoRuneText((int)currentNeil, neil);

        var stone = dictionary[ResourcesTags.Stone_1.ToString()];
        var currentStone = EventsResources.onGetCurentStone?.Invoke(level);

        _updateQuestsTextScript.SetThreeRawImage(ResourcesTags.Stone_1.ToString(), level);
        _updateQuestsTextScript.SetThreeRuneText((int)currentStone, stone);

        var cloth = dictionary[ResourcesTags.Cloth_1.ToString()];
        var currentCloth = EventsResources.onGetCurentClouth?.Invoke(level);

        _updateQuestsTextScript.SetFourRawImage(ResourcesTags.Cloth_1.ToString(), level);
        _updateQuestsTextScript.SetFourRuneText((int)currentCloth, cloth);
    }

    /// <summary>
    /// ����������� ������ �� �����
    /// </summary>
    private void TrackingQuestValue() {
        if(QuestConstants.noneQuest == _currentQuest) {
            Debug.Log("��������� ��������� �������� ��� ����������� ������");
            _questNull = true;
            _updateQuestsTextScript.DefoultValue();
        } else {
            Debug.Log("������� ����������� �����");
            _questNull = false;
        }
    }

    /// <summary>
    /// ����� ���������  ������������ ����� �� PlaerPrefs ���� ���������� �� ������� �� ����������� �������� �� ���������.
    /// </summary>
    private void ReloadQuestInPlaerPrefs() {
        if(PlayerPrefs.HasKey(QuestConstants.key)) {
            _currentQuest = PlayerPrefs.GetString(QuestConstants.key);
        } else {
            _currentQuest = QuestConstants.noneQuest;
        }

        // �������� ���� ��������� ������.
    }

    private float GetTimeToOpenPanel() {
        return _timeToOpenPanel;
    }
}
