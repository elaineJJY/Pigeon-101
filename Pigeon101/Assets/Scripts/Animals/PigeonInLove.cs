using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PigeonInLove : MonoBehaviour
{   
    public AudioClip inLove;
    GameObject player;
    public bool isLoved = false;
    void Start()
    {
        player = Camera.main.gameObject;
        inLove = Resources.Load<AudioClip>("Audio/InLove");
    }

    private void OnEnable()
    {
        Invoke("DeleteScript", 60f);
        // show heart
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GetComponent<AudioController>().Play(inLove);
        UIController.Instance.ShowHint("The other pigeon seem to be very interested in you! Approach it and start a  courtship!");
    }
    private void OnDisable()
    {
        // hide heart
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void DeleteScript()
    {
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        // if is close to the player, finishing the courtship and start to build a nest(horizontal distance < 0.5f)
        if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.transform.position.x, player.transform.position.z)) < 0.5f){
           StartCoroutine(FinishingLove());
        }
    }
    IEnumerator FinishingLove()
    {   
        // trigger finish courtship animation
        // GetComponent<RandomMovement>().StopMoving(1f);
        UIController.Instance.ShowHint("Pairing successful. Let's make a home next! Natural nest can be built on a cliff.");
        // yield return new WaitForSeconds(1f);
        // pigeon becomes smaller
        transform.localScale = new Vector3(1f, 1f, 1f);
        isLoved = true;
        GameObject cliff = GameObject.Find("Cliff Generator").GetComponent<AutoPlaceOnPlane>().PlaceObjectOnPlane();
        GetComponent<RandomMovement>().targetPosition = cliff.transform.position;
        DeleteScript();
        yield return null;
        
    }
}
