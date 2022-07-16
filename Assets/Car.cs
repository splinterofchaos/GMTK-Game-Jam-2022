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

    float engineCooldown = 0;
    const float ENGINE_COOLDOWN = 1.0f;

    private void Start() {
        body = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    static Vector2 Project(Vector2 a, Vector2 b) => a * Vector2.Dot(a, b);

    public void FixedUpdate() {
        body.drag = controller.Drifting() ? config.driftingDrag : config.drag;

        if (engineCooldown <= 0) {
            float thrust = controller.Drifting() ? 1 : controller.Thrust();
            float strength = controller.Drifting() ? config.driftingThrustStrength
                                                   : config.thrustStrength;
            body.AddForce(transform.up * thrust * strength);

            float turning = controller.Drifting() ? config.driftingTurnStrength
                                                  : config.turnStrength;
            angularVelocity = controller.Turn() * Time.fixedDeltaTime * turning;
        }

        engineCooldown -= Time.fixedDeltaTime;

        angle += angularVelocity;
        body.MoveRotation(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        engineCooldown = ENGINE_COOLDOWN;
    }
}
