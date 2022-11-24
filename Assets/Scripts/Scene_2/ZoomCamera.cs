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

    void Update()
    {
        movementCamera();

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currenDistTouch = (touchZero.position - touchOne.position).magnitude;

            float defference = currenDistTouch - distTouch;

            Zoom(defference * 0.01f); // Умножение для плавности.
        }



        Zoom(Input.GetAxis(SCROLL_IN_MOUSE));
    }


    private void movementCamera ()
    {
        if (Input.GetMouseButtonDown(LEFT_BUTTON_IN_MOUSE))
        {
            touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(LEFT_BUTTON_IN_MOUSE))
        {
            Vector3 direction = touch - Camera.main.ScreenToWorldPoint(Input.mousePosition);  //Delta Camera
            Camera.main.transform.position += direction;
            // Camera.main.transform.position += direction * Time.deltaTime;
        }

        /// Проверка границ 
        checkingBoundaryX( _zoomPosMaxX, _zoomPosMinX);
        checkingBoundaryY( _zoomPosMaxY, _zoomPosMinY);
        string axisBoundary = "z";
        checkingBoundaryZ(axisBoundary, _zoomPosMaxZ, _zoomPosMinZ);


    }



    /// <summary>
    /// Проверка границы по оси X
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    private void checkingBoundaryX( float max, float min)
    { // ось Х

        if (Camera.main.transform.position.x > max)
        {
            Vector3 camerPosi = new Vector3(max, Camera.main.transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = camerPosi;
        }

        if (Camera.main.transform.position.x < min)
        {
            Vector3 camerPosi = new Vector3(min, Camera.main.transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = camerPosi;
        }
        
    }

    /// <summary>
    /// Проверка границы по оси Y
    /// </summary>
    private void checkingBoundaryY( float max, float min)
    {  // Ось У
        if (Camera.main.transform.position.y > max)
        {
            Vector3 camerPosi = new Vector3(Camera.main.transform.position.x, max, Camera.main.transform.position.z);
            Camera.main.transform.position = camerPosi;
        }

        if (Camera.main.transform.position.y < min)
        {
            Vector3 camerPosi = new Vector3(Camera.main.transform.position.x, min, Camera.main.transform.position.z);
            Camera.main.transform.position = camerPosi;
        }
    }
    
    
    /// <summary>
    /// Проверка границы по оси Z
    /// </summary>
    private void checkingBoundaryZ(string axis, float max, float min)
        {  // Ось Z
            if (axis == "z")
        {
            if (Camera.main.transform.position.z >= max)
            {
                Vector3 camerPosi = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, max);
                Camera.main.transform.position = camerPosi;
            }

            if (Camera.main.transform.position.z <= min)
            {
                Vector3 camerPosi = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, min);
                Camera.main.transform.position = camerPosi;
            }
        }

    }




    private void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, _zoomMin, _zoomMax);
    }
}
