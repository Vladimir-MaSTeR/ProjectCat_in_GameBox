using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [Header("Стартовые настройки ресурсов")]
    [SerializeField] private int _startCloath;
    [SerializeField] private int _startLog;
    [SerializeField] private int _startNeil;
    [SerializeField] private int _startStone;
    [SerializeField] private int _startPrestige;

    [Header("Текстовые поля для отображения ресурсов")]
    [SerializeField] private Text _cloathText;
    [SerializeField] private Text _logText;
    [SerializeField] private Text _neilText;
    [SerializeField] private Text _stoneText;
    [SerializeField] private Text _prestigeText;

    [Header("Панели")]
    [SerializeField] private GameObject _menuPanel;


    private int _currentCloath;
    private int _currentLog;
    private int _currentNeil;
    private int _currentStone;
    private int _currentPrestige;

    private const int ONE_SCENE_INDEX = 0;
    private const int TWO_SCENE_INDEX = 1;


    private void Start()
    {
        StartResourcesText(); // пока без сохранения

        _menuPanel.SetActive(false);
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

    private void SaveResources()
    {
        //TODO
    }

    private void StartResourcesText()
    {
        _currentCloath = _startCloath;
        _cloathText.text = _currentCloath.ToString();

        _currentLog = _startLog;
        _logText.text = _currentLog.ToString();

        _currentNeil = _startNeil;
        _neilText.text = _currentNeil.ToString();

        _currentStone = _startStone;
        _stoneText.text = _currentStone.ToString();

        _currentPrestige = _startPrestige;
        _prestigeText.text = _currentPrestige.ToString();
    }

}
