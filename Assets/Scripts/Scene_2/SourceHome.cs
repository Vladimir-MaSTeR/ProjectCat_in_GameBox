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
    /// SFX Fireplace amb - ���� ������ �� ����
    /// </summary>
    [Header("SFX Fireplace amb")]
    [SerializeField] private AudioClip _fon_Fireplace;
    /// <summary>
    ///  ���� �� ������� , ��� �������� (error)
    /// </summary>
    [Header("SFX false click")]
    [SerializeField] private AudioClip _click_Error;
    /// <summary>
    /// SFX Fireplace interaction - ���� �� ������
    /// </summary>
    [Header("SFX Fireplace")]
    [SerializeField] private AudioClip _click_Fireplace;
    /// <summary>
    /// SFX Fix button - ������ ������� �� ����� ������� ������(�������)
    /// </summary>
    [Header("SFX Fix button")]
    [SerializeField] private AudioClip _click_RepairObj;
    /// <summary>
    ///  SFX Furniture fix loop - ���� �� ����� ������� ������
    /// </summary>
    [Header("SFX Furniture fix loop")]
    [SerializeField] private AudioClip _fon_RepairObj;
    /// <summary>
    /// SFX Furniture upgrade - ���������� ������(������� ������� ����� ���������� ����� ������, ���������� �������)
    /// </summary>
    [Header("SFX Furniture upgrade")]
    [SerializeField] private AudioClip _click_RepairLvUp;
    /// <summary>
    ///  ������ � ������� ������ ������
    /// </summary>
    [Tooltip("������ ������ � ����")]
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

        SourceHome.onSounds�lickFireplace += click_Fireplace;
        SourceHome.onSounds�lickRepairObj += click_RepairObj;
        SourceHome.onSounds�lickLvUpObj += click_LvUpObj;
        SourceHome.onSounds�lickFalseRepair += click_error;



    }

    private void OnDisable()
    {
        //   SourceHome.onAnimationReadyUpFireplace -= _animReadyUp;

        SourceHome.onSounds�lickFireplace -= click_Fireplace;
        SourceHome.onSounds�lickRepairObj -= click_RepairObj;
        SourceHome.onSounds�lickLvUpObj -= click_LvUpObj;
        SourceHome.onSounds�lickFalseRepair -= click_error;




    }




    //   [Tooltip(" �������� ���-�� ������� <�� ��������, ���� �������, (0 = �������. 1 ���������)>")]
    //  public static Action onAnimationReadyUpFireplace;
    [Tooltip("���� �������(����) �� �����")]
    public static Action onSounds�lickFireplace;
    [Tooltip("���� �������(����) ������ ������ (����������)")]
    public static Action onSounds�lickRepairObj;
    [Tooltip("���� ��������� ������ ������ (��������)")]
    public static Action onSounds�lickLvUpObj;
    [Tooltip("���� ������ �������� ������ (��������)")]
    public static Action onSounds�lickFalseRepair;


    private void fon_Fireplace()
    {
        {
            _fonFireplace.Play();
            _fonFireplace.loop = true;
        }
    }

    private void click_Fireplace()
    {
       // if (!_sourceClick.isPlaying ) // ���� �������
        {
            _sourceClick.clip = _click_Fireplace;
            _sourceClick.Play();
        }
        if (!_fonRepair.isPlaying) // ��� �������
        {
            _fonRepair.clip = _fon_RepairObj;
            _fonRepair.Play();
        }
        Debug.Log(" ���� ������� ������");

    }

    private void click_RepairObj()
    {
       // if (!_sourceClick.isPlaying) // ���� �������
        {
            _sourceClick.clip = _click_RepairObj;
            _sourceClick.Play();
        }
        if (!_fonRepair.isPlaying) // ��� �������
        {
            _fonRepair.clip = _fon_RepairObj;
            _fonRepair.Play();
        }

        Debug.Log(" ���� ������� ");

    }

    private void click_LvUpObj()
    {
       // if (_sourceClick.isPlaying) // ���� �������
        {
            _sourceClick.clip = _click_RepairLvUp;
            _sourceClick.Play();
        }
       // if (_fonRepair.isPlaying) // ��� �������
        {
            _fonRepair.clip = _fon_RepairObj;
            _fonRepair.Stop();
        }

        Debug.Log(" ���� Lv Up");

    }

    private void click_error()
    {
        // if (_sourceClick.isPlaying) // ���� �������
        {
            _sourceClick.clip = _click_Error;
            _sourceClick.Play();
        }


        Debug.Log(" ���� ��� �����");

    }

}
