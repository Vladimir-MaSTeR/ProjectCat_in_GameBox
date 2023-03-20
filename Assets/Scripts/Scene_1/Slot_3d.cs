using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_3d : MonoBehaviour, IDropHandler, IPointerDownHandler { 


    [Header("������������� �����")]
    [SerializeField]
    private int _ID;

    public void OnPointerDown(PointerEventData eventData) {    

        if(gameObject.GetComponentInChildren<CanvasGroup>() == null) { //���� ������
            //Debug.Log($"E�� ���� �� ������ ������ � ���������������� = {_ID}");

            //GameObject oldObject = MeargGameEvents.onGetOldObject?.Invoke();
            //if(oldObject != null) {
            //    Debug.Log($"��� ���������� ���� =  {oldObject.tag}");
            //}
            GameObject oldObject = MeargGameEvents.onGetOldObject?.Invoke();
            DragObject(oldObject);
            MeargGameEvents.onClearVariables?.Invoke();
           
        } else {
            GameObject oldObject = MeargGameEvents.onGetOldObject?.Invoke();
            var oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();

            //var oldSlot2 = oldObject.GetComponentInParent<Slot_3d>().gameObject.transform;
            //if(oldSlot) {
            //    Debug.Log($"��������� ��������� ���� = {oldSlot.transform}");
            //}

            //Debug.Log($"���������222 ��������� ���� = {oldSlot2}");

            if(oldObject != null) {
                var oldRuneTag = oldObject.tag;
                var currentRuneTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;

                //Debug.Log($"��� ��������� ���� = {oldRuneTag}");
                //Debug.Log($"��� ���� � ������� ����� ���������� ������� = {currentRuneTag}");

                if(oldRuneTag != currentRuneTag) {

                    var oldPosition = oldObject.transform;

                    var oldSlotPosition = gameObject.GetComponentInChildren<CanvasGroup>().gameObject.transform;

                    var otherItemTransform = oldPosition;
                    otherItemTransform.SetParent(transform);
                    otherItemTransform.localPosition = Vector3.zero;

                    var currentObject = oldSlotPosition;
                    currentObject.SetParent(oldSlot.transform);
                    currentObject.localPosition = Vector3.zero;


                   //gameObject.GetComponent<Image>().color = new Vector4(0 / 255.0f, 0 / 255.0f, 0 / 255.0f, 0f);
                    //MeargGameEvents.onClearVariables?.Invoke();

                    FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject); // 3 � ���
                }
            }

            GameObject pointerObject = gameObject.GetComponentInChildren<CanvasGroup>().gameObject;
            MeargGameEvents.onSelectedSlot?.Invoke(_ID, pointerObject);
        }

    }
    public void OnDrop(PointerEventData eventData) {
        if(ResourcesTags.Spark.ToString() != eventData.pointerDrag.tag) {

            if(gameObject.GetComponentInChildren<CanvasGroup>() == null) {
                //var otherItemTransform = eventData.pointerDrag.transform;
                //otherItemTransform.SetParent(transform);                    // ������ � ������� ���� �������� ��������         
                //otherItemTransform.localPosition = Vector3.zero;            // � �������� ��� �������


                //// ��� � ���
                //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject);
            } else {

                var parentTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;
                var childrenTag = eventData.pointerDrag.tag;
                //Debug.Log($"������������ ��� = {parentTag}");
                //Debug.Log($"��� �������� = {childrenTag}");

                var parentId = gameObject.GetComponentInChildren<Item_3d>().GetItemId();
                var childrenId = eventData.pointerDrag.GetComponentInChildren<Item_3d>().GetItemId();

                if(parentTag == ResourcesTags.Log_3.ToString() || parentTag == ResourcesTags.Cloth_3.ToString()
                 || parentTag == ResourcesTags.Stone_3.ToString() || parentTag == ResourcesTags.Neil_3.ToString()) {
                    return;
                }

                if(parentTag == childrenTag && parentId != childrenId) {

                    //����� 3�
                    /**
                     * ���� ���������: ������� ����� ������� ID. �������������� ���  ������, 
                     * � ���� ������ ������� ���� ������� ������ � ���� ������� � ������� �����,
                     * ����� �������� ����� ��� ������ ����� ���� � �����������: 
                     *      � ����� ����� ���������(ID)
                     *      ����� ������� ���� ����������(�������� :) )
                     *      ����� ���� ���������� (���)
                     *      
                     *      ������������!!!
                     * **/


                    SoundsEvents.onPositiveMeargeSound?.Invoke();
                    Destroy(eventData.pointerDrag);
                    Destroy(this.gameObject.GetComponentInChildren<CanvasGroup>().gameObject);

                    MeargGameEvents.onClearVariables?.Invoke();
                    MeargGameEvents.onGetOldObject?.Invoke();

                    InvokeEventSpavnItem(childrenTag);

                } else {
                    SoundsEvents.onNegativeMeargeSound?.Invoke();
                }


            }
        }

    }

    private void InvokeEventSpavnItem(string tag) {
        if(ResourcesTags.Cloth_1.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Cloth_2.ToString(), _ID);
        }

        if(ResourcesTags.Cloth_2.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Cloth_3.ToString(), _ID);
        }

        if(ResourcesTags.Log_1.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Log_2.ToString(), _ID);
        }

        if(ResourcesTags.Log_2.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Log_3.ToString(), _ID);
        }

        if(ResourcesTags.Neil_1.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Neil_2.ToString(), _ID);
        }

        if(ResourcesTags.Neil_2.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Neil_3.ToString(), _ID);
        }

        if(ResourcesTags.Stone_1.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Stone_2.ToString(), _ID);
        }

        if(ResourcesTags.Stone_2.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Stone_3.ToString(), _ID);
        }
    }

    public int GetSlotID() {
        return _ID;
    }

    public void SelectSlot() {
        this.gameObject.GetComponent<Image>().color = new Vector4(79 / 255.0f, 165 / 255.0f, 63 / 255.0f, 0.3f);
    }

    public void DeselectSlot() {
        this.gameObject.GetComponent<Image>().color = new Vector4(0 / 255.0f, 0 / 255.0f, 0 / 255.0f, 0f);
    }

    public void DragObject(GameObject oldgameObject) {
        if(oldgameObject != null) {
            var otherItemTransform = oldgameObject.transform;
            otherItemTransform.SetParent(transform);                    // ������ � ������� ���� �������� ��������         
            otherItemTransform.localPosition = Vector3.zero;            // � �������� ��� �������

            
            FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject); // 3 � ���
            
        }
    }

    private void CheckResorces(string parentTag, string oneTag, string twoTag, string threeTag, Sprite sp_2, Sprite sp_3) {
        if(parentTag == oneTag) {
            gameObject.GetComponentInChildren<Item>().GetComponentInChildren<Image>().sprite = sp_2;
            gameObject.GetComponentInChildren<CanvasGroup>().tag = twoTag;
        }

        if(parentTag == twoTag) {
            gameObject.GetComponentInChildren<Item>().GetComponentInChildren<Image>().sprite = sp_3;
            gameObject.GetComponentInChildren<CanvasGroup>().tag = threeTag;
        }
    }

    
    private List<GameObject> FindMatch(GameObject eventData, Vector2 vector) {

        List<GameObject> cashFindTiles = new List<GameObject>();
        float laserLength = 1.5f;

        //RaycastHit2D hit2D = Physics2D.Raycast(eventData.pointerDrag.transform.position, vector, laserLength); 2d

        Ray ray = new Ray(eventData.transform.position, vector);
        Debug.DrawRay(transform.position, vector * laserLength, Color.red);

        var parentTag = "1";
        var childrenTag = "2";

        var parentId = 1;
        var childrenId = 1;

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, laserLength)) {
            //Debug.Log("���� �� ���-�� �����");

            parentTag = this.gameObject.GetComponentInChildren<CanvasGroup>().tag;
            childrenTag = hit.collider.gameObject.GetComponent<CanvasGroup>().tag;

            parentId = this.gameObject.GetComponentInChildren<Item_3d>().GetItemId();
            childrenId = hit.collider.gameObject.GetComponent<Item_3d>().GetItemId();

            //Debug.Log($"parentTag = {parentTag}");
            //Debug.Log($"childrenTag = {childrenTag}"); 

            //Debug.Log($"parentId = {parentId}");
            //Debug.Log($"childrenId = {childrenId}");



            while(parentTag == childrenTag && parentId != childrenId) {
                cashFindTiles.Add(hit.collider.gameObject);

                Debug.Log($"���������� ���������� � ������");
                Debug.Log($"������ �������� = {cashFindTiles.Count}");

                ray = new Ray(hit.collider.gameObject.transform.position, vector);
                if(Physics.Raycast(ray, out hit, laserLength)) {
                    childrenTag = hit.collider.gameObject.GetComponent<CanvasGroup>().tag;
                } else {
                    childrenTag = "2";
                }
            }
        }

        return cashFindTiles;
    }

    private void DeleteSprite(GameObject eventData, Vector2[] vectorArray) {
        List<GameObject> cashFindList = new List<GameObject>();

        for(int i = 0; i < vectorArray.Length; i++) {
            cashFindList.AddRange(FindMatch(eventData, vectorArray[i]));
        }

        if(cashFindList.Count >= 2) {
            UpdateSparks(cashFindList.Count);

            for(int i = 0; i < cashFindList.Count; i++) {
                //���������� ����� �� ���� ���
                AddResouces(cashFindList[i].gameObject.tag);
                Destroy(cashFindList[i].gameObject);
            }

            AddResouces(eventData.tag);
            Destroy(eventData);          
        }
    }

    private void FindAllMath(GameObject eventData) {
        //Debug.Log($"����� ����� ���������� ������ � �����");
        DeleteSprite(eventData, new Vector2[2] { Vector2.up, Vector2.down });

        //Debug.Log($"����� ����� ���������� � ���� � � �����");
        DeleteSprite(eventData, new Vector2[2] { Vector2.left, Vector2.right });

        //Destroy(eventData.pointerDrag);
    }

    private void UpdateSparks(int count) {
        if(count == 2) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, true);
            Debug.Log($"����������� ������� �� {1}");
        }
        if(count == 3) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(3, true);
            Debug.Log($"����������� ������� �� {3}");
        }
        if(count == 4) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(5, true);
            Debug.Log($"����������� ������� �� {5}");
        }
    }

    private void AddResouces(string tag) {
        if(ResourcesTags.Cloth_1.ToString() == tag) {
            EventsResources.onClouthInBucket?.Invoke(1, 1, 1);
        }
        if(ResourcesTags.Cloth_2.ToString() == tag) {
            EventsResources.onClouthInBucket?.Invoke(2, 1, 1);
        }
        if(ResourcesTags.Cloth_3.ToString() == tag) {
            EventsResources.onClouthInBucket?.Invoke(3, 1, 1);
        }

        if(ResourcesTags.Log_1.ToString() == tag) {
            EventsResources.onLogInBucket?.Invoke(1, 1, 1);
        }
        if(ResourcesTags.Log_2.ToString() == tag) {
            EventsResources.onLogInBucket?.Invoke(2, 1, 1);
        }
        if(ResourcesTags.Log_3.ToString() == tag) {
            EventsResources.onLogInBucket?.Invoke(3, 1, 1);
        }

        if(ResourcesTags.Neil_1.ToString() == tag) {
            EventsResources.onNeilInBucket?.Invoke(1, 1, 1);
        }
        if(ResourcesTags.Neil_2.ToString() == tag) {
            EventsResources.onNeilInBucket?.Invoke(2, 1, 1);
        }
        if(ResourcesTags.Neil_3.ToString() == tag) {
            EventsResources.onNeilInBucket?.Invoke(3, 1, 1);
        }

        if(ResourcesTags.Stone_1.ToString() == tag) {
            EventsResources.onStoneInBucket?.Invoke(1, 1, 1);
        }
        if(ResourcesTags.Stone_2.ToString() == tag) {
            EventsResources.onStoneInBucket?.Invoke(2, 1, 1);
        }
        if(ResourcesTags.Stone_3.ToString() == tag) {
            EventsResources.onStoneInBucket?.Invoke(3, 1, 1);
        }

    }

   
}
