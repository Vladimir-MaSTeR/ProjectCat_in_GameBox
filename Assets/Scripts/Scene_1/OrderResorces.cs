using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderResorces : MonoBehaviour, IPointerDownHandler
{
    [Header("œÂÂÏÂÌÌ˚Â")]
    [SerializeField] private float _time = 90f;

    [SerializeField] private int _countSpawnItem = 2;
    [SerializeField] private string _resTag;
    [SerializeField] private Image _completeImage;

    [Header("“ÂÍÒÚ")]
    //[SerializeField] private TextMeshPro _timerText;
    [SerializeField] private TextMeshProUGUI _timerTextUI;

    private int _id;
    private float _currentTime;
    private bool _allowOrderResorces = false;

    private void Start()
    {
        _id = GetInstanceID();

        _currentTime = _time;
        _timerTextUI.text = _currentTime.ToString();

        _completeImage.gameObject.SetActive(false);
        _timerTextUI.gameObject.SetActive(true);

        ReloadSaveTimer();
    }


    private void Update()
    {
        Timer();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (_allowOrderResorces == true)
        {
            Debug.Log("≈—“‹  À»  œŒ œ–≈ƒÃ≈“”");
            _allowOrderResorces = false;

            for (int i = 0; i < _countSpawnItem; i++)
            {
                EventsResources.onSpawnItem?.Invoke(_resTag);
            }
            

            _completeImage.gameObject.SetActive(false);
            _timerTextUI.gameObject.SetActive(true);
            _currentTime = _time;
        }
    }

    private void OnEnable()
    {
        ButtonsEvents.onSaveResouces += SaveTimer;
    }

    private void OnDisable()
    {
        ButtonsEvents.onSaveResouces -= SaveTimer;
    }

    private void Timer()
    {
        if (_currentTime > 0 && _allowOrderResorces == false)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimerText(_currentTime);

        }  else {
            _allowOrderResorces = true;
            _timerTextUI.gameObject.SetActive(false);
            _completeImage.gameObject.SetActive(true);
        }
    }

    private void UpdateTimerText(float time)
    {
        if (time < 0)
        {
            time = 0;
        }

        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);
        _timerTextUI.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }

    private void SaveTimer()
    {
        var timeName = $"currentTimeOrderResorces {_id}";

        PlayerPrefs.SetFloat("currentTimeOrderResorces", _currentTime);

        if (_allowOrderResorces == true)
        {
            PlayerPrefs.SetInt("allowOrderResorces", 1);
        } else
        {
            PlayerPrefs.SetInt("allowOrderResorces", 0);
        }

        PlayerPrefs.Save();
    }

    private void ReloadSaveTimer()
    {
        if (PlayerPrefs.HasKey("currentTimeOrderResorces"))
        {
            _currentTime = PlayerPrefs.GetFloat("currentTimeOrderResorces");
        }

        if (PlayerPrefs.HasKey("allowOrderResorces"))
        {
           var currentAllow = PlayerPrefs.GetInt("allowOrderResorces");

            if (currentAllow == 1)
            {
                _allowOrderResorces = true;
            } else
            {
                _allowOrderResorces = false;
            }
        }
    }



}
