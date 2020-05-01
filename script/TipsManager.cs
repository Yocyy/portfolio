using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsManager : MonoBehaviour
{
    public GameObject Tips_1;     // 1枚目の画像
    public GameObject Tips_2;     // 2枚目の画像
    public float stopCount;               // 画像切り替えのフレームカウント
    public float flame;
    public int ChangeCount;
    public float Alpha;             // 画像フェードイン/アウト
    public bool bTips;
    public int change;            // 画像切り替えON/OFF
    public bool bAxis;            // 軸の判定用

    public int mode_hitode;
    public GameObject[] hitodes;

    public bool bGoal;
    public bool bJump;
    public bool bDivision;
    public bool bPause;
    public bool bList;
    public bool bHitodeAction;
    public bool bNext;
    public bool tri_LTButton;

    public Cnrl CnrlCS;
    public PauseManager PauseManagerCS;
    public HitodeController HitodeControllerCS;
    public HitodeListCameraController HLCControllerCS;
    public NextStageCube NextStageCubeCS;

    // Start is called before the first frame update
    void Start()
    {
        Tips_1 = this.transform.Find("Canvas/Images/Tips_1").gameObject;
        Tips_2 = this.transform.Find("Canvas/Images/Tips_2").gameObject;
        Tips_1.SetActive(false);
        Tips_2.SetActive(false);
        stopCount = 0;
        flame = 0;
        ChangeCount = 100;
        Alpha = 0f;
        bTips = false;
        bAxis = false;
        change = 0;

        hitodes = GameObject.FindGameObjectsWithTag("Player");

        CnrlCS = GameObject.Find("Cnrl").GetComponent<Cnrl>();
        mode_hitode = CnrlCS.mode_hitode;

        PauseManagerCS = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        HitodeControllerCS = hitodes[mode_hitode].GetComponent<HitodeController>();
        HLCControllerCS = GameObject.Find("ChangeCameraDirector").GetComponent<HitodeListCameraController>();
        NextStageCubeCS = GameObject.Find("NextStageCube").GetComponent<NextStageCube>();

    }

    // Update is called once per frame
    void Update()
    {
        Tips_1.GetComponent<Image>().color = new Color(1f, 1f, 1f, Alpha);
        Tips_2.GetComponent<Image>().color = new Color(1f, 1f, 1f, Alpha);

        mode_hitode = CnrlCS.mode_hitode;
        hitodes = GameObject.FindGameObjectsWithTag("Player");
        HitodeControllerCS = hitodes[mode_hitode].GetComponent<HitodeController>();

        bGoal = HitodeControllerCS.Goal;
        bJump = HitodeControllerCS.flag_jump;
        bDivision = HitodeControllerCS.One_Division;
        bPause = PauseManagerCS.bPause;
        bList = HLCControllerCS.bList;
        bHitodeAction = HitodeControllerCS.flag_action;
        bNext = NextStageCubeCS.bNext;
        tri_LTButton = HLCControllerCS.tri_LTButton;

        if(Input.GetAxis("Horizontal") == 0)
        {
            bAxis = false;
        }
        else
        {
            bAxis = true;
        }

        if (bPause == false && bList == false && FadeManager.isFadeIn == false &&
            FadeManager.isFadeOut == false && bHitodeAction == false && bNext == false &&
            bAxis == false && bJump == false && bDivision == false && bGoal == false)
        {
            if (!Input.anyKeyDown)
            {
                stopCount += Time.deltaTime;
            }
            else if (Input.anyKeyDown)
            {
                stopCount = 0;
                bTips = false;
            }
        }
        else if (bPause == true || bList == true || FadeManager.isFadeIn == true ||
                FadeManager.isFadeOut == true || bHitodeAction == true || bNext == true || 
                bAxis == true || bJump == true || bDivision == true || bGoal == true)
        {
            stopCount = 0;
        }

        if(stopCount >= 1)
        {
            bTips = true;
            Alpha += 0.1f;
        }
        else
        {
            bTips = false;
        }

        if(Alpha >= 1f)
        {
            Alpha = 1f;
        }

        if(bTips)
        {
            flame += Time.deltaTime;
        }
        else if(!bTips)
        {
            Alpha -= 0.1f;
            flame = 0;

            if(Alpha < 0f)
            {
                Alpha = 0f;
                change = 0;
            }
        }
        
        if(flame >= 2f)
        {
            change = 1;
        }
        if(flame >= 4f)
        {
            change = 0;
            flame = 0;
        }

        switch(change)
        {
            case 0:
                Tips_1.SetActive(true);
                Tips_2.SetActive(false);
                break;

            case 1:
                Tips_1.SetActive(false);
                Tips_2.SetActive(true);
                break;

            default:
                break;
        }
    }
}
