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
        body.drag = controller.Drifting() ? config.driftingDrag : config.drag;

        float thrust = controller.Drifting() ? 1 : controller.Thrust();
        forwardForce = transform.up * thrust * (
            controller.Drifting() ? config.driftingThrustStrength
                                  : config.thrustStrength);

        body.AddForce(forwardForce);

        float turning = controller.Drifting() ? config.driftingTurnStrength
                                              : config.turnStrength;
        angle += controller.Turn() * Time.fixedDeltaTime * turning;
        body.MoveRotation(angle);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log($"Entered trigger with {collision.gameObject}");
    }
}
