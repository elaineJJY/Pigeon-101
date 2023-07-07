using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 30f;
    void Start()
    {
        
    }
    void OnEnable()
    {
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
