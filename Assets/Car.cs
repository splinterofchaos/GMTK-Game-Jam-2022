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

    float drag;

    float engineCooldown = 0;

    bool drifting = false;

    float bumpCountdown;

    TrailRenderer[] trails;

    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        jetAudioSource.volume = 0;
        drifting = false;
        drag = body.drag;
        trails = this.GetComponentsInChildren<TrailRenderer>();
        Debug.Log("Trailrenderers found: " +trails.Length);
    }

    private void Update() {
    }

    public void FixedUpdate() {
        /*if (roller != null && roller.speedLevel <= 2) {
            bumpCountdown -= Time.deltaTime;
            if (bumpCountdown <= 0) {
                body.AddForce(new Vector2(Random.value, Random.value).normalized *
                              config.bumpImpulse,
                              ForceMode2D.Impulse);
                roller.Bump(config.rollerBumpImpulse);
                bumpCountdown = config.timeUntilBump;
            }
        } else {
            bumpCountdown = config.timeUntilBump;
        }*/

        if (controller.Drifting() != drifting)
        {
            jetAudioSource.clip = controller.Drifting() ?
                                  config.driftingJetSound :
                                  config.jetSound;
            jetAudioSource.Play();
            drifting = controller.Drifting();

            // roller.Bump(config.rollerBumpImpulse);
            
        }

        if(drifting)
        {
            body.drag = 0;
            foreach (TrailRenderer trail in trails)
            {
                trail.emitting = false;
            }
        }
        else
        {
            body.drag = drag;
            foreach (TrailRenderer trail in trails)
            {
                trail.emitting = true;
            }
        }

        body.drag = drifting ? config.driftingDrag : config.drag;

        GameSettings settings = GameSettings.instance;
        float globalVolume = settings == null ? 1 :
            settings.globalVolume * settings.soundFxVolume;
        jetAudioSource.volume =
            (controller.Drifting() ? 0 : controller.Thrust()) * globalVolume;

        float thrust = drifting ? 0 : controller.Thrust();
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
        roller.Bump(config.rollerBumpImpulse);
    }
}
