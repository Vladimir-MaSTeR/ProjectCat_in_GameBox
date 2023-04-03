using UnityEngine;

public class Spider : MonoBehaviour
{
    #region Переменные движка
    [Header("Переменные")]
    [SerializeField]
    [Tooltip("Скорость паука")]
    private float _speed = 2.5f;
    

    // задумка следующая, певое число - спавн, второе куда следовать паку.
    [SerializeField]
    [Tooltip("Кординаты паука ВОРА (Нечетные - точки спавна), (Четные - точки следования)")]
    private Vector3[] _tiefSpiderPoints;

    [SerializeField]
    [Tooltip("Кординаты паука ПЕРЕМЕШИВАТЕЛЯ (Нечетные - точки спавна), (Четные - точки следования)")]
    private Vector3[] _randomSpiderPoints;

    [SerializeField]
    [Tooltip("Кординаты паука ЗАМОРАЖИВАЮЩЕГО (Нечетные - точки спавна), (Четные - точки следования)")]
    private Vector3[] _HoldSpiderPoints;

    #endregion

    #region Приватные переменные

    private bool _startTiefSpider = false;
    private bool _startRandomSpider = false;
    private bool _startHoldSpider = false;

    private Vector3 _currentMovePointTiefSpider;
    private Vector3 _currentMovePointRandomSpider;
    private Vector3 _currentMovePointHoldSpider;

    private int currentSpawnPointHoldSpider;

    #endregion

    private void FixedUpdate() {
        if(_startTiefSpider) {
            TiefSpiderMove(_currentMovePointTiefSpider);
        }

        if(_startRandomSpider) {
            RandomSpiderMove(_currentMovePointRandomSpider);
        }

        if(_startHoldSpider) {
            HoldSpiderMove(_currentMovePointHoldSpider);
        }

    }

    private void OnEnable() {
        MeargGameEvents.onThiefSpider += StartTiefSpider;
        MeargGameEvents.onRandomSpider += StartRandomSpider;
        MeargGameEvents.onHoldSpider += StartHoldSpider;
        MeargGameEvents.onGetCurrentSpawnPointHoldSpider += GetCurrentSpawnPointHoldSpider;
    }

    private void OnDisable() {
        MeargGameEvents.onThiefSpider -= StartTiefSpider;
        MeargGameEvents.onRandomSpider -= StartRandomSpider;
        MeargGameEvents.onHoldSpider -= StartHoldSpider;
        MeargGameEvents.onGetCurrentSpawnPointHoldSpider -= GetCurrentSpawnPointHoldSpider;
    }

    #region ПАУК ЗАМОРАЖИВАЮЩИЙ методы

    private void StartHoldSpider() {
        HoldSpider();
        _startHoldSpider = true;
    }

    private void HoldSpider() {
        if(_HoldSpiderPoints != null) {
            this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            if(_HoldSpiderPoints.Length >= 8) {
                Vector3 oneSpawnPointTiefSpider = _HoldSpiderPoints[0];
                Vector3 oneMovePointTiefSpider = _HoldSpiderPoints[1];

                Vector3 twoSpawnPointTiefSpider = _HoldSpiderPoints[2];
                Vector3 twoMovePointTiefSpider = _HoldSpiderPoints[3];

                Vector3 threeSpawnPointTiefSpider = _HoldSpiderPoints[4];
                Vector3 threeMovePointTiefSpider = _HoldSpiderPoints[5];

                Vector3 fourSpawnPointTiefSpider = _HoldSpiderPoints[6];
                Vector3 fourMovePointTiefSpider = _HoldSpiderPoints[7];

                currentSpawnPointHoldSpider = Random.Range(0, 4); // для интов максимальная граница НЕ ВКЛЮЧИТЕЛЬНО
                //Debug.Log($"currentSpawnPoint = {currentSpawnPoint}");

                if(currentSpawnPointHoldSpider == 0) {
                    this.transform.position = oneSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = oneMovePointTiefSpider;                   
                } else if(currentSpawnPointHoldSpider == 1) {
                    this.transform.position = twoSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = twoMovePointTiefSpider;                  
                } else if(currentSpawnPointHoldSpider == 2) {
                    this.transform.position = threeSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = threeMovePointTiefSpider;
                } else if(currentSpawnPointHoldSpider == 3) {
                    this.transform.position = fourSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = fourMovePointTiefSpider;
                }
            } else {
                Vector3 oneSpawnPointTiefSpider = _HoldSpiderPoints[0];
                Vector3 oneMovePointTiefSpider = _HoldSpiderPoints[1];

                this.transform.position = oneSpawnPointTiefSpider;
                _currentMovePointHoldSpider = oneMovePointTiefSpider;
            }
        }
    }

