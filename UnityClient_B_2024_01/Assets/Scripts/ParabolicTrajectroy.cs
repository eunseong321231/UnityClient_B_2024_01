using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicTrajectroy : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int resolution = 30;
    public float timeStep = 0.1f;

    public Transform launchPoint;
    public float myRotation;
    public float launchPower;
    public float launchAngle;
    public float launchDirection;
    public float gravity = 9.8f;
    public GameObject projectilePrefabs;
    // Start is called before the first frame update

    Vector3 CalculatePositionAtTime(float time)
    {
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;
        float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float z = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float y = launchPower * time * Mathf.Sin(launchAngleRad) + 0.5f * gravity * time * time;

        return launchPoint.position + new Vector3(x, y, z);
    }

    void RenderTrajectory()
    {
        lineRenderer.positionCount = resolution;
        Vector3[] points = new Vector3[resolution];

        for(int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;
            points[i] = CalculatePositionAtTime(t);
        }

        lineRenderer.SetPositions(points);
    }

    public void LaunchProjectile(GameObject _object)
    {
        GameObject temp = Instantiate(_object);
        temp.transform.position = launchPoint.position;
        temp.transform.rotation = launchPoint.rotation;

        Rigidbody rb = temp.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = temp.AddComponent<Rigidbody>();
        }
        if(rb != null)
        {
            rb.isKinematic = false;

            float launchAngleRad = Mathf.Deg2Rad * launchAngle;
            float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

            float initalVelocityX = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
            float initalVelocityZ = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
            float initalVelocityY = launchPower * Mathf.Sin(launchAngleRad);

            Vector3 initialVelocity = new Vector3(initalVelocityX, initalVelocityY, initalVelocityZ);

            rb.velocity = initialVelocity;
        }
    }

    void Update()
    {
        RenderTrajectory();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile(projectilePrefabs); 
        }
    }
}
