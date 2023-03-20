using System;
using UnityEngine;

public class MeargGameEvents
{
    public static Action<int, GameObject> onSelectedSlot;
    public static Action<int> onSelectedNullSlot;
    public static Action onClearVariables;
    public static Func<GameObject> onGetOldObject;
    public static Func<Slot_3d> onGetOldSlot;
}
