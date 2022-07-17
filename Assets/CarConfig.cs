using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CarConfig : ScriptableObject {
    public float thrustStrength;
    public float brakeStrength;
    public float turnStrength;
    public float drag;
    public AudioClip jetSound;

    [Header("Drifting")]
    public float driftingThrustStrength;
    public float driftingBrakeStrenght;
    public float driftingTurnStrength;
    public float driftTolerance;
    public float driftingDrag;
    public AudioClip driftingJetSound;

    [Header("Misc")]
    public float engineCooldownOnCollision = 2f;
    public float collisionForceMultiplier = 100;
    public float speedLevelMultiplier = 2f;
    public float speedLevelBase = 6f;
    [Tooltip("Number of seconds until we bump a ship moving too slowly.")]
    public float timeUntilBump = 2;
    public float bumpImpulse = 5;
    public float rollerBumpImpulse = 5;
}
