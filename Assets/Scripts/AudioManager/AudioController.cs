using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer _compAudioMixer;

    [SerializeField] private VolumeConfig masterVolumeConfig;
    [SerializeField] private VolumeConfig musicVolumeConfig;
    [SerializeField] private VolumeConfig sfxVolumeConfig;
    public float GetMasterVolume() => masterVolumeConfig.volume;
    public float GetMusicVolume() => musicVolumeConfig.volume;
    public float GetSFXVolume() => sfxVolumeConfig.volume;
    void Awake()
    {
        ApplyVolumes();
    }

    public void SetMasterVolume(float value)
    {
        masterVolumeConfig.volume = Mathf.Clamp(value, 0f, 1f);
        _compAudioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
        musicVolumeConfig.volume = Mathf.Clamp(value, 0f, 1f);
        _compAudioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolumeConfig.volume = Mathf.Clamp(value, 0f, 1f);
        _compAudioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void ApplyVolumes()
    {
        SetMasterVolume(masterVolumeConfig.volume);
        SetMusicVolume(musicVolumeConfig.volume);
        SetSFXVolume(sfxVolumeConfig.volume);
    }
}