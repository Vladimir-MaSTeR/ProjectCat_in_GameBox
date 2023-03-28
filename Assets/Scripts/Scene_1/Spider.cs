using UnityEngine;

public class Spider : MonoBehaviour
{
    #region Переменные движка
    [Header("Переменные")]

    [SerializeField]
    [Tooltip("Время через которое появится паук")]
    private float _emergenceTime;

    [SerializeField]
    [Tooltip("Скорость паука")]
    private float _speed;

    // задумка следующая, певое число - спавн, второе куда следовать паку.
    [SerializeField]
    [Tooltip("Точки передвижения пака (Нечетные - точки спавна), (Четные - точки следования)")]  
    private Vector3[] _movePoints;
    #endregion

    #region Приватные переменные
    private float _currentTime;
    private float _currentSpeed;

    private Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    #endregion

    private void Start() {
        _currentTime = _emergenceTime;
        _currentSpeed = _speed;
    }

    private void Update() {
        MoveSpider();
    }

    private void MoveSpider() {
        Vector3 oldSpawnPoint;
        Vector3 currentSpawnPoint;
        Vector3 currentMovePoint;

        if(_movePoints != null && _movePoints.Length >= 2) {
            currentSpawnPoint = _movePoints[0]; 
            currentMovePoint = _movePoints[1];

            if(_currentTime <= 0) {
                // заспавнить паука (Или включить его)
                // двигать его к точке.
                // в конце пути вызвать эвент одного из вредительств, удалить объект и перезапустить таймер

                //this.gameObject.SetActive(true);

                this.transform.LookAt(currentMovePoint);
                this.transform.position = Vector3.MoveTowards(this.transform.position, currentMovePoint, _speed * Time.deltaTime);

                if(this.transform.position == currentMovePoint) {
                    //вызывать эвент вредительства
                    _currentTime = _emergenceTime;
                }
                //_rigidbody.AddForce(0f, currentMovePoint.y * _speed, 0f );

            } else {
                this.transform.position = currentSpawnPoint;
                _currentTime -= Time.deltaTime;
            }
        }
    }
}
