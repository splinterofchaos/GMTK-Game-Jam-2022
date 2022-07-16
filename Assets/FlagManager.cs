using System.Collections;
using System.Linq;
using UnityEngine;

public class FlagManager : MonoBehaviour {
    public static FlagManager instance;

    Flag[] flags;

    public int laps;
    public int currentFlag;

    IEnumerator Start() {
        yield return new WaitForEndOfFrame();
        ArenaEvents.LapCountChanged(laps);
    }

    private void OnEnable() {
        instance = this;
        flags = GetComponentsInChildren<Flag>();
    }

    private void OnDisable() {
        if (instance == this) instance = null;
    }

    bool OnLastLap() => laps == 1;

    public void OnFlagTouched(Flag touchedFlag) {
        touchedFlag.SetIsNext(false);

        Flag nextFlag =
            flags.FirstOrDefault(f => f.order == touchedFlag.order + 1);

        if (OnLastLap()) Destroy(touchedFlag.gameObject);

        if (nextFlag == null) {
            laps--;
            ArenaEvents.LapCountChanged(laps);
            nextFlag = flags.First(f => f.order == 0);
        }

        if (nextFlag != null) nextFlag.SetIsNext(true);
    }
}
