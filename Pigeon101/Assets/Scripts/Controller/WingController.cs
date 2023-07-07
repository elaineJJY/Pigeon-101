using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingController : MonoBehaviour
{
    // Start is called before the first frame update
    public SkeletonManager skeletonManager;
    private bool previousConfidenceValue;
    public static bool active = false;
    public Transform a;
    public Transform b;
    public Transform c;
    public List<Transform> joints;
    
    private void Start()
    {   
        skeletonManager = FindObjectOfType<SkeletonManager>();
        previousConfidenceValue = skeletonManager.hasConfidence;
    }

    void OnEnable()
    {   

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            for(int j = 0; j < child.childCount; j++)
                joints.Add(child.GetChild(j));
        }
    }

    private void Update()
    {   
        // // calculate the direction plane of points abc
        // Vector3 planeNormal = Vector3.Cross(b.position - a.position, c.position - a.position).normalized;

        // // Rotate each joint to align with the plane abc
        // foreach(Transform joint in joints)
        // {
        //     // Calculate the rotation to align the joint with the plane normal
        //     Quaternion targetRotation = Quaternion.FromToRotation(joint.up, planeNormal) * joint.rotation;

        //     // Apply the rotation to the joint
        //     joint.rotation = targetRotation;
        // }

        if(active){
            if (skeletonManager.hasConfidence != previousConfidenceValue)
            {
                previousConfidenceValue = skeletonManager.hasConfidence;

                if (previousConfidenceValue || Input.GetKeyDown(KeyCode.Space))
                {
                    // Debug.Log("Skeleton has confidence.");
                    // eat food if player touches it
                    
                    SetLayerRecursively(gameObject, 5); // make the object visible
                }
                else
                {
                    // Debug.Log("Skeleton has no confidence.");
                    SetLayerRecursively(gameObject, 6); // make the object invisible
                }
            }

            if(skeletonManager.hasConfidence)
            {
                eatIfCloseEnough(GameObject.FindGameObjectsWithTag("Food"));
            }
        }
        
    }

    void eatIfCloseEnough(GameObject[] foods)
    {   
        Transform player = Camera.main.transform;
        foreach (GameObject food in foods)
        {
            // check if is close enough
            float distance = Vector2.Distance(new Vector2(player.position.x, player.position.z), new Vector2(food.transform.position.x, food.transform.position.z));
            if (distance < 1f)
            {
                // eat the food
                UIController.Instance.ShowHint("Ymmy!");
                Destroy(food);
            }
        }  

    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
