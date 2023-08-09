using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioSource soundtrackAudioSource;
    private Slider volumeSlider;

    void Start()
    {
        volumeSlider = GetComponent<Slider>();

        // Set the initial slider value to the current volume
        volumeSlider.value = soundtrackAudioSource.volume; 
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        soundtrackAudioSource.volume = volume;
    }
}