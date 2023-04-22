using System;
public class HomeEvents {
    public static Action<int> onOpenFireplacePanel; // открывает панель камина
    public static Func<float> onGetTimeToOpenPanel;
}
