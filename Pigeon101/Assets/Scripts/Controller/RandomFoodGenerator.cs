using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFoodGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private AutoPlaceOnPlane FoodGenerator;

    public float minInterval = 60f; 
    public float maxInterval = 30f; 

    private float timer = 0f;
    private float interval = 0f;

    void Start()
    {
        FoodGenerator = gameObject.GetComponent<AutoPlaceOnPlane>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if the time interval has been reached
        if (timer >= interval)
        {   
            GenerateFood();
            GenerateFood();
            GenerateFood();
            SetRandomInterval(); // Set the next random time interval
            timer = 0f; // Reset the timer
        }
    }

    void SetRandomInterval()
    {
        interval = Random.Range(minInterval, maxInterval);
    }

    void GenerateFood()
    {

        // Instantiate the food prefab
        GameObject food = FoodGenerator.PlaceObjectOnPlane();
       
        // trigger 
    }
}
