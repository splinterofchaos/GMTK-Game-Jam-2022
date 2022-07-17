using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;

    public string[] levels;
    public string currentLevel;
    public string titleScene;

    private void OnEnable() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable() {
        if (instance == this) instance = null;
    }

    public void LoadTitle() {
        currentLevel = null;
        SceneManager.LoadScene(titleScene);
    }

    public void LoadLevel(string name) {
        currentLevel = name;
        SceneManager.LoadScene(name);
    }

    public string NextLevel() {
        int i = 0;
        for (; i < levels.Length; ++i) {
            if (levels[i] == currentLevel) break;
        }

        return i + 1 >= levels.Length ? null : levels[i + 1];
    }

    public void LoadNextLevel() {
        string next = NextLevel();
        if (next != null) LoadLevel(next);
    }
}
