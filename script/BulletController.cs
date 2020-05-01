using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float time = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
           Destroy(gameObject);
        }
    }

    void Start()
    {
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            //time = 0;
           // flag_bullet_time = false;
        }

        if(time>= 2)
        {
            Destroy(gameObject);
            time = 0;
        }
    }
}
