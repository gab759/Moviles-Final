using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioController audioController;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSfx;

    void Start()
    {
        // Seteamos los valores iniciales
        sliderMaster.value = audioController.GetMasterVolume();
        sliderMusic.value = audioController.GetMusicVolume();
        sliderSfx.value = audioController.GetSFXVolume();

        // Asignamos los eventos
        sliderMaster.onValueChanged.AddListener(audioController.SetMasterVolume);
        sliderMusic.onValueChanged.AddListener(audioController.SetMusicVolume);
        sliderSfx.onValueChanged.AddListener(audioController.SetSFXVolume);
    }
}