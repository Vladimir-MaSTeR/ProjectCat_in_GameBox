using UnityEngine;

public class UiSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _fonSource;

    [SerializeField] private AudioClip _click_Clip;
    [SerializeField] private AudioClip _swich_Clip;

    private float _currentVol = 0.5f;

    private void Start()
    {
        ReloadVol();
    }


    private void Update()
    {
        _fonSource.volume = _currentVol;
    }

    private void OnEnable()
    {
        ButtonsEvents.onSaveResouces += SaveVol;
    }

    private void OnDisable()
    {
        ButtonsEvents.onSaveResouces -= SaveVol;
    }

    public void SetVolume(float vol)
    {
        _currentVol = vol;
    }

    public void ClickButtonsSounds()
    {
        _fonSource.PlayOneShot(_click_Clip);
    }
    public void ClickSwichSceneButtons()
    {
        _fonSource.PlayOneShot(_swich_Clip);
    }


    private void SaveVol()
    {
        PlayerPrefs.SetFloat("currentVolUI", _currentVol);
        PlayerPrefs.Save();
    }

    private void ReloadVol()
    {
        if (PlayerPrefs.HasKey("currentVolUI"))
        {
            _currentVol = PlayerPrefs.GetFloat("currentVolUI");
        }
    }

}
