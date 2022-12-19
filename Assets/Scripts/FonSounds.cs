using UnityEngine;
using UnityEngine.SceneManagement;

public class FonSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _fonSource;

    [SerializeField] private AudioClip _mearg_fonClip;
    [SerializeField] private AudioClip _home_fonClip;

    private float _currentVol = 0.5f;

    //[SerializeField] private AudioClip clip;


    private void Start()
    {
        ReloadVol();

        if (SceneManager.GetActiveScene().buildIndex == SceneIndexConstants.MEARG_SCENE_INDEX 
            || SceneManager.GetActiveScene().buildIndex == SceneIndexConstants.HISTORY_SCENE_INDEX)
        {
            _fonSource.clip = _mearg_fonClip;
            _fonSource.Play();
           
            _fonSource.loop = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == SceneIndexConstants.HOME_SCENE_INDEX)
        {
            _fonSource.clip = _home_fonClip;
            _fonSource.Play();
            
            _fonSource.loop = true;
        }
    }

    private void OnEnable()
    {
        ButtonsEvents.onSaveResouces += SaveVol;
    }

    private void OnDisable()
    {
        ButtonsEvents.onSaveResouces -= SaveVol;
    }

    private void Update()
    {
        _fonSource.volume = _currentVol;
    }

    public void SetVolume(float vol)
    {
        _currentVol = vol;
    }

    private void SaveVol()
    {
        PlayerPrefs.SetFloat("currentVolFon", _currentVol);
        PlayerPrefs.Save();
    }

    private void ReloadVol()
    {
        if (PlayerPrefs.HasKey("currentVolFon"))
        {
            _currentVol = PlayerPrefs.GetFloat("currentVolFon");
        }
    }

}
