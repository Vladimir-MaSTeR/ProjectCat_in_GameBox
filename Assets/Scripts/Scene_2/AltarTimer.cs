using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;

public class AltarTimer : MonoBehaviour
{
    [Header("Загружать сохранения или стартовые значения ресурсов")]
    [SerializeField] private bool loadResorces = true;

    [SerializeField]
    private float _timerAddRunse;
    /// <summary>
    /// таймер для круглой руны
    /// </summary>
    private float _timerRunse01_circleNow;
    /// <summary>
    /// таймер для квадратной руны
    /// </summary>
    private float _timerRunse02_squareNow;
    /// <summary>
    /// таймер для прямоугольной руны
    /// </summary>
    private float _timerRunse03_rectangleNow;
    /// <summary>
    /// таймер для треугольной руны
    /// </summary>
    private float _timerRunse04_triangleNow;

    /// <summary>
    /// колличество круглой руны для добавления
    /// </summary>
    private int _addAmountRunse01_circle;
    /// <summary>
    /// колличество квадратной руны для добавления
    /// </summary>
    private int _addAmountRunse02_square;
    /// <summary>
    /// колличество прямоугольной руны для добавления
    /// </summary>
    private int _addAmountRunse03_rectangle;
    /// <summary> 
    /// колличество треугольной руны для добавления
    /// </summary>
    private int _addAmountRunse04_triangle;


    void Start()
    {
        LoadResouces();

    }

    private void Update()
    {
        
        
        if (_timerRunse01_circleNow > 0)
        {
            timerAltar (_timerRunse01_circleNow, _addAmountRunse01_circle,1);
        }
        if (_timerRunse02_squareNow > 0)
        {
            timerAltar (_timerRunse02_squareNow, _addAmountRunse02_square,2);
        }
        if (_timerRunse03_rectangleNow > 0)
        {
            timerAltar(_timerRunse03_rectangleNow, _addAmountRunse03_rectangle, 3);
        }
        if (_addAmountRunse04_triangle > 0)
        {
            timerAltar(_addAmountRunse04_triangle, _addAmountRunse04_triangle, 4);
        }


    }

    private void OnEnable()
    {
        Altar.onAddRunse += altarTimerStart;
        Altar.onCheckAltarActivate += _checkAltarTime;

    }

    private void OnDisable()
    {
        Altar.onAddRunse -= altarTimerStart;
        Altar.onCheckAltarActivate -= _checkAltarTime;

    }

    public static Action<int> onTimerAltarOff;


    /// <summary>
    /// Старт таймера
    /// </summary>
    /// <param name="Lv"></param>
    /// <param name="number"></param>
    private void altarTimerStart(int Lv, int number)
    {
        if (number == 1)
        {
            _timerRunse01_circleNow = _timerAddRunse;
            _addAmountRunse01_circle = Lv * 4;

        }

        if (number == 2)
        {
            _timerRunse02_squareNow = _timerAddRunse;
            _addAmountRunse02_square = Lv * 4;

        }

        if (number == 3)
        {
            _timerRunse03_rectangleNow = _timerAddRunse;
            _addAmountRunse03_rectangle = Lv * 4;

        }

        if (number == 4)
        {
            _timerRunse04_triangleNow = _timerAddRunse;
            _addAmountRunse04_triangle = Lv * 4;

        }

        SaveResources();
    }


    /// <summary>
    /// работа таймера
    /// </summary>
    /// <param name="_timerRunse"></param>
    /// <param name="_addAmountRunse"></param>
    /// <param name="numberAltar"></param>
    private void timerAltar( float _timerRunse ,int _addAmountRunse, int numberAltar)
    {
        _timerRunse -= Time.deltaTime;

        if (_timerRunse <= 0)
        {
            _timerRunse = 0;
            /// Выдать круглае руны в колличестве _addAmountRunse  
            /// Активировать аларь numberAltar
            onTimerAltarOff?.Invoke(numberAltar);
            SaveResources();

        }
    }




    /// <summary>
    /// Проверка запука таймера type алтаря (вернуть время таймера type алтаря )
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private float _checkAltarTime (int type)
    {
        if (type == 1)
        {
            return _timerRunse01_circleNow;
        }
        else if (type == 2)
        {
            return _timerRunse02_squareNow;
        }
        else if (type == 3)
        {
            return _timerRunse03_rectangleNow;
        }
        else if (type == 4)
        {
            return _timerRunse04_triangleNow;
        }
        else 
        {
            return 0;
        }
    }




    #region Save - Load



    private void SaveResources()
    {
        PlayerPrefs.SetFloat("timerRunse01Now", _timerRunse01_circleNow);
        PlayerPrefs.SetFloat("timerRunse02Now", _timerRunse02_squareNow);
        PlayerPrefs.SetFloat("timerRunse03Now", _timerRunse03_rectangleNow);
        PlayerPrefs.SetFloat("timerRunse04Now", _timerRunse04_triangleNow);

        PlayerPrefs.SetInt("addAmountRunse01", _addAmountRunse01_circle);
        PlayerPrefs.SetInt("addAmountRunse02", _addAmountRunse02_square);
        PlayerPrefs.SetInt("addAmountRunse03", _addAmountRunse03_rectangle);
        PlayerPrefs.SetInt("addAmountRunse04", _addAmountRunse04_triangle);




    PlayerPrefs.Save();
    }

    private void LoadResouces()
    {
        if (loadResorces)
        {
            if (PlayerPrefs.HasKey("timerRunse01Now"))
            {
                _timerRunse01_circleNow = PlayerPrefs.GetFloat("timerRunse01Now");
            }
            if (PlayerPrefs.HasKey("timerRunse02Now"))
            {
                _timerRunse02_squareNow = PlayerPrefs.GetFloat("timerRunse02Now");
            }
            if (PlayerPrefs.HasKey("timerRunse03Now"))
            {
                _timerRunse03_rectangleNow = PlayerPrefs.GetFloat("timerRunse03Now");
            }
            if (PlayerPrefs.HasKey("timerRunse04Now"))
            {
                _timerRunse04_triangleNow = PlayerPrefs.GetFloat("timerRunse04Now");
            }

            if (PlayerPrefs.HasKey("addAmountRunse01"))
            {
                _addAmountRunse01_circle = PlayerPrefs.GetInt("addAmountRunse01");
            }
            if (PlayerPrefs.HasKey("addAmountRunse02"))
            {
                _addAmountRunse02_square = PlayerPrefs.GetInt("addAmountRunse02");
            }
            if (PlayerPrefs.HasKey("addAmountRunse03"))
            {
                _addAmountRunse03_rectangle = PlayerPrefs.GetInt("addAmountRunse03");
            }
            if (PlayerPrefs.HasKey("addAmountRunse04"))
            {
                _addAmountRunse04_triangle = PlayerPrefs.GetInt("addAmountRunse04");
            }


        }
    }

    #endregion






}
