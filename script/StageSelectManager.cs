using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public AudioClip t_cursor;
    public AudioClip start;
    public AudioClip t_error;
    private AudioSource audioSource;

    public GameObject StageImage_1;
    public GameObject StageImage_2;
    public GameObject StageImage_3;
    public GameObject StageImage_4;
    public GameObject StageImage_5;
    public GameObject StageImage_6;
    public GameObject Hitode;
    public int Cursor;
    public int SceneIndexNum;
    public int HitodeCount;
    public bool bHitode;
    private bool SelectFlag;
    private bool LStickLeft;
    private bool LStickRight;
    private bool One;

    float minAngle = 0.0F;
    float maxAngle = 90.0F;
    float rotTime = 0f;
    int countAnim = 0;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        Cursor = 0;
        SceneIndexNum = SaveLoadManager.LoadPlayingData();
        bHitode = false;
        SelectFlag = false;
        One = false;
        Hitode = GameObject.Find("Hitode_Title").gameObject;
        anim = Hitode.GetComponent<Animator>();
        StageImage_1 = this.transform.Find("StageSelectCanvas/BlackPanel/Stage_1").gameObject;
        StageImage_2 = this.transform.Find("StageSelectCanvas/BlackPanel/Stage_2").gameObject;
        StageImage_3 = this.transform.Find("StageSelectCanvas/BlackPanel/Stage_3").gameObject;
        StageImage_4 = this.transform.Find("StageSelectCanvas/BlackPanel/Stage_4").gameObject;
        StageImage_5 = this.transform.Find("StageSelectCanvas/BlackPanel/Stage_5").gameObject;
        StageImage_6 = this.transform.Find("StageSelectCanvas/BlackPanel/Stage_6").gameObject;

        Hitode.transform.position = new Vector3(7.5f, -1.5f, 3f);
        Hitode.transform.rotation = Quaternion.Euler(0f, 90, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // カーソル移動          
        if (LStickRight && !One && !SelectFlag)
        {
            Cursor = (Cursor + 1) % 6;
            audioSource.PlayOneShot(t_cursor);
            if (LStickRight)
            {
                One = true;
            }
        }

        if (LStickLeft && !One && !SelectFlag)
        {
            Cursor = (Cursor + (6 - 1)) % 6;
            audioSource.PlayOneShot(t_cursor);
            if (LStickLeft)
            {
                One = true;
            }
        }

        // ヒトデちゃん移動
        if (bHitode)
        {
            countAnim++;
            anim.SetBool("is_walk", true);
            rotTime += 0.02f;
            float angle = Mathf.LerpAngle(maxAngle, minAngle, rotTime);
            Hitode.transform.eulerAngles = new Vector3(0, angle, 0);
            if (countAnim >= 60)
            {
                Hitode.transform.Translate(0.1f, 0, 0);
                HitodeCount++;
            }
        }

        if (HitodeCount >= 60)
        {
            FadeManager.FadeOut(SceneIndexNum);   // フェードアウト パターン1
        }

        switch (Cursor)
        {
            // ステージ1-1
            case 0:
                StageImage_1.SetActive(true);
                StageImage_2.SetActive(false);
                StageImage_3.SetActive(false);
                StageImage_4.SetActive(false);
                StageImage_5.SetActive(false);
                StageImage_6.SetActive(false);

                if(Input.GetButtonDown("HitodeListDeside") && !SelectFlag)
                {
                    
                    if (SceneIndexNum >= 1)
                    {
                        bHitode = true;
                        SceneIndexNum = 1;
                        //FadeManager.FadeOut(SceneIndexNum);  // フェードアウト パターン2
                        Debug.Log("おされたよ！");
                        SelectFlag = true;
                        audioSource.PlayOneShot(start);
                    }
                    else
                    {
                        Debug.Log("まだクリアしてないよ！");
                        audioSource.PlayOneShot(t_error);
                    }
                   
                }

                break;

            // ステージ2-1
            case 1:
                StageImage_1.SetActive(false);
                StageImage_2.SetActive(true);
                StageImage_3.SetActive(false);
                StageImage_4.SetActive(false);
                StageImage_5.SetActive(false);
                StageImage_6.SetActive(false);

                if (Input.GetButtonDown("HitodeListDeside") && !SelectFlag)
                {
                   
                    if (SceneIndexNum >= 5)
                    {
                        bHitode = true;
                        SceneIndexNum = 5;
                        //FadeManager.FadeOut(SceneIndexNum);  // フェードアウト パターン2
                        Debug.Log("おされたよ！");
                        SelectFlag = true;
                        audioSource.PlayOneShot(start);
                    }
                    else
                    {
                        Debug.Log("まだクリアしてないよ！");
                        audioSource.PlayOneShot(t_error);
                    }
                   
                }

                break;

            // ステージ3-1
            case 2:
                StageImage_1.SetActive(false);
                StageImage_2.SetActive(false);
                StageImage_3.SetActive(true);
                StageImage_4.SetActive(false);
                StageImage_5.SetActive(false);
                StageImage_6.SetActive(false);

                if (Input.GetButtonDown("HitodeListDeside") && !SelectFlag)
                {
                    
                    if (SceneIndexNum >= 9)
                    {
                        bHitode = true;
                        SceneIndexNum = 9;
                        //FadeManager.FadeOut(SceneIndexNum);  // フェードアウト パターン2
                        Debug.Log("おされたよ！");
                        SelectFlag = true;
                        audioSource.PlayOneShot(start);
                    }
                    else
                    {
                        Debug.Log("まだクリアしてないよ！");
                        audioSource.PlayOneShot(t_error);
                    }
                   
                }

                break;

            // ステージ4-1
            case 3:
                StageImage_1.SetActive(false);
                StageImage_2.SetActive(false);
                StageImage_3.SetActive(false);
                StageImage_4.SetActive(true);
                StageImage_5.SetActive(false);
                StageImage_6.SetActive(false);

                if (Input.GetButtonDown("HitodeListDeside") && !SelectFlag)
                {
                    
                    if (SceneIndexNum >= 13)
                    {
                        bHitode = true;
                        SceneIndexNum = 13;
                        //FadeManager.FadeOut(SceneIndexNum);  // フェードアウト パターン2
                        Debug.Log("おされたよ！");
                        SelectFlag = true;
                        audioSource.PlayOneShot(start);
                    }
                    else
                    {
                        Debug.Log("まだクリアしてないよ！");
                        audioSource.PlayOneShot(t_error);
                    }
                   
                }

                break;

            // ステージ5-1
            case 4:
                StageImage_1.SetActive(false);
                StageImage_2.SetActive(false);
                StageImage_3.SetActive(false);
                StageImage_4.SetActive(false);
                StageImage_5.SetActive(true);
                StageImage_6.SetActive(false);

                if (Input.GetButtonDown("HitodeListDeside") && !SelectFlag)
                {
                    
                    if (SceneIndexNum >= 17)
                    {
                        bHitode = true;
                        SceneIndexNum = 17;
                        //FadeManager.FadeOut(SceneIndexNum);  // フェードアウト パターン2
                        Debug.Log("おされたよ！");
                        SelectFlag = true;
                        audioSource.PlayOneShot(start);
                    }
                    else
                    {
                        Debug.Log("まだクリアしてないよ！");
                        audioSource.PlayOneShot(t_error);
                    }
                   
                }

                break;

            // ステージ6-1
            case 5:
                StageImage_1.SetActive(false);
                StageImage_2.SetActive(false);
                StageImage_3.SetActive(false);
                StageImage_4.SetActive(false);
                StageImage_5.SetActive(false);
                StageImage_6.SetActive(true);

                if (Input.GetButtonDown("HitodeListDeside") && !SelectFlag)
                {
                    
                    if (SceneIndexNum >= 21)
                    {
                        bHitode = true;
                        SceneIndexNum = 21;
                        //FadeManager.FadeOut(SceneIndexNum);  // フェードアウト パターン2
                        Debug.Log("おされたよ！");
                        SelectFlag = true;
                        audioSource.PlayOneShot(start);
                    }
                    else
                    {
                        Debug.Log("まだクリアしてないよ！");
                        audioSource.PlayOneShot(t_error);
                    }
                   
                }

                break;
        }

        if (One)
        {
            LStickLeft = false;
            LStickRight = false;
            if ((Input.GetAxis("Horizontal") == 0f))
            {
                One = false;
            }
        }

        if (!One)
        {
            LStickLeft = (Input.GetAxis("Horizontal") < 0f);
            LStickRight = (Input.GetAxis("Horizontal") > 0f);
        }
    }
}
