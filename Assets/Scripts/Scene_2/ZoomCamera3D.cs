using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera3D : MonoBehaviour
{
	[SerializeField]
	private Transform _myCamera;
	/// <summary>
	/// �������� ������
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
	private float deltaTouchTemp; // ��������� ����� ����� ������ ��� �������
	private float deltaTouch; // ��������� ����� ����� ������ ��� ��������
	//  Delta �� 0 �� 10
	private float xPositTemp2;
	private float yPositTemp2;
	private float xPositDelta2;
	private float yPositDelta2;
	[SerializeField] private RectTransform _fonObj ;

	[Header("������� ������")]
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
		xPosit = transform.localPosition.x;
		yPosit = transform.localPosition.y;
		zPosit = transform.localPosition.z;
		//Debug.Log("����� � " + (int)xPosit + " � � " + (int)yPosit + " � Z " + (int)zPosit);
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
	/// ���������� �����
	/// </summary>
	private void changeCameraTouch()
	{
		movingCameraTouch();


		
        {
			// Debug.Log("Touch 0 ��� 3+ ");
		}
	}


	/// <summary>
	/// ���������� ����� ������������ 
	/// </summary>
	 
	private void movingCameraTouch ()
    {
		if (Input.touchCount == 1)
		{
			// foreach (Touch touch in Input.touches) {

			Touch touchZero = Input.GetTouch(0);
			var touch = touchZero;
			if (touch.phase == TouchPhase.Began) /// ������ ������� ��������
			{
				_firstPointOneTouch = touch.position;
				xPositTemp = _firstPointOneTouch.x;
				yPositTemp = _firstPointOneTouch.y;
				// Debug.Log("��� � " + (int)xPositTemp + " � � " + (int)yPositTemp);
				xPositDelta = 0;
				yPositDelta = 0;
			}
			if (
				touch.phase == TouchPhase.Moved) /// �������� 
			{

				_secondPointOneTouch = touch.position;
				xPositDelta = (_secondPointOneTouch.x - _firstPointOneTouch.x) / Screen.height; // * 90
				yPositDelta = (_secondPointOneTouch.y - _firstPointOneTouch.y) / Screen.width; // * 180 
				xPosit = xPosit - xPositDelta * 1;
				yPosit = yPosit - yPositDelta * 1;
				// zPosit = zPosit ;
				_myCamera.localPosition = new Vector3(xPosit, yPosit, zPosit);

				// Debug.Log("Del � " + (int)(xPositDelta * 1) + " � � " + (int)(yPositDelta * 1));
				// Debug.Log("���� � " + (int)xPosit + " � � " + (int)yPosit);
			}

			_myCamera.localPosition = new Vector3(xPosit, yPosit, _myCamera.localPosition.z);



		}
		
	}


	private void zoomCameraTouch()
	{
		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			// _firstPointTwoTouch = new Vector3 touchOne.position ;
			// Debug.Log("���2  " + touchZero + " � 2 " + touchOne);

			if (
				touchZero.phase == TouchPhase.Began ||  /// ������ ������� 
				touchOne.phase == TouchPhase.Began) /// ������ ������� 
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
					touchOne.phase == TouchPhase.Moved) /// �������� 
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
				(deltaTouch > deltaTouchTemp + 10f || deltaTouch < deltaTouchTemp - 10f)
				{
					// var zPositDelta = xPositDelta + xPositDelta2 + yPositDelta + yPositDelta2;
					var zPositNew = _myCamera.position.z + ((deltaTouch - deltaTouchTemp) / 100 );     // zPositDelta * 10;
					_myCamera.position = new Vector3(_myCamera.position.x, _myCamera.position.y, zPositNew);
					checkingBoundaryZ();

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
    /// �������� ������� �� ��� X
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    private void checkingBoundary(float maxX, float minX, float maxY, float minY, float maxZ, float minZ)
	{
	//	checkingBoundaryZ(); //(maxZ, minZ);
		 if (onBoundary == true)
		{
			Vector3 camerPosi = _myCamera.localPosition;
			Vector3 camerPosiMax = new Vector3(maxX, maxY, maxZ);
			Vector3 camerPosiMin = new Vector3(minX, minY, minZ);

			// �������� �� ��� �
			if (camerPosi.x >= camerPosiMax.x)
			{
				camerPosi = new Vector3(maxX, camerPosi.y, camerPosi.z);
				_myCamera.localPosition = camerPosi;
				xPosit = maxX;
			}
			if (camerPosi.x <= camerPosiMin.x)
			{
				camerPosi = new Vector3(minX, camerPosi.y, camerPosi.z);
				_myCamera.localPosition = camerPosi;
				xPosit = minX;
			}

			// �������� �� ��� Y
			if (camerPosi.y >= camerPosiMax.y)
			{
				camerPosi = new Vector3(camerPosi.x, maxY, camerPosi.z);
				_myCamera.localPosition = camerPosi;
				yPosit = maxY;
			}
			if (camerPosi.y <= camerPosiMin.y)
			{
				camerPosi = new Vector3(camerPosi.x, minY, camerPosi.z);
				_myCamera.localPosition = camerPosi;
				yPosit = minY;
			}

			// �������� �� ��� Z
			if (camerPosi.z >= camerPosiMax.z)
			{
				camerPosi = new Vector3(camerPosi.x, camerPosi.y, maxZ);
				_myCamera.localPosition = camerPosi;
				zPosit = maxZ;
			}
			if (camerPosi.z <= camerPosiMin.z)
			{
				camerPosi = new Vector3(camerPosi.x, camerPosi.y, minZ);
				_myCamera.localPosition = camerPosi;
				zPosit = minZ;
			}
		}
	}


	private void checkingBoundaryZ ()
    {
		// checkingBoundary(_zoomPosMaxX, _zoomPosMinX, _zoomPosMaxY, _zoomPosMinY, _zoomPosMaxZ, _zoomPosMinZ);
		if(_myCamera.localPosition.z > _zoomPosMaxZ)
        {
			_myCamera.localPosition = new Vector3 (_myCamera.localPosition.x, _myCamera.localPosition.y, _zoomPosMaxZ);
        }
		if (_myCamera.localPosition.z < _zoomPosMinZ)
		{
			_myCamera.localPosition = new Vector3(_myCamera.localPosition.x, _myCamera.localPosition.y, _zoomPosMinZ);
		}

		float deltaZ = ((zPosit - _myCamera.localPosition.z) * (5f / 15f));
		// Debug.Log(deltaZ);
		_zoomPosMaxX = _zoomPosMaxX - deltaZ;

		deltaZ = ((zPosit - _myCamera.localPosition.z) * (4f / 15f));
		_zoomPosMinX = _zoomPosMinX + deltaZ;


		deltaZ = ((zPosit - _myCamera.localPosition.z) * (1f / 15f));
		_zoomPosMaxY = _zoomPosMaxY - deltaZ;
		deltaZ = ((zPosit - _myCamera.localPosition.z) * (10f / 15f));
		_zoomPosMinY = _zoomPosMinY + deltaZ;

		zPosit = _myCamera.localPosition.z;


	}


	private void checkFonPosit(float _deltaZoom)
	{
		float xTurnFon = _fonObj.localPosition.x + ((4500f / 15f) * _deltaZoom);

		var _fonlocalPosition = new Vector3(xTurnFon, _fonObj.localPosition.y, _fonObj.localPosition.z);
		// Debug.Log(_fonlocalPosition);

		_fonObj.localPosition = _fonlocalPosition;
	}

	public void clickTurnR(bool _turn)
	{

		float yZoomDelta = _objCamera.localEulerAngles.y;
		if (_turn == true )
		{
			if (yZoomDelta > 360-12f || yZoomDelta < 20) //-15f+3f
			{

				yZoomDelta = yZoomDelta - 1.5f;
				checkFonPosit(1.5f);

				// Debug.Log("R+ " + yZoomDelta);
			}
			else
			{
				_objCamera.rotation = Quaternion.Euler(_objCamera.rotation.x, -12f, _objCamera.rotation.z);


			}
		}
		else // if (_turn == false)
		{
			{

				if (yZoomDelta < 18  || yZoomDelta > 360 - 15f) // 15f+3f
				{
					yZoomDelta = yZoomDelta + 1.5f;
					checkFonPosit(-1.5f);

					// Debug.Log("L+ " + yZoomDelta);
				}
				else
				{

					_objCamera.rotation = Quaternion.Euler(_objCamera.rotation.x, 18f, _objCamera.rotation.z);

				}

			}

		}
		_objCamera.rotation = Quaternion.Euler(_objCamera.rotation.x, yZoomDelta, _objCamera.rotation.z);

	}


}
