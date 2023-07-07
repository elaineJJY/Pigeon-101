using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.XR.ARSubsystems;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class AutoPlaceOnPlane : MonoBehaviour
{   
    
    public GameObject[] prefabsToPlace; // Object to place on the ground
    public Vector3 offset = new Vector3(0, -3, -3); 
    public PlaneAlignment planeType;
    public bool onAllHorizontalPlane = false;
    
    private PlaneDetector planeDetector;

    private GameObject player;
 

    private void OnEnable()
    {
       planeDetector = FindObjectOfType<PlaneDetector>();
       player = Camera.main.gameObject;
    }


    public GameObject PlaceObjectOnPlane()
    {   
        if (planeType == PlaneAlignment.HorizontalUp)
        {   
            bool placeOnGround = planeDetector.horizontalPlanes.Count == 0 || !onAllHorizontalPlane;
            ARPlane plane = placeOnGround ? planeDetector.groundPlane : planeDetector.horizontalPlanes[Random.Range(0, planeDetector.horizontalPlanes.Count)];
            return PlaceObjectOnHorizontalPlane(plane);
        }
        else if (planeType == PlaneAlignment.Vertical)
        {
            return PlaceObjectOnVerticalPlane();
        }
        return null;
    }

    public GameObject PlaceObjectOnPlane(Vector3 position)
    {
        GameObject prefabToPlace = prefabsToPlace[Random.Range(0, prefabsToPlace.Length)];
        GameObject obj = Instantiate(prefabToPlace, position, Quaternion.identity);
        return obj;
    }

    // private GameObject PlaceObjectOnRandomPlane()
    // {   
    //     if (planeType == PlaneAlignment.HorizontalUp)
    //     {   
    //         ARPlane plane = onlyOnGround ? planeDetector.groundPlane : planeDetector.horizontalPlanes[Random.Range(0, planeDetector.horizontalPlanes.Count)];
    //         return PlaceObjectOnHorizontalPlane(plane);
    //     }
    //     else if (planeType == PlaneAlignment.Vertical)
    //     {
    //         return PlaceObjectOnVerticalPlane();
    //     }
    //     return null;
    // }

    private GameObject PlaceObjectOnVerticalPlane(){
        // ARPlane plane = planeDetector.wallPlane;
        // Vector3 position = plane.center;
        // // position.y = planeDetector.groundPlane.center.y;
        // position += offset;
        
        // GameObject prefabToPlace = prefabsToPlace[Random.Range(0, prefabsToPlace.Length)];
        // GameObject obj = Instantiate(prefabToPlace, position, Quaternion.identity);   
        // obj.transform.rotation = plane.transform.rotation;

        GameObject obj = PlaceObjectOnHorizontalPlane(planeDetector.wallPlane);
        // make the object face to the player, and keep the y axis
        obj.transform.LookAt(player.transform);
        obj.transform.rotation = Quaternion.Euler(0, obj.transform.rotation.eulerAngles.y, 0);
        return obj;
    }

    private GameObject PlaceObjectOnHorizontalPlane(ARPlane plane){

         // User's position
        Vector3 playerPosition = player.transform.position;

        // User's forward direction
        Vector3 playerForward = player.transform.forward;

        // Calculate the forward position 5 units away from the user
        Vector3 forwardPosition = playerPosition + playerForward * 5f;

        Vector3 position = plane.center;
        position.x = forwardPosition.x + Random.Range(-1f, 1f) * 3f;
        position.z = forwardPosition.z + Random.Range(0f, 1f) * 3f;
        position.y = plane.center.y;
        position += offset;

        GameObject prefabToPlace = prefabsToPlace[Random.Range(0, prefabsToPlace.Length)];
        GameObject obj = Instantiate(prefabToPlace, position, Quaternion.identity);    
        return obj;
    }

    // private GameObject PlaceObjectOnGround(){
       
    //     ARPlane plane = planeDetector.groundPlane;
    //     Vector3 onTheGround = plane.center;
    //     onTheGround.x = forwardPosition.x + Random.Range(-1f, 1f) * 3f;
    //     onTheGround.z = forwardPosition.z + Random.Range(0f, 1f) * 3f;
    //     onTheGround += offset;

    //     GameObject prefabToPlace = prefabsToPlace[Random.Range(0, prefabsToPlace.Length)];
    //     GameObject obj = Instantiate(prefabToPlace, onTheGround, Quaternion.identity);
    //     return obj;
    // }


}
