using System.Linq;
using UnityEngine;

public class FlagManager : MonoBehaviour {
    public static FlagManager instance;

    [SerializeField] Flag[] flags;

    public int laps;
    public int currentFlag;

    private void OnEnable() {
        instance = this;
    }

    private void OnDisable() {
        if (instance == this) instance = null;
    }

    public void OnFlagTouched(Flag touchedFlag) {
        touchedFlag.SetIsNext(false);

        Flag nextFlag =
            flags.FirstOrDefault(f => f.order == touchedFlag.order + 1);

        if (laps == 0) Destroy(touchedFlag.gameObject);

        if (nextFlag == null) {
            laps--;
            nextFlag = flags.First(f => f.order == 0);
        }

        nextFlag.SetIsNext(true);
    }
}
