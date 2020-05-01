using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider : MonoBehaviour
{
    public bool flag_Jump = false;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "HitodeHand")
        {
            flag_Jump = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "HitodeHand")
        {
            flag_Jump = false;
        }
    }
}
