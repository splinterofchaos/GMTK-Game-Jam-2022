using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRollerScript : MonoBehaviour
{
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

    private void Start()
    {
        baseCamRotation = diceCam.transform.rotation;
        dieRB = die.GetComponent<Rigidbody>();
        lastShipPos = ship.gameObject.transform.position;
        lastShipVelocity = new Vector3(0, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 shipVelocity = (ship.transform.position - lastShipPos) / Time.deltaTime;
        Vector3 shipAccel = (shipVelocity - lastShipVelocity) / Time.deltaTime;
        lastShipPos = ship.transform.position;
        lastShipVelocity = shipVelocity;

        var rot = Quaternion.FromToRotation(Vector3.up, Vector3.forward);

        diceCam.transform.rotation = baseCamRotation * Quaternion.AngleAxis(ship.angle, Vector3.forward);
        dieRB.AddForceAtPosition(-(rot * shipAccel) * forceFactor, die.transform.position, ForceMode.Force);
    }
}
