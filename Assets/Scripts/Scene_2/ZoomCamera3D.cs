using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera3D : MonoBehaviour
{
	[SerializeField]
	private Transform _myCamera;
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


	}

	void Update()
	{
		changeCameraTouch();



		checkingBoundary(_zoomPosMaxX, _zoomPosMinX, _zoomPosMaxY, _zoomPosMinY, _zoomPosMaxZ, _zoomPosMinZ);

	}




	/// <summary>
	/// Управление Тачем
	/// </summary>
	private void changeCameraTouch()
	{
		if (Input.touchCount == 1)
		{
			// foreach (Touch touch in Input.touches)
			{
				Touch touchZero = Input.GetTouch(0);
				var touch = touchZero;
				if (
				// touch.position.x > Screen.width/2 &  // правая половина экрана
				touch.phase == TouchPhase.Began) /// первое нажание 
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
					_myCamera.position = new Vector3(xPosit, yPosit, _myCamera.position.z);

					// Debug.Log("Del Х " + (int)(xPositDelta * 1) + " и У " + (int)(yPositDelta * 1));
					// Debug.Log("таши Х " + (int)xPosit + " и У " + (int)yPosit);
				}

				_myCamera.position = new Vector3(xPosit, yPosit, _myCamera.position.z);



			}
		}
		else if (Input.touchCount == 2)
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
			//	Debug.Log("тык2 Х " + (int)xPositTemp2 + " и У " + (int)yPositTemp2);
				xPositDelta2 = 0;
				yPositDelta2 = 0;
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
				Debug.Log("тыкD1 Х " + xPositDelta + " и У " + yPositDelta + 
					"тыкD2 Х " + xPositDelta2 + " и У " + yPositDelta2);

				var zPositDelta = xPositDelta + yPositDelta + xPositDelta2 + yPositDelta2 ;
				zPosit = zPosit + zPositDelta *10 ;
				Debug.Log("камD z " + zPositDelta + " и z камер " + zPosit);

				_myCamera.position = new Vector3(_myCamera.position.x, _myCamera.position.y, zPosit);

			}
			//if( )
			{

            }

			_myCamera.position = new Vector3(xPosit, yPosit, _myCamera.position.z);


		}
		else
        {
			// Debug.Log("Touch 0 или 3+ ");
		}

	}




	/// <summary>
	/// Проверка границы по оси X
	/// </summary>
	/// <param name="max"></param>
	/// <param name="min"></param>
	private void checkingBoundary(float maxX, float minX, float maxY, float minY, float maxZ, float minZ)
	{
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





}
