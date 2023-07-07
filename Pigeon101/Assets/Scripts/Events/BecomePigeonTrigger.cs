using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class BecomePigeonTrigger : MonoBehaviour
{
    private GameObject SkeletonParent;
    private AutoPlaceOnPlane PigeonGenerator;
    public GameObject shit;

    // add audio
    public AudioClip becomePigeon;

    public GameObject[] NextStorys;

    private void OnEnable()
    {
        if(ManomotionManager.Instance != null) {
            ManomotionManager.Instance.ShouldCalculateGestures(true);
            ManomotionManager.Instance.ShouldCalculateSkeleton3D(true);
        }
        PigeonGenerator = GameObject.Find("Pigeon Generator").GetComponent<AutoPlaceOnPlane>();
        
    }
    void Update()
    {
        if (SkeletonParent == null)
        {
            SkeletonParent = GameObject.Find("SkeletonParent");
            SetLayerRecursively(SkeletonParent, 6);
            
        }
        Transform player = Camera.main.transform;
        bool playerIsFaceToPigeon = Vector3.Dot(player.forward, transform.position - player.position) > 0;
    
        GestureInfo gestureInfo = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        ManoGestureTrigger mano_gesture_trigger = gestureInfo.mano_gesture_trigger;
        bool takenRightAction = mano_gesture_trigger == ManoGestureTrigger.RELEASE_GESTURE || mano_gesture_trigger == ManoGestureTrigger.GRAB_GESTURE;

        // trigger the next story
        if (playerIsFaceToPigeon && takenRightAction || Input.GetKeyDown(KeyCode.Space))
        {
            SetLayerRecursively(SkeletonParent, 5); // make wing visible

            // generate 2 pigeons
            PigeonGenerator.PlaceObjectOnPlane();

            WingController.active = true;
            for(int i = 0; i < NextStorys.Length; i++) {
                NextStorys[i].SetActive(true);
            }
            UIController.Instance.ShowHint("Congratulations, you've now become a pigeon! Get something to eat using your wings!");
            // change the placement prefab to Shit
            // GameObject.Find("AR Session").GetComponent<ARPlacementInteractable>().placementPrefab = shit;
            GetComponent<AudioController>().Play(becomePigeon);
            Destroy(gameObject, 1f);
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
