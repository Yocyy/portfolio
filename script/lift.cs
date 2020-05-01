using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lift : MonoBehaviour
{
    private Rigidbody rb;
    public float LiftPower;
    private float power_con;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = LiftPower;
        rb.useGravity = false;
        power_con = 0;
    }

    // Update is called once per frame
    void Update()
    {
        power_con += Time.deltaTime;

        if(power_con > 0.01f)
        {
            Vector3 force = new Vector3(0.0f, LiftPower * 1.5f, 0.0f);    // 力を設定
            rb.AddForce(force);  // 力を加える
            power_con = 0;
        }
    }
}
