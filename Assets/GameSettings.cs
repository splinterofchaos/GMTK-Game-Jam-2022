using UnityEngine;

public class GameSettings : MonoBehaviour {
    public static GameSettings instance;

    public float globalVolume;
    public float _soundFxVolume;

    public float soundFxVolume => globalVolume * _soundFxVolume;

    private void Awake() {
        instance = this;
    }
}
