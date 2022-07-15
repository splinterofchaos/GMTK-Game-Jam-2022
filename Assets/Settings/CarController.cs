using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    public virtual float Turn() => 0;
    public virtual float Thrust() => 0;
    public virtual float Brakes() => 0;
}
