using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera3D : MonoBehaviour
{
	[SerializeField]
	private Transform _myCamera;
	/// <summary>
	/// родитель камеры
	/// </summary>
	private Transform _objCamera;
	private float yRotationCamera;
	//public Vector3 offset;
	private Vector3 _firstPointOneTouch;
	private Vector3 _secondPointOneTouch;
	private Vector3 _firstPointTwoTouch;
	private Vector3 _secondPointTwoTouch;	
	private float xPosit;
	private float yPosit;
	private float zPosit;
	private float xPositTemp;
	private float yPositTemp;
	private float xPositDelta;
	private float yPositDelta;
	private float deltaTouchTemp; // растояние между двумя тачами при нажании
	private float deltaTouch; // растояние между двумя тачами при движении
	//  Delta от 0 до 10
	private float xPositTemp2;
	private float yPositTemp2;
	private float xPositDelta2;
	private float yPositDelta2;

	[Header("Границы камеры")]
	[SerializeField] private bool onBoundary = true;
	[SerializeField] private float _zoomMin = 1;
	[SerializeField] private float _zoomMax = 8;
	[SerializeField] private float _zoomPosMaxX = 8;
	[SerializeField] private float _zoomPosMinX = 1;
	[SerializeField] private float _zoomPosMaxY = 8;
	[SerializeField] private float _zoomPosMinY = 1;
	[SerializeField] private float _zoomPosMaxZ = 8;
	[SerializeField] private float _zoomPosMinZ = 1;

	void Start()
	{
		xPosit = transform.position.x;
		yPosit = transform.position.y;
		zPosit = transform.position.z;
		Debug.Log("старт Х " + (int)xPosit + " и У " + (int)yPosit + " и Z " + (int)zPosit);
		_objCamera = _myCamera.parent.transform;

	}

	void Update()
	{
		changeCameraTouch();
		zoomCameraTouch();


		checkingBoundary(_zoomPosMaxX, _zoomPosMinX, _zoomPosMaxY, _zoomPosMinY, _zoomPosMaxZ, _zoomPosMinZ);
		checkingBoundaryZ();

	}

	private void FixedUpdate()
    {
		//checkingBoundaryZ();

	}


	/// <summary>
	/// Управление Тачем
	/// </summary>
	private void changeCameraTouch()
	{
		movingCameraTouch();


		
        {
			// Debug.Log("Touch 0 или 3+ ");
		}
	}


	/// <summary>
	/// Управление Тачем передвижение 
	/// </summary>
	 
	private void movingCameraTouch ()
    {
		if (Input.touchCount == 1)
		{
			// foreach (Touch touch in Input.touches) {

			Touch touchZero = Input.GetTouch(0);
			var touch = touchZero;
			if (touch.phase == TouchPhase.Began) /// первое нажание косанием
			{
				_firstPointOneTouch = touch.position;
				xPositTemp = _firstPointOneTouch.x;
				yPositTemp = _firstPointOneTouch.y;
				// Debug.Log("тык Х " + (int)xPositTemp + " и У " + (int)yPositTemp);
				xPositDelta = 0;
				yPositDelta = 0;
			}
			if (
				touch.phase == TouchPhase.Moved) /// движение 
			{

				_secondPointOneTouch = touch.position;
				xPositDelta = (_secondPointOneTouch.x - _firstPointOneTouch.x) / Screen.height; // * 90
				yPositDelta = (_secondPointOneTouch.y - _firstPointOneTouch.y) / Screen.width; // * 180 
				xPosit = xPosit - xPositDelta * 1;
				yPosit = yPosit - yPositDelta * 1;
				// zPosit = zPosit ;
				_myCamera.position = new Vector3(xPosit, yPosit, zPosit);

				// Debug.Log("Del Х " + (int)(xPositDelta * 1) + " и У " + (int)(yPositDelta * 1));
				// Debug.Log("таши Х " + (int)xPosit + " и У " + (int)yPosit);
			}

			_myCamera.position = new Vector3(xPosit, yPosit, _myCamera.position.z);



		}
		
	}


	private void zoomCameraTouch()
	{
		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			// _firstPointTwoTouch = new Vector3 touchOne.position ;
			// Debug.Log("тыщ2  " + touchZero + " и 2 " + touchOne);

			if (
				touchZero.phase == TouchPhase.Began ||  /// первое нажание 
				touchOne.phase == TouchPhase.Began) /// второе нажание 
			{
				_firstPointOneTouch = touchZero.position;
				xPositTemp = _firstPointOneTouch.x;
				yPositTemp = _firstPointOneTouch.y;
				_firstPointTwoTouch = touchOne.position;
				xPositTemp2 = _firstPointTwoTouch.x;
				yPositTemp2 = _firstPointTwoTouch.y;
				xPositDelta2 = 0;
				yPositDelta2 = 0;
				deltaTouchTemp = Vector3.Distance(touchZero.position, touchOne.position);
			}

			if (touchZero.phase == TouchPhase.Moved ||
					touchOne.phase == TouchPhase.Moved) /// движение 
			{
				_secondPointOneTouch = touchZero.position;
				_secondPointTwoTouch = touchOne.position;

				xPositDelta = (_secondPointOneTouch.x - _firstPointOneTouch.x) / Screen.height; // * 90
				yPositDelta = (_secondPointOneTouch.y - _firstPointOneTouch.y) / Screen.width; // * 180 
				xPositDelta2 = (_secondPointTwoTouch.x - _firstPointTwoTouch.x) / Screen.height; // * 90
				yPositDelta2 = (_secondPointTwoTouch.y - _firstPointTwoTouch.y) / Screen.width; // * 180 
				deltaTouch = Vector3.Distance(touchZero.position, touchOne.position);

				// Debug.Log("deltaTouch " + deltaTouch + "deltaTouchTemp  " + deltaTouchTemp);

				if //(deltaTouch != deltaTouchTemp )
				(deltaTouch > deltaTouchTemp + 10f || deltaTouch < deltaTouchTemp - 10)
				{
					// var zPositDelta = xPositDelta + xPositDelta2 + yPositDelta + yPositDelta2;
					var zPositNew = zPosit + (deltaTouch - deltaTouchTemp) / 100;     // zPositDelta * 10;
					_myCamera.position = new Vector3(_myCamera.position.x, _myCamera.position.y, zPositNew);
					checkingBoundaryZ();
					Debug.Log("zPosit " + zPosit);

				}
				else
				{
					//var zPositDelta = xPositDelta + xPositDelta2 + yPositDelta + yPositDelta2;
					//var yZoomDelta = zPositDelta * 10;
					//_objCamera.rotation = Quaternion.Euler(_objCamera.rotation.x, yZoomDelta, _objCamera.rotation.z);
					//Debug.Log("deltaTouch " + deltaTouch);
				}

				////			_myCamera.position = new Vector3(xPosit, yPosit, _myCamera.position.z);


			}
		}
	}
	



    /// <summary>
    /// Проверка границы по оси X
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    private void checkingBoundary(float maxX, float minX, float maxY, float minY, float maxZ, float minZ)
	{
	//	checkingBoundaryZ(); //(maxZ, minZ);
		 if (onBoundary == true)
		{
			Vector3 camerPosi = _myCamera.position;
			Vector3 camerPosiMax = new Vector3(maxX, maxY, maxZ);
			Vector3 camerPosiMin = new Vector3(minX, minY, minZ);

			// проверка по оси Х
			if (camerPosi.x >= camerPosiMax.x)
			{
				camerPosi = new Vector3(maxX, camerPosi.y, camerPosi.z);
				_myCamera.position = camerPosi;
				xPosit = maxX;
			}
			if (camerPosi.x <= camerPosiMin.x)
			{
				camerPosi = new Vector3(minX, camerPosi.y, camerPosi.z);
				_myCamera.position = camerPosi;
				xPosit = minX;
			}

			// проверка по оси Y
			if (camerPosi.y >= camerPosiMax.y)
			{
				camerPosi = new Vector3(camerPosi.x, maxY, camerPosi.z);
				_myCamera.position = camerPosi;
				yPosit = maxY;
			}
			if (camerPosi.y <= camerPosiMin.y)
			{
				camerPosi = new Vector3(camerPosi.x, minY, camerPosi.z);
				_myCamera.position = camerPosi;
				yPosit = minY;
			}

			// проверка по оси Z
			if (camerPosi.z >= camerPosiMax.z)
			{
				camerPosi = new Vector3(camerPosi.x, camerPosi.y, maxZ);
				_myCamera.position = camerPosi;
				zPosit = maxZ;
			}
			if (camerPosi.z <= camerPosiMin.z)
			{
				camerPosi = new Vector3(camerPosi.x, camerPosi.y, minZ);
				_myCamera.position = camerPosi;
				zPosit = minZ;
			}
		}
	}


	private void checkingBoundaryZ ()
    {
		// checkingBoundary(_zoomPosMaxX, _zoomPosMinX, _zoomPosMaxY, _zoomPosMinY, _zoomPosMaxZ, _zoomPosMinZ);
		if(_myCamera.position.z > _zoomPosMaxZ)
        {
			_myCamera.position = new Vector3 (_myCamera.position.x, _myCamera.position.y, _zoomPosMaxZ);
        }
		if (_myCamera.position.z < _zoomPosMinZ)
		{
			_myCamera.position = new Vector3(_myCamera.position.x, _myCamera.position.y, _zoomPosMinZ);
		}

		float deltaZ = ((zPosit - _myCamera.position.z) * (5f / 15f));
		Debug.Log(deltaZ);
		_zoomPosMaxX = _zoomPosMaxX - deltaZ;

		deltaZ = ((zPosit - _myCamera.position.z) * (4f / 15f));
		_zoomPosMinX = _zoomPosMinX + deltaZ;


		deltaZ = ((zPosit - _myCamera.position.z) * (1f / 15f));
		_zoomPosMaxY = _zoomPosMaxY - deltaZ;
		deltaZ = ((zPosit - _myCamera.position.z) * (10f / 15f));
		_zoomPosMinY = _zoomPosMinY + deltaZ;

		zPosit = _myCamera.position.z;


	}



}
