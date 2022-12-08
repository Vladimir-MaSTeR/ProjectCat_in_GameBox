using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    [Header("Панели")]
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _resorcesPanel;
    [SerializeField] private GameObject _questsPanel;

    [Header("Звук")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clickButtonClip;

    private const int ONE_SCENE_INDEX = 0;
    private const int TWO_SCENE_INDEX = 1;



    private void Start()
    {
        _menuPanel.SetActive(false);
        _resorcesPanel.SetActive(false);
        _questsPanel.SetActive(false);
    }


    public void ClickMeargSceneButton()
    {
        //SaveResources();
        ButtonsEvents.onSaveResouces?.Invoke();
        if (SceneManager.GetActiveScene().buildIndex != ONE_SCENE_INDEX)
        {
            SceneManager.LoadScene(ONE_SCENE_INDEX);
        }
    }

    public void ClickHomeSceneButton()
    {
       // SaveResources();
        ButtonsEvents.onSaveResouces?.Invoke();
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
        ButtonsEvents.onReloadResources?.Invoke();
        //ReloadResourcesText();
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
    }

    public void ClickButtonsSoundClic()
    {
        _source.PlayOneShot(_clickButtonClip);
    }

    public void ClickReloadSaveButton()
    {
        PlayerPrefs.DeleteAll();
        ButtonsEvents.onStartResourcesText?.Invoke();
        //StartResourcesText();
    }
}
