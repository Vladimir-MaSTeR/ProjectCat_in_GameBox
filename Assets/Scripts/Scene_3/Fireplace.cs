using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class Fireplace : MonoBehaviour, IPointerClickHandler {
    
    [Tooltip("—криптоблќбджект камина")]
    [SerializeField]
    private ObjectsHouseInfo _fireplaceInfo;

    private void Awake() { _fireplaceInfo.Level = 0; }

    private void Start() {
        if(_fireplaceInfo.Level == 0) {
            GameObject instantiateObject = Instantiate(_fireplaceInfo.Prefabs[0], 
            new Vector3(transform.position.x, transform.position.y, transform.position.z), 
            Quaternion.identity);
            instantiateObject.transform.SetParent(this.transform);
        }

        if(_fireplaceInfo.Level == 1) {
            GameObject instantiateObject = Instantiate(_fireplaceInfo.Prefabs[1], 
            new Vector3(transform.position.x, transform.position.y, transform.position.z), 
            Quaternion.identity);
            instantiateObject.transform.SetParent(this.transform);
        }
        
        if(_fireplaceInfo.Level == 2) {
            GameObject instantiateObject = Instantiate(_fireplaceInfo.Prefabs[2], 
            new Vector3(transform.position.x, transform.position.y, transform.position.z), 
            Quaternion.identity);
            instantiateObject.transform.SetParent(this.transform);
        }
    }



    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log($" сть клик по камину, его id = {_fireplaceInfo.Id} и его уровень = {_fireplaceInfo.Level}");
        HomeEvents.onOpenInfoPanels?.Invoke(_fireplaceInfo.Id);
    }
}
