using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Altar_1 : MonoBehaviour, IDropHandler, IPointerDownHandler {

    #region Переменные движка
    [Header("Настройки")]

    [Tooltip("Идентификатор алтаря")]
    [SerializeField] 
    private int _id = 0;

    [Space(2)]                              // отступ в инспекторе между полями
    [Header("Поля для объектов")]

    [Tooltip("Текст таймера")]
    [SerializeField] 
    private TextMeshProUGUI _timerTextUI;

    //[Tooltip("Картинка галочки для обозначения готовности алтаря")]
    //[SerializeField] 
    //private Image _completeImage;                  // временное решение, до переезда в 3д
    #endregion

    #region Приватные переменные
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

            //Возвращаем искорку на своё место
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //вызываем эвент мгновенно сдвига вниз рун на одну строчку и обнуляе счетчик до следующего “сдвига” до изначального
            MeargGameEvents.onSetTimeToSpawnRuns?.Invoke(0);

            //вызывать эвент уменьшения искорки.
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, false);


            // Debug.Log($"Искру перетащили на алтарь, нужно убавить время");
        }
    }

    public void OnPointerDown(PointerEventData eventData) {

        var currentSparcs = EventsResources.onGetSparkCurrentValue?.Invoke();

        if(currentSparcs > 0) {

            //Возвращаем искорку на своё место
            //eventData.pointerDrag.transform.localPosition = Vector3.zero;
            //eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //вызываем эвент мгновенно сдвига вниз рун на одну строчку и обнуляе счетчик до следующего “сдвига” до изначального
            MeargGameEvents.onSetTimeToSpawnRuns?.Invoke(0);

            //вызывать эвент уменьшения искорки.
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, false);


            // Debug.Log($"Искру перетащили на алтарь, нужно убавить время");
        } else {
            //Проигрывать звук отказа использования алтаря 
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
