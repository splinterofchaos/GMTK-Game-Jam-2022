using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    Rigidbody2D body;
    ParticleSystem particleSystem;
    float angle = 0;
    float angularVelocity = 0;

    [SerializeField] CarController controller;

    [SerializeField] CarConfig config;

    // Updated each frame except while drifting so that the car can maintain
    // its drift velocity.
    Vector2 forwardForce;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    static Vector2 Project(Vector2 a, Vector2 b) => a * Vector2.Dot(a, b);

    public void FixedUpdate() {
        Vector2 driftingThrust = Vector2.zero;
        if (!controller.Drifting()) {
            forwardForce = transform.up * controller.Thrust() * config.thrustStrength;
        } else {
            driftingThrust = transform.up * config.driftingThrustStrength;
        }

        body.AddForce(forwardForce + driftingThrust);

        // Modify the sideways velocity to make the ship move more forward.
        Vector2 forwardVelocity = Project(transform.up, body.velocity);
        Vector2 sidewaysVelocity = Project(transform.right, body.velocity);
        body.velocity = forwardVelocity + sidewaysVelocity * (
            controller.Drifting() ? config.driftingVelocityCorrection
                                  : config.velocityCorrection);

        float turning = controller.Drifting() ? config.driftingTurnStrength
                                              : config.turnStrength;
        angle += controller.Turn() * Time.fixedDeltaTime * turning;
        body.MoveRotation(angle);
    }

    void StartDrift() {

    }
}
