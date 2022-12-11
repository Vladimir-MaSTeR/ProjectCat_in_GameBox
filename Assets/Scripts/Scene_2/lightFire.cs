using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightFire : MonoBehaviour
{
    /// <summary>
    ///  Источник света (огонь)
    /// </summary>
    [SerializeField]
    private Light _fire;
    /// <summary>
    /// Интенсивность в прошлый кадр
    /// </summary>
    private float _fireIntensity;
    /// <summary>
    /// класс с уровнем предмета
    /// </summary>
 //   [SerializeField]   private ClickFireplace _clFireplace;
    /// <summary>
    /// уровнь предмета
    /// </summary>
    [SerializeField]
    private int _LvFire ;
    private float _lightMin;
    private float _lightMax;


    void Awake ()
    {
       // _LvFire = _clFireplace.LvFireplaceNow;
       // _fire = transform.GetComponent<Light>();

    }


    // Update is called once per frame
    void Update()
    {
        // _LvFire = _clFireplace.LvFireplaceNow;
        _lightMin = _LvFire * 6 ;
        _lightMax = _LvFire * 9 ;
        var _deltaLight = (_lightMax - _lightMin) / (60*1.5f);
        _dynamicsLight(_lightMin, _lightMax, _deltaLight);


    }

    private void _dynamicsLight(float _lightMin, float _lightMax, float _deltaLight )
    {


        if (_LvFire > 0)
        {
            if (_fire.intensity <= _lightMin)
            {
                _fire.intensity = _lightMin + _deltaLight;
                _fireIntensity = _lightMin;

            }
            else if (_fire.intensity >= _lightMax)
            {
                _fire.intensity = _lightMax - _deltaLight;
                _fireIntensity = _lightMax;

            }
            else if (_fireIntensity > _fire.intensity)
            {
                _fireIntensity = _fire.intensity;
                _fire.intensity = _fire.intensity - _deltaLight;
            }
            else if (_fireIntensity < _fire.intensity)
            {
                _fireIntensity = _fire.intensity;
                _fire.intensity = _fire.intensity + _deltaLight;
            }


        }
    }




}
