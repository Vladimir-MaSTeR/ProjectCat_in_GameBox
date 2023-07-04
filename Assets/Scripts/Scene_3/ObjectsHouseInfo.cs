using UnityEngine;

[CreateAssetMenu(fileName = "ObjectsHouseInfo", menuName = "Custom/NewObjectHome")]
public class ObjectsHouseInfo : ScriptableObject {

    [SerializeField]
    private IdObjectsHome _id;
    
    [SerializeField]
    private int _level = 0;
    
    [SerializeField]
    private GameObject[] _prefabs;

    #region ÃÅÒÒÅÐÛ È ÑÅÒÒÅÐÛ

    public IdObjectsHome Id => this._id; 
    public GameObject[] Prefabs => this._prefabs;
    public int Level { get => _level; set => _level = value; }

    #endregion
   
}
