using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton =  null;

    List<Transform> waypoints = new List<Transform>();

    public Transform GetWaypointByIndex(int index)
    {
        index %= waypoints.Count;
        return waypoints[index];
    }
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.singleton = this;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            waypoints.Add(this.transform.GetChild(i));
        }
    }

}
