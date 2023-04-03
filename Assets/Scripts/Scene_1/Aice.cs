using UnityEngine;
using UnityEngine.EventSystems;

public class Aice : MonoBehaviour {

    #region Переменные движка

    [Tooltip("Кол-во тапов по льду для его разморозки")]
    [SerializeField]
    private int _defrostCount = 3;

    [Header("Картинки льда")]
    
    [Tooltip("Картинка льда для первой колонки")]
    [SerializeField]
    private GameObject _oneAice;

    [Tooltip("Картинка льда для второй колонки")]
    [SerializeField]
    private GameObject _twoAice;

    [Tooltip("Картинка льда для третьей колонки")]
    [SerializeField]
    private GameObject _threeAice;

    [Tooltip("Картинка льда для четвертой колонки")]
    [SerializeField]
    private GameObject _fourAice;

    #endregion

    private void Start() {
        _oneAice.SetActive(false);
        _twoAice.SetActive(false);
        _threeAice.SetActive(false);
        _fourAice.SetActive(false);
    }

    private void OnEnable() {
        MeargGameEvents.onAiceRuns += ActiveAice;
        MeargGameEvents.onGetdefrostCount += GetDefrostCount;
        MeargGameEvents.onFalseHoldColumn += DeactiveAice;
    }

    private void OnDisable() {
        MeargGameEvents.onAiceRuns -= ActiveAice;
        MeargGameEvents.onGetdefrostCount -= GetDefrostCount;
        MeargGameEvents.onFalseHoldColumn -= DeactiveAice;
    }

    private void ActiveAice() {
        var currentSpawnPointHoldSpider = MeargGameEvents.onGetCurrentSpawnPointHoldSpider?.Invoke();

        if(currentSpawnPointHoldSpider == null) {
            return;
        }

        if(currentSpawnPointHoldSpider == 0) {
            _oneAice.SetActive(true);
        } else if(currentSpawnPointHoldSpider == 1) {
            _twoAice.SetActive(true);
        } else if(currentSpawnPointHoldSpider == 2) {
            _threeAice.SetActive(true);
        } else if(currentSpawnPointHoldSpider == 3) {
            _fourAice.SetActive(true);
        }

    }

    private void DeactiveAice(int column) {
        if(column == 0) {
            _oneAice.SetActive(false);
        } else if(column == 1) {
            _twoAice.SetActive(false);
        } else if(column == 2) {
            _threeAice.SetActive(false);
        } else if(column == 3) {
            _fourAice.SetActive(false);
        }
    }

    private int GetDefrostCount() {
        return _defrostCount;
    }
}
