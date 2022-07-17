using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStarter : MonoBehaviour {
    CanvasGroup canvasGroup;
    AudioSource audioSource;
    [SerializeField] TMPro.TextMeshProUGUI readySetGo;
    public float countdownFadeTime = 2;

    IEnumerator Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.volume = GameSettings.instance != null ?
            GameSettings.instance.soundFxVolume : 1;

        readySetGo.text = "3\nReady...";
        yield return new WaitForSeconds(1);
        readySetGo.text = "2\nSet...";
        audioSource.Play();
        yield return new WaitForSeconds(1);
        readySetGo.text = "1\nGo!";
        audioSource.Play();
        ArenaEvents.RaceStarted();

        float startTime = Time.time;
        while (Time.time - startTime < countdownFadeTime) {
            float t = (Time.time - startTime) / countdownFadeTime;
            canvasGroup.alpha = 1 - t;
            yield return new WaitForEndOfFrame();
        }
    }
}
