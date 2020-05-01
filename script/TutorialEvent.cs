using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    public GameObject Sprite_1;     // 1枚目の画像
    public GameObject Sprite_2;     // 2枚目の画像
    public float flame;               // 画像切り替えのフレームカウント
    public float ChangeCount;
    public float Alpha;             // 画像フェードイン/アウト
    public bool bEnter;             // 当たり判定に入った時
    public bool bExit;              // 当たり判定を出たとき
    public bool bChange;            // 画像切り替えON/OFF

    // Start is called before the first frame update
    void Start()
    {
        Sprite_1 = this.transform.Find("Sprite_1").gameObject;
        Sprite_2 = this.transform.Find("Sprite_2").gameObject;
        Sprite_1.SetActive(false);
        Sprite_2.SetActive(false);
        flame = 0;
        ChangeCount = 1f;
        Alpha = 0f;
        bEnter = false;
        bExit = false;
        bChange = false;
    }

    // Update is called once per frame
    void Update()
    {
        Sprite_1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Alpha);
        Sprite_2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Alpha);

        // 当たり判定に入った時
        if (bEnter)
        {
            Alpha += 0.05f;
            
        }

        // Alphaが増えきったら１に固定
        if(Alpha > 1f)
        {
            Alpha = 1f;
            flame += Time.deltaTime;
        }

        // フレームで切り替え
        if(flame <= 1.5f)
        {
            bChange = true;
        }
        else if(flame >= 1.5)
        {
            bChange = false;
        }

        if(flame >= 3f)
        {
            flame = 0;
        }

        // 画像切り替え
        if(bEnter && bChange)
        {
            Sprite_1.SetActive(true);
            Sprite_2.SetActive(false);
        }
        else if (bEnter && !bChange)
        {
            Sprite_1.SetActive(false);
            Sprite_2.SetActive(true);
        }

        // 当たり判定を出たら
        if (bExit)
        {
            bEnter = false;
            Alpha -= 0.05f;
        }

        if (Alpha <= 0f)
        {
            Alpha = 0f;
            bExit = false;
            Sprite_1.SetActive(false);
            Sprite_2.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bExit = true;
        }
    }

}
