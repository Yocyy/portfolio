using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float coefficient;   // 空気抵抗係数
    public Vector3 velocity;    // 風速
    public HaritukuController haritukuController; //値参照用
    public GameObject particle;

    void Start()
    {
        
    }

    void Update()
    {
        if(haritukuController.Get_Flag())
        {
            particle.SetActive(false);
        }

        if (!haritukuController.Get_Flag())
        {
            particle.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.GetComponent<Rigidbody>() == null)
        {
            return;
        }

        if (!haritukuController.Get_Flag())
        {
            if(col.gameObject.CompareTag("Player"))
            {
                // 相対速度計算
                var relativeVelocity = velocity - col.GetComponent<Rigidbody>().velocity;

                // 空気抵抗を与える
                col.GetComponent<Rigidbody>().AddForce(coefficient * relativeVelocity);
            }
            
            if(col.gameObject.CompareTag("enemy"))
            {
                // 相対速度計算
                var relativeVelocity = velocity - col.GetComponent<Rigidbody>().velocity;

                // 空気抵抗を与える
                col.GetComponent<Rigidbody>().AddForce((coefficient / 200) * (relativeVelocity / 200));
            }
        }
        
    }
}
