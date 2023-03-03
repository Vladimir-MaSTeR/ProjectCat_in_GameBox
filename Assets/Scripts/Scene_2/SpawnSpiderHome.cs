using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSpiderHome : MonoBehaviour
{

    [Tooltip("����� ��������� ������ [������ : 0 - ���, 1 - ���, 2 - ���]")]
    [SerializeField] private int[] deltaTimeSpawnSpider = new int[3];
    private DateTime dateTimeStart;
    private DateTime dateTimeEnd;

    [SerializeField]
    private List<string> nameObjHome = new List<string>();
    [SerializeField]
    private List<int> statusObjHome = new List<int>();


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    //[Tooltip("������ �� ������ ������� � ����")]
    //public static Func<string> onStatusSpiderHome;

    private void OnEnable()
    {
        ClickRepair.onStatusSpiderHome += statusSpiderHome;
        Altar.onStatusSpiderHome += statusSpiderHome;

    }

    private void OnDisable()
    {
        ClickRepair.onStatusSpiderHome -= statusSpiderHome;
        Altar.onStatusSpiderHome -= statusSpiderHome;


    }


    /// <summary>
    /// �������� ������� ������� �� ����� ������
    /// </summary>
    /// <param name="_nameObj"> ��� �������</param>
    /// <param name="_typeStatus"> ������ 1-���, 2 - ��������, 0 - null</param>
    private void statusSpiderHome(string _nameObj, int _typeStatus)
    {
        nameObjHome.Add(_nameObj);
        statusObjHome.Add(_typeStatus);

    }

    private void checkTimer()
    {
        /// ����� ������
        var _dateTimeNow = DateTime.Now; 
        /// ����� ������(�����)
        var _dateTimeStart = dateTimeEnd;
        do
        {
            _dateTimeStart.AddHours(deltaTimeSpawnSpider[2]);
            _dateTimeStart.AddMinutes(deltaTimeSpawnSpider[1]);
            _dateTimeStart.AddSeconds(deltaTimeSpawnSpider[0]);

            if (_dateTimeStart >= _dateTimeNow)
            {
                /// ����� ������ �� ������
                //var spawnTime = spawnTimePassedWork(_dateTimeStart);
                Debug.Log("+1");
            }
            else
            {
                dateTimeEnd = _dateTimeStart;
                break;
            }
        } while (true);

        
    }




    private void addSpawnSpider(int _timePassed)
    {
        if (checkAllSpider() == false)
        {

            int timeMinutes;



        }
        else
        {
            Debug.Log("������ ��� � ������");
        }

    }

    /// <summary>
    /// �������� ������� ���� ��������� � ����
    /// </summary>
    /// <returns></returns>
    private bool checkAllSpider()
    {
        for (int i = 0; i < statusObjHome.Count; i++)
        {
            if (statusObjHome[i] == 1)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// ����� ��������� � ����
    /// </summary>
    /// <returns></returns>
    private string checkLackSpider()
    {

        if (checkAllSpider() == false)
        {
            do
            {
                int nObj = Random.Range(0, nameObjHome.Count+1);
                if ((statusObjHome[nObj] == 1))
                {
                    return nameObjHome[nObj];

                }
            } while (true);
        }
        return "null";

    }


    /// <summary>
    /// ������ ������� ������ �� ������
    /// </summary>
    /// <param name="startSpider"></param>
    /// <returns></returns>
    private int spawnTimePassedWork(DateTime startSpider )
    {
        int timerWork;
        var time = startSpider - dateTimeEnd ;
        timerWork = time.Seconds + time.Minutes * 60  + time.Hours * 3600 + time.Days * 3600 *24;
        return timerWork;   
    }

    /// <summary>
    /// ������ ������� ����� ������
    /// </summary>
    /// <param name="startSpider"></param>
    /// <returns></returns>
    private int spawnTimePassedSpider(DateTime startSpider)
    {
        int timerSpider;
        var time = DateTime.Now - startSpider;
        timerSpider = time.Seconds + time.Minutes * 60 + time.Hours * 3600 + time.Days * 3600 * 24;
        return timerSpider;
    }





}
