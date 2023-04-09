using System;
using UnityEngine;

public class MeargGameEvents
{
    public static Action<int, GameObject> onSelectedSlot;
    public static Action<int> onSelectedNullSlot;
    public static Action onClearVariables;
    public static Action onClearOldSlot;
    public static Func<GameObject> onGetOldObject;
    public static Func<GameObject> onGetCurrentObject;
    public static Action<GameObject> onSetOldObject;
    public static Action<GameObject> onStartEventSetOldObject;
    public static Func<Slot_3d> onGetOldSlot;
    public static Func<Slot_3d> onGetCurrentSlot;
    public static Action<Slot_3d> onSetOldSlot;
    public static Func<float> onGetCurrentTimeSpawnOldColumn;
    public static Action<float> onSetTimeToSpawnRuns;


    //public static Action<CanvasGroup> onStartGame;

    //Spider
    public static Func<float> onGetSpiderTime;
    public static Func<int> onGetTiefRunsCount;
    public static Func<int> onGetCurrentSpawnPointHoldSpider;
    public static Func<int> onGetdefrostCount; // возвращает кол-во тапов по льду для его разморозки
    public static Action onStartSpidersTime;
    public static Action<int> onFalseHoldColumn; // разморозить колонку

    public static Action onHoldSpider;   // вызов замараживающего паука
    public static Action onRandomSpider; // вызов перемешивающего руны паука
    public static Action onThiefSpider;  // вызов ворующего руны пауыка

    public static Action onTiefRuns;    // эвент для воровства рун
    public static Action onRandomRuns;    // эвент для воровства рун
    public static Action onAiceRuns;    // эвент для воровства рун
}
