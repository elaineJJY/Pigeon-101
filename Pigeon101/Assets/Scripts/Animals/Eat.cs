using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{   
    public string foodTag = "Food";

    public GameObject target;

    private float speed;
    public AudioClip eat;

    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<RandomMovement>().speed;
    }

    // Update is called once per frame
    void Update()
    {   
        float distance = float.MaxValue;
        if(target == null && GameObject.FindWithTag(foodTag) != null)
        {   
            GameObject[] foods = GameObject.FindGameObjectsWithTag(foodTag);
            target = getTargetFood(foods);
            GetComponent<RandomMovement>().targetPosition = target.transform.position;
        }
        if(target != null){
            GetComponent<RandomMovement>().speed = 1.5f;
            GetComponent<RandomMovement>().targetPosition = target.transform.position;
            distance = Vector3.Distance(transform.position, target.transform.position);
        }
        
        // eat the food
        if (target != null && distance < 0.1f)
        {   
            GetComponent<AudioController>().Play(eat);
            Destroy(target);
            target = null;
            GetComponent<RandomMovement>().speed = speed;
            // pigeon becomes bigger
            transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            // trigger eating animation
            GetComponent<RandomMovement>().StopMoving(5f);
            
            if(GetComponent<PigeonInLove>() == null)
                this.gameObject.AddComponent<PigeonInLove>();
        }
    }

    GameObject getTargetFood(GameObject[] foods){
        GameObject closestFood = GameObject.FindWithTag(foodTag);
        float minDistance = float.MaxValue;
        
        foreach(GameObject food in foods){
            float distance = Vector3.Distance(transform.position, food.transform.position);
            if(distance < minDistance){
                minDistance = distance;
                closestFood = food;
            }
        }
        // 50% to change the target
        if(Random.Range(0, 2) == 0) {
            closestFood = foods[Random.Range(0, foods.Length)];
        }
        
        return closestFood;
    }
}
