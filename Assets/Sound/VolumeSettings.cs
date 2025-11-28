using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mixer;      // Referenz zum AudioMixer
    public Slider slider;         // Der UI-Slider
    public string parameterName;  // z.B. "MusicVolume"

    void Start()
    {
        // Optional: gespeicherte Lautstärke laden
        float savedVolume = PlayerPrefs.GetFloat(parameterName, 0f);
        mixer.SetFloat(parameterName, savedVolume);
        slider.value = savedVolume;
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat(parameterName, volume);
        PlayerPrefs.SetFloat(parameterName, volume);
    }
}
