using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRollerScript : MonoBehaviour {
    [SerializeField]
    Car ship;
    [SerializeField]
    GameObject die;
    Rigidbody dieRB;
    [SerializeField]
    Camera diceCam;
    [SerializeField]
    float forceFactor = 0.3f;

    Quaternion baseCamRotation;

    Vector3 lastShipPos;
    Vector3 lastShipVelocity;

    [SerializeField] Transform bulletFace;
    [SerializeField] Transform boostFace;
    [SerializeField] Transform lazerFace;
    [SerializeField] Transform mineFace;
    [SerializeField] Transform fireFace;
    [SerializeField] Transform oilFace;

    [SerializeField] string DEBUG_topFace;

    [SerializeField] public float speedLevel { get; private set; }


    [SerializeField]
    ProjectileScript[] weapons = new ProjectileScript[6];

    bool WeaponCooldown = false;

    private void Start()
    {
        baseCamRotation = diceCam.transform.rotation;
        dieRB = die.GetComponent<Rigidbody>();
        lastShipPos = ship.gameObject.transform.position;
        lastShipVelocity = new Vector3(0, 0, 0);
    }

    private void Update() {
        Transform[] transforms = new Transform[] {
            bulletFace, boostFace, lazerFace, mineFace, fireFace, oilFace
        };

        float maxDotProduct = 1;
        Transform maxTransform = null;
        foreach (Transform t in transforms) {
            float dot = Vector3.Dot(t.position - die.transform.position,
                                    Vector3.up);
            if (dot > maxDotProduct) {
                maxDotProduct = dot;
                maxTransform = t;
            }
        }

        DEBUG_topFace = maxTransform == bulletFace ? "bullet" :
                        maxTransform == boostFace ? "boost" :
                        maxTransform == lazerFace ? "lazer" :
                        maxTransform == mineFace ? "mine" :
                        maxTransform == fireFace ? "fire" :
                        maxTransform == oilFace ? "oil" :
                        "NONE!?";
        speedLevel = maxTransform == bulletFace ? 6 :
                     maxTransform == boostFace ? 5 :
                     maxTransform == lazerFace ? 4 :
                     maxTransform == mineFace ? 3 :
                     maxTransform == fireFace ? 2 :
                     maxTransform == oilFace ? 1 :
                     0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 shipVelocity = (ship.transform.position - lastShipPos) / Time.fixedDeltaTime;
        Vector3 shipAccel = (shipVelocity - lastShipVelocity) / Time.fixedDeltaTime;
        lastShipPos = ship.transform.position;
        lastShipVelocity = shipVelocity;

        var rot = Quaternion.FromToRotation(Vector3.up, Vector3.forward);

        //diceCam.transform.rotation = baseCamRotation * Quaternion.AngleAxis(ship.angle, Vector3.forward);
        dieRB.AddForceAtPosition((rot * shipAccel) * forceFactor, die.transform.position, ForceMode.Force);
    }

    public void Bump(float impulse) {
        Vector3 r = new Vector3(Random.value, Random.value,
                                Random.value);
        dieRB.AddForce(r.normalized * impulse, ForceMode.Impulse);
    }

    public void ToggleGravity(bool gravity)
    {
        dieRB.useGravity = gravity;
    }

    public ProjectileScript GetWeapon()
    {
        return weapons[0];
    }
}
