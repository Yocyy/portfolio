using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    public bool flag_Enter = false;     // N-1 ～ N-3のフラグ
    public bool flag_Exit = false;      // N-1 ～ N-3のフラグ
    public bool flag_N_4_Enter = false; // N-4のフラグ
    public bool flag_N_4_Exit = false;  // N-4のフラグ
    public bool flag_Harituku = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Goal"))
        {
            flag_Enter = true;}

        if(other.gameObject.CompareTag("N_4Goal"))
        {
            flag_N_4_Enter = true;
        }

        if (other.gameObject.CompareTag("Harituku"))
        {
            flag_Harituku = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            flag_Exit = true;
        }

        if (other.gameObject.CompareTag("N_4Goal"))
        {
            flag_N_4_Exit = true;
        }

        if (other.gameObject.CompareTag("Harituku"))
        {
            flag_Harituku = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Harituku"))
        {
            flag_Harituku = true;
        }
    }
}
