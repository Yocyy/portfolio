using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanketusenBulletController : MonoBehaviour
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
        if (time >= 1.0f)
        {
            Destroy(gameObject);
            //gameObject.GetComponent<Rigidbody>().useGravity = true;
            time = 0;
            
        }
    }
}
