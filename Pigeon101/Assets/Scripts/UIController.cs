using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{   
    public static UIController Instance;
    public GameObject hintPopup;

    // Start is called before the first frame update
    void Start()
    {
        UIController.Instance = this;
    }

    void OnEnable()
    {
       ShowHint("Welcome to PigeonFind! \n Try to find the special pigeon and catch it. If you can't find it for a while, tap the screen to place a food to attract it!");
    }

    public void ShowHint(string text) 
    {   
       // call the ShowHintCoroutine
       StartCoroutine(hintPopup.GetComponent<Popup>().Show(text));
    }

}



