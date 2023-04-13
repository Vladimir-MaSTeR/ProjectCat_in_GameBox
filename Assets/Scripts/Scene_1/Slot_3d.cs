using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_3d : MonoBehaviour, IDropHandler, IPointerDownHandler { 


    [Header("������������� �����")]
    [SerializeField]
    private int _ID;

    #region ��� ������� SelectedAndMeargItem
    [Header("������ �� ������� ������ SelectedAndMeargItem")]

    [Tooltip("��� ������� SelectedAndMeargItem")]
    [SerializeField]
    private SelectedAndMeargItem _selectedAndMeargItem;
    #endregion

    private bool _selectedSlot = false;

    private void OnEnable() {
        //MeargGameEvents.onStartGame += Game; // ������ ����� ����� ����������.
        //MeargGameEvents.onStartEventSetOldObject += StartEventSetOldObject; // ����� - �������
    }

    private void OnDisable() {
        //MeargGameEvents.onStartGame -= Game;
        //MeargGameEvents.onStartEventSetOldObject -= StartEventSetOldObject;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Game(this.gameObject.GetComponentInChildren<CanvasGroup>());
    }

    private void Game(CanvasGroup �anvasGroupObject) {
        //Debug.Log($"���� �� ������");

        //var currentRuns = this.gameObject.GetComponentInChildren<CanvasGroup>();
        CanvasGroup currentRuns = �anvasGroupObject;
        // �������� ������ ���� � ���� 
        GameObject oldObject = null;
        Slot_3d oldSlot = null;

        if(currentRuns != null) { // ���� �������� �� ����
            SelectSlot(this.gameObject); // �������� ���� � �����
            //MeargGameEvents.onSelectedSlot?.Invoke(_ID, currentRuns.gameObject); // ���������� ����� ���� �������
            _selectedAndMeargItem.CheckSelectedInSlot(_ID, currentRuns.gameObject);

            // �������� ������ ���� � ���� 
            //oldObject = MeargGameEvents.onGetOldObject?.Invoke();
            oldObject = _selectedAndMeargItem.GetOldGameObject();
            //Debug.Log($"�����--Game: ������� ����� ��������� ������� ������� = {oldObject}");
            //oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();
            oldSlot = _selectedAndMeargItem.GetOldSlot();
            //Debug.Log($"�����--Game: ������� ����� ��������� ������� ����� = {oldSlot}");

            // ���� ������ ���� ��������� � �� ����� �������� ����� �� ��������� ��� ��������
            if(oldSlot != null && oldSlot.gameObject != this.gameObject) {
                DeselectSlot(oldSlot.gameObject);
                //Debug.Log($"�������� �������� ����{oldSlot}");
            }

            // ���� ������ ������ ���������� ��� ������ ��� ����� ���� �������� ������� ��� ���������
            if(oldObject != null) {
                //�������� ���� ���
                var oldRuneTag = oldObject.tag;
                var currentRuneTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;

                //Debug.Log($"��� ��������� ���� = {oldRuneTag}");
                //Debug.Log($"��� ���� � ������� ����� ���������� ������� = {currentRuneTag}");

                //���� ���� �� ����� ������ �� ����� �������� �������.
                if(oldRuneTag != currentRuneTag) {
                    Debug.Log($"�����--Game: ���� ������� ����� ������ �������");

                    var oldPositionTransform = oldObject.transform;
                    var oldSlotPosition = gameObject.GetComponentInChildren<CanvasGroup>().gameObject.transform;

                    var otherItemTransform = oldPositionTransform;
                    otherItemTransform.SetParent(transform);
                    otherItemTransform.localPosition = Vector3.zero;
                    //Debug.Log($"���������� ���� � ������ ������� ��������");

                    var currentObject = oldSlotPosition;
                    currentObject.SetParent(oldSlot.transform);
                    currentObject.localPosition = Vector3.zero;
                    //Debug.Log($"���������� ���� � ������ ������");

                    //��������� �������� ������� � ������ ����� ����� ������������ ���
                    DeselectSlot(this.gameObject);
                    //Debug.Log($"������� ��������� ������� ������{this.gameObject}");
                    DeselectSlot(oldSlot.gameObject);
                    //Debug.Log($"������� ��������� ������ ������{oldSlot.gameObject}");

                    
                    _selectedAndMeargItem.ClearVariables();
                    _selectedAndMeargItem.ClearSlot();

                    //���� ���� ��������� �����, ������ ���� ����� ���������.
                } else {
                    Debug.Log($"�����--Game: ���� �� ������� ����� ��������");
                    var oldRuneId = oldObject.GetComponent<Item_3d>().GetItemId();
                    var currentRuneId = gameObject.GetComponentInChildren<Item_3d>().GetItemId();

                    if(oldRuneId != currentRuneId) {
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
                        Destroy(oldObject);
                        Destroy(this.gameObject.GetComponentInChildren<CanvasGroup>().gameObject);

                        InvokeEventSpavnItem(currentRuneTag);

                        //��������� �������� ������� ���� ����� ������
                        DeselectSlot(this.gameObject);

                        _selectedAndMeargItem.ClearVariables();
                        _selectedAndMeargItem.ClearSlot();
                    } else {
                        SoundsEvents.onNegativeMeargeSound?.Invoke();
                    }
                }
            }


        }

        if(currentRuns == null) { //���� �������� �� ������ ������
            
            //MeargGameEvents.onSelectedSlot?.Invoke(_ID, null);
            _selectedAndMeargItem.CheckSelectedInSlot(_ID, null);
            //Debug.Log($"�����--Game: �������� �� ������ ������");

            //oldObject = MeargGameEvents.onGetOldObject?.Invoke();
            oldObject = _selectedAndMeargItem.GetOldGameObject();
            //Debug.Log($"�����--Game: ������� ����� ��������� ������� ������� ��� ����� �� ������ ������ = {oldObject}");
            //oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();

            //oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();
            oldSlot = _selectedAndMeargItem.GetOldSlot();
            //Debug.Log($"�����--Game: ������� ����� ��������� ������� ����� ��� ����� �� ������ ������= {oldSlot}");

            if(oldObject != null) {
                DragObject(oldObject);
                DeselectSlot(oldSlot.gameObject);

                _selectedAndMeargItem.ClearVariables();
                _selectedAndMeargItem.ClearSlot();
            }
        }
    }

    #region ������ �������� �� ��������

    //public void OnPointerDown(PointerEventData eventData) {    

    //    if(gameObject.GetComponentInChildren<CanvasGroup>() == null) { //���� ������
    //        //Debug.Log($"E�� ���� �� ������ ������ � ���������������� = {_ID}");

    //        GameObject oldObject = MeargGameEvents.onGetOldObject?.Invoke();
    //        var oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();
    //        DragObject(oldObject);
    //        //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject); // 3 � ���
    //        MeargGameEvents.onClearVariables?.Invoke();
    //        //DeselectSlot(oldSlot.gameObject);

    //    } else {
    //        //SelectSlot(this.gameObject);

    //        GameObject oldObject = MeargGameEvents.onGetOldObject?.Invoke();
    //        var oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();

    //        //Debug.Log($"���������222 ��������� ���� = {oldSlot2}");

    //        if(oldObject != null) {
    //            var oldRuneTag = oldObject.tag;
    //            var currentRuneTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;

    //            //Debug.Log($"��� ��������� ���� = {oldRuneTag}");
    //            //Debug.Log($"��� ���� � ������� ����� ���������� ������� = {currentRuneTag}");

    //            if(oldRuneTag != currentRuneTag) {

    //                var oldPosition = oldObject.transform;

    //                var oldSlotPosition = gameObject.GetComponentInChildren<CanvasGroup>().gameObject.transform;

    //                var otherItemTransform = oldPosition;
    //                otherItemTransform.SetParent(transform);
    //                otherItemTransform.localPosition = Vector3.zero;

    //                var currentObject = oldSlotPosition;
    //                currentObject.SetParent(oldSlot.transform);
    //                currentObject.localPosition = Vector3.zero;

    //                //DeselectSlot(oldSlot.gameObject);
    //                //DeselectSlot(this.gameObject);
    //                MeargGameEvents.onClearVariables?.Invoke();

    //                //FindAllMath(oldObject); // 3 � ��� //�� ��������� ���������� ���� ����� ��� � ������ ����
    //                //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject); // 3 � ���
    //                //FindAllMath(oldSlot.GetComponentInChildren<CanvasGroup>().gameObject); // 3 � ��� //�� ���������

    //            } else {
    //                var oldRuneId = oldObject.GetComponent<Item_3d>().GetItemId();
    //                var currentRuneId = gameObject.GetComponentInChildren<Item_3d>().GetItemId();

    //                if(oldRuneId != currentRuneId) {
    //                    //����� 3�
    //                    /**
    //                     * ���� ���������: ������� ����� ������� ID. �������������� ���  ������, 
    //                     * � ���� ������ ������� ���� ������� ������ � ���� ������� � ������� �����,
    //                     * ����� �������� ����� ��� ������ ����� ���� � �����������: 
    //                     *      � ����� ����� ���������(ID)
    //                     *      ����� ������� ���� ����������(�������� :) )
    //                     *      ����� ���� ���������� (���)
    //                     *      
    //                     *      ������������!!!
    //                     * **/


    //                    SoundsEvents.onPositiveMeargeSound?.Invoke();
    //                    //Destroy(eventData.pointerDrag);
    //                    Destroy(oldObject);
    //                    MeargGameEvents.onGetOldObject?.Invoke();
    //                    MeargGameEvents.onClearVariables?.Invoke();
    //                    Destroy(this.gameObject.GetComponentInChildren<CanvasGroup>().gameObject);



    //                    InvokeEventSpavnItem(currentRuneTag);
    //                    //DeselectSlot(oldSlot.gameObject);
    //                    //DeselectSlot(this.gameObject);
    //                } else {
    //                    SoundsEvents.onNegativeMeargeSound?.Invoke();
    //                }
    //            }
    //        }

    //        GameObject pointerObject = gameObject.GetComponentInChildren<CanvasGroup>().gameObject;
    //        MeargGameEvents.onSelectedSlot?.Invoke(_ID, pointerObject);
    //    }

    //}

    public void OnDrop(PointerEventData eventData) {
    //    if(ResourcesTags.Spark.ToString() != eventData.pointerDrag.tag) {

    //        if(gameObject.GetComponentInChildren<CanvasGroup>() == null) {
    //            //var otherItemTransform = eventData.pointerDrag.transform;
    //            //otherItemTransform.SetParent(transform);                    // ������ � ������� ���� �������� ��������         
    //            //otherItemTransform.localPosition = Vector3.zero;            // � �������� ��� �������


    //            //// ��� � ���
    //            //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject);
    //        } else {

    //            var parentTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;
    //            var childrenTag = eventData.pointerDrag.tag;
    //            //Debug.Log($"������������ ��� = {parentTag}");
    //            //Debug.Log($"��� �������� = {childrenTag}");

    //            var parentId = gameObject.GetComponentInChildren<Item_3d>().GetItemId();
    //            var childrenId = eventData.pointerDrag.GetComponentInChildren<Item_3d>().GetItemId();

    //            if(parentTag == ResourcesTags.Log_3.ToString() || parentTag == ResourcesTags.Cloth_3.ToString()
    //             || parentTag == ResourcesTags.Stone_3.ToString() || parentTag == ResourcesTags.Neil_3.ToString()) {
    //                return;
    //            }

    //            if(parentTag == childrenTag && parentId != childrenId) {

    //                //����� 3�
    //                /**
    //                 * ���� ���������: ������� ����� ������� ID. �������������� ���  ������, 
    //                 * � ���� ������ ������� ���� ������� ������ � ���� ������� � ������� �����,
    //                 * ����� �������� ����� ��� ������ ����� ���� � �����������: 
    //                 *      � ����� ����� ���������(ID)
    //                 *      ����� ������� ���� ����������(�������� :) )
    //                 *      ����� ���� ���������� (���)
    //                 *      
    //                 *      ������������!!!
    //                 * **/


    //                SoundsEvents.onPositiveMeargeSound?.Invoke();
    //                Destroy(eventData.pointerDrag);
    //                Destroy(this.gameObject.GetComponentInChildren<CanvasGroup>().gameObject);

    //                MeargGameEvents.onClearVariables?.Invoke();
    //                MeargGameEvents.onGetOldObject?.Invoke();

    //                InvokeEventSpavnItem(childrenTag);

    //            } else {
    //                SoundsEvents.onNegativeMeargeSound?.Invoke();
    //            }


    //        }
    //    }

    }

    #endregion

    private void InvokeEventSpavnItem(string tag) {
        if(ResourcesTags.Cloth_1.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Cloth_2.ToString(), _ID);
        }

        if(ResourcesTags.Cloth_2.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Cloth_3.ToString(), _ID);
        }
        if(ResourcesTags.Cloth_3.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Cloth_4.ToString(), _ID);
        }

        if(ResourcesTags.Log_1.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Log_2.ToString(), _ID);
        }

        if(ResourcesTags.Log_2.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Log_3.ToString(), _ID);
        }
        if(ResourcesTags.Log_3.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Log_4.ToString(), _ID);
        }

        if(ResourcesTags.Neil_1.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Neil_2.ToString(), _ID);
        }

        if(ResourcesTags.Neil_2.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Neil_3.ToString(), _ID);
        }
        if(ResourcesTags.Neil_3.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Neil_3.ToString(), _ID);
        }

        if(ResourcesTags.Stone_1.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Stone_2.ToString(), _ID);
        }

        if(ResourcesTags.Stone_2.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Stone_3.ToString(), _ID);
        }

        if(ResourcesTags.Stone_3.ToString().Equals(tag)) {
            EventsResources.onSpawnItemToSlot?.Invoke(ResourcesTags.Stone_4.ToString(), _ID);
        }
    }
    
    public int GetSlotID() {
        return _ID;
    }

    public bool GetSelected() {
        return _selectedSlot;
    }

    public void SetSelected(bool value) {
        _selectedSlot = value;
    }

    public void SelectSlot(GameObject slot) {
        slot.GetComponentInChildren<Slot_3d>().SetSelected(true);
        slot.GetComponent<Image>().color = new Vector4(79 / 255.0f, 165 / 255.0f, 63 / 255.0f, 0.3f);
    }

    public void DeselectSlot(GameObject gameObject) {
        gameObject.GetComponent<Image>().color = new Vector4(0 / 255.0f, 0 / 255.0f, 0 / 255.0f, 0f);
        gameObject.GetComponentInChildren<Slot_3d>().SetSelected(false);
    }

    public void DragObject(GameObject oldgameObject) {
        if(oldgameObject != null) {
            var otherItemTransform = oldgameObject.transform;
            otherItemTransform.SetParent(transform);                    // ������ � ������� ���� �������� ��������         
            otherItemTransform.localPosition = Vector3.zero;            // � �������� ��� �������


            //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject); // 3 � ���

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

        RaycastHit2D hit2D = Physics2D.Raycast(eventData.transform.position, vector, laserLength);

        Ray ray = new Ray(eventData.transform.position, vector);
        Debug.DrawRay(transform.position, vector * laserLength, Color.red);

        var parentTag = "1";
        var childrenTag = "2";

        var parentId = 1;
        var childrenId = 1;

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, laserLength)) {
            //Debug.Log("���� �� ���-�� �����");

            parentTag = eventData.GetComponentInChildren<CanvasGroup>().tag;
            childrenTag = hit.collider.gameObject.GetComponent<CanvasGroup>().tag;

            parentId = eventData.GetComponentInChildren<Item_3d>().GetItemId();
            childrenId = hit.collider.gameObject.GetComponent<Item_3d>().GetItemId();

            //Debug.Log($"parentTag = {parentTag}");
            //Debug.Log($"childrenTag = {childrenTag}");

            //Debug.Log($"parentId = {parentId}");
            //Debug.Log($"childrenId = {childrenId}");

            while(parentTag == childrenTag && parentId != childrenId) {
                cashFindTiles.Add(hit.collider.gameObject);

                //Debug.Log($"� ����� ���� � �� �� ����� ����� ��������");
                //Debug.Log($"���������� ���������� � ������");
                //Debug.Log($"������ �������� = {cashFindTiles.Count}");

                ray = new Ray(hit.collider.gameObject.transform.position, vector);
                if(Physics.Raycast(ray, out hit, laserLength)) {
                    childrenTag = hit.collider.gameObject.GetComponent<CanvasGroup>().tag;
                    childrenId = hit.collider.gameObject.GetComponent<Item_3d>().GetItemId();
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
            //Debug.Log($"�������� �� �� ����� ����� ��������");
            cashFindList.AddRange(FindMatch(eventData, vectorArray[i]));
        }

        if(cashFindList.Count >= 2) {
            UpdateSparks(cashFindList.Count);

            for(int i = 0; i < cashFindList.Count; i++) {
                //Debug.Log($"��� �� �� ����� ����� ��������");
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
