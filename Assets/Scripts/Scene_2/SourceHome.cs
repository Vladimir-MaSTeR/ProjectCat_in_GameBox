using UnityEngine.SceneManagement;
using UnityEngine;

public class SourceHome : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    /// <summary>
    /// SFX Fireplace amb - ���� ������ �� ����
    /// </summary>
    [Header("SFX Fireplace amb")]
    [SerializeField] private AudioClip _fon_Fireplace;
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







    private void Start()
    {
    

    }


}
