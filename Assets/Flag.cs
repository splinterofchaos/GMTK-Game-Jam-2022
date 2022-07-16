using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
    public int order = 0;
    bool isNext = false;

    SpriteRenderer spriteRenderer;

    IEnumerator Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetIsNext(order == 0);

        // Let the flag counter initialize first.
        yield return new WaitForEndOfFrame();
        ArenaEvents.AddFlag();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnCollision();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        UnityEditor.Handles.Label(transform.position + transform.up, new GUIContent(order.ToString()));
    }
#endif

    public void SetIsNext(bool isNext) {
        this.isNext = isNext;
        spriteRenderer.color = isNext ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1);
    }

    public void OnCollision() {
        if (!isNext) return;
        FlagManager.instance.OnFlagTouched(this);
        ArenaEvents.FlagCleared();
    }
}
