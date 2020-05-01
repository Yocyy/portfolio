using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
            obj.GetComponent<HitodeController>().b_ice = true;
        }

        if (obj.gameObject.CompareTag("enemy"))
        {

        }
    }

}
