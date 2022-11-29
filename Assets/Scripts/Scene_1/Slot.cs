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

        } else
        {

            var parentTag = gameObject.GetComponentInChildren<CanvasGroup>().tag;
            var childrenTag = eventData.pointerDrag.tag;
            Debug.Log($"Родительский тег = {parentTag}");
            Debug.Log($"Тег предмета = {childrenTag}");

            var parentId = gameObject.GetComponentInChildren<Item>().GetItemId();
            var childrenId = eventData.pointerDrag.GetComponentInChildren<Item>().GetItemId();


            if ( parentTag == childrenTag && parentId != childrenId)
            {

                CheckLog(parentTag, "Log_1", "Log_2", "Log_3", _log_2_sprite, _log_3_sprite);
                CheckLog(parentTag, "Cloth_1", "Cloth_2", "Cloth_3", _cloth_2_sprite, _cloth_3_sprite);
                CheckLog(parentTag, "Stone_1", "Stone_2", "Stone_3", _stone_2_sprite, _stone_3_sprite);
                CheckLog(parentTag, "Neil_1", "Neil_2", "Neil_3", _neil_2_sprite, _neil_3_sprite);
                

                var childAmount = eventData.pointerDrag.GetComponentInChildren<Item>().GetCurrentAmountForText();
                var parentAmount = gameObject.GetComponentInChildren<Item>().GetCurrentAmountForText();
                var currentAmount = childAmount + parentAmount;

                if (currentAmount > 3)
                {
                    currentAmount = 3;
                }

                gameObject.GetComponentInChildren<Text>().text = currentAmount.ToString();
                gameObject.GetComponentInChildren<Item>().SetCurrentAmountForText(currentAmount);

                // EventsForMearge.onPositiveMeargeSound?.Invoke();

                Destroy(eventData.pointerDrag);
            } else
            {
               // EventsForMearge.onNoMeargeSound?.Invoke();
            }
        }
        
    }

    private void CheckLog(string parentTag, string oneTag, string twoTag, string threeTag, Sprite sp_2, Sprite sp_3)
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

}
