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
    public float engineCooldownOnCollision = 0.5f;
    public float collisionForceMultiplier = 100;
}
