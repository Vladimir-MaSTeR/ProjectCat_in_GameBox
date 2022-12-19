using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    [SerializeField] private Image _historyImage;

    [SerializeField] private Sprite _historySpriteOne;
    [SerializeField] private Sprite _historySpriteTwo;
    [SerializeField] private Sprite _historySpriteThree;
    
    [SerializeField] private Text _text;

    [SerializeField] private float _nextTextTime = 5f;

    private float _currentNextTextTime;
    private float _curentText = 0;
    private int _oneStart = 0;   // первый запуск игры или нет. (0 - первый. 1 - не первый запуск игры)

    private void Start()
    {
        _historyImage.sprite = _historySpriteOne;
        //_historyImageTwo.SetActive(false);
        _currentNextTextTime = _nextTextTime;
        _curentText = 0;

        ReloadSave();
        UpdateText();
    }

    private void Update()
    {
       // UpdateText();
        Timer();
    }

    public void ClickMissHisstory()
    {
        _oneStart = 1;
        Save();

        SceneManager.LoadScene(SceneIndexConstants.HOME_SCENE_INDEX);
    }

    private void UpdateText()
    {
        if (_oneStart == 0)
        {
            if (_curentText == 0)
            {
                _historyImage.sprite = _historySpriteOne;

                _text.text = HistoryText.ONE_CART_T1;
                _currentNextTextTime = _nextTextTime;
            }
            else if (_curentText == 1)
            {
                _text.text = HistoryText.ONE_CART_T2;
                _currentNextTextTime = _nextTextTime;
            }
            else if (_curentText == 2)
            {
                _historyImage.sprite = _historySpriteTwo;

                _text.text = HistoryText.TWO_CART_T1;
                _currentNextTextTime = _nextTextTime;
            }
            else if (_curentText == 3)
            {
                _text.text = HistoryText.TWO_CART_T2;
                _currentNextTextTime = _nextTextTime;
            }
            else if (_curentText == 4)
            {
                _text.text = HistoryText.TWO_CART_T3;
                _currentNextTextTime = _nextTextTime;
            }
            else if (_curentText == 5)
            {
                _text.text = HistoryText.TWO_CART_T4;
                _currentNextTextTime = _nextTextTime;
            }
            else if (_curentText == 6)
            {
                //{
                //    _text.gameObject.SetActive(false);
                //    _historyImage.sprite = _historySpriteThree;

                //    _currentNextTextTime = 3f;

                // SceneManager.LoadScene(1);

                _oneStart = 1;
                Save();

                SceneManager.LoadScene(SceneIndexConstants.HOME_SCENE_INDEX);
            }

            //else if (_curentText == 7)
            //{
            //    _oneStart = 1;
            //    Save();

            //    SceneManager.LoadScene(SceneIndexConstants.HOME_SCENE_INDEX);
            //}

        } else
        {
            SceneManager.LoadScene(SceneIndexConstants.HOME_SCENE_INDEX);
        }
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
            Debug.Log($"загрузил oneStart = {_oneStart}");
        }
    }

    private void Timer()
    {
        if (_currentNextTextTime > 0)
        {
            _currentNextTextTime -= Time.deltaTime;
        } else
        {
            _curentText++;
            UpdateText();
        }
    }

   
}
