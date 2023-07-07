
// using System;
using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{   
    public bool horizontalOnly = true;  
    private Vector3 centerPoint;
    public float radius = 5f;   
    public float speed = 0.5f;   
    private float tempSpeed;
    private PlaneDetector planeDetector;
    public Vector3 targetPosition;

    public AudioClip fly;

    void Start()
    {   
        centerPoint = transform.position;
    }
    
    void OnEnable()
    {
        planeDetector = GameObject.FindObjectOfType<PlaneDetector>();
        targetPosition = GetRandomPosition();
    }

    void Update()
    {   
        
        // if we have reached the target position, get a new one
        if (transform.position == targetPosition)
        {   
            // Debug.Log("Reached target position");
            centerPoint = transform.position;
            targetPosition = GetRandomPosition();
            StopMoving(UnityEngine.Random.Range(5f, 10f));
        }
        else {
            // adapt the direction to the target position
            if (horizontalOnly)
            {
                targetPosition.y = transform.position.y;
            }
            try
            {
                transform.LookAt(targetPosition);
            }
            catch (System.Exception)
            {
                Debug.Log("targetPosition:" + targetPosition);
            }
            
            // move towards the target position at a constant speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (speed == 0)
        {
           
            // the rotation of the animal is horizontal
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
        

    }

    // get a random position within the radius
    private Vector3 GetRandomPosition()
    {
        // select a random direction to move towards
        Vector3 randomDirection = Random.insideUnitSphere * radius;

        // select a random distance within the radius
        Vector3 position = centerPoint + randomDirection;
        position.y = Mathf.Clamp(position.y, -2, 5);
        return position;
    }

    public void StopMoving(float seconds)
    {   
        // Debug.Log("StopMoving:" + seconds + " seconds");
        tempSpeed = speed;
        speed = 0;
        StartCoroutine(ResumeMoving(seconds));
    }

    IEnumerator ResumeMoving(float seconds)
    {   
        yield return new WaitForSeconds(seconds);
        // Debug.Log("ResumeMoving");
        speed = tempSpeed;
        GetComponent<AudioController>().Play(fly);
    }
    
}
