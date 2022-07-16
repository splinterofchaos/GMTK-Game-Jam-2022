using UnityEngine;

public class FlagCounter : MonoBehaviour {
    [SerializeField] [HideInInspector]
    TMPro.TextMeshProUGUI guiText;

    private void Awake() {
        guiText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void OnEnable() {
        ArenaEvents.lapCountChanged += UpdateCount;
    }

    private void OnDisable() {
        ArenaEvents.lapCountChanged -= UpdateCount;
    }

    string LapsStr(int count) => count == 1 ? "lap" : "laps";

    void UpdateCount(int count) =>
        guiText.text = $"{count} {LapsStr(count)} remaining";
}
