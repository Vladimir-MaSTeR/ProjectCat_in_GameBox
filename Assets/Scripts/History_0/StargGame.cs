using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StargGame : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _historyPanel;


    private int _oneStart = 0;   // ������ ������ ���� ��� ���. (0 - ������. 1 - �� ������ ������ ����)

    private void Start()
    {
        _mainPanel.SetActive(true);
        _historyPanel.SetActive(false);

        ReloadSave();


    }


    public void ClickStartGame()
    {
        if (_oneStart == 0)
        {

            _mainPanel.SetActive(false);
            _historyPanel.SetActive(true);
        } else
        {
            SceneManager.LoadScene(SceneIndexConstants.HOME_SCENE_INDEX);
            // SceneManager.LoadScene(SceneIndexConstants.MEARG_SCENE_INDEX); // ����� �������
        }
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
