using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kai : MonoBehaviour
{
    public AudioClip kai_toziru;
    public AudioClip kai_tozinai;
    private new AudioSource audio;

    public GameObject kaigara;
    private Animation anim;
    private Vector3 playersize;
    private float rimitsize;
    // Start is called before the first frame update
    void Start()
    {
        anim = kaigara.gameObject.GetComponent<Animation>();
        playersize = new Vector3(0, 0, 0);
        rimitsize = 16.0f;

        // 音関連
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        // プレイヤータグが当たった時
        if (col.gameObject.CompareTag("Player"))
        {
            // プレイヤーのサイズを取得
            playersize = col.gameObject.transform.localScale;

            // 規定サイズ以上なら
            if(playersize.x > rimitsize)
            {
                audio.PlayOneShot(kai_tozinai);
                anim.Play("Kai_Tozinai");
            }
            // 規定サイズ以下なら
            else if (playersize.x < rimitsize)
            {
                audio.PlayOneShot(kai_toziru);
                anim.Play("Kai_Toziru");
                gameObject.GetComponent<BoxCollider>().enabled = false;
                col.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
                col.gameObject.GetComponent<HitodeController>().destroy = true;
            }
        }
    }
}
