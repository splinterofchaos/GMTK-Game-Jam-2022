using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
    // Start is called before the first frame update
    IEnumerator Start() {
        // Let the flag counter initialize first.
        yield return new WaitForEndOfFrame();
        ArenaEvents.AddFlag();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnCollision();
    }

    public void OnCollision() {
        ArenaEvents.FlagCleared();
        Destroy(gameObject);
    }
}
