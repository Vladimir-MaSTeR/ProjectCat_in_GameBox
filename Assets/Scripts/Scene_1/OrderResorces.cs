using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OrderResorces : MonoBehaviour, IPointerDownHandler, IDropHandler {
    [Header("����������")]
    [SerializeField] private int _id;

    [Space(20)] // ������ � ���������� ����� ������
    [SerializeField] private float _time = 90f;
    [SerializeField] private float _minusTimeInSpark = 5;

    [Space(20)] // ������ � ���������� ����� ������
    [SerializeField] private int _countSpawnItemOneMoment = 2;
    [SerializeField] private int _countSpawnOnClick = 8;

    [Space(20)] // ������ � ���������� ����� ������
    [SerializeField] private string _resTag;
    [SerializeField] private Image _completeImage;

    [Header("�����")]
    //[SerializeField] private TextMeshPro _timerText;
    [SerializeField] private TextMeshProUGUI _timerTextUI;

    private int _curent_id;
    private float _currentTime;
    private bool _allowOrderResorces = false;
    private int _currentCountSpawnOnClick = 8;

    private string timeName;
    private string allowName;
    private string countSpawnOnClickName;

    private void Start()
    {
        //_id = GetInstanceID();
        _curent_id =_id;
       

       timeName = $"currentTimeOrderResorces{_curent_id}";
       allowName = $"currentAllow{_curent_id}";
       countSpawnOnClickName = $"currentCountSpawnOnClick{_curent_id}";

       _currentTime = _time;
        _currentCountSpawnOnClick = _countSpawnOnClick;
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
           // Debug.Log("���� ���� �� ��������");
            SoundsEvents.onTapOrderResouces?.Invoke();
            //_allowOrderResorces = false;

            if (_currentCountSpawnOnClick > 0)
            {
                _timerTextUI.gameObject.SetActive(true);
               // _timerTextUI.text = _currentCountSpawnOnClick.ToString();

                for (int i = 0; i < _countSpawnItemOneMoment; i++)
                {
                    EventsResources.onSpawnItem?.Invoke(_resTag);
                }

                _currentCountSpawnOnClick--;
                _timerTextUI.text = _currentCountSpawnOnClick.ToString();

                if (_currentCountSpawnOnClick <= 0)
                {
                    _allowOrderResorces = false;
                    _currentCountSpawnOnClick = _countSpawnOnClick;
                }
            }           
            
            _completeImage.gameObject.SetActive(false);
            //_timerTextUI.gameObject.SetActive(true);
            _currentTime = _time;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if(ResourcesTags.Spark.ToString() == eventData.pointerDrag.tag) {

            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true; //_canvasGroup.blocksRaycasts = true;

            //MinusTime(_minusTimeInSpark);
            SpawnForSparks(_time);

           // Debug.Log($"����� ���������� �� ������, ����� ������� �����");
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
            _timerTextUI.text = _currentCountSpawnOnClick.ToString();
            //_currentCountSpawnOnClick = _countSpawnOnClick;
            //_timerTextUI.gameObject.SetActive(false);
            _completeImage.gameObject.SetActive(true);
        }
    }

    private void UpdateTimerText(float time) {
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

        PlayerPrefs.SetInt(countSpawnOnClickName, _currentCountSpawnOnClick);
        Debug.Log($"�������� countSpawnOnClickName = {_currentCountSpawnOnClick}");

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

        if (PlayerPrefs.HasKey(countSpawnOnClickName))
        {
            _currentCountSpawnOnClick = PlayerPrefs.GetInt(countSpawnOnClickName);
            Debug.Log($"�������� countSpawnOnClickName = {_currentCountSpawnOnClick}");
        }
    }

    private void MinusTime(float time) {
        if(_currentTime > 0) {
            _currentTime -= time;
            Debug.Log($"����� ������� ��������� �� {time} ���");

            //�������� ����� ���������� �������.
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, false);
        }
    }

    private void SpawnForSparks(float time) {
        if(_currentTime > 0) {

            for(int i = 0; i < _countSpawnItemOneMoment; i++) {
                EventsResources.onSpawnItem?.Invoke(_resTag);
            }

            _currentTime = time;
            Debug.Log($"������ ����������� � = {_currentTime}");

            //�������� ����� ���������� �������.
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, false);
        }
    }


}
