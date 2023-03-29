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

    //Spider
    public static Func<float> onGetSpiderTime;
    public static Action onStartSpidersTime;

    public static Action onHoldSpider;   // ����� ��������������� �����
    public static Action onRandomSpider; // ����� ��������������� ���� �����
    public static Action onThiefSpider;  // ����� ��������� ���� ������
}
