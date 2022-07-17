using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimer : MonoBehaviour {
    TMPro.TextMeshProUGUI timerText;
    float start = 0;
    bool stopped = false;

    void OnEnable() {
        timerText = GetComponent<TMPro.TextMeshProUGUI>();
        start = Time.time;

        ArenaEvents.onVictory += OnVictory;
    }

    private void OnDisable() {
        ArenaEvents.onVictory -= OnVictory;
    }

    void Update() {
        if (stopped) return;
        float lifetime = Time.time - start;
        timerText.text = $"Time: {lifetime:0.00}";
    }

    void OnVictory() => stopped = true;
}
