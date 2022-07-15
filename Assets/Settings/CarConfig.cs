using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CarConfig : ScriptableObject {
    public float thrustStrength;
    public float brakeStrength;
    public float turnStrength;
    public float velocityCorrection;

    [Header("Drifting")]
    public float driftingThrustStrength;
    public float driftingBrakeStrenght;
    public float driftingTurnStrength;
    public float driftingVelocityCorrection;
    public float driftTolerance;
}
