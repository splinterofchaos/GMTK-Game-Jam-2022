using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGui : MonoBehaviour {
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GameObject loadNextLevelText;
    [SerializeField] Transform raceTimerDestination;
    [SerializeField] GameObject raceTimer;

    [SerializeField] float raceTimerMoveSpeed = 10;
    [SerializeField] float alphaIncreaseTime = 3;

    bool finished = false;

    private void OnEnable() {
        ArenaEvents.onVictory += OnVictory;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    void OnDisable() {
        ArenaEvents.onVictory -= OnVictory;
    }

    private void Update() {
        if (!finished) return;

        raceTimer.transform.position =
            Vector3.MoveTowards(raceTimer.transform.position,
                                raceTimerDestination.position,
                                raceTimerMoveSpeed);
        canvasGroup.alpha = Mathf.Min(
            canvasGroup.alpha + (Time.deltaTime / alphaIncreaseTime), 1);
    }

    void OnVictory() {
        if (LevelManager.instance == null ||
            LevelManager.instance.NextLevel() == null) {
            loadNextLevelText.gameObject.SetActive(false);
        }

        finished = true;
    }
}
