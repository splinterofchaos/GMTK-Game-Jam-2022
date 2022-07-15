using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    Rigidbody2D body;
    float angle = 0;

    [SerializeField] CarController controller;

    [SerializeField] CarConfig config;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() {
        float forwardForce = controller.Thrust() * config.thrustStrength -
                             controller.Brakes() * config.brakeStrength;
        body.AddForce(transform.up * forwardForce);

        angle += controller.Turn() * Time.fixedDeltaTime * config.turnStrength;
        body.MoveRotation(angle);
    }
}
