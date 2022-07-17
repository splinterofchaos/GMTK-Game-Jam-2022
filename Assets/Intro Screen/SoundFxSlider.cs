using UnityEngine.UI;
using UnityEngine;

public class SoundFxSlider : MonoBehaviour {
    [SerializeField] Slider slider;
    AudioSource testingAudioSource;

    void Start() {
        slider = GetComponent<Slider>();
        testingAudioSource = GetComponent<AudioSource>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value) {
        GameSettings.instance._soundFxVolume = value;
        testingAudioSource.volume = GameSettings.instance.soundFxVolume;
        if (!testingAudioSource.isPlaying) testingAudioSource.Play();
    }
}
