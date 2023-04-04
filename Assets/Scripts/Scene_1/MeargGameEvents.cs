using System;
using UnityEngine;

public class MeargGameEvents
{
    public static Action<int, GameObject> onSelectedSlot;
    public static Action<int> onSelectedNullSlot;
    public static Action onClearVariables;
    public static Func<GameObject> onGetOldObject;
    public static Func<Slot_3d> onGetOldSlot;
    public static Func<float> onGetCurrentTimeSpawnOldColumn;
    public static Action<float> onSetTimeToSpawnRuns;

    public static Action<GameObject> onStartMeargThree;

    //Spider
    public static Func<float> onGetSpiderTime;
    public static Func<int> onGetTiefRunsCount;
    public static Func<int> onGetCurrentSpawnPointHoldSpider;
    public static Func<int> onGetdefrostCount; // ���������� ���-�� ����� �� ���� ��� ��� ����������
    public static Action onStartSpidersTime;
    public static Action<int> onFalseHoldColumn; // ����������� �������

    public static Action onHoldSpider;   // ����� ��������������� �����
    public static Action onRandomSpider; // ����� ��������������� ���� �����
    public static Action onThiefSpider;  // ����� ��������� ���� ������

    public static Action onTiefRuns;    // ����� ��� ��������� ���
    public static Action onRandomRuns;    // ����� ��� ��������� ���
    public static Action onAiceRuns;    // ����� ��� ��������� ���
}