    private void HoldSpiderMove(Vector3 movePoint) {
        this.transform.LookAt(movePoint);
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _speed * Time.deltaTime);

        if(this.transform.position == movePoint) {
            //вызывать эвент вредительства и эвент обновления времени воявления паука
            MeargGameEvents.onAiceRuns?.Invoke();
            MeargGameEvents.onStartSpidersTime?.Invoke();
            _startHoldSpider = false;
        }
    }

    private int GetCurrentSpawnPointHoldSpider() {
        return currentSpawnPointHoldSpider;
    }


    #endregion

    #region ПАУК ПЕРЕМЕШИВАЛЬЩИК методы
    private void StartRandomSpider() {
        RandomSpider();
        _startRandomSpider = true;
    }

    private void RandomSpider() {
        if(_randomSpiderPoints != null) {
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
  
            Vector3 oneSpawnPointTiefSpider = _randomSpiderPoints[0];
            Vector3 oneMovePointTiefSpider = _randomSpiderPoints[1];

            this.transform.position = oneSpawnPointTiefSpider;
            _currentMovePointRandomSpider = oneMovePointTiefSpider;
            
        }
    }

    private void RandomSpiderMove(Vector3 movePoint) {
        this.transform.LookAt(movePoint);
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _speed * Time.deltaTime);

        if(this.transform.position == movePoint) {
            //вызывать эвент вредительства и эвент обновления времени воявления паука
            MeargGameEvents.onRandomRuns?.Invoke();
            MeargGameEvents.onStartSpidersTime?.Invoke();
            _startRandomSpider = false;
        }
    }
    #endregion

    #region ПАУК ВОР методы
    private void StartTiefSpider() {
        TiefSpider();
        _startTiefSpider = true;
    }

    private void TiefSpider() {       
        if(_tiefSpiderPoints != null) {
            this.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            if(_tiefSpiderPoints.Length >= 4) {
                Vector3 oneSpawnPointTiefSpider = _tiefSpiderPoints[0];
                Vector3 oneMovePointTiefSpider = _tiefSpiderPoints[1];

                Vector3 twoSpawnPointTiefSpider = _tiefSpiderPoints[2];
                Vector3 twoMovePointTiefSpider = _tiefSpiderPoints[3];

                var currentSpawnPoint = Random.Range(0, 2); // для интов максимальная граница НЕ ВКЛЮЧИТЕЛЬНО
                //Debug.Log($"currentSpawnPoint = {currentSpawnPoint}");

                if(currentSpawnPoint == 0) {
                    this.transform.position = oneSpawnPointTiefSpider;
                    _currentMovePointTiefSpider = oneMovePointTiefSpider;
                    this.transform.rotation = Quaternion.Euler(311f, 281f, 75f);
                } else if(currentSpawnPoint == 1) {
                    this.transform.position = twoSpawnPointTiefSpider;
                    _currentMovePointTiefSpider = twoMovePointTiefSpider;
                    this.transform.rotation = Quaternion.Euler(320f, 104f, 258f);
                }
            } else {
                Vector3 oneSpawnPointTiefSpider = _tiefSpiderPoints[0];
                Vector3 oneMovePointTiefSpider = _tiefSpiderPoints[1];

                this.transform.position = oneSpawnPointTiefSpider;
                _currentMovePointTiefSpider = oneMovePointTiefSpider;             
            }
        }
    }

    private void TiefSpiderMove(Vector3 movePoint) {
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _speed * Time.deltaTime);

        if(this.transform.position == movePoint) {

            int tiefRunsCount = (int)(MeargGameEvents.onGetTiefRunsCount?.Invoke()); // Получаем кол-во рун для  кражи
            for(int i = 0; i < tiefRunsCount; i++) {
                MeargGameEvents.onTiefRuns?.Invoke();
            }

            MeargGameEvents.onStartSpidersTime?.Invoke();
            _startTiefSpider = false;
        }
    }
    #endregion

}
