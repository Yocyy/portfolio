using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HaritukuController : MonoBehaviour
{
    public bool flag_harituku = false;
    public bool flag_cut;
    public int haritukuID;
    public Vector3 tmpPos;
    public Quaternion tmpRot;
    void Start()
    {
        flag_cut = false;
    }

    // 間欠泉のON/OFF用
    void OnTriggerStay(Collider col)
    {
        // プレイヤーが張り付いたかどうか
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.GetComponent<HitodeController>().flag_harituku)
            {
                if (col.gameObject.CompareTag("Player"))
                {
                    flag_cut = true;
                }
            }
            else if (!(col.GetComponent<HitodeController>().flag_harituku))
            {
                if (col.gameObject.CompareTag("Player"))
                {
                    flag_cut = false;
                }
            }
        }

    }

    // フラグ渡し用関数
    public bool Get_Flag()
    {
        return flag_cut;
    }

    // Update is called once per frame
    void Update()
    {
        // flag_harituku = false;
        //Debug.Log(flag_harituku);
        tmpPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        tmpRot = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HaritukuCollider"))
        {
            flag_harituku = true;
           //Debug.Log(flag_harituku);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("HaritukuCollider"))
        {
            flag_harituku = false;
            flag_cut = false;
            //Debug.Log(flag_harituku);
        }

    }

}
