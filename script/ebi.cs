using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ebi : MonoBehaviour
{
    public AudioClip alive;
    public AudioClip dead;
    private new AudioSource audio;

    public bool soundON;
    public int dwind_con;

    public GameObject Awind; // 規定サイズ以上用
    public GameObject Dwind; // 規定サイズ以下用
    public HaritukuController haritukuController; //値参照用
    private Vector3 playersize;
    private float rimitsize;

    // アニメーション
    private Animator anim;

    void Start()
    {
        playersize = new Vector3(0, 0, 0);
        rimitsize = 19.0f;
        Awind.SetActive(false);
        Dwind.SetActive(false);
        // 音関連
        audio = gameObject.GetComponent<AudioSource>();
        // アニメーション
        anim = GetComponent<Animator>();
        soundON = false;
        dwind_con = 0;
    }

    void Update()
    {
        if (haritukuController.flag_cut)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Awind.SetActive(false);
            Dwind.SetActive(false);
        }
        else if (!haritukuController.flag_cut)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        if (soundON)
        {
            dwind_con++;
            if (dwind_con > 20)
            {
                Dwind.SetActive(false);
                anim.SetBool("EBI", false);
                soundON = false;
                dwind_con = 0;
            }
        }
    }

    // 判定
    void OnTriggerStay(Collider Col)
    {
        // プレイヤータグが当たった時
        if (Col.gameObject.CompareTag("Player"))
        {
            dwind_con = 0;
            // プレイヤーのサイズを取得
            playersize = Col.gameObject.transform.localScale;
            if (!haritukuController.flag_cut)
            {
                anim.SetBool("EBI", true);
            }
            // プレイヤーサイズが規定サイズ以上なら
            if (playersize.x > rimitsize)
            {
                Awind.SetActive(true);
                if (!soundON)
                {
                    audio.PlayOneShot(alive);
                    soundON = true;
                }

            }
            // プレイヤーサイズが規定サイズ以下なら
            else if (playersize.x < rimitsize)
            {
                if (!haritukuController.flag_cut)
                {
                    Dwind.SetActive(true);
                    if (!soundON)
                    {
                        audio.PlayOneShot(dead);
                        soundON = true;
                    }
                }
                    
            }
        }
    }

    // ヒトデが離れたら止める
    void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.CompareTag("Player"))
        {
            anim.SetBool("EBI", false);
            Awind.SetActive(false);
            Dwind.SetActive(false);
            soundON = false;
        }
    }
}
