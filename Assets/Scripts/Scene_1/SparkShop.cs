using System;
using TMPro;
using UnityEngine;

public class SparkShop : MonoBehaviour {

    [Header("Колличество покупки искорок")]
    [Tooltip("Колличество покупки искорок за один клик по кновке")]
    [SerializeField]
    private int _sparkBuyCount;

    [Tooltip("Текст текущего значения искорки")]
    [SerializeField]
    private TextMeshProUGUI _sparkCurrentCountText;

    [Tooltip("Текст колличества искорок за клик по кнопке покупки")]
    [SerializeField]
    private TextMeshProUGUI _sparkBuyButtonText;

    private string BUY_TEXT = "Купить +";

    private void OnEnable() {
        MeargGameEvents.onActiveSparkShopPanel += UpdateSparkCountText;
    }
    private void OnDisable() {
        MeargGameEvents.onActiveSparkShopPanel -= UpdateSparkCountText;
    }


    /// <summary>
    /// метод вызывается евентом 
    /// обновляет текста 
    /// </summary>
    private void UpdateSparkCountText() {
        // получили текущее значение искорки
        var currentSparkValue = EventsResources.onGetSparkCurrentValue?.Invoke();

        // обновили текст текущего кол-ва искорки
        _sparkCurrentCountText.text = currentSparkValue.ToString();

        // написали текст кнопки покупки
        _sparkBuyButtonText.text = String.Concat(BUY_TEXT, _sparkBuyCount);
    }

    /// <summary>
    /// ВОЗМОЖНО СТОИТ ВЫЗЫВАТЬ ЕВЕНТОМ
    /// 
    /// метод покупки искорок
    /// </summary>
    public void ClickButtonBuySpark() {
        //Прибовляем искорки
        EventsResources.onAddOrDeductSparkValue?.Invoke(_sparkBuyCount, true);

        //вызываем метод обновления текстов
        UpdateSparkCountText();
    }
}
