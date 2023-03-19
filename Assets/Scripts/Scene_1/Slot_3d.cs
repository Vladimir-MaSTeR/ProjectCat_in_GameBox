using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_3d : MonoBehaviour, IDropHandler { 


    [Header("Идентификатор слота")]
    [SerializeField]
    private int _ID;


    public void OnDrop(PointerEventData eventData) {
        if(ResourcesTags.Spark.ToString() != eventData.pointerDrag.tag) {

            if(gameObject.GetComponentInChildren<CanvasGroup>() == null) {
                var otherItemTransform = eventData.pointerDrag.transform;
                otherItemTransform.SetParent(transform);                    // Ставим в текущий слот назначая родителя         
                otherItemTransform.localPosition = Vector3.zero;            // И обнуляем его позицию


                // три в ряд
                FindAllMath(eventData);
            } else {

                var parentTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;
                var childrenTag = eventData.pointerDrag.tag;
                //Debug.Log($"Родительский тег = {parentTag}");
                //Debug.Log($"Тег предмета = {childrenTag}");

                var parentId = gameObject.GetComponentInChildren<Item_3d>().GetItemId();
                var childrenId = eventData.pointerDrag.GetComponentInChildren<Item_3d>().GetItemId();

                if(parentTag == ResourcesTags.Log_3.ToString() || parentTag == ResourcesTags.Cloth_3.ToString()
                 || parentTag == ResourcesTags.Stone_3.ToString() || parentTag == ResourcesTags.Neil_3.ToString()) {
                    return;
                }

                if(parentTag == childrenTag && parentId != childrenId) {

                    //МЕРДЖ 3д
                    /**
                     * Идея следующая: Каждому слоту сделать ID. Соответственно при  мердже, 
                     * в этом классе удалять руну которую тащили и руну стоящую в текущем слоте,
                     * затем посылать ивент для спавна новой руны с информацией: 
                     *      в каком слоте заспанить(ID)
                     *      какой уровень руны заспавнить(подумать :) )
                     *      какую руну заспавнить (таг)
                     *      
                     *      РЕАЛИЗОВАННО!!!
                     * **/


                    SoundsEvents.onPositiveMeargeSound?.Invoke();
                    Destroy(eventData.pointerDrag);
                    Destroy(this.gameObject.GetComponentInChildren<CanvasGroup>().gameObject);

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

    private List<GameObject> FindMatch(PointerEventData eventData, Vector2 vector) {

        List<GameObject> cashFindTiles = new List<GameObject>();
        float laserLength = 1.3f;

        //RaycastHit2D hit2D = Physics2D.Raycast(eventData.pointerDrag.transform.position, vector, laserLength); 2d

        Ray ray = new Ray(eventData.pointerDrag.transform.position, vector);
        Debug.DrawRay(transform.position, vector * laserLength, Color.red);

        var parentTag = "1";
        var childrenTag = "2";

        var parentId = 1;
        var childrenId = 1;

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, laserLength)) {
            Debug.Log("Лучь во что-то попал");

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

                Debug.Log($"Совпадение добавленно в список");
                Debug.Log($"Список размером = {cashFindTiles.Count}");

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

    private void DeleteSprite(PointerEventData eventData, Vector2[] vectorArray) {
        List<GameObject> cashFindList = new List<GameObject>();

        for(int i = 0; i < vectorArray.Length; i++) {
            cashFindList.AddRange(FindMatch(eventData, vectorArray[i]));
        }

        if(cashFindList.Count >= 2) {
            UpdateSparks(cashFindList.Count);

            for(int i = 0; i < cashFindList.Count; i++) {
                //отправлять евент на сбор рун
                AddResouces(cashFindList[i].gameObject.tag);
                Destroy(cashFindList[i].gameObject);
            }

            AddResouces(eventData.pointerDrag.gameObject.tag);
            Destroy(eventData.pointerDrag.gameObject);          
        }
    }

    private void FindAllMath(PointerEventData eventData) {
        //Debug.Log($"НАЧАТ ПОИСК СОВПАДЕНИЙ СВЕРХУ И СНИЗУ");
        DeleteSprite(eventData, new Vector2[2] { Vector2.up, Vector2.down });

        //Debug.Log($"НАЧАТ ПОИСК СОВПАДЕНИЙ С ЛЕВА И С ПРАВА");
        DeleteSprite(eventData, new Vector2[2] { Vector2.left, Vector2.right });

        //Destroy(eventData.pointerDrag);
    }

    private void UpdateSparks(int count) {
        if(count == 2) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(1, true);
            Debug.Log($"Увеличиваем искорки на {1}");
        }
        if(count == 3) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(3, true);
            Debug.Log($"Увеличиваем искорки на {3}");
        }
        if(count == 4) {
            EventsResources.onAddOrDeductSparkValue?.Invoke(5, true);
            Debug.Log($"Увеличиваем искорки на {5}");
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
