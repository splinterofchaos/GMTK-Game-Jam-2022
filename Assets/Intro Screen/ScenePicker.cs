using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenePicker : MonoBehaviour {
    [SerializeField] Button button;
    [SerializeField] TMPro.TextMeshProUGUI buttonText;

    [SerializeField] string _sceneName;

    public string sceneName {
        get => _sceneName;
        set {
            _sceneName = value;
            buttonText.text = value;
        }
    }

    public void OnClick() => LevelManager.instance.LoadLevel(sceneName);

    private void OnValidate() {
        sceneName = sceneName;
    }
}
