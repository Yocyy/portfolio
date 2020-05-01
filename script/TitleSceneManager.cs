using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;


public class TitleSceneManager : MonoBehaviour
{
    // 音関連
    public AudioClip t_cursor;
    public AudioClip start;
    public AudioClip select;
    public AudioClip t_error;

    private AudioSource audioSource;

    public GameObject TitleCanvas01;  // 
    public GameObject TitleCanvas02;  // 
    public GameObject TitleLogo;
    public GameObject Hitode;         // ヒトデちゃん
    public GameObject PressTxt;             // PressEnterのテキスト
    public GameObject NewGameTxt;           // ニューゲームのテキスト
    public GameObject ContinueTxt;          // コンティニューのテキスト
    public GameObject StageSelectTxt;       // ステージセレクトのテキスト
    public GameObject QuitTxt;
    public GameObject Cover;
    public GameObject Cover_1;
    public GameObject Cover_2;
    public GameObject Cover_3;
    public GameObject Cover_4;
    public float f_Count;                 // 
    public float SelectCount;
    public int Cursor;                // カーソルくん
    public int PlayingSceneIndexNum;  // コンティニュー用
    public int ClearSceneIndexNum;    // ステージセレクト用
    public float f_time;                // PresseEnterの点滅で使うやつ
    public float speed;               // PresseEnterの点滅で使うやつ
    public bool StartFlag;            // 
    public bool bHitodeMove;              // 
    public bool SelectFlag;           // PressEnter後の管理用
    public bool CountFlag;            // 誤操作用
    public bool StageSelectFlag;
    private bool LStickUp;
    private bool LStickDown;
    private bool One;
    public bool bEnd;                 // 最終ステージをクリアしたかどうか
    public Animator anim;

   
    public float Alpha;
    public bool HitodeStop;

    float hitodeRotSpeed = 1f;
    float hitodeMoveSpeed = 4f;
    float minAngle = 0.0F;
    float maxAngle = 90.0F;
    float rotTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        FadeManager.FadeIn();
        audioSource = gameObject.GetComponent<AudioSource>();
        Cursor = 0;
        speed = 0.1f;
        f_Count = 0;
        SelectCount = 0;
        f_time = 0;
        StartFlag = false;
        bHitodeMove = false;
        SelectFlag = false;
        CountFlag = false;
        StageSelectFlag = false;
        PlayingSceneIndexNum = SaveLoadManager.LoadPlayingData();
        ClearSceneIndexNum = SaveLoadManager.LoadClearData();
        One = false;
        
        Hitode = GameObject.Find("Hitode_Title").gameObject;
        anim = Hitode.GetComponent<Animator>();
        TitleCanvas01 = this.transform.Find("TitleCanvas_1").gameObject;
        TitleCanvas02 = this.transform.Find("TitleCanvas_2").gameObject;
        Cover = this.transform.Find("TitleCanvas_1/Cover").gameObject;
        Cover_1 = this.transform.Find("TitleCanvas_2/Menu/Cover_1").gameObject;
        Cover_2 = this.transform.Find("TitleCanvas_2/Menu/Cover_2").gameObject;
        Cover_3 = this.transform.Find("TitleCanvas_2/Menu/Cover_3").gameObject;
        Cover_4 = this.transform.Find("TitleCanvas_2/Menu/Cover_4").gameObject;
        PressTxt = this.transform.Find("TitleCanvas_1/Cover/PressAnyButton").gameObject;
        NewGameTxt = this.transform.Find("TitleCanvas_2/Menu/Cover_1/NewGame").gameObject;
        ContinueTxt = this.transform.Find("TitleCanvas_2/Menu/Cover_2/Continue").gameObject;
        StageSelectTxt = this.transform.Find("TitleCanvas_2/Menu/Cover_3/StageSelect").gameObject;
        QuitTxt = this.transform.Find("TitleCanvas_2/Menu/Cover_4/Quit").gameObject;

        TitleLogo = this.transform.Find("Background/TitleLogo").gameObject;

        HitodeStop = false;

        TitleCanvas02.SetActive(false);

        if (ClearSceneIndexNum >= 12)
        {
            Hitode.transform.position = new Vector3(-16f, -1.5f, 3f);
            Hitode.transform.rotation = Quaternion.Euler(0f, 0, 0f);
            TitleCanvas01.SetActive(false);
            anim.SetBool("is_walk", true);
            bEnd = true;
            Alpha = 0f;
        }
        else if(ClearSceneIndexNum >= 0 && ClearSceneIndexNum <= 11)
        {
            Hitode.transform.position = new Vector3(7.5f, -1.5f, 3f);
            Hitode.transform.rotation = Quaternion.Euler(0f, 90, 0f);
            TitleCanvas01.SetActive(true);
            bEnd = false;
            Alpha = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Press Enter前 */
        // Press Enter
       
        Debug.Log(Hitode.transform.rotation.y);

        // 最終ステージからタイトルもどる演出
        if (bEnd)
        {
            // タイトルロゴのアルファ
            TitleLogo.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Alpha);

            if (!HitodeStop)
            {
                // ヒトデちゃん入場
                Hitode.transform.position += new Vector3(Time.deltaTime * hitodeMoveSpeed, 0, 0);
            }
            else if(HitodeStop)
            {
                // ヒトデちゃん回転
                rotTime += (Time.deltaTime * hitodeRotSpeed);
                float angle = Mathf.LerpAngle(minAngle, maxAngle, rotTime);
                Hitode.transform.eulerAngles = new Vector3(0, angle, 0);
               
            }

            // タイトルロゴが出るタイミング
            if (Hitode.transform.position.x >= 4f)
            {
                Alpha += 0.01f;
            }
            // ヒトデちゃんが止まるタイミング
            if (Hitode.transform.position.x >= 8f)
            {
                HitodeStop = true;
            }

            if (Alpha >= 1f)
            {
                Alpha = 1f;
                
            }
            
            // ヒトデちゃんのアニメーションが止まるタイミング
            if(Hitode.transform.rotation.y >= 0.7f)
            {

                anim.SetBool("is_walk", false);
                rotTime = 0;
                bEnd = false;
            }

        }

