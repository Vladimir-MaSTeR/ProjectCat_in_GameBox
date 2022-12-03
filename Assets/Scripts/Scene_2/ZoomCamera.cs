using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private float _zoomMin = 1;
    [SerializeField] private float _zoomMax = 8;


    [SerializeField] private float _zoomPosMaxX = 8;
    [SerializeField] private float _zoomPosMinX = 1;

    [SerializeField] private float _zoomPosMaxY = 8;
    [SerializeField] private float _zoomPosMinY = 1;

    [SerializeField] private float _zoomPosMaxZ = 8;
    [SerializeField] private float _zoomPosMinZ = 1;


    private Vector3 touch;

    private const int LEFT_BUTTON_IN_MOUSE = 0;
    private const String SCROLL_IN_MOUSE = "Mouse ScrollWheel";




    
    private void Start()
    {

        // Camera.main.orthographicSize = _zoomMax;

    }

    void Update()
    {
        //touchZoom(); // приблежение по клику

        changeCameraTouch();

        // zoom колесом мыши
        Zoom(Input.GetAxis(SCROLL_IN_MOUSE)*3);

        ScalingBoundaryX();
        movementCamera();
    
    }


    /// <summary>
    /// ”правление “ачем
    /// </summary>
    private void changeCameraTouch()
    {
        if (Input.GetMouseButtonDown(LEFT_BUTTON_IN_MOUSE))
        {
            touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // zoom тач
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            //// исходна€ позици€ кликов(Touch)
            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currenDistTouch = (touchZero.position - touchOne.position).magnitude;

            float defference = currenDistTouch - distTouch;

            Zoom(defference * 0.01f); // ”множение дл€ плавности.
        }
        // перемешение камеры “ач
        if (Input.GetMouseButton(LEFT_BUTTON_IN_MOUSE))
        {
            Vector3 direction = touch - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
            // Camera.main.transform.position += direction * Time.deltaTime;
            checkingBoundary(_zoomPosMaxX, _zoomPosMinX, _zoomPosMaxY, _zoomPosMinY, _zoomPosMaxZ, _zoomPosMinZ);

        }

    }


    /// <summary>
    /// перемешение камеры мышкой
    /// </summary>
    private void movementCamera()
    {
        if (Input.GetMouseButton(LEFT_BUTTON_IN_MOUSE))
        {
            Vector3 direction = touch - Camera.main.ScreenToWorldPoint(Input.mousePosition);  //Delta Camera
            Camera.main.transform.position += direction;
            // Camera.main.transform.position += direction * Time.deltaTime;
        }
        checkingBoundary(_zoomPosMaxX, _zoomPosMinX, _zoomPosMaxY, _zoomPosMinY, _zoomPosMaxZ, _zoomPosMinZ);
    }


    /// <summary>
    /// изменение границ от скейла(приближени€)
    /// 
    /// </summary>
    private void ScalingBoundaryX()
    {
        
        var _scalSize = Camera.main.orthographicSize;
        var _deltaBoundaryX =  _scalSize * 3f / 5f;
        _zoomPosMaxX = (69 - _deltaBoundaryX);
        _zoomPosMinX = (45 + _deltaBoundaryX);
        var _deltaBoundaryY = _scalSize * 3f / 5f;
        _zoomPosMaxY = (41 - _deltaBoundaryY);
        _zoomPosMinY = (20 + _deltaBoundaryY);

    }


    /// <summary>
    /// ѕроверка границы по оси X
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    private void checkingBoundary( float maxX, float minX , float maxY, float minY, float maxZ, float minZ)
    {
        Vector3 camerPosi = Camera.main.transform.position;
        Vector3 camerPosiMax = new Vector3(maxX, maxY, maxZ);
        Vector3 camerPosiMin = new Vector3(minX, minY, minZ);

        // проверка по оси ’
        if (camerPosi.x >= camerPosiMax.x)
        {
            camerPosi = new Vector3(maxX, camerPosi.y, camerPosi.z);
            Camera.main.transform.position = camerPosi;
        }
        if (camerPosi.x <= camerPosiMin.x)
        {
            camerPosi = new Vector3(minX, camerPosi.y, camerPosi.z);
            Camera.main.transform.position = camerPosi;
        }

        // проверка по оси Y
        if (camerPosi.y >= camerPosiMax.y)
        {
            camerPosi = new Vector3(camerPosi.x, maxY, camerPosi.z);
            Camera.main.transform.position = camerPosi;
        }
        if (camerPosi.y <= camerPosiMin.y)
        {
            camerPosi = new Vector3(camerPosi.x, minY, camerPosi.z);
            Camera.main.transform.position = camerPosi;
        }

        // проверка по оси Z
        if (camerPosi.z >= camerPosiMax.z)
        {
            camerPosi = new Vector3(camerPosi.x, camerPosi.y, minZ);
            Camera.main.transform.position = camerPosi;
        }
        if (camerPosi.z <= camerPosiMin.z)
        {
            camerPosi = new Vector3(camerPosi.x, camerPosi.y, minZ);
            Camera.main.transform.position = camerPosi;
        }

    }

    /// <summary>
    ///  приближение при cameraTouchZoomOn вкл/выкл
    /// </summary>
    [SerializeField] private bool cameraTouchZoomOn;
    private float cameraTouchZoomSpeed = 0.01f;
    private void touchZoom()
    {

        if (cameraTouchZoomOn)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,  _zoomMin,  cameraTouchZoomSpeed);
        }
        else
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, _zoomMax, cameraTouchZoomSpeed);

        }
    }



    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, _zoomMin, _zoomMax);
    }
}
