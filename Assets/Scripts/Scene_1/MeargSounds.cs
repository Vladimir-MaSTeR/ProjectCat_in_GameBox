using UnityEngine;
using UnityEngine.Serialization;
public class MeargSounds : MonoBehaviour {
    
    #region œ≈–≈ÃÂÕÕ€≈ ƒ¬»∆ ¿

    [SerializeField]
    private AudioSource _meargSource;

    [SerializeField]
    private AudioClip _spawnRunsClip;
    [SerializeField]
    private AudioClip _positiveMeargeClip;
    [SerializeField]
    private AudioClip _noMeargeClip;
    [SerializeField]
    private AudioClip _boxOpenClip;
    [SerializeField]
    private AudioClip _tapOrderClip;
    [SerializeField]
    private AudioClip _mathClip;

    [Space(20)]
    [Header("¿ÎÚ‡¸")]
    [SerializeField]
    private AudioClip _altareUpClip;
    [SerializeField]
    private AudioClip _altareDownClip;
    [SerializeField]
    private AudioClip _altareOpenClip;

    [Space(20)]
    [Header("œ‡ÛÍË")]
    [SerializeField]
    private AudioClip _startHoldSpiderClip;
    [SerializeField]
    private AudioClip _startTiefSpiderClip;
    [SerializeField]
    private AudioClip _startRandomSpiderClip;
    [SerializeField]
    private AudioClip _deathSpiderClip;

    #endregion

    private float _currentVol = 0.5f;


    private void OnEnable() {
        SoundsEvents.onSpawnRuns += SpawnSonds;
        SoundsEvents.onPositiveMeargeSound += PositiveMeargeSonds;
        SoundsEvents.onNegativeMeargeSound += NoMeargeSonds;
        SoundsEvents.onBoxOpenSounds += BoxOpenSonds;
        SoundsEvents.onTapOrderResouces += TapOrderSonds;
        SoundsEvents.onMathSound += MathSouds;

        SoundsEvents.onHoldSpiderSound += HoldSpiderSounds;
        SoundsEvents.onThiefSpiderSound += TiefSpiderSounds;
        SoundsEvents.onRandomSpiderSound += RandomSpiderSounds;
        SoundsEvents.onDeathSpider += DeathSpiderSounds;

        SoundsEvents.onAltareUp += AltareUpSounds;
        SoundsEvents.onAltareDown += AltareDownSouds;
        SoundsEvents.onAltareOpen += AltareOpenSounds;
    }

    private void OnDisable() {
        SoundsEvents.onSpawnRuns -= SpawnSonds;
        SoundsEvents.onPositiveMeargeSound -= PositiveMeargeSonds;
        SoundsEvents.onNegativeMeargeSound -= NoMeargeSonds;
        SoundsEvents.onBoxOpenSounds -= BoxOpenSonds;
        SoundsEvents.onTapOrderResouces -= TapOrderSonds;
        SoundsEvents.onMathSound -= MathSouds;

        SoundsEvents.onHoldSpiderSound -= HoldSpiderSounds;
        SoundsEvents.onThiefSpiderSound -= TiefSpiderSounds;
        SoundsEvents.onRandomSpiderSound -= RandomSpiderSounds;
        SoundsEvents.onDeathSpider -= DeathSpiderSounds;

        SoundsEvents.onAltareUp -= AltareUpSounds;
        SoundsEvents.onAltareDown -= AltareDownSouds;
        SoundsEvents.onAltareOpen -= AltareOpenSounds;
    }

    private void Update() { _meargSource.volume = _currentVol; }
    public void SetVolume(float vol) { _currentVol = vol; }

    #region SPIDERS

    private void DeathSpiderSounds() { _meargSource.PlayOneShot(_deathSpiderClip); }
    private void RandomSpiderSounds() { _meargSource.PlayOneShot(_startRandomSpiderClip); }
    private void TiefSpiderSounds() { _meargSource.PlayOneShot(_startTiefSpiderClip); }
    private void HoldSpiderSounds() { _meargSource.PlayOneShot(_startHoldSpiderClip); }

    #endregion

    #region ALTARE

    private void AltareOpenSounds() { _meargSource.PlayOneShot(_altareOpenClip); }
    private void AltareDownSouds() { _meargSource.PlayOneShot(_altareDownClip); }
    private void AltareUpSounds() { _meargSource.PlayOneShot(_altareUpClip); }

    #endregion

    private void MathSouds() { _meargSource.PlayOneShot(_mathClip); }

    private void SpawnSonds() { _meargSource.PlayOneShot(_spawnRunsClip); }

    private void PositiveMeargeSonds() { _meargSource.PlayOneShot(_positiveMeargeClip); }

    private void NoMeargeSonds() { _meargSource.PlayOneShot(_noMeargeClip); }


    private void BoxOpenSonds() { _meargSource.PlayOneShot(_boxOpenClip); }
    private void TapOrderSonds() { _meargSource.PlayOneShot(_tapOrderClip); }
}