        // 通常のタイトル
        if (!bEnd)
        {
            
            if (!StartFlag)
            {
                f_time = Mathf.Abs(Mathf.Sin(Time.time * 2));
                TitleCanvas01.SetActive(true);

                Cover.GetComponent<Image>().color = new Color(1f, 1f, 1f, f_time);
                PressTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, f_time);

                if (Input.anyKeyDown)
                {
                    StartFlag = true;
                    audioSource.PlayOneShot(select);
                }
            }

            /* Press Enter後 */

            if (StartFlag)
            {
                if (!CountFlag)
                {
                    f_Count += Time.deltaTime;
                }

                f_time = Mathf.Abs(Mathf.Sin(Time.time * 15));

                Cover.GetComponent<Image>().color = new Color(1f, 1f, 1f, f_time);
                PressTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, f_time);

                // 秒数経過で変化
                if (f_Count >= 1f)
                {

                    TitleCanvas01.SetActive(false);
                    TitleCanvas02.SetActive(true);
                    CountFlag = true;
                    SelectFlag = true;
                    f_Count = 0;
                }

                // ヒトデちゃん移動
                if (bHitodeMove)
                {
                    anim.SetBool("is_walk", true);
                    rotTime += (Time.deltaTime * hitodeRotSpeed);
                    float angle = Mathf.LerpAngle(maxAngle, minAngle, rotTime);
                    Hitode.transform.eulerAngles = new Vector3(0, angle, 0);

                    if(Hitode.transform.rotation.y <= 0.0f)
                    {

                        
                        Hitode.transform.position += new Vector3(Time.deltaTime * hitodeMoveSpeed, 0, 0);
                        
                    }
                    
                }
                
                if (Hitode.transform.position.x >= 17f)
                {
                    FadeManager.FadeOut(PlayingSceneIndexNum);     // フェードアウト パターン1
                }
                
                if (StageSelectFlag)
                {
                    SelectCount += Time.deltaTime;
                    if (SelectCount >= 0.8f)
                    {
                        SceneManager.LoadScene("StageSelectScene");
                    }
                }

                // カーソル移動          
                if (LStickDown && !One && SelectFlag)
                {
                    Cursor = (Cursor + 1) % 4;
                    audioSource.PlayOneShot(t_cursor);
                    if (LStickDown)
                    {
                        One = true;
                    }
                }

                if (LStickUp && !One && SelectFlag)
                {
                    Cursor = (Cursor + (4 - 1)) % 4;
                    audioSource.PlayOneShot(t_cursor);
                    if (LStickUp)
                    {
                        One = true;
                    }
                }


                switch (Cursor)
                {
                    // ニューゲーム
                    case 0:
                        NewGameTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
                        ContinueTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        StageSelectTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        QuitTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                        Cover_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);

                        if (Input.GetButtonDown("HitodeListDeside") && !bHitodeMove && SelectFlag)
                        {
                            SelectFlag = false;
                            bHitodeMove = true;
                            
                            audioSource.PlayOneShot(start);
                            PlayingSceneIndexNum = 1;
                            //SaveLoadManager.DeleteData();      // セーブデータ削除
                                                               //FadeManager.FadeOut(SceneIndexNum);   // フェードアウト パターン2
                        }


                        break;

                    // コンティニュー
                    case 1:
                        NewGameTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        ContinueTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
                        StageSelectTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        QuitTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                        Cover_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);

                        if (Input.GetButtonDown("HitodeListDeside") && SelectFlag)
                        {

                            if (ClearSceneIndexNum >= 1)
                            {
                                audioSource.PlayOneShot(start);
                                
                                SelectFlag = false;
                                bHitodeMove = true;
                            }
                            else
                            {
                                Debug.Log("まだ選べないよ！");
                                audioSource.PlayOneShot(t_error);
                            }
                        }

                        break;

                    // ステージセレクト
                    case 2:
                        NewGameTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        ContinueTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        StageSelectTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
                        QuitTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                        Cover_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);

                        if (Input.GetButtonDown("HitodeListDeside") && SelectFlag)
                        {
                            if (ClearSceneIndexNum >= 4)
                            {
                                audioSource.PlayOneShot(select);
                               
                                SelectFlag = false;
                                StageSelectFlag = true;
                            }
                            else
                            {
                                Debug.Log("まだ選べないよ！");
                                audioSource.PlayOneShot(t_error);
                            }
                        }

                        break;

                    // ゲーム終了
                    case 3:
                        NewGameTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        ContinueTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        StageSelectTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        QuitTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
                        Cover_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.25f);
                        Cover_4.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                        if (Input.GetButtonDown("HitodeListDeside") && SelectFlag)
                        {
                            GameQuit();
                            audioSource.PlayOneShot(t_error);
                        }

                        break;
                }
            }
        }

        if (One)
        {
            LStickUp = false;
            LStickDown = false;
            if ((Input.GetAxis("HitodeSelectLStickDown") == 0f) && (Input.GetAxis("HitodeSelectLStickUp") == 0f))
            {
                One = false;
            }
        }

        if (!One)
        {
            LStickUp = (Input.GetAxis("HitodeSelectLStickUp") < 0f);
            LStickDown = (Input.GetAxis("HitodeSelectLStickDown") > 0f);
        }

    }

    /* ゲーム終了処理 */
    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
    UnityEngine.Application.Quit();
#endif
    }
}
