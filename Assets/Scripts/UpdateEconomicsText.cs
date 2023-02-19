using UnityEngine;
using UnityEngine.UI;

public class UpdateEconomicsText : MonoBehaviour
{
    [Header("Текстовый поля")]
    [SerializeField]
    private Text _comfortText;

    [SerializeField]
    private Text _SparkText;

    private int _currentComfort;
    private int _currentSpark;

    private void OnEnable() {
        EventsResources.onUpdateComfortValue += CheckUpdateCompfort;
        EventsResources.onUpdateSparkValue += CheckUpdateSpark;
    }

    private void OnDisable() {
        EventsResources.onUpdateComfortValue -= CheckUpdateCompfort;
        EventsResources.onUpdateSparkValue -= CheckUpdateSpark;
    }


    private void CheckUpdateCompfort(int newValue) {
        _comfortText.text = newValue.ToString();
    }
    private void CheckUpdateSpark(int newValue) {
        _SparkText.text = newValue.ToString();
    }
}
