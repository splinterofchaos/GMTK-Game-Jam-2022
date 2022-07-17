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
    [SerializeField] AudioSource collisionAudioSource;
    [SerializeField] AudioSource flagAudioSource;

    [SerializeField] DieRollerScript roller;

    float drag;

    float engineCooldown = 0;

    bool drifting = false;

    float bumpCountdown;

    [SerializeField]
    float boostpower = 2.5f;
    [SerializeField]
    float boostduration = 2f;
    float currentduration = 0f;
    bool started = false;

    TrailRenderer[] trails;

    [SerializeField]
    Transform WeaponLeft;
    [SerializeField]
    Transform WeaponRight;
    [SerializeField]
    Transform WeaponBack;


    private void OnEnable() {
        body = GetComponent<Rigidbody2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        jetAudioSource.volume = 0;
        drifting = false;
        drag = body.drag;
        trails = this.GetComponentsInChildren<TrailRenderer>();
        Debug.Log("Trailrenderers found: " + trails.Length);

        ArenaEvents.onRaceStarted += StartEngines;
    }

    private void OnDisable() {
        ArenaEvents.onRaceStarted -= StartEngines;
    }

    private void Update() {
    }

    void Bump(float multiplier = 1) {
        roller.Bump(config.bumpImpulse * multiplier);
        bumpCountdown = config.timeUntilBump;
    }

    public void FixedUpdate() {
        if (!started) {
            return;
        }

        if (roller != null && roller.faceNumber <= 2) {
            bumpCountdown -= Time.deltaTime;
            if (bumpCountdown <= 0) {
                body.AddForce(new Vector2(Random.value, Random.value).normalized *
                              config.bumpImpulse,
                              ForceMode2D.Impulse);
                Bump(10);
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

            // roller.Bump(config.rollerBumpImpulse);

        }

        if (drifting) {
            body.drag = 0;
            foreach (TrailRenderer trail in trails) {
                trail.emitting = false;
            }
            roller.ToggleGravity(false);
        } else {
            body.drag = drag;
            foreach (TrailRenderer trail in trails) {
                trail.emitting = true;
            }
            roller.ToggleGravity(true);
        }

        body.drag = drifting ? config.driftingDrag : config.drag;

        jetAudioSource.volume = SoundEffectVolume();

        float thrust = drifting ? 0 : controller.Thrust();
        float power = 1 - (engineCooldown / config.engineCooldownOnCollision);
        float strength = drifting ? config.driftingThrustStrength
                                  : config.thrustStrength;

        if (roller != null && !drifting) {
            strength = config.speedLevelBase +
                config.speedLevelMultiplier * roller.faceNumber;
            thrust = 1;
        }

        body.AddForce(transform.up * thrust * strength * power * (currentduration > 0 ? boostpower : 1));

        float turning = drifting ? config.driftingTurnStrength
                                 : config.turnStrength;

        engineCooldown = Mathf.Max(0, engineCooldown - Time.fixedDeltaTime);

        body.AddTorque(controller.Turn() * Time.fixedDeltaTime * turning);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        engineCooldown = config.engineCooldownOnCollision;
        Bump();

        collisionAudioSource.volume = SoundEffectVolume();
        collisionAudioSource.Play();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        Bump(2);

        flagAudioSource.volume = SoundEffectVolume();
        flagAudioSource.Play();
    }

    void StartEngines() => started = true;

    float SoundEffectVolume() {
        GameSettings settings = GameSettings.instance;
        return settings == null ? 1 : settings.soundFxVolume;
    }
}
