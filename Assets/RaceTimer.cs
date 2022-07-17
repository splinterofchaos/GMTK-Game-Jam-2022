using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimer : MonoBehaviour {
    TMPro.TextMeshProUGUI timerText;
    float start = 0;
    bool stopped = true;

    void OnEnable() {
        timerText = GetComponent<TMPro.TextMeshProUGUI>();

        ArenaEvents.onRaceStarted += StartTimer;
        ArenaEvents.onVictory += OnVictory;
    }

    private void OnDisable() {
        ArenaEvents.onVictory -= OnVictory;
        ArenaEvents.onRaceStarted -= StartTimer;
    }

    void Update() {
        if (stopped) return;
        float lifetime = Time.time - start;
        timerText.text = $"Time: {lifetime:0.00}";
    }

    void StartTimer() {
        start = Time.time;
        stopped = false;
    }

    void OnVictory() => stopped = true;
}
