using UnityEngine;
using System.Collections;
using System.Collections.Generic; //allows us to use lists

public class AICarScript : MonoBehaviour
{

    public Vector3 centerOfMass;
    private List<Transform> path;
    public Transform pathGroup;

    public float maxSteer = 60; //15

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    public int currentPathObj;
    public float distFromPath = 20; //20
    public float maxTorque = 50; //50
    public float currentSpeed;
    public float topSpeed = 150; //150  
    public float decelerationSpeed = 10; //10


    private Rigidbody rb;

    void Start()
    {
        path = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
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

    void Update()
    {
        getSteer();
        Move();
    }

    void getSteer()
    {
        Vector3 steerVector = transform.InverseTransformPoint(new Vector3(path[currentPathObj].position.x, transform.position.y, path[currentPathObj].position.z));
        float newSteer = maxSteer * (steerVector.x / steerVector.magnitude);
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;

        if (steerVector.magnitude <= distFromPath)
        {
            currentPathObj++;
            if (currentPathObj >= path.Count)
            {
                currentPathObj = 0;
            }
        }
    }

    void Move()
    {
        currentSpeed = 2 * (22 / 7) * wheelRL.radius * wheelRL.rpm * 60 / 1000; //2
        currentSpeed = Mathf.Round(currentSpeed);
    }

}