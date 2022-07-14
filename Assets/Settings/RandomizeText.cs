using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeText : MonoBehaviour {
    TMPro.TextMeshProUGUI guiText;

    void Start() {
        guiText = GetComponent<TMPro.TextMeshProUGUI>();
        guiText.text = $"THIS IS #{Random.Range(0, 100)}";
    }
}
