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
        this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.SmoothDamp(dampDirection, (nextwaypoint.position - this.transform.position).normalized, ref turnVelocity, 0.5f) ;
        dampDirection = direction;

        //var angle = Mathf.Acos(direction.x) * 57.2958f + (direction.y > 0 ? 0 : 180);

        //rb.MoveRotation(angle);
        
        this.transform.position += direction * speed * Time.deltaTime;
        if ((this.transform.position - nextwaypoint.position).magnitude < 2)
        {
            nextwaypoint = GameManager.singleton.GetWaypointByIndex(++waypointindex);
            Debug.Log("Reached waypoint " + waypointindex);
        }
        //this.transform.position = Vector3.Slerp()
    }
}
