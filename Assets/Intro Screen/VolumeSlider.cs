using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    Slider slider;

    void Start() {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value) {
        GameSettings.instance.globalVolume = value;
    }
}
