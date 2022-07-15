using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : CarController {
    [SerializeField] float thrust;
    [SerializeField] float turn;
    [SerializeField] float brakes;

    private void Update() {
        thrust = Input.GetAxis("Thrust");
        turn = Input.GetAxis("Turn");
        brakes = Input.GetAxis("Brakes");
    }

    public override float Thrust() => thrust;
    public override float Turn() => turn;
    public override float Brakes() => brakes;
}
