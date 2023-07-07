using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.XR.ARSubsystems;
public class PlaneDetector : MonoBehaviour
{   
    
    private ARPlaneManager planeManager;
    public float groundHeight = 0;
    public ARPlane groundPlane;
    public ARPlane wallPlane;
    public GameObject defaultGround;
    public List<ARPlane> horizontalPlanes = new List<ARPlane>();

    private void Awake()
    {
        planeManager = FindObjectOfType<ARPlaneManager>(); // Get the AR Plane Manager component
    }

    private void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged; // Register planes changed event
        groundHeight = defaultGround.transform.position.y;
    }

    private void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged; // Unregister planes changed event
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {   
        // if wall(the child) not exists, generate wall
        if (transform.childCount <= 0)
        {   
            foreach (ARPlane plane in args.added)
            {   
                if (plane.classification == PlaneClassification.Floor || plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    horizontalPlanes.Add(plane);
                }
                if (plane.alignment == PlaneAlignment.HorizontalUp && plane.center.y < groundHeight)
                {
                   groundPlane = plane;
                   groundHeight = plane.center.y;
                   defaultGround.transform.position = plane.center;
                }
                if (plane.classification == PlaneClassification.Wall || plane.classification == PlaneClassification.None)
                {   
                    // if the plane is further replace it with the current wall
                    if (wallPlane == null || Vector3.Distance(transform.position, plane.center) > Vector3.Distance(transform.position, wallPlane.center))
                    {
                        wallPlane = plane;
                    }
                }
            }
        }
    }
}
