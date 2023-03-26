
using UnityEngine;
using UnityEngine.EventSystems;

public class Altar_2 : MonoBehaviour, IDropHandler
{
    #region Переменные движка
    [Header("Настройки")]

    [Tooltip("Идентификатор алтаря")]
    [SerializeField]
    private int _id = 1;

    #endregion

    #region Приватные переменные
    private int _curent_id;
   
    #endregion

    private void Start() {
        _curent_id = _id;
    }


    public void OnDrop(PointerEventData eventData) {
        if(ResourcesTags.Spark.ToString() == eventData.pointerDrag.tag) {

            //Возвращаем искорку на своё место
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //вызываем эвент обнуления счетчик спавна рун до изначального значения
            float startTimeSpawnRuns = (float)(MeargGameEvents.onGetCurrentTimeSpawnOldColumn?.Invoke());
            MeargGameEvents.onSetTimeToSpawnRuns?.Invoke(startTimeSpawnRuns);

            //вызывать эвент уменьшения искорки.
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, false);


            // Debug.Log($"Искру перетащили на алтарь, нужно убавить время");
        }
    }
}
