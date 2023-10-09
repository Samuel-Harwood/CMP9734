using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCarController : MonoBehaviour
{
    private List<Transform> path;
    public Transform pathGroup;
    public int currentPathObj;
    public Vector3 velocity;
    public Rigidbody rb;
    public float speed = 8.0f;
    public float maxSteer = 60; //15
    public float maxSpeed = 10.0f;
    public float distFromPath = 2f; //20
    public float minSpeed = 0f;
    public float acceleration = 0.4f;


    void Start()
    {
      
        path = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        getPath();

    }

    void getPath()
    {
        Transform[] childObjects = pathGroup.GetComponentsInChildren<Transform>();

        for (int i = 0; i < childObjects.Length; i++)
        {
            Transform temp = childObjects[i];
            if (temp.gameObject.GetInstanceID() != GetInstanceID())
                path.Add(temp);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 displacement = path[currentPathObj].position - transform.position;
        displacement.y = 0;
        float distFromPath = displacement.magnitude;
        if (distFromPath < 1.2f)
        {
            currentPathObj++;
            if (currentPathObj >= path.Count)
            {
                currentPathObj = 0;
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

        // Move towards the current waypoint
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
        speed * Time.deltaTime, 0.0f); //10.0f


        Quaternion rotation = Quaternion.LookRotation(desiredForward);

        rb.MoveRotation(rotation);
        
    }


   
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Red")
        {
            if(speed > 0.0f)
            {
                speed -= acceleration;
            }
        }
     
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Red")
        {
            
            Debug.Log("Redlight");
           
            speed -= acceleration;
            
            //speed = 0.0f;
        }
        //if (other.tag == "Green")
        //{
        //    Debug.Log("GreenLight");
        //    speed = 10.0f;
        //}
        //if (other.tag == "Car")
        //{
        //    Debug.Log("Car");
        //    speed = 0.0f;
        //}
    }

    //void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("Exited Trigger");
    //}


}
