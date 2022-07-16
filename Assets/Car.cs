using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    Rigidbody2D body;
    ParticleSystem particleSystem;
    public float angle = 0;
    float angularVelocity = 0;

    [SerializeField] CarController controller;

    [SerializeField] CarConfig config;

    [SerializeField] AudioSource jetAudioSource;

    float engineCooldown = 0;

    bool drifting = false;

    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        jetAudioSource.volume = 0;
        drifting = false;
        angle = transform.rotation.eulerAngles.z;
    }

    static Vector2 Project(Vector2 a, Vector2 b) => a * Vector2.Dot(a, b);

    public void FixedUpdate() {
        if (controller.Drifting() != drifting) {
            jetAudioSource.clip = controller.Drifting() ?
                                  config.driftingJetSound :
                                  config.jetSound;
            jetAudioSource.Play();
            drifting = controller.Drifting();
        }

        body.drag = drifting ? config.driftingDrag : config.drag;

        GameSettings settings = GameSettings.instance;
        float globalVolume = settings == null ? 1 :
            settings.globalVolume * settings.soundFxVolume;
        jetAudioSource.volume = controller.Thrust() * globalVolume;

        float thrust = drifting ? 1 : controller.Thrust();
        float power = 1 - (engineCooldown / config.engineCooldownOnCollision);
        float strength = drifting ? config.driftingThrustStrength
                                    : config.thrustStrength;
        body.AddForce(transform.up * thrust * strength * power);

        float turning = drifting ? config.driftingTurnStrength
                                 : config.turnStrength;
        angularVelocity = controller.Turn() * Time.fixedDeltaTime * turning;

        engineCooldown = Mathf.Max(0, engineCooldown - Time.fixedDeltaTime);

        angle += angularVelocity;
        body.MoveRotation(angle);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        engineCooldown = config.engineCooldownOnCollision;
    }
}
