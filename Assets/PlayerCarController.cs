using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : CarController {
    [SerializeField] float thrust;
    [SerializeField] float turn;
    [SerializeField] float brakes;
    [SerializeField] bool drift;

    private void Update() {
        thrust = Input.GetAxis("Thrust");
        turn = Input.GetAxis("Turn");
        brakes = Input.GetAxis("Brakes");
        drift = Input.GetButton("Drift");
    }

    public override float Thrust() => thrust;
    public override float Turn() => turn;
    public override float Brakes() => brakes;
    public override bool Drifting() => drift;
}
