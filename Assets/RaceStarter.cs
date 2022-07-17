using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStarter : MonoBehaviour {
    CanvasGroup canvasGroup;
    AudioSource audioSource;
    [SerializeField] TMPro.TextMeshProUGUI readySetGo;

    [SerializeField] AudioClip readyClip;
    [SerializeField] AudioClip setClip;
    [SerializeField] AudioClip goClip;

    [SerializeField] MusicPlayer musicPlayer;

    public float countdownFadeTime = 2;

    IEnumerator Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(readyClip);
        audioSource.volume = GameSettings.instance != null ?
            GameSettings.instance.soundFxVolume : 1;

        readySetGo.text = "3\nReady...";
        yield return new WaitForSeconds(1);
        readySetGo.text = "2\nSet...";
        audioSource.PlayOneShot(setClip);
        yield return new WaitForSeconds(1);
        readySetGo.text = "1\nGo!";
        audioSource.PlayOneShot(goClip);
        if (musicPlayer != null) musicPlayer.Play();
        ArenaEvents.RaceStarted();

        float startTime = Time.time;
        while (Time.time - startTime < countdownFadeTime) {
            float t = (Time.time - startTime) / countdownFadeTime;
            canvasGroup.alpha = 1 - t;
            yield return new WaitForEndOfFrame();
        }
    }
}
