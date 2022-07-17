using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryGui : MonoBehaviour {
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] ScenePicker nextLevelButton;
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
        if (LevelManager.instance != null) {
            string nextLevel = LevelManager.instance.NextLevel();
            if (nextLevel != null) {
                Debug.Log("Set next level to" + nextLevel);
                nextLevelButton.sceneName = nextLevel;
            } else {
                Debug.Log("Deactivated next level button");
                nextLevelButton.gameObject.SetActive(false);
            }
        } else {
            Debug.Log("No level manager");
        }

        finished = true;
    }
}
