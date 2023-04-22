using System;
public class HomeEvents {
    
    /**
     * открывает панель предмета
     */
    public static Action<int> onOpenInfoPanels; 
    
    /**
     * Открывает панель ресурсов
     */
    public static Action onOpenResourcesPanel; 
    
    /**
     * Эвенты закрытия панелей предмета
     */
    public static Action onClosedFireplacePanel; 
    public static Action onClosedKitchenPanel; 
    public static Action onClosedArmchairPanel; 
    
    /**
     * получение времени необходимого удержания пальца на предмете для открытия его панели
     */
    public static Func<float> onGetTimeToOpenPanel;

    /**
     * Получение текущего уровня камина
     */
    public static Func<int> onGetCurrentFireplaceLevel;
    /**
     * Получение текущего уровня кухни
     */
    public static Func<int> onGetCurrentKitchenLevel;
    /**
     * Получение текущего уровня кресла
     */
    public static Func<int> onGetCurrentArmchairLevel;
}
