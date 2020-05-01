using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    public AudioClip cursor;
    public AudioClip pause_op;
    public AudioClip pause_ed;
    public AudioClip restart;
    public AudioClip backtitle;
    private AudioSource audioSource;

    public GameObject[] hitodes;                          // ヒトデちゃんの配列
    public int mode_hitode;                               // ヒトデちゃんの操作権

    public HitodeController HitodeControllerCS;           // プレイヤーのスクリプトを入れるため
    public Cnrl CnrlCS;                                   // Cnrlのスクリプトを入れるため
    public HitodeListCameraController HLCControllerCS;    // ヒトデリストのスクリプトを入れるため

    public GameObject PauseCanvas;       // ポーズ画面のキャンバス
    public GameObject PlayTxt;                 // プレイ再開
    public GameObject RestartTxt;              // リスタート
    public GameObject TitleTxt;                // タイトル
    public int presentSceneIndex;        // 現在のシーンインデックス番号を入れる箱
    public bool bPause;
    public bool bNot;                    // 誤操作用
    public int Cursor;
    private bool LStickUp;
    private bool LStickDown;
    private bool One;

    // Start is called before the first frame update
    void Start()
    {
        FadeManager.FadeIn();
        audioSource = gameObject.GetComponent<AudioSource>();
        bPause = false;
        bNot = false;
        Cursor = 0;
        presentSceneIndex = SceneManager.GetActiveScene().buildIndex;     // 現在のシーンインデックス番号を入れる
        One = false;

        hitodes = GameObject.FindGameObjectsWithTag("Player");
        CnrlCS = GameObject.Find("Cnrl").GetComponent<Cnrl>();
        mode_hitode = CnrlCS.mode_hitode;
        HitodeControllerCS = hitodes[mode_hitode].GetComponent<HitodeController>();
        HLCControllerCS = GameObject.Find("ChangeCameraDirector").GetComponent<HitodeListCameraController>();

        PauseCanvas = this.transform.Find("PauseCanvas").gameObject;
        PlayTxt = this.transform.Find("PauseCanvas/PausePanel/PlayCover/Play").gameObject;
        RestartTxt = this.transform.Find("PauseCanvas/PausePanel/RestartCover/Restart").gameObject;
        TitleTxt = this.transform.Find("PauseCanvas/PausePanel/TitleCover/Title").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        hitodes = GameObject.FindGameObjectsWithTag("Player");
        mode_hitode = CnrlCS.mode_hitode;
        HitodeControllerCS = hitodes[mode_hitode].GetComponent<HitodeController>();

        if (FadeManager.isFadeIn == false && !HitodeControllerCS.Goal)
        {
            if (!bPause && Input.GetButtonDown("Pause") && !bNot)   // ポーズフラグオン
            {
                bPause = true;
                audioSource.PlayOneShot(pause_op);
            }
            else if (bPause && Input.GetButtonDown("Pause") && !bNot)   // ポーズフラグオフ
            {
                bPause = false;
                audioSource.PlayOneShot(pause_ed);
            }

            if (bPause)
            {
                PauseCanvas.SetActive(true);   // ポーズ画面を開く
                Time.timeScale = 0f;           // ゲーム内の時間を止める
                HitodeControllerCS.enabled = false;    // プレイヤーのスクリプトを止める
                HLCControllerCS.enabled = false;          // リストのスクリプトを止める

                // カーソル移動
                if (LStickDown && !One)
                {
                    Cursor = (Cursor + 1) % 3;
                    audioSource.PlayOneShot(cursor);
                    if (LStickDown)
                    {
                        One = true;
                    }  
                }

                if (LStickUp && !One)
                {
                    Cursor = (Cursor + (3 - 1)) % 3;
                    audioSource.PlayOneShot(cursor);
                    if (LStickUp)
                    {
                        One = true;
                    }
                }

                switch (Cursor)
                {
                    // プレイ再開
                    case 0:
                        PlayTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
                        RestartTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        TitleTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);

                        if (Input.GetButtonDown("HitodeListDeside"))
                        {
                            bPause = false;
                            audioSource.PlayOneShot(pause_ed);
                        }

                        break;

                    // リスタート
                    case 1:
                        PlayTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        RestartTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);
                        TitleTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);

                        if (Input.GetButtonDown("HitodeListDeside"))
                        {
                            bNot = true;
                            audioSource.PlayOneShot(restart);
                            bPause = false;
                            FadeManager.FadeOut(presentSceneIndex);
                        }

                        break;

                    // タイトル
                    case 2:
                        PlayTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        RestartTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 0.25f);
                        TitleTxt.GetComponent<Text>().color = new Color(1f, 1f, 1f, 1f);

                        if (Input.GetButtonDown("HitodeListDeside"))
                        {
                            bNot = true;
                            bPause = false;
                            audioSource.PlayOneShot(backtitle);
                            FadeManager.FadeOut(0);
                        }

                        break;
                }

            }
            else if (!bPause)
            {
                PauseCanvas.SetActive(false);  // ポーズ画面をたたむ
                Time.timeScale = 1f;           // ゲーム内の時間を進める
                HitodeControllerCS.enabled = true;     // プレイヤーのスクリプトを再開させる
                HLCControllerCS.enabled = true;          // リストのスクリプトを再開
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
}
