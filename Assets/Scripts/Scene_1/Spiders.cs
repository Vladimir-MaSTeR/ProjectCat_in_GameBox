using UnityEngine;

public class Spiders : MonoBehaviour {
    [SerializeField]
    [Tooltip("����� ����� ������� �������� ����")]
    private float _emergenceTime;

    [SerializeField]
    [Tooltip("����������� ��� ������� ������ ����")]
    private int _tiefRunsCount = 1;

    private float _currentTime;
    private int _currentSpider; // ���������� �������� �� ��������� ����� ����� 0 = �������� ����. 1 = �������� ����. 2 = �������������� ���� ����.

    private bool _startTimer = true;

    private void Start() {
        _currentTime = _emergenceTime;
    }

    private void FixedUpdate() {
        TimerEmergenceSpiders();
    }

    private void OnEnable() {
        MeargGameEvents.onGetSpiderTime += GetSpiderTime;
        MeargGameEvents.onStartSpidersTime += StartSpidersTime;
        MeargGameEvents.onGetTiefRunsCount += GetTiefRunsCount;
    }

    private void OnDisable() {
        MeargGameEvents.onGetSpiderTime -= GetSpiderTime;
        MeargGameEvents.onStartSpidersTime -= StartSpidersTime;
        MeargGameEvents.onGetTiefRunsCount -= GetTiefRunsCount;
    }

    private void TimerEmergenceSpiders() {
        if(_currentTime <= 0) {
            if(_startTimer) {
                _currentSpider = Random.Range(0, 3); //��� int,� ������������ �������� �� ������������
                                                     //�������� ����� ��������� �����. � ����������� �� ���������� �������� ����������.

                if(_currentSpider == 0) {
                    MeargGameEvents.onHoldSpider?.Invoke();
                } else if(_currentSpider == 1) {
                    MeargGameEvents.onThiefSpider?.Invoke();
                } else if(_currentSpider == 2) {
                    MeargGameEvents.onRandomSpider?.Invoke();
                }

                ////MeargGameEvents.onHoldSpider?.Invoke();
                ////MeargGameEvents.onThiefSpider?.Invoke();
                //MeargGameEvents.onRandomSpider?.Invoke();

                _startTimer = false;
            }
        } else {
            _currentTime -= Time.deltaTime;
        }
    }

    private void StartSpidersTime() {
        _currentTime = _emergenceTime;
        _startTimer = true;
    }

    private float GetSpiderTime() {
        return _emergenceTime;
    }

    private int GetTiefRunsCount() {
        return _tiefRunsCount;
    }
}
