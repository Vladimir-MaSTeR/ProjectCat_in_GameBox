using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spider : MonoBehaviour, IPointerDownHandler {
    #region Переменные движка

    [SerializeField]
    [Tooltip("Объект на котором лежит скрипт с настройками для паука")]
    private Spiders _settingsSpider;

    [Header("Переменные")]
    [SerializeField]
    [Tooltip("Полоска жизней паука")]
    private GameObject _healthBarObject;

    [SerializeField]
    [Tooltip("Картинка заполнения для жизней")]
    private Image _imageHealthBar;
    

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

    private Animator animator;

    private bool _startTiefSpider = false;
    private bool _startRandomSpider = false;
    private bool _startHoldSpider = false;

    private Vector3 _currentMovePointTiefSpider;
    private Vector3 _currentMovePointRandomSpider;
    private Vector3 _currentMovePointHoldSpider;

    private int _currentSpawnPointHoldSpider;


    private float _currentHealth;
    private float _maxHealth;

    private float _currentSpeed;
    private float _currentDamage;

    private bool _death = false;

    #endregion

    private void Start() {
        animator = GetComponent<Animator>();

        _currentSpeed = _settingsSpider.GetSpeedSpiders();
        _maxHealth = _settingsSpider.GetHealthSpiders();
        _currentHealth = _maxHealth;
        _currentDamage = _settingsSpider.GetDamageSpiders();

        _healthBarObject.SetActive(false);

    }

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

    private void Update() {
        if(!_death) {
            CheckDeathSpider();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        //Debug.Log("ЕСТЬ КЛИК ПО ПАУКУ");
        _healthBarObject.SetActive(true);

        if(_currentHealth > 0) {
            _currentHealth -= _currentDamage;
            _imageHealthBar.fillAmount = _currentHealth / _maxHealth;
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

    #region ПАУЧЬЯ СМЕРТЬ
    /// <summary>
    /// Отслеживает смерть пака
    /// </summary>
    private void CheckDeathSpider() {
        if(_currentHealth <= 0) {
            _death = true;
            Debug.Log("ПАУК УМЕР");

            // вызвать анимацию смерти
            animator.SetBool("Death", true);

            // прервать его движение
            MeargGameEvents.onStartSpidersTime?.Invoke();
            _startTiefSpider = false;
            _startRandomSpider = false;
            _startHoldSpider = false;

            // проиграть звук
            SoundsEvents.onDeathSpider?.Invoke();
            
        } 
    }

    /// <summary>
    /// Метод вызывается из аниматора, после анимации смерти паука. 
    /// </summary>
    private void CheckEndAnimDeath() {
        // обновить параметры жизней и скрыть их
        UpdateSpiderHealthValue();

        // переместить паука в дефолтную позицию.
        Vector3 defaultPosition = _HoldSpiderPoints[0];
        this.transform.position = defaultPosition;
        _death = false;
    } 

    private void UpdateSpiderHealthValue() {
        // обновить параметры жизней и скрыть их
        animator.SetBool("Death", false);
        _currentHealth = _maxHealth;
        _imageHealthBar.fillAmount = _currentHealth / _maxHealth;
        _healthBarObject.SetActive(false);
    }
    #endregion

    #region ПАУК ЗАМОРАЖИВАЮЩИЙ методы

    private void StartHoldSpider() {
        HoldSpider();
        _startHoldSpider = true;
        UpdateSpiderHealthValue();
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

                _currentSpawnPointHoldSpider = Random.Range(0, 4); // для интов максимальная граница НЕ ВКЛЮЧИТЕЛЬНО
                //Debug.Log($"currentSpawnPoint = {currentSpawnPoint}");

                if(_currentSpawnPointHoldSpider == 0) {
                    this.transform.position = oneSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = oneMovePointTiefSpider;                   
                } else if(_currentSpawnPointHoldSpider == 1) {
                    this.transform.position = twoSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = twoMovePointTiefSpider;                  
                } else if(_currentSpawnPointHoldSpider == 2) {
                    this.transform.position = threeSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = threeMovePointTiefSpider;
                } else if(_currentSpawnPointHoldSpider == 3) {
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
        //this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _speed * Time.deltaTime);
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _currentSpeed * Time.deltaTime);

        if(this.transform.position == movePoint) {
            //вызывать эвент вредительства и эвент обновления времени воявления паука
            MeargGameEvents.onAiceRuns?.Invoke();
            MeargGameEvents.onStartSpidersTime?.Invoke();
            _startHoldSpider = false;
        }
    }

    private int GetCurrentSpawnPointHoldSpider() {
        return _currentSpawnPointHoldSpider;
    }


    #endregion

    #region ПАУК ПЕРЕМЕШИВАЛЬЩИК методы
    private void StartRandomSpider() {
        RandomSpider();
        _startRandomSpider = true;
        UpdateSpiderHealthValue();
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
        //this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _speed * Time.deltaTime);
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _currentSpeed * Time.deltaTime);

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
        UpdateSpiderHealthValue();
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
        //this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _speed * Time.deltaTime);
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _currentSpeed * Time.deltaTime);

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
