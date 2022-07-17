using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour {
    [SerializeField] LevelManager levelManager;

    [SerializeField] ScenePicker scenePickerPrefab;

    void Start() {
        foreach (string level in levelManager.levels) {
            ScenePicker picker = Instantiate(scenePickerPrefab, transform);
            picker.sceneName = level;
        }
    }
}
