using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    Rigidbody2D body;
    ParticleSystem particleSystem;
    float angularVelocity = 0;

    [SerializeField] CarController controller;

    [SerializeField] CarConfig config;

    [SerializeField] AudioSource jetAudioSource;

    [SerializeField] DieRollerScript roller;

    float engineCooldown = 0;

    bool drifting = false;

    float bumpCountdown;

    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        jetAudioSource.volume = 0;
        drifting = false;
    }

    private void Update() {
    }

    public void FixedUpdate() {
        if (roller != null && roller.speedLevel <= 2) {
            bumpCountdown -= Time.deltaTime;
            if (bumpCountdown <= 0) {
                body.AddForce(new Vector2(Random.value, Random.value).normalized *
                              config.bumpImpulse,
                              ForceMode2D.Impulse);
                bumpCountdown = config.timeUntilBump;
            }
        } else {
            bumpCountdown = config.timeUntilBump;
        }

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
        jetAudioSource.volume =
            (controller.Drifting() ? 1 : controller.Thrust()) * globalVolume;

        float thrust = drifting ? 1 : controller.Thrust();
        float power = 1 - (engineCooldown / config.engineCooldownOnCollision);
        float strength = drifting ? config.driftingThrustStrength
                                  : config.thrustStrength;
        if (roller != null && !drifting) {
            strength = config.speedLevelBase +
                config.speedLevelMultiplier * roller.speedLevel;
            thrust = 1;
        }
        body.AddForce(transform.up * thrust * strength * power);

        float turning = drifting ? config.driftingTurnStrength
                                 : config.turnStrength;

        engineCooldown = Mathf.Max(0, engineCooldown - Time.fixedDeltaTime);

        body.AddTorque(controller.Turn() * Time.fixedDeltaTime * turning);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        engineCooldown = config.engineCooldownOnCollision;
    }
}
