using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SourceHome : MonoBehaviour
{
    [SerializeField] private AudioSource _fonFireplace;

    [SerializeField] private AudioSource _sourceClick;

    [SerializeField] private AudioSource _fonRepair;

    /// <summary>
    /// SFX Fireplace amb - звук камина на фоне
    /// </summary>
    [Header("SFX Fireplace amb")]
    [SerializeField] private AudioClip _fon_Fireplace;
    /// <summary>
    ///  клик по объекту , нет ресурсов (error)
    /// </summary>
    [Header("SFX false click")]
    [SerializeField] private AudioClip _click_Error;
    /// <summary>
    /// SFX Fireplace interaction - клик по камину
    /// </summary>
    [Header("SFX Fireplace")]
    [SerializeField] private AudioClip _click_Fireplace;
    /// <summary>
    /// SFX Fix button - кнопка нажатия во время починки мебели(молоток)
    /// </summary>
    [Header("SFX Fix button")]
    [SerializeField] private AudioClip _click_RepairObj;
    /// <summary>
    ///  SFX Furniture fix loop - звук во время починки мебели
    /// </summary>
    [Header("SFX Furniture fix loop")]
    [SerializeField] private AudioClip _fon_RepairObj;
    /// <summary>
    /// SFX Furniture upgrade - улучьшение мебели(озвучка момента когда появляется новая мебель, происходит апгрейд)
    /// </summary>
    [Header("SFX Furniture upgrade")]
    [SerializeField] private AudioClip _click_RepairLvUp;
    /// <summary>
    ///  объект с классом камина ремонт
    /// </summary>
    [Tooltip("объект камина в доме")]
    [SerializeField] private GameObject _fireplaceObje;

    void Start()
    {
        if (_fireplaceObje.GetComponent<ClickRepair>().LvObj > 0)
        {
            fon_Fireplace();
        }


    }



    private void OnEnable()
    {
        //    SourceHome.onAnimationReadyUpFireplace += _animReadyUp;

        SourceHome.onSoundsСlickFireplace += click_Fireplace;
        SourceHome.onSoundsСlickRepairObj += click_RepairObj;
        SourceHome.onSoundsСlickLvUpObj += click_LvUpObj;
        SourceHome.onSoundsСlickFalseRepair += click_error;



    }

    private void OnDisable()
    {
        //   SourceHome.onAnimationReadyUpFireplace -= _animReadyUp;

        SourceHome.onSoundsСlickFireplace -= click_Fireplace;
        SourceHome.onSoundsСlickRepairObj -= click_RepairObj;
        SourceHome.onSoundsСlickLvUpObj -= click_LvUpObj;
        SourceHome.onSoundsСlickFalseRepair -= click_error;




    }




    //   [Tooltip(" Изменить Кол-во гвоздей <Ур предмета, колл ресурса, (0 = убавить. 1 прибавить)>")]
    //  public static Action onAnimationReadyUpFireplace;
    [Tooltip("звук нажатие(клик) на камин")]
    public static Action onSoundsСlickFireplace;
    [Tooltip("звук нажатие(клик) ремонт объета (деревянный)")]
    public static Action onSoundsСlickRepairObj;
    [Tooltip("звук повышения уровня объета (предмета)")]
    public static Action onSoundsСlickLvUpObj;
    [Tooltip("звук ошибки улучшить объета (предмета)")]
    public static Action onSoundsСlickFalseRepair;


    private void fon_Fireplace()
    {
        {
            _fonFireplace.Play();
            _fonFireplace.loop = true;
        }
    }

    private void click_Fireplace()
    {
       // if (!_sourceClick.isPlaying ) // звук ремонта
        {
            _sourceClick.clip = _click_Fireplace;
            _sourceClick.Play();
        }
        if (!_fonRepair.isPlaying) // фон ремонта
        {
            _fonRepair.clip = _fon_RepairObj;
            _fonRepair.Play();
        }
        Debug.Log(" Звук ремонта камина");

    }

    private void click_RepairObj()
    {
       // if (!_sourceClick.isPlaying) // звук ремонта
        {
            _sourceClick.clip = _click_RepairObj;
            _sourceClick.Play();
        }
        if (!_fonRepair.isPlaying) // фон ремонта
        {
            _fonRepair.clip = _fon_RepairObj;
            _fonRepair.Play();
        }

        Debug.Log(" Звук ремонта ");

    }

    private void click_LvUpObj()
    {
       // if (_sourceClick.isPlaying) // звук ремонта
        {
            _sourceClick.clip = _click_RepairLvUp;
            _sourceClick.Play();
        }
       // if (_fonRepair.isPlaying) // фон ремонта
        {
            _fonRepair.clip = _fon_RepairObj;
            _fonRepair.Stop();
        }

        Debug.Log(" Звук Lv Up");

    }

    private void click_error()
    {
        // if (_sourceClick.isPlaying) // звук ремонта
        {
            _sourceClick.clip = _click_Error;
            _sourceClick.Play();
        }


        Debug.Log(" Звук нет ресов");

    }

}
