using System;
using UnityEngine;
public class EndAnim : MonoBehaviour {
    public void EndAnimation() {
        // Debug.Log("�� ���� ������ ����������� ���� ����� ���������");
        Destroy(this.gameObject.GetComponentInParent<Item_3d>().gameObject);
        MeargGameEvents.onSetStartTimerTrue?.Invoke();
    }
}
