using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComfortManager : MonoBehaviour
{
    [Tooltip("����� ������ �� ����")]
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
    /// �������� ������� ������� �� ��� � ����
    /// </summary>
    /// <param name="_nameObj"> ���</param>
    /// <param name="_��"> ���������</param>
    /// <param name="comfort">��� ���</param>
    private void statusComfortHome(string _nameObj, float _health�oint, float _comfort)
    {
        nameObjHome.Add(_nameObj);
        healthObjHome.Add(_health�oint);
        comfortObjHome.Add(_comfort);

    }

    private void atackHomeObj (string _nameObj, int _timeWork, DateTime _dataTimeAtack)
    {
        var indexObj = nameObjHome.IndexOf(_nameObj);
        float _comfortAddSparks;

        float comfortGiveSparksInSek = 200 / (60 * 60); // 200 ���� � ��� = 1 �����
        _comfortAddSparks = _timeWork * comfortObjHome[indexObj] * comfortGiveSparksInSek;
        ///_comfortAddSparks += // healthObjHome[indexObj] -1 ���������� � ������

    }






}
