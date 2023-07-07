using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CliffController : MonoBehaviour
{
    private AutoPlaceOnPlane PigeonGenerator;
    private int count = 0;
    private float timer = 0.0f;
    private float resetInterval = 60.0f; // Reset the count every 60 seconds
    public GameObject nestPrefab;
    private List<GameObject> pigeonBuiltNest;

    private void OnEnable()
    {
        PigeonGenerator = GameObject.Find("Pigeon Generator").GetComponent<AutoPlaceOnPlane>();
        this.pigeonBuiltNest = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // every 1 min, refresh the count
        timer += Time.deltaTime;
        if (timer >= resetInterval)
        {
            count = 0;
            timer = 0;
            pigeonBuiltNest.Clear();
        }
    }

    // making the nest
    private void OnCollisionEnter(Collision collision)
    {
        
        GameObject pigeon = collision.gameObject;
        if(pigeon.tag == "Pigeon") {
            RandomMovement randomMovement = pigeon.GetComponent<RandomMovement>();
            randomMovement.targetPosition = pigeon.transform.position;
        }
        if (pigeon.tag == "Pigeon" && count < 2)
        {   
            PigeonInLove pigeonInLove = pigeon.GetComponent<PigeonInLove>();
            if(pigeonBuiltNest.Contains(pigeon) || pigeonInLove!=null){
                return;
            }
            StartCoroutine(WaitAndGeneratePigeon(5f, pigeon));
        }
    }

    IEnumerator WaitAndGeneratePigeon(float waitTime, GameObject pigeon)
    {   
        
        count++;
        pigeonBuiltNest.Add(pigeon);
        RandomMovement randomMovement = pigeon.GetComponent<RandomMovement>();
        randomMovement.StopMoving(waitTime);
        Vector3 pigeonPosition = pigeon.transform.position;
        GameObject nest = Instantiate(nestPrefab, pigeonPosition, Quaternion.identity);
        nest.transform.position = pigeonPosition;
        nest.transform.parent = this.transform;
        yield return new WaitForSeconds(waitTime);
        GameObject pigeonGenerated = PigeonGenerator.PlaceObjectOnPlane(pigeonPosition + new Vector3(0f,0.1f,0f));
        
        pigeonGenerated.AddComponent<DestoryTimer>();
        pigeonGenerated.GetComponent<DestoryTimer>().timer = 40f;
    }
}
