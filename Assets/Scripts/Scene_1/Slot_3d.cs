using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot_3d : MonoBehaviour, IDropHandler, IPointerDownHandler { 


    [Header("Идентификатор слота")]
    [SerializeField]
    private int _ID;

    #region Для скрипта SelectedAndMeargItem
    [Header("Объктн на котором скрипт SelectedAndMeargItem")]

    [Tooltip("Для скрипта SelectedAndMeargItem")]
    [SerializeField]
    private SelectedAndMeargItem _selectedAndMeargItem;
    #endregion

    private bool _selectedSlot = false;

    private void OnEnable() {
        //MeargGameEvents.onStartGame += Game; // скорей всего стало рудиментом.
        //MeargGameEvents.onStartEventSetOldObject += StartEventSetOldObject; // проба - удалить
    }

    private void OnDisable() {
        //MeargGameEvents.onStartGame -= Game;
        //MeargGameEvents.onStartEventSetOldObject -= StartEventSetOldObject;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Game(this.gameObject.GetComponentInChildren<CanvasGroup>());
    }

    private void Game(CanvasGroup сanvasGroupObject) {
        //Debug.Log($"Клик по клетке");

        //var currentRuns = this.gameObject.GetComponentInChildren<CanvasGroup>();
        CanvasGroup currentRuns = сanvasGroupObject;
        // получаем старые руну и слот 
        GameObject oldObject = null;
        Slot_3d oldSlot = null;

        if(currentRuns != null) { // если кликнули по руне
            SelectSlot(this.gameObject); // выделяем слот с руной
            //MeargGameEvents.onSelectedSlot?.Invoke(_ID, currentRuns.gameObject); // запоминаем какую руну выбрали
            _selectedAndMeargItem.CheckSelectedInSlot(_ID, currentRuns.gameObject);

            // получаем старые руну и слот 
            //oldObject = MeargGameEvents.onGetOldObject?.Invoke();
            oldObject = _selectedAndMeargItem.GetOldGameObject();
            //Debug.Log($"МЕТОД--Game: ВЫЗВАЛИ ЭВЕНТ ПОЛУЧЕНИЯ СТАРОГО ОБЪЕКТА = {oldObject}");
            //oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();
            oldSlot = _selectedAndMeargItem.GetOldSlot();
            //Debug.Log($"МЕТОД--Game: ВЫЗВАЛИ ЭВЕНТ ПОЛУЧЕНИЯ СТАРОГО СЛОТА = {oldSlot}");

            // если старый слот сущесвует и не равен текущему слоту то перестаем его выделять
            if(oldSlot != null && oldSlot.gameObject != this.gameObject) {
                DeselectSlot(oldSlot.gameObject);
                //Debug.Log($"Перестал выделять слот{oldSlot}");
            }

            // если старый объект существует это значит что нужно руны поменять местами или смерджить
            if(oldObject != null) {
                //Получаем теги рун
                var oldRuneTag = oldObject.tag;
                var currentRuneTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;

                //Debug.Log($"Тег выбранной руны = {oldRuneTag}");
                //Debug.Log($"Тег руны с которой хотим поменяться местами = {currentRuneTag}");

                //Если теги не равны значит их нужно поменять местами.
                if(oldRuneTag != currentRuneTag) {
                    Debug.Log($"МЕТОД--Game: ТЕГИ СОВПАЛИ БЕДЕМ МЕНЯТЬ МЕСТАМИ");

                    var oldPositionTransform = oldObject.transform;
                    var oldSlotPosition = gameObject.GetComponentInChildren<CanvasGroup>().gameObject.transform;

                    var otherItemTransform = oldPositionTransform;
                    otherItemTransform.SetParent(transform);
                    otherItemTransform.localPosition = Vector3.zero;
                    //Debug.Log($"Переставил руну в клетку которую кликнули");

                    var currentObject = oldSlotPosition;
                    currentObject.SetParent(oldSlot.transform);
                    currentObject.localPosition = Vector3.zero;
                    //Debug.Log($"Переставил руну в старую клетку");

                    //перестаем выделять текущий и старый слоты после перестановки рун
                    DeselectSlot(this.gameObject);
                    //Debug.Log($"Отменил выделение текущей клетки{this.gameObject}");
                    DeselectSlot(oldSlot.gameObject);
                    //Debug.Log($"Отменил выделение старой клетки{oldSlot.gameObject}");

                    
                    _selectedAndMeargItem.ClearVariables();
                    _selectedAndMeargItem.ClearSlot();

                    //Если теги оказались равны, значит руны нужно смерджить.
                } else {
                    Debug.Log($"МЕТОД--Game: ТЕГИ НЕ СОВПАЛИ БУДЕМ МЕРДЖИТЬ");
                    var oldRuneId = oldObject.GetComponent<Item_3d>().GetItemId();
                    var currentRuneId = gameObject.GetComponentInChildren<Item_3d>().GetItemId();

                    if(oldRuneId != currentRuneId) {
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
                        Destroy(oldObject);
                        Destroy(this.gameObject.GetComponentInChildren<CanvasGroup>().gameObject);

                        InvokeEventSpavnItem(currentRuneTag);

                        //перестаем выделять текущий слот после мерджа
                        DeselectSlot(this.gameObject);

                        _selectedAndMeargItem.ClearVariables();
                        _selectedAndMeargItem.ClearSlot();
                    } else {
                        SoundsEvents.onNegativeMeargeSound?.Invoke();
                    }
                }
            }


        }

        if(currentRuns == null) { //если кликнули по пустой ячейке
            
            //MeargGameEvents.onSelectedSlot?.Invoke(_ID, null);
            _selectedAndMeargItem.CheckSelectedInSlot(_ID, null);
            //Debug.Log($"МЕТОД--Game: КЛИКНУЛИ ПО ПУСТОЙ КЛЕТКЕ");

            //oldObject = MeargGameEvents.onGetOldObject?.Invoke();
            oldObject = _selectedAndMeargItem.GetOldGameObject();
            //Debug.Log($"МЕТОД--Game: ВЫЗВАЛИ ЭВЕНТ ПОЛУЧЕНИЯ СТАРОГО ОБЪЕКТА ПРИ КЛИКЕ НА ПУСТУЮ КЛЕТКУ = {oldObject}");
            //oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();

            //oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();
            oldSlot = _selectedAndMeargItem.GetOldSlot();
            //Debug.Log($"МЕТОД--Game: ВЫЗВАЛИ ЭВЕНТ ПОЛУЧЕНИЯ СТАРОГО СЛОТА ПРИ КЛИКЕ НА ПУСТУЮ КЛЕТКУ= {oldSlot}");

            if(oldObject != null) {
                DragObject(oldObject);
                DeselectSlot(oldSlot.gameObject);

                _selectedAndMeargItem.ClearVariables();
                _selectedAndMeargItem.ClearSlot();
            }
        }
    }

    #region СТАРОЕ КАНДИДАТ НА УДАЛЕНИЕ

    //public void OnPointerDown(PointerEventData eventData) {    

    //    if(gameObject.GetComponentInChildren<CanvasGroup>() == null) { //если пустая
    //        //Debug.Log($"Eть клик по пустой клетке с идентификатогром = {_ID}");

    //        GameObject oldObject = MeargGameEvents.onGetOldObject?.Invoke();
    //        var oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();
    //        DragObject(oldObject);
    //        //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject); // 3 в ряд
    //        MeargGameEvents.onClearVariables?.Invoke();
    //        //DeselectSlot(oldSlot.gameObject);

    //    } else {
    //        //SelectSlot(this.gameObject);

    //        GameObject oldObject = MeargGameEvents.onGetOldObject?.Invoke();
    //        var oldSlot = MeargGameEvents.onGetOldSlot?.Invoke();

    //        //Debug.Log($"Трансформ222 выбранной руны = {oldSlot2}");

    //        if(oldObject != null) {
    //            var oldRuneTag = oldObject.tag;
    //            var currentRuneTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;

    //            //Debug.Log($"Тег выбранной руны = {oldRuneTag}");
    //            //Debug.Log($"Тег руны с которой хотим поменяться местами = {currentRuneTag}");

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

    //                //FindAllMath(oldObject); // 3 в ряд //не сработало получилось тоже самое что и строка ниже
    //                //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject); // 3 в ряд
    //                //FindAllMath(oldSlot.GetComponentInChildren<CanvasGroup>().gameObject); // 3 в ряд //не сработало

    //            } else {
    //                var oldRuneId = oldObject.GetComponent<Item_3d>().GetItemId();
    //                var currentRuneId = gameObject.GetComponentInChildren<Item_3d>().GetItemId();

    //                if(oldRuneId != currentRuneId) {
    //                    //МЕРДЖ 3д
    //                    /**
    //                     * Идея следующая: Каждому слоту сделать ID. Соответственно при  мердже, 
    //                     * в этом классе удалять руну которую тащили и руну стоящую в текущем слоте,
    //                     * затем посылать ивент для спавна новой руны с информацией: 
    //                     *      в каком слоте заспанить(ID)
    //                     *      какой уровень руны заспавнить(подумать :) )
    //                     *      какую руну заспавнить (таг)
    //                     *      
    //                     *      РЕАЛИЗОВАННО!!!
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
    //            //otherItemTransform.SetParent(transform);                    // Ставим в текущий слот назначая родителя         
    //            //otherItemTransform.localPosition = Vector3.zero;            // И обнуляем его позицию


    //            //// три в ряд
    //            //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject);
    //        } else {

    //            var parentTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;
    //            var childrenTag = eventData.pointerDrag.tag;
    //            //Debug.Log($"Родительский тег = {parentTag}");
    //            //Debug.Log($"Тег предмета = {childrenTag}");

    //            var parentId = gameObject.GetComponentInChildren<Item_3d>().GetItemId();
    //            var childrenId = eventData.pointerDrag.GetComponentInChildren<Item_3d>().GetItemId();

    //            if(parentTag == ResourcesTags.Log_3.ToString() || parentTag == ResourcesTags.Cloth_3.ToString()
    //             || parentTag == ResourcesTags.Stone_3.ToString() || parentTag == ResourcesTags.Neil_3.ToString()) {
    //                return;
    //            }

    //            if(parentTag == childrenTag && parentId != childrenId) {

    //                //МЕРДЖ 3д
    //                /**
    //                 * Идея следующая: Каждому слоту сделать ID. Соответственно при  мердже, 
    //                 * в этом классе удалять руну которую тащили и руну стоящую в текущем слоте,
    //                 * затем посылать ивент для спавна новой руны с информацией: 
    //                 *      в каком слоте заспанить(ID)
    //                 *      какой уровень руны заспавнить(подумать :) )
    //                 *      какую руну заспавнить (таг)
    //                 *      
    //                 *      РЕАЛИЗОВАННО!!!
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
            otherItemTransform.SetParent(transform);                    // Ставим в текущий слот назначая родителя         
            otherItemTransform.localPosition = Vector3.zero;            // И обнуляем его позицию


            //FindAllMath(gameObject.GetComponentInChildren<CanvasGroup>().gameObject); // 3 в ряд

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
            //Debug.Log("Лучь во что-то попал");

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

                //Debug.Log($"А МОЖЕТ БЫТЬ И ИЗ ЗА ЭТОГО ЦИКЛА ЗАВИСАЕМ");
                //Debug.Log($"Совпадение добавленно в список");
                //Debug.Log($"Список размером = {cashFindTiles.Count}");

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
            //Debug.Log($"ВОЗМОЖНО ИЗ ЗА ЭТОГО ЦИКЛА ЗАВИСАЕМ");
            cashFindList.AddRange(FindMatch(eventData, vectorArray[i]));
        }

        if(cashFindList.Count >= 2) {
            UpdateSparks(cashFindList.Count);

            for(int i = 0; i < cashFindList.Count; i++) {
                //Debug.Log($"ИЛИ ИЗ ЗА ЭТОГО ЦИКЛА ЗАВИСАЕМ");
                //отправлять евент на сбор рун
                AddResouces(cashFindList[i].gameObject.tag);
                Destroy(cashFindList[i].gameObject);
            }

            AddResouces(eventData.tag);
            Destroy(eventData);          
        }
    }

    private void FindAllMath(GameObject eventData) {
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
