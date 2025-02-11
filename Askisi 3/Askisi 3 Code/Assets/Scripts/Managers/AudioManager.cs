using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Main Music Settings")]
    [SerializeField] private AudioClip mainMusic;       // Main music clip
    [SerializeField, Range(0f, 1f)] private float mainMusicVolume = 0.1f; // Default volume for main music

    [Header("SFX Settings")]
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f; // Default volume for sound effects

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Create and configure the music AudioSource
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = mainMusic;
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = mainMusicVolume;
        musicSource.Play();

        // Create and configure the SFX AudioSource
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = sfxVolume;
    }

    /// <summary>
    /// Plays a one-shot sound effect and allows an optional volume scale override.
    /// </summary>
    /// <param name="clip">The audio clip to play.</param>
    /// <param name="volumeScale">Override multiplier on the default SFX volume (default is 1f).</param>
    public void PlaySound(AudioClip clip, float volumeScale = 1f)
    {
        if(clip == null)
            return;

        sfxSource.PlayOneShot(clip, volumeScale);
    }

    /// <summary>
    /// Updates the main music volume.
    /// </summary>
    /// <param name="volume">A value between 0 and 1.</param>
    public void SetMainMusicVolume(float volume)
    {
        mainMusicVolume = volume;
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }

    /// <summary>
    /// Updates the default SFX volume.
    /// </summary>
    /// <param name="volume">A value between 0 and 1.</param>
    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
        }
    }
}