using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generationFrost : MonoBehaviour
{
    
    /// <summary>
    /// создаваемый обьект 
    /// </summary>
    [SerializeField]
    private GameObject _newFrost;

    /// <summary>
    /// Позиция генерации нового объекта 
    /// </summary>
                    [SerializeField]
    private Vector3 _startFrost;

    /// <summary>
    /// Радиус герерации
    /// </summary>
    [SerializeField]
    private float _radiusGener;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private float speedGener = 1;
    private LayerMask layerN;

    void Start()
    {
        StartCoroutine(newSwon());
    }

   
    private IEnumerator newSwon ()
    {
        yield return new WaitForSeconds(speedGener);
        {
            do
            {
                _startFrost = new Vector3(_ran(-_radiusGener, _radiusGener), _ran(-_radiusGener, _radiusGener), _ran(-_radiusGener, _radiusGener));
            }
            while ((Mathf.Abs(_startFrost.x) + Mathf.Abs(_startFrost.z)) > _radiusGener);



            _startFrost = _startFrost + transform.position;
                // + Мировые координаты обьекта (нужно)
                // (transform.parent.position + transform.position)  ; 
            if (checksVoid(_startFrost) != null)
            {
                var _newObjSwon = Instantiate(_newFrost, _startFrost, Quaternion.identity);
                _newObjSwon.transform.SetParent(gameObject.transform);
            }
        }
        StartCoroutine(newSwon());





    }




    /// <summary>
    /// Проверка на наличие в точки колайдеров
    /// </summary>
    /// <returns></returns>
    private Collider[] checksVoid(Vector3 _position)
    {
        // радиус обьекта
        float _radiusСhecks = 0;
        Vector3 _radiusObj = _newFrost.transform.localScale;

        if (_radiusObj.x > _radiusObj.y)
        {
            _radiusСhecks = _radiusObj.x;
            if (_radiusObj.z > _radiusObj.x)
            {
                _radiusСhecks = _radiusObj.z;
            }
        }
        else if (_radiusObj.z > _radiusObj.y)
        {
            _radiusСhecks = _radiusObj.z;
        }
        else
        {
            _radiusСhecks = _radiusObj.y;
        }
        
        Collider[] _collsControl = Physics.OverlapSphere(_position, _radiusСhecks, layerN);
        return _collsControl;
    }

    /// <summary>
    /// Случайное число от min до max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private float _ran (float min, float max)
    {
       return Random.Range(min, max);
    }


}
