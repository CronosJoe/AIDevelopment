using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSystem : MonoBehaviour
{
    public int boidCount = 100;
    public float radius = 10f;
    public float neighborhoodRadius = 5f;

    public float mass = 1.0f;
    public float maxForce = 5.0f;
    public float maxSpeed = 10.0f;

    public GameObject boidPrefab;
    public Transform boidTarget;
    private Transform[] boidTransform;
    private Vector3[] boidPosition;
    private Vector3[] boidVelocity;


    // Start is called before the first frame update
    private void Start()
    {
        boidTransform = new Transform[boidCount]; //array with size of the amount of boids
        boidPosition = new Vector3[boidCount];
        boidVelocity = new Vector3[boidCount];

        for(int i = 0; i<boidCount; i++)
        {
            GameObject newBoid = Instantiate(boidPrefab, transform.position + Random.insideUnitSphere * radius, transform.rotation);
            boidTransform[i] = newBoid.transform;
            boidPosition[i] = boidTransform[i].position;
            boidVelocity[i] = boidTransform[i].forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
