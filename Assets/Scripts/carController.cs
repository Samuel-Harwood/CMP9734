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
    public Vector3 velocity;
    public float speed = 10.0f;
    // Start is called before the first frame update


    void Start()
    {
        wps = new List<Transform>();
        GetComponent<Rigidbody>();
        GameObject wp;
        wp = GameObject.Find("Waypoint Car (0)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (1)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (2)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (3)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (4)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (5)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (6)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (7)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (8)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (9)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (10)");
        wps.Add(wp.transform);
        wp = GameObject.Find("Waypoint Car (11)");
        wps.Add(wp.transform);

        SetRoute();
        initialDelay = Random.Range(0.0f, 1.0f);
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
        velocity = displacement;
        velocity.Normalize();
        velocity *= speed;
        //apply velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        rb.MovePosition(newPosition);

        //align to velocity
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
        8.0f * Time.deltaTime, 0.0f); //10.0f
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);

    }

    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(2, 3);

        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> { wps[4], wps[6], wps[7], wps[2] };
        if (routeNumber == 1) route = new List<Transform> { wps[0], wps[8], wps[9], wps[5] };
        if (routeNumber == 2) route = new List<Transform> { wps[4], wps[6], wps[11], wps[10], wps[1] };
        if (routeNumber == 3) route = new List<Transform> { wps[0], wps[1] };


        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.55f, route[0].position.z);
        targetWP = 1;

    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Red")
        {
            Debug.Log("RedLight");
            speed = 0.0f;
        }
        if (other.tag == "Green")
        {
            Debug.Log("GreenLight");
            speed = 10.0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited Trigger");
    }


}
