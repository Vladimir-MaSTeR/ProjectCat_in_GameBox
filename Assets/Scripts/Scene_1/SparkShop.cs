using System;
using TMPro;
using UnityEngine;

public class SparkShop : MonoBehaviour {

    [Header("����������� ������� �������")]
    [Tooltip("����������� ������� ������� �� ���� ���� �� ������")]
    [SerializeField]
    private int _sparkBuyCount;

    [Tooltip("����� �������� �������� �������")]
    [SerializeField]
    private TextMeshProUGUI _sparkCurrentCountText;

    [Tooltip("����� ����������� ������� �� ���� �� ������ �������")]
    [SerializeField]
    private TextMeshProUGUI _sparkBuyButtonText;

    private string BUY_TEXT = "������ +";

    private void OnEnable() {
        MeargGameEvents.onActiveSparkShopPanel += UpdateSparkCountText;
    }
    private void OnDisable() {
        MeargGameEvents.onActiveSparkShopPanel -= UpdateSparkCountText;
    }


    /// <summary>
    /// ����� ���������� ������� 
    /// ��������� ������ 
    /// </summary>
    private void UpdateSparkCountText() {
        // �������� ������� �������� �������
        var currentSparkValue = EventsResources.onGetSparkCurrentValue?.Invoke();

        // �������� ����� �������� ���-�� �������
        _sparkCurrentCountText.text = currentSparkValue.ToString();

        // �������� ����� ������ �������
        _sparkBuyButtonText.text = String.Concat(BUY_TEXT, _sparkBuyCount);
    }

    /// <summary>
    /// �������� ����� �������� �������
    /// 
    /// ����� ������� �������
    /// </summary>
    public void ClickButtonBuySpark() {
        //���������� �������
        EventsResources.onAddOrDeductSparkValue?.Invoke(_sparkBuyCount, true);

        //�������� ����� ���������� �������
        UpdateSparkCountText();
    }
}
