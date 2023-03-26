
using UnityEngine;
using UnityEngine.EventSystems;

public class Altar_2 : MonoBehaviour, IDropHandler
{
    #region ���������� ������
    [Header("���������")]

    [Tooltip("������������� ������")]
    [SerializeField]
    private int _id = 1;

    #endregion

    #region ��������� ����������
    private int _curent_id;
   
    #endregion

    private void Start() {
        _curent_id = _id;
    }


    public void OnDrop(PointerEventData eventData) {
        if(ResourcesTags.Spark.ToString() == eventData.pointerDrag.tag) {

            //���������� ������� �� ��� �����
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

            //�������� ����� ��������� ������� ������ ��� �� ������������ ��������
            float startTimeSpawnRuns = (float)(MeargGameEvents.onGetCurrentTimeSpawnOldColumn?.Invoke());
            MeargGameEvents.onSetTimeToSpawnRuns?.Invoke(startTimeSpawnRuns);

            //�������� ����� ���������� �������.
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, false);


            // Debug.Log($"����� ���������� �� ������, ����� ������� �����");
        }
    }
}
