using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_4GoalAnimationCube : MonoBehaviour
{
    public bool N_4StageClear = false;
    public bool N_4SleepStart = false;
    public bool se_con;
    public GameObject soundmanager;
    public AudioClip Goal_Se;
    private AudioSource g_audio;

    void Start()
    {
        g_audio = gameObject.GetComponent<AudioSource>();
        se_con = false;
    }

    void OnTriggerEnter(Collider hit)
    {
        // 接触したオブジェクトのタグが"Player"の場合
        if (hit.gameObject.CompareTag("Player"))
        {
            N_4StageClear = true;
            if (!se_con)
            {
                soundmanager.GetComponent<AudioSource>().Stop();
                g_audio.PlayOneShot(Goal_Se);
                se_con = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            N_4SleepStart = true;
        }
    }
}
