using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWind : MonoBehaviour
{
    float coefficient = 100;   // 空気抵抗係数
    Vector3 velocity;   // 風速
    public float Setvelocity_y;
    public float Setvelocity_x;
    float time = 0;

    private void Start()
    {
        velocity = new Vector3(Setvelocity_x, Setvelocity_y, 0);
    }
    void OnTriggerStay(Collider col)
    {
        if (col.GetComponent<Rigidbody>() == null)
        {
            return;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            // 相対速度計算
            var relativeVelocity = velocity - col.GetComponent<Rigidbody>().velocity;

            // 空気抵抗を与える
            col.GetComponent<Rigidbody>().AddForce(coefficient * relativeVelocity);
        }

        if (col.gameObject.CompareTag("enemy"))
        {
            // 相対速度計算
            var relativeVelocity = velocity - col.GetComponent<Rigidbody>().velocity;

            // 空気抵抗を与える
            col.GetComponent<Rigidbody>().AddForce((coefficient / 200) * (relativeVelocity / 200));
        }
    }





    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1.5f)
        {
            // 弾の消滅
            Destroy(gameObject);
            time = 0;
        }
    }
}