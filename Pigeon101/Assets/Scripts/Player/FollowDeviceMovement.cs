using UnityEngine;
using UnityEngine.XR;

public class FollowDeviceMovement : MonoBehaviour
{
    void Update()
    {
        // Get the local position and rotation of the device
        Vector3 devicePosition = InputTracking.GetLocalPosition(XRNode.CenterEye);

        // Update the position of the object to match the device in height
        transform.position = new Vector3(transform.position.x, devicePosition.y, transform.position.z);
        
    }
}
