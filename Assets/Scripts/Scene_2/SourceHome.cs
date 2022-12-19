using UnityEngine.SceneManagement;
using UnityEngine;

public class SourceHome : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    /// <summary>
    /// SFX Fireplace amb - звук камина на фоне
    /// </summary>
    [Header("SFX Fireplace amb")]
    [SerializeField] private AudioClip _fon_Fireplace;
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







    private void Start()
    {
    

    }


}
