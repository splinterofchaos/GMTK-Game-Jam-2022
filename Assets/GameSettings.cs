using UnityEngine;

public class GameSettings : MonoBehaviour {
    public static GameSettings instance;

    public float globalVolume;
    public float _soundFxVolume;
    public float _musicVolume;

    public float soundFxVolume => globalVolume * _soundFxVolume;
    public float musicVolume => globalVolume * _musicVolume;

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
