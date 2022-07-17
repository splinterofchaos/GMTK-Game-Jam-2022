using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointScript : MonoBehaviour
{
    int waypointindex = 0;
    Transform nextwaypoint;
    Quaternion baserotation;
    Vector3 dampDirection;
    Vector3 turnVelocity;
    float speed = 3;
    float angle;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        nextwaypoint = GameManager.singleton.GetWaypointByIndex(waypointindex);
        baserotation = this.transform.rotation;
        dampDirection = Vector3.up;
        angle = transform.rotation.eulerAngles.z;
        rb = this.GetComponent<Rigidbody2D>();
    }

    Vector3 DeltaToWaypoint() {
        Vector3 delta = nextwaypoint.position - this.transform.position;
        delta.z = 0;
        return delta;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 delta = DeltaToWaypoint();
        Vector3 direction = Vector3.SmoothDamp(dampDirection, delta.normalized, ref turnVelocity, 0.5f) ;
        dampDirection = direction;

        angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x) - 90;
        rb.MoveRotation(angle);

        rb.MovePosition(this.transform.position + direction * speed * Time.fixedDeltaTime);
        if (delta.magnitude < 2)
        {
            nextwaypoint = GameManager.singleton.GetWaypointByIndex(++waypointindex);
            Debug.Log("Reached waypoint " + waypointindex);
        }
        //this.transform.position = Vector3.Slerp()
    }
}
