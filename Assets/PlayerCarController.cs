using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : CarController {
    [SerializeField] float thrust;
    [SerializeField] float turn;
    [SerializeField] float brakes;
    [SerializeField] bool drift;
    [SerializeField] bool firing;
    bool finished;

    private void OnEnable() {
        ArenaEvents.onVictory += OnVictory;
    }

    private void OnDisable() {
        ArenaEvents.onVictory -= OnVictory;
    }

    private void Update() {
        thrust = Input.GetAxis("Thrust");
        turn = Input.GetAxis("Turn");
        brakes = Input.GetAxis("Brakes");
        drift = Input.GetButton("Drift");
        firing = Input.GetKey(KeyCode.Q);

        if (finished && drift) {
            ArenaEvents.LoadNextLevel();
        }

        if (Input.GetButton("Cancel") && LevelManager.instance != null) {
            LevelManager.instance.LoadTitle();
        }
    }

    void OnVictory() => finished = true;

    public override float Thrust() => finished ? 0 : thrust;
    public override float Turn() => finished ? 0 : turn;
    public override float Brakes() => finished ? 0 : brakes;
    public override bool Drifting() => !finished && drift;
    public override bool Firing() => finished ? false : firing;
}
