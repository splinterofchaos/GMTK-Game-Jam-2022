using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRollerScript : MonoBehaviour {
    [SerializeField]
    Car ship;
    [SerializeField]
    GameObject die;
    Rigidbody dieRB;
    Material dieMat;
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

    [SerializeField] public int faceNumber { get; private set; }


    [SerializeField]
    ProjectileScript[] weapons = new ProjectileScript[6];

    float DieUsed = 0;

    int lastFace = 0;

    private void Start()
    {
        baseCamRotation = diceCam.transform.rotation;
        dieRB = die.GetComponent<Rigidbody>();
        lastShipPos = ship.gameObject.transform.position;
        lastShipVelocity = new Vector3(0, 0, 0);
        dieMat = die.GetComponent<MeshRenderer>().material;
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
        faceNumber = maxTransform == bulletFace ? 6 :
                     maxTransform == boostFace ? 5 :
                     maxTransform == lazerFace ? 4 :
                     maxTransform == mineFace ? 3 :
                     maxTransform == fireFace ? 2 :
                     maxTransform == oilFace ? 1 :
                     0;

        if ((lastFace != faceNumber) && (faceNumber == 6 || faceNumber == 5)    )
        {
            Debug.Log("Face Changed to: " + faceNumber);
            DieUsed = 0;
            Debug.Log(dieMat);
            dieMat.SetFloat("_Used",  DieUsed);
            lastFace = faceNumber;
        }
    }

    public bool TryBoost()
    {
        if (DieUsed == 0)
        {
            DieUsed = 1;
            dieMat.SetFloat("_Used", DieUsed);
            return true;
        }
        return false;
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
        Vector3 r = new Vector3(Random.value, Random.value+0.25f,
                                Random.value);
        dieRB.AddForce(r.normalized * impulse, ForceMode.Impulse);
        dieRB.AddTorque(new Vector3(Random.value, Random.value, Random.value));
    }

    public void ToggleGravity(bool gravity)
    {
        dieRB.useGravity = gravity;
        if (gravity)
            dieMat.SetFloat("_AntiGrav", 0);
        else
            dieMat.SetFloat("_AntiGrav", 1);
        if (!gravity)
        {
            Bump(0.3f);
            dieRB.AddTorque(new Vector3(Random.value, Random.value, Random.value) *0.2f);
        }
    }

    public ProjectileScript GetWeapon()
    {
        return weapons[0];
    }
}
