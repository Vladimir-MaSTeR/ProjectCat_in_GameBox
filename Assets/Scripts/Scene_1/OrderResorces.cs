using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderResorces : MonoBehaviour, IPointerDownHandler
{
    [Header("œÂÂÏÂÌÌ˚Â")]
    [SerializeField] private float _time = 90f;
    [SerializeField] private int _id;

    [SerializeField] private int _countSpawnItem = 2;
    [SerializeField] private string _resTag;
    [SerializeField] private Image _completeImage;

    [Header("“ÂÍÒÚ")]
    //[SerializeField] private TextMeshPro _timerText;
    [SerializeField] private TextMeshProUGUI _timerTextUI;

    private int _curent_id;
    private float _currentTime;
    private bool _allowOrderResorces = false;

    private string timeName;
    private string allowName;

    private void Start()
    {
        //_id = GetInstanceID();
        _curent_id =_id;

       timeName = $"currentTimeOrderResorces {_curent_id}";
       allowName = $"currentAllow {_curent_id}";

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
        

        PlayerPrefs.SetFloat(timeName, _currentTime);


       
        if (_allowOrderResorces == true)
        {
            PlayerPrefs.SetInt(allowName, 1);
        } else
        {
            PlayerPrefs.SetInt(allowName, 0);
        }

        PlayerPrefs.Save();
    }

    private void ReloadSaveTimer()
    {
        if (PlayerPrefs.HasKey(timeName))
        {
            _currentTime = PlayerPrefs.GetFloat(timeName);
        }

        if (PlayerPrefs.HasKey(allowName))
        {
           var currentAllow = PlayerPrefs.GetInt(allowName);

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
