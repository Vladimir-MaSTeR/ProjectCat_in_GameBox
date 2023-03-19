using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrainingSc1 : MonoBehaviour
{
    [SerializeField] private GameObject _trainingPanel;
    [SerializeField] private Text _text;

    private const string ONE = "����� ������� ����, ���������� � � �������� ���� � (������).";
    private const string TWO = "������ 30 ������ �� ������ ������� �� ������� ���� ������ ��� ����, ������ �� ��������������� �������.";
    private const string THREE = "��� ��������� ���� ���������� ������ ���������� ���������� 2 ���������� ����.";
    private const string FOURE = "�������! � ��� ���� ��� ����������� ���� ��� �������������� ������. ������� ������� �� ����� � �����.";

    private int _oneStart = 0;
    private int _currentText = 0;

    //private void Start()
    //{
    //    ReloadSave();

    //    if (_oneStart == 1)
    //    {
    //        _trainingPanel.SetActive(true);
            
    //    }
    //    else
    //    {
    //        _trainingPanel.SetActive(false);
    //    }
    //}

    //private void Update()
    //{
    //    if (_currentText == 0)
    //    {
    //        _text.text = ONE;
    //    }
    //    if (_currentText == 1)
    //    {
    //        _text.text = TWO;
    //    }
    //    if (_currentText == 2)
    //    {
    //        _text.text = THREE;
    //    }
    //    if (_currentText == 3)
    //    {
    //        _text.text = FOURE;
            
    //    }
    //    if (_currentText == 4)
    //    {
    //        _currentText++;
    //        _oneStart = 2;
    //        Save();

    //        EventsResources.onStoneInBucket?.Invoke(1, 6, 1);
    //        EventsResources.onLogInBucket?.Invoke(1, 3, 1);
    //        EventsResources.onClouthInBucket?.Invoke(1, 3, 1);

    //        ButtonsEvents.onSaveResouces?.Invoke();

    //        SceneManager.LoadScene(SceneIndexConstants.HOME_SCENE_INDEX);

    //        //�������� �������� � ����������� �� ����� � �����.
    //    }
    //}

    public void ClickNextButton()
    {
        _currentText++;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("oneStart", _oneStart);

        PlayerPrefs.Save();
    }


    private void ReloadSave()
    {
        if (PlayerPrefs.HasKey("oneStart"))
        {
            _oneStart = PlayerPrefs.GetInt("oneStart");
            Debug.Log($"�������� oneStart = {_oneStart}");
        }
    }
}
