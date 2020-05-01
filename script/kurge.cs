using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class kurge : MonoBehaviour
{
    private Rigidbody kurage;
  // public bool stopk;
    // Start is called before the first frame update
    void Start()
    {
        kurage = GetComponent<Rigidbody>();
        kurage.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
   
}
