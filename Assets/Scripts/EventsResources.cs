using System;
using System.Collections.Generic;
using UnityEngine;

public class EventsResources
{
    //"�������� ���-�� ����� <�� ��������, ���� �������, (0 = �������. 1 ���������)>"
    public static Action<int, int, int> onClouthInBucket; // ����� | ������ ��. - �� ��������.
                                                          //         ������ ��. - ���� �������.
                                                          //         ������ ��. - ��������� ��� ������� ������ (0 = �������. 1 ���������)

    //" �������� ���-�� ������ <�� ��������, ���� �������, (0 = �������. 1 ���������)>"
    public static Action<int, int, int> onLogInBucket;   // ������ | ������ ��. - �� ��������.
                                                         //          ������ ��. - ���� �������.
                                                         //          ������ ��. - ��������� ��� ������� ������ (0 = �������. 1 ���������)   

    //�������� ���-�� ������<�� ��������, ���� �������, (0 = �������. 1 ���������)>"
    public static Action<int, int, int> onStoneInBucket;  // ������ | ������ ��. - �� ��������.
                                                          //          ������ ��. - ���� �������.
                                                          //          ������ ��. - ��������� ��� ������� ������ (0 = �������. 1 ���������)  

    [Tooltip(" �������� ���-�� ������� <�� ��������, ���� �������, (0 = �������. 1 ���������)>")]
    public static Action<int, int, int> onNeilInBucket;  // ������ | ������ ��. - �� ��������.
                                                         //          ������ ��. - ���� �������.
                                                         //          ������ ��. - ��������� ��� ������� ������ (0 = �������. 1 ���������)

    public static Action<bool> onHoldBucket;


    public static Func<int, int> onGetCurentClouth; // ���������� ���������� ������� ����������� �����
    public static Func<int, int> onGetCurentLog;    // ���������� ���������� ������� ����������� ������
    public static Func<int, int> onGetCurentStone;  // ���������� ���������� ������� ����������� �����
    public static Func<int, int> onGetCurentNeil;   // ���������� ���������� ������� ����������� �������


//---------------------------------------------------------------------------------------------------------------------------------
    public static Func<int> onGetComfortCurrentValue; // ���������� ���������� ������� ����������� ����
    public static Action<int, bool> onAddOrDeductComfortValue; // �������� ���-�� ����
                                                               //   ������ ��. - ���-�� �������.
                                                               //   ������ ��. - ��������� ��� ������� ������.(true = ���������. false �������)
    public static Action<int> onUpdateComfortValue; // ����� ���������� ��� ���������� ������ ����

    public static Func<int> onGetSparkCurrentValue;   // ���������� ���������� ������� ����������� �������
    public static Action<int, bool> onAddOrDeductSparkValue; // �������� ���-�� �������
                                                             //   ������ ��. - ���-�� �������.
                                                             //   ������ ��. - ��������� ��� ������� ������.(true = ���������. false �������)
    public static Action<int> onUpdateSparkValue; // ����� ���������� ��� ���������� ������ �������

    //---------------------------------------------------------------------------------------------------------------------------------

    [Tooltip("����� �������� ������� ������ �������� � �������� ������� �� ����������� �������� ��� ������")]
    public static Func<int, IDictionary<string, int>> onGetFireplaceDictionary;

    [Tooltip("����� �������� ������� ������ �������� � �������� ������� �� ����������� �������� ��� �����")]
    public static Func<int, IDictionary<string, int>> onGetChairDictionary;

    [Tooltip("����� �������� ������� ������ �������� � �������� ������� �� ����������� �������� ��� �����")]
    public static Func<int, IDictionary<string, int>> onGetTableDictionary;

    [Tooltip("���������� ���������� ������� �������� ���� � �� ������� � ����")]
    public static Func<int, IDictionary<string, int>> onGetLvObjHome;

    [Tooltip("���������� ���������� ������� ������� ��� ���������� ������")]
    //public static Func<int, IDictionary<string, int>> onGetQuestsDictionary;


    public static Action onUpdateQuest;
    
    [Tooltip("����� ���������� ������  �� �������� ������, �������� ����� ����� ��������� ������� ������")]
    public static Action onEndFireplaceQuest;

    [Tooltip("����� ���������� ������  �� �������� �����, �������� ����� ����� ��������� ������� �����")]
    public static Action onEndChairQuest;

    [Tooltip("����� ���������� ������  �� �������� �����, �������� ����� ����� ��������� ������� �����")]
    public static Action onEndTableQuest;

    [Tooltip("����� ��������� ��������� ���������� ������ �� �������� ")]
    public static Action onAnimationReadyUpFireplace;

    [Tooltip("����� ��������� ��������� ���������� ����� �� ��������")]
    public static Action onAnimationReadyUpChair;

    [Tooltip("����� ��������� ��������� ����������  ����� �� ��������")]
    public static Action onAnimationReadyUpTable;


    [Tooltip("����� ���������� �������� ������")]
    public static Action onEndMainQuest;

    [Tooltip("����� ��������� ������  �� �������� ������")]
    public static Action<int> onFireplaceQuest;
    [Tooltip("����� ��������� ������  �� �������� �����")]
    public static Action<int> onChairQuest;
    [Tooltip("����� ��������� ������  �� �������� �����")]
    public static Action<int> onTableQuest;

    [Tooltip("����� ��� ������ ������ ��������")]
    public static Action<string> onSpawnItem;



}