using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStarter : MonoBehaviour {
    CanvasGroup canvasGroup;
    [SerializeField] TMPro.TextMeshProUGUI readySetGo;
    public float countdownFadeTime = 2;

    IEnumerator Start() {
        canvasGroup = GetComponent<CanvasGroup>();

        readySetGo.text = "3\nReady...";
        yield return new WaitForSeconds(1);
        readySetGo.text = "2\nSet...";
        yield return new WaitForSeconds(1);
        readySetGo.text = "1\nGo!";
        ArenaEvents.RaceStarted();

        float startTime = Time.time;
        while (Time.time - startTime < countdownFadeTime) {
            float t = (Time.time - startTime) / countdownFadeTime;
            canvasGroup.alpha = 1 - t;
            yield return new WaitForEndOfFrame();
        }
    }
}
