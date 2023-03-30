using UnityEngine;

public class Spider : MonoBehaviour
{
    #region ���������� ������
    [Header("����������")]
    [SerializeField]
    [Tooltip("�������� �����")]
    private float _speed = 2.5f;
    

    // ������� ���������, ����� ����� - �����, ������ ���� ��������� ����.
    [SerializeField]
    [Tooltip("��������� ����� ���� (�������� - ����� ������), (������ - ����� ����������)")]
    private Vector3[] _tiefSpiderPoints;

    [SerializeField]
    [Tooltip("��������� ����� �������������� (�������� - ����� ������), (������ - ����� ����������)")]
    private Vector3[] _randomSpiderPoints;

    [SerializeField]
    [Tooltip("��������� ����� ��������������� (�������� - ����� ������), (������ - ����� ����������)")]
    private Vector3[] _HoldSpiderPoints;

    #endregion

    #region ��������� ����������

    private bool _startTiefSpider = false;
    private bool _startRandomSpider = false;
    private bool _startHoldSpider = false;

    //private float _currentSpeed;

    //private Rigidbody _rigidbody;


    //private Vector3 oneSpawnPointTiefSpider;
    //private Vector3 twoSpawnPointTiefSpider;

    private Vector3 _currentMovePointTiefSpider;
    private Vector3 _currentMovePointRandomSpider;
    private Vector3 _currentMovePointHoldSpider;
    //private Vector3 twoMovePointTiefSpider;

    #endregion

    //private void Awake() {
    //    _rigidbody = GetComponent<Rigidbody>();
    //}

    //private void Start() {
    //    _currentSpeed = _speed;
    //}

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
    }

    private void OnDisable() {
        MeargGameEvents.onThiefSpider -= StartTiefSpider;
        MeargGameEvents.onRandomSpider -= StartRandomSpider;
        MeargGameEvents.onHoldSpider -= StartHoldSpider;
    }

    #region ���� �������������� ������

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

                var currentSpawnPoint = Random.Range(0, 5); // ��� ����� ������������ ������� �� ������������
                //Debug.Log($"currentSpawnPoint = {currentSpawnPoint}");

                if(currentSpawnPoint == 0) {
                    this.transform.position = oneSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = oneMovePointTiefSpider;                   
                } else if(currentSpawnPoint == 1) {
                    this.transform.position = twoSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = twoMovePointTiefSpider;                  
                } else if(currentSpawnPoint == 2) {
                    this.transform.position = threeSpawnPointTiefSpider;
                    _currentMovePointHoldSpider = threeMovePointTiefSpider;
                } else if(currentSpawnPoint == 3) {
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
            //�������� ����� ������������� � ����� ���������� ������� ��������� �����
            MeargGameEvents.onStartSpidersTime?.Invoke();
            _startHoldSpider = false;
        }
    }


    #endregion

    #region ���� ��������������� ������
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
            //�������� ����� ������������� � ����� ���������� ������� ��������� �����
            MeargGameEvents.onStartSpidersTime?.Invoke();
            _startRandomSpider = false;
        }
    }
    #endregion

    #region ���� ��� ������
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

                var currentSpawnPoint = Random.Range(0, 2); // ��� ����� ������������ ������� �� ������������
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
        //this.transform.LookAt(movePoint);
        this.transform.position = Vector3.MoveTowards(this.transform.position, movePoint, _speed * Time.deltaTime);

        if(this.transform.position == movePoint) {
            //�������� ����� ������������� � ����� ���������� ������� ��������� �����

            int tiefRunsCount = (int)(MeargGameEvents.onGetTiefRunsCount?.Invoke()); // �������� ���-�� ��� ���  �����
            for(int i = 0; i < tiefRunsCount; i++) {
                MeargGameEvents.onTiefRuns?.Invoke();
            }

            MeargGameEvents.onStartSpidersTime?.Invoke();
            _startTiefSpider = false;
        }
    }
    #endregion

}
