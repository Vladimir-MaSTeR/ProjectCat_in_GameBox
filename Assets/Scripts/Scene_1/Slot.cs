using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Sprite _log_2_sprite;
    [SerializeField] private Sprite _log_3_sprite;

    [SerializeField] private Sprite _cloth_2_sprite;
    [SerializeField] private Sprite _cloth_3_sprite;

    [SerializeField] private Sprite _stone_2_sprite;
    [SerializeField] private Sprite _stone_3_sprite;

    [SerializeField] private Sprite _neil_2_sprite;
    [SerializeField] private Sprite _neil_3_sprite;

    public void OnDrop(PointerEventData eventData)
    {
        
        if (gameObject.GetComponentInChildren<CanvasGroup>() == null)
        {
            var otherItemTransform = eventData.pointerDrag.transform;
            otherItemTransform.SetParent(transform);                    // Ставим в текущий слот назначая родителя         
            otherItemTransform.localPosition = Vector3.zero;            // И обнуляем его позицию


            // три в ряд
            //FindAllMath(eventData);
        } else
        {

            var parentTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;
            var childrenTag = eventData.pointerDrag.tag;
            Debug.Log($"Родительский тег = {parentTag}");
            Debug.Log($"Тег предмета = {childrenTag}");

            var parentId = gameObject.GetComponentInChildren<Item>().GetItemId();
            var childrenId = eventData.pointerDrag.GetComponentInChildren<Item>().GetItemId();

            if (parentTag == ResourcesTags.Log_3.ToString() || parentTag == ResourcesTags.Cloth_3.ToString()
             || parentTag == ResourcesTags.Stone_3.ToString() || parentTag == ResourcesTags.Neil_3.ToString())
            {
                return;
            }

            if (parentTag == childrenTag && parentId != childrenId)
            {

                CheckResorces(parentTag, ResourcesTags.Log_1.ToString(), ResourcesTags.Log_2.ToString(), ResourcesTags.Log_3.ToString(), _log_2_sprite, _log_3_sprite);
                CheckResorces(parentTag, ResourcesTags.Cloth_1.ToString(), ResourcesTags.Cloth_2.ToString(), ResourcesTags.Cloth_3.ToString(), _cloth_2_sprite, _cloth_3_sprite);
                CheckResorces(parentTag, ResourcesTags.Stone_1.ToString(), ResourcesTags.Stone_2.ToString(), ResourcesTags.Stone_3.ToString(), _stone_2_sprite, _stone_3_sprite);
                CheckResorces(parentTag, ResourcesTags.Neil_1.ToString(), ResourcesTags.Neil_2.ToString(), ResourcesTags.Neil_3.ToString(), _neil_2_sprite, _neil_3_sprite);


                    var childAmount = eventData.pointerDrag.GetComponentInChildren<Item>().GetCurrentAmountForText();
                    var parentAmount = gameObject.GetComponentInChildren<Item>().GetCurrentAmountForText();
                    var currentAmount = childAmount + parentAmount;

                    if (currentAmount > 3)
                    {
                        currentAmount = 3;
                    }

                    gameObject.GetComponentInChildren<Text>().text = currentAmount.ToString();
                    gameObject.GetComponentInChildren<Item>().SetCurrentAmountForText(currentAmount);

                 SoundsEvents.onPositiveMeargeSound?.Invoke();

                Destroy(eventData.pointerDrag);
            }
             else
                {
                SoundsEvents.onNegativeMeargeSound?.Invoke();
                }


        }
        
    }

    private void CheckResorces(string parentTag, string oneTag, string twoTag, string threeTag, Sprite sp_2, Sprite sp_3)
    {
        if (parentTag == oneTag)
        {
            gameObject.GetComponentInChildren<Item>().GetComponentInChildren<Image>().sprite = sp_2;
            gameObject.GetComponentInChildren<CanvasGroup>().tag = twoTag;
        }

        if (parentTag == twoTag)
        {
            gameObject.GetComponentInChildren<Item>().GetComponentInChildren<Image>().sprite = sp_3;
            gameObject.GetComponentInChildren<CanvasGroup>().tag = threeTag;
        }
    }

    private List<GameObject> FindMatch(PointerEventData eventData, Vector2 vector)
    {

        List<GameObject> cashFindTiles = new List<GameObject>();
        float laserLength = 1.5f;
        RaycastHit2D hit2D = Physics2D.Raycast(eventData.pointerDrag.transform.position, vector, laserLength);
        Debug.DrawRay(transform.position, Vector2.right * laserLength, Color.red);



        var parentTag = "1";
        var childrenTag = "2";

        var parentId = 1;
        var childrenId = 1;

        if (hit2D.collider != null)
        {
            //Debug.Log($"Найденно совпадение в РЯДУ");

            parentTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;
            childrenTag = hit2D.collider.gameObject.GetComponent<CanvasGroup>().tag;

             parentId = gameObject.GetComponentInChildren<Item>().GetItemId();
             childrenId = hit2D.collider.gameObject.GetComponentInChildren<Item>().GetItemId();

            //Debug.Log($"parentTag = {parentTag}");
            //Debug.Log($"childrenTag = {childrenTag}");
        }

       

        while (hit2D.collider != null && parentTag == childrenTag && parentId != childrenId)
        //while (hit2D.collider != null && hit2D.collider.gameObject.GetComponent<CanvasGroup>() == gameObject.GetComponentInChildren<CanvasGroup>())
            //while (hit2D.collider != null && hit2D.collider.gameObject.GetComponent<CanvasGroup>().CompareTag(gameObject.GetComponentInChildren<CanvasGroup>().tag))
            //  while (hit2D.collider != null && parentTag == childrenTag)
            {
            cashFindTiles.Add(hit2D.collider.gameObject);
            Debug.Log($"Совпадение добавленно в список");

            hit2D = Physics2D.Raycast(hit2D.collider.gameObject.transform.position, vector, laserLength);
            childrenTag = hit2D.collider.gameObject.GetComponent<CanvasGroup>().tag;
            }

        return cashFindTiles;
    }

    private void DeleteSprite(PointerEventData eventData, Vector2[] vectorArray)
    {
        List<GameObject> cashFindList = new List<GameObject>();

        for (int i = 0; i < vectorArray.Length; i++)
        {
            cashFindList.AddRange(FindMatch(eventData, vectorArray[i]));
        }

        if (cashFindList.Count >= 2)
        {
            for (int i = 0; i < cashFindList.Count; i++)
            {
                Destroy(cashFindList[i].gameObject);
            }
        }
    }

    private void FindAllMath(PointerEventData eventData)
    {
        DeleteSprite(eventData, new Vector2[2] { Vector2.up, Vector2.down});
        DeleteSprite(eventData, new Vector2[2] { Vector2.left, Vector2.right});

       // Destroy(eventData.pointerDrag);
    }

}
