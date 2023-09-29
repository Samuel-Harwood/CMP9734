using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP = 0;
    public float dist;
    public Rigidbody rb;
    public bool go = false;
    public float initialDelay;
    // Start is called before the first frame update
    void Start()
    {
        wps = new List<Transform>();
        GetComponent<Rigidbody>();
        GameObject wp;

        wp = GameObject.Find("Waypoint Car (1)");
        wps.Add(wp.transform);

        wp = GameObject.Find("Waypoint Car (2)");
        wps.Add(wp.transform);

        wp = GameObject.Find("Waypoint Car (3)");
        wps.Add(wp.transform);

        wp = GameObject.Find("Waypoint Car (4)");
        wps.Add(wp.transform);

        SetRoute();
        initialDelay = Random.Range(2.0f, 12.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }

        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;
        if (dist < 0.1f)
        {
            targetWP++;
            if (targetWP >= route.Count)
            {
                SetRoute();
                return;
            }
        }
        //calculate velocity for this frame
        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 10f;
        //apply velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        rb.MovePosition(newPosition);

        //align to velocity
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
        10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);

    }

    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 2);
        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1]   };
        else if (routeNumber == 1) route = new List<Transform> { wps[2], wps[3] };
        else if (routeNumber == 2) route = new List<Transform> { wps[1], wps[0] };
        else if (routeNumber == 3) route = new List<Transform> { wps[3], wps[2] };


        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.0f, route[0].position.z);
        targetWP = 1;
    }
}
