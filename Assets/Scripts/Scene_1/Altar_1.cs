using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Altar_1 : MonoBehaviour, IDropHandler, IPointerDownHandler {

    #region ���������� ������
    [Header("���������")]

    [Tooltip("������������� ������")]
    [SerializeField] 
    private int _id = 0;

    [Space(2)]                              // ������ � ���������� ����� ������
    [Header("���� ��� ��������")]

    [Tooltip("����� �������")]
    [SerializeField] 
    private TextMeshProUGUI _timerTextUI;

    //[Tooltip("�������� ������� ��� ����������� ���������� ������")]
    //[SerializeField] 
    //private Image _completeImage;                  // ��������� �������, �� �������� � 3�
    #endregion

    #region ��������� ����������
    private int _curent_id;
    private float _currentTime;

    private string timeName;
    private string allowName;
    #endregion

    private void Start() {
        _curent_id = _id;

        timeName = $"currentTimeOrderResorces{_curent_id}";
        allowName = $"currentAllow{_curent_id}";

        _currentTime = 0;

        //_completeImage.gameObject.SetActive(false);
        //_timerTextUI.gameObject.SetActive(true);

        ReloadSaveTimer();
    }

    //private void Update() {
    //    Timer();
    //}

    public void OnDrop(PointerEventData eventData) {
        if(ResourcesTags.Spark.ToString() == eventData.pointerDrag.tag) {

            //���������� ������� �� ��� �����
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //�������� ����� ��������� ������ ���� ��� �� ���� ������� � ������� ������� �� ���������� �������� �� ������������
            MeargGameEvents.onSetTimeToSpawnRuns?.Invoke(0);

            //�������� ����� ���������� �������.
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, false);


            // Debug.Log($"����� ���������� �� ������, ����� ������� �����");
        }
    }

    public void OnPointerDown(PointerEventData eventData) {

        var currentSparcs = EventsResources.onGetSparkCurrentValue?.Invoke();

        if(currentSparcs > 0) {

            //���������� ������� �� ��� �����
            //eventData.pointerDrag.transform.localPosition = Vector3.zero;
            //eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //�������� ����� ��������� ������ ���� ��� �� ���� ������� � ������� ������� �� ���������� �������� �� ������������
            MeargGameEvents.onSetTimeToSpawnRuns?.Invoke(0);

            //�������� ����� ���������� �������.
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, false);


            // Debug.Log($"����� ���������� �� ������, ����� ������� �����");
        } else {
            //����������� ���� ������ ������������� ������ 
        }
    }

private void OnEnable() {
        ButtonsEvents.onSaveResouces += SaveTimer;
        //MeargGameEvents.onSetTimeToSpawnRuns += UpdateCurrentTime;
    }

    private void OnDisable() {
        ButtonsEvents.onSaveResouces -= SaveTimer;
        //MeargGameEvents.onSetTimeToSpawnRuns -= UpdateCurrentTime;
    }

    //private void UpdateCurrentTime(float time) {
    //    _currentTime = time;
    //}

    //private void Timer() {
    //    if(_currentTime > 0) {
    //        _currentTime -= Time.deltaTime;
    //        UpdateTimerText(_currentTime);

    //    } else {
    //        _currentTime = (float)(MeargGameEvents.onGetCurrentTimeSpawnOldColumn?.Invoke());
    //        //_completeImage.gameObject.SetActive(true);
    //    }
    //}

    //private void UpdateTimerText(float time) {
    //    if(time < 0) {
    //        time = 0;
    //    }

    //    var minutes = Mathf.FloorToInt(time / 60);
    //    var seconds = Mathf.FloorToInt(time % 60);
    //    _timerTextUI.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    //}

    private void SaveTimer() {
        PlayerPrefs.SetFloat(timeName, _currentTime);

        PlayerPrefs.Save();
    }

    private void ReloadSaveTimer() {
        if(PlayerPrefs.HasKey(timeName)) {
            _currentTime = PlayerPrefs.GetFloat(timeName);
        }
    }
}
