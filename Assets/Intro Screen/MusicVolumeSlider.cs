using UnityEngine.UI;
using UnityEngine;

public class MusicVolumeSlider : MonoBehaviour {
    [SerializeField] Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value) {
        GameSettings.instance._musicVolume = value;
    }
}
