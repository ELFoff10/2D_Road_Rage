using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private const string VolumeParameter = "Volume";

    private void Start()
    {
        // Load the saved volume level or set a default value
        float savedVolume = PlayerPrefs.GetFloat(VolumeParameter, 0.5f);
        volumeSlider.value = savedVolume;
        SetVolume();
    }

    private void SetVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat(VolumeParameter, Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat(VolumeParameter, volume);
        PlayerPrefs.Save();
    }
}
