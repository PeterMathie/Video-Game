using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  //Script requires Ridid Body on gameObject to run

public class RandomFlight : MonoBehaviour
{
    [SerializeField] float idleSpeed, turnSpeed;
    [SerializeField] Vector2 changeTargetEveryFromTo;
    [SerializeField] Transform homeTarget, flyingTarget;
    [SerializeField] Vector2 radiusMinMax;
    [SerializeField] Vector2 yMinMax;
    
    private Rigidbody body;
    public float changeTarget = 5f, timeSinceTarget = 0f, speed = 10f, zturn, prevz, turnSpeedBackup;
    private Vector3 rotateTarget, position, direction;
    private Quaternion lookRotation;
    public float distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        // init rigid body
        body = GetComponent<Rigidbody>();
        // backing up the input value of turn speed, to be used again after it 
        turnSpeedBackup = turnSpeed;
        // current direction bird is facing -> the direction we want the bird to move
        direction = Quaternion.Euler(transform.eulerAngles) * (Vector3.forward);
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // calc distances
        distanceFromTarget = Vector3.Magnitude(flyingTarget.position - body.position);

        // Change the target
        if(changeTarget < 0f)
        {
            rotateTarget = ChangeDirection(body.transform.position);
            changeTarget = Random.Range(changeTargetEveryFromTo.x, changeTargetEveryFromTo.y);
            timeSinceTarget = 0f;
        }

        //Turn when you reach the height limits
        if(body.transform.position.y < yMinMax.x + 10f || body.transform.position.y > yMinMax.y - 10f)
        {
            if(body.transform.position.y < yMinMax.x + 10f)
            {
                rotateTarget.y = 1f;
            }
            else
            {
                rotateTarget.y = -1f;
            }
        }

        // calm turn to wihtin 45 degrees
        zturn = Mathf.Clamp(Vector3.SignedAngle(rotateTarget, direction, Vector3.up), -45f, 45f);

        // update times
        changeTarget -= Time.fixedDeltaTime;
        timeSinceTarget += Time.fixedDeltaTime;

        // rotate towards target
        if(rotateTarget != Vector3.zero) 
        {
            lookRotation = Quaternion.LookRotation(rotateTarget, Vector3.up);
        }
        Vector3 rotation = Quaternion.RotateTowards(body.transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime).eulerAngles;
        body.transform.eulerAngles = rotation;

        // rotate on z axis to tilt body towards turn direction
        float temp = prevz;
        if(prevz < zturn)
        {
            prevz += Mathf.Min(turnSpeed * Time.fixedDeltaTime, zturn - prevz);
        }
        else
        {
            prevz -= Mathf.Min(turnSpeed * Time.fixedDeltaTime, prevz - zturn);
        }

        prevz = Mathf.Clamp(prevz, -45f, 45f);
        body.transform.Rotate(0f, 0f, prevz - temp, Space.Self);

        // Move bird
        direction = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;

        body.velocity = speed * direction;
        //hard limit the height 
        if(body.transform.position.y < yMinMax.x || body.transform.position.y > yMinMax.y)
        {
            position = body.transform.position;
            position.y = Mathf.Clamp(position.y, yMinMax.x, yMinMax.y);
            body.transform.position = position;
        }

    }

     // Select a new direction to fly in randomly
    private Vector3 ChangeDirection(Vector3 currentPosition)
    {
        Vector3 newDir;
    
        //if the bird flies too far away it will tend towards the island again
        if (distanceFromTarget > radiusMinMax.y)
        {
            newDir = flyingTarget.position - currentPosition;
        }
        else if (distanceFromTarget < radiusMinMax.x)
        {
            newDir = currentPosition - flyingTarget.position;
        } 
        else
        {
            // 360-degree freedom of choice on the horizontal plane
            float angleXZ = Random.Range(-Mathf.PI, Mathf.PI);
            // Limited max steepness of ascent/descent in the vertical direction
            float angleY = Random.Range(-Mathf.PI / 48f, Mathf.PI / 48f);
            // Calculate direction
            newDir = Mathf.Sin(angleXZ) * Vector3.forward + Mathf.Cos(angleXZ) * Vector3.right + Mathf.Sin(angleY) * Vector3.up;
        }
        return newDir.normalized;
    }
    
    
}
