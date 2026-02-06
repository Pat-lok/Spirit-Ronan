using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("SFX Clips")]
    public AudioClip armoredSliceCorrect;
    public AudioClip armoredSliceWrong;
    public AudioClip splitterSlice;
    public AudioClip friendlyTap;
    public AudioClip bombExplosion;
    public AudioClip gameOverSound;
    public AudioClip comboSound;
    public AudioClip sliceSFX;
    public AudioClip finalSliceSFX;

    private bool sfxMuted = false;
    private bool musicMuted = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (musicSource != null && musicSource.clip != null)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // 🔊 پخش افکت
    public void PlaySFX(AudioClip clip)
    {
        if (sfxMuted || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // 🔇 قطع / وصل SFX
    public void ToggleSFX()
    {
        sfxMuted = !sfxMuted;
        Debug.Log("SFX Muted: " + sfxMuted);
    }

    // 🎵 قطع / وصل موسیقی
    public void ToggleMusic()
    {
        musicMuted = !musicMuted;

        if (musicMuted)
            musicSource.Pause();
        else
            musicSource.Play();
    }
}
