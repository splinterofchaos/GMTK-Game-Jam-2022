using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CarConfig : ScriptableObject {
    public float thrustStrength;
    public float brakeStrength;
    public float turnStrength;
    public float drag;

    [Header("Drifting")]
    public float driftingThrustStrength;
    public float driftingBrakeStrenght;
    public float driftingTurnStrength;
    public float driftTolerance;
    public float driftingDrag;
}
