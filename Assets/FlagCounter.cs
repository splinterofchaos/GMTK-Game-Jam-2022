using UnityEngine;

public class FlagCounter : MonoBehaviour {
    TMPro.TextMeshProUGUI guiText;

    private void Start() {
        guiText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void OnEnable() {
        ArenaEvents.flagCountChanged += UpdateCount;
    }

    private void OnDisable() {
        ArenaEvents.flagCountChanged -= UpdateCount;
    }

    void UpdateCount(int count) => guiText.text = $"{count} flags remaining";
}
