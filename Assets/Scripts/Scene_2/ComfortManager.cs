using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComfortManager : MonoBehaviour
{
    [Tooltip("время выхода из игры")]
    private DateTime dateTimeEnd;

    [SerializeField]
    private List<string> nameObjHome = new List<string>();
    [SerializeField]
    private List<float> healthObjHome = new List<float>();
    [SerializeField]
    private List<float> comfortObjHome = new List<float>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnEnable()
    {
        ClickRepair.onStatusComfortHome += statusComfortHome;
        // Altar.onStatusComfortHome += statusComfortHome;
        SpawnSpiderHome.onAtackSpiderHome += atackHomeObj;


    }

    private void OnDisable()
    {
        ClickRepair.onStatusComfortHome -= statusComfortHome;
        // Altar.onStatusComfortHome -= statusComfortHome;
        SpawnSpiderHome.onAtackSpiderHome += atackHomeObj;


    }




    /// <summary>
    /// Проверка статуса обьекта на уют в доме
    /// </summary>
    /// <param name="_nameObj"> имя</param>
    /// <param name="_НР"> прочность</param>
    /// <param name="comfort">мак уют</param>
    private void statusComfortHome(string _nameObj, float _healthРoint, float _comfort)
    {
        nameObjHome.Add(_nameObj);
        healthObjHome.Add(_healthРoint);
        comfortObjHome.Add(_comfort);

    }

    private void atackHomeObj (string _nameObj, int _timeWork, DateTime _dataTimeAtack)
    {
        var indexObj = nameObjHome.IndexOf(_nameObj);
        float _comfortAddSparks;

        float comfortGiveSparksInSek = 200 / (60 * 60); // 200 уюта в час = 1 искре
        _comfortAddSparks = _timeWork * comfortObjHome[indexObj] * comfortGiveSparksInSek;
        ///_comfortAddSparks += // healthObjHome[indexObj] -1 уменьшение в минуты

    }






}
