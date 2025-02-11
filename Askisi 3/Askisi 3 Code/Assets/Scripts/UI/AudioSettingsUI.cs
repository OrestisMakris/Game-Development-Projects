// filepath: /C:/Users/orest/OneDrive - University of Patras/Documents/GitHub/Game-Development-Projects/Askisi 3/Askisi 3 Code/Assets/Scripts/Managers/AudioSettingsUI.cs
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start()
    {
        // Initialize sliders with current values.
        masterVolumeSlider.value = AudioListener.volume;
        // Assume AudioManager is already active, or provide default fallback values:
        musicVolumeSlider.value = AudioManager.Instance != null ? GetMusicVolume() : 0.1f;
        sfxVolumeSlider.value = AudioManager.Instance != null ? GetSfxVolume() : 1f;

        // Register callbacks for when their values change.
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    private void OnMasterVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMainMusicVolume(value);
        }
    }

    private void OnSfxVolumeChanged(float value)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSfxVolume(value);
        }
    }

    private float GetMusicVolume()
    {
        
        return AudioManager.Instance != null ? AudioManager.Instance.GetMusicVolume() : 0.1f;
    }

    private float GetSfxVolume()
    {
        return AudioManager.Instance != null ? AudioManager.Instance.GetSfxVolume() : 1f;
    }
}