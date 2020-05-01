using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Hit_Restart : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.maxDistance = 10.0f;
    }
    // オブジェクトと接触した時に呼ばれるコールバック
    void OnCollisionEnter(Collision hit)
    {
        // 接触したオブジェクトのタグが"Player"の場合
        if (hit.gameObject.CompareTag("Player"))
        {
            // プレイヤー消去処理
            audioSource.Play();
            hit.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            hit.gameObject.GetComponent<HitodeController>().destroy = true;
        }
    }
}