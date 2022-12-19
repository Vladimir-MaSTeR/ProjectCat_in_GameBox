using UnityEngine;

public class MeargSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _meargSource;

    [SerializeField] private AudioClip _spawnRunsClip;
    [SerializeField] private AudioClip _positiveMeargeClip;
    [SerializeField] private AudioClip _noMeargeClip;
    [SerializeField] private AudioClip _bucketClip;
    [SerializeField] private AudioClip _tapOrderClip;

    private float _currentVol = 0.5f;


    private void OnEnable()
    {
        SoundsEvents.onSpawnRuns += SpawnSonds;
        SoundsEvents.onPositiveMeargeSound += PositiveMeargeSonds;
        SoundsEvents.onNegativeMeargeSound += NoMeargeSonds;
        SoundsEvents.onBucketSounds += BucketSonds;
        SoundsEvents.onTapOrderResouces += TapOrderSonds;
    }

    private void OnDisable()
    {
        SoundsEvents.onSpawnRuns -= SpawnSonds;
        SoundsEvents.onPositiveMeargeSound -= PositiveMeargeSonds;
        SoundsEvents.onNegativeMeargeSound -= NoMeargeSonds;
        SoundsEvents.onBucketSounds -= BucketSonds;
        SoundsEvents.onTapOrderResouces -= TapOrderSonds;
    }

    private void Update()
    {
        _meargSource.volume = _currentVol;
    }

    public void SetVolume(float vol)
    {
        _currentVol = vol;
    }

    private void SpawnSonds()
    {
        _meargSource.PlayOneShot(_spawnRunsClip);
    }

    private void PositiveMeargeSonds()
    {
        _meargSource.PlayOneShot(_positiveMeargeClip);
    }

    private void NoMeargeSonds()
    {
        _meargSource.PlayOneShot(_noMeargeClip);
    }

    private void BucketSonds()
    {
        _meargSource.PlayOneShot(_bucketClip);
    }
    private void TapOrderSonds()
    {
        _meargSource.PlayOneShot(_tapOrderClip);
    }


}
