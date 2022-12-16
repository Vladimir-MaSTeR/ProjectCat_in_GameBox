using UnityEngine.SceneManagement;
using UnityEngine;

public class FonAndUiSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    [SerializeField] private AudioClip _mearg_fonClip;
    [SerializeField] private AudioClip _home_fonClip;

    //[SerializeField] private AudioClip clip;


    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneIndexConstants.MEARG_SCENE_INDEX)
        {
            _source.PlayOneShot(_mearg_fonClip);
            _source.loop = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == SceneIndexConstants.HOME_SCENE_INDEX)
        {
            _source.PlayOneShot(_home_fonClip);
            _source.loop = true;
        }

    }



}
