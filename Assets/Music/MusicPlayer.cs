using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    AudioSource audioSource;

    private void OnEnable() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (GameSettings.instance == null) return;
        audioSource.volume = GameSettings.instance.musicVolume;
    }

    public void Play() {
        Debug.Log("PLYA");

        audioSource.Play();
    }
}
