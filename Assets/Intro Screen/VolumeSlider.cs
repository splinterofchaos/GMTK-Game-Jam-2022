using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    Slider slider;
    AudioSource testingAudioSource;

    void Start() {
        slider = GetComponent<Slider>();
        testingAudioSource = GetComponent<AudioSource>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value) {
        GameSettings.instance.globalVolume = value;
        testingAudioSource.volume = value;
        if (!testingAudioSource.isPlaying) testingAudioSource.Play();
    }
}
