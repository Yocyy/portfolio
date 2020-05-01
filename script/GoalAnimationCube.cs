using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimationCube : MonoBehaviour
{
    public bool StageClear = false;
    public bool SleepStart = false;
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
            if(!se_con)
            {
                soundmanager.GetComponent<AudioSource>().Stop();
                g_audio.PlayOneShot(Goal_Se);
                se_con = true;
            }
            StageClear = true;
            ////現在のシーンのインデックス番号を取得
            //int nowSceneIndexNumber = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(++nowSceneIndexNumber);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {     
            SleepStart = true;
        }
    }
}
