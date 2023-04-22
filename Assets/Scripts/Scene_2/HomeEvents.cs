using System;
public class HomeEvents {
    
    /**
     * ��������� ������ ��������
     */
    public static Action<int> onOpenInfoPanels; 
    
    /**
     * ��������� ������ ��������
     */
    public static Action onOpenResourcesPanel; 
    
    /**
     * ������ �������� ������� ��������
     */
    public static Action onClosedFireplacePanel; 
    public static Action onClosedKitchenPanel; 
    public static Action onClosedArmchairPanel; 
    
    /**
     * ��������� ������� ������������ ��������� ������ �� �������� ��� �������� ��� ������
     */
    public static Func<float> onGetTimeToOpenPanel;

    /**
     * ��������� �������� ������ ������
     */
    public static Func<int> onGetCurrentFireplaceLevel;
    /**
     * ��������� �������� ������ �����
     */
    public static Func<int> onGetCurrentKitchenLevel;
    /**
     * ��������� �������� ������ ������
     */
    public static Func<int> onGetCurrentArmchairLevel;
}
