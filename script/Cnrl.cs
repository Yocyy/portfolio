using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Cnrl : MonoBehaviour
{
    public int max_hitode;  // 現在存在するヒトデ数
    public int mode_hitode; // モード
    public int mass;
    public Transform playerTransform;
    public GameObject[] GetPlayerTag;
    public Transform cursorTransform;
    public GameObject btnPref;       // ボタン
    public Sprite btnImage;          // 生きてるイメージ
    public Sprite deadBtnImage;      // 死んでるイメージ
    public GameObject[] Buttons;
    public GameObject[] Hitodes;
    public HitodeListCameraController HLCControllerCS;

    void Start()
    {
        GetPlayerTag = GameObject.FindGameObjectsWithTag("Player");
        max_hitode = 1;
        ButtonInstance().GetComponent<Button>().Select();
        cursorTransform = gameObject.transform;
        mass = 0;
        HLCControllerCS = GameObject.Find("ChangeCameraDirector").GetComponent<HitodeListCameraController>();
    }

    void Update()
    {

        for (int i = 0; i < max_hitode;i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                mode_hitode = i;

                GetPlayerTag = GameObject.FindGameObjectsWithTag("Player"); // PlayerTagを格納
            }
        }

        GetPlayerTag = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject gmObj in GetPlayerTag)
        {
            int id = gmObj.GetComponent<HitodeController>().ID;
            
            if (id == HLCControllerCS.cursor)
            {
                cursorTransform = gmObj.transform;
            }
            if (id == mode_hitode)
            {
                playerTransform = gmObj.transform;
            }
        }
    }

    public void Hitodeplus()
    { 
        max_hitode++;
    }
    

    public void HitodeChange(int num)
    {


        mode_hitode = num;

        GetPlayerTag = GameObject.FindGameObjectsWithTag("Player");


        foreach (GameObject gmObj in GetPlayerTag)
        {
            int id = gmObj.GetComponent<HitodeController>().ID;
            if (id == mode_hitode)
            {
                playerTransform = gmObj.transform;
            }
        }
    }

    public GameObject ButtonInstance()
    {
        GameObject btn = Instantiate(btnPref);

        Image born = btn.GetComponent<Image>();

        born.sprite = btnImage;

        btn.transform.SetParent(GameObject.Find("HitodeListCanvas/Scroll View/Viewport/Content").GetComponent<RectTransform>(), false);

        btn.GetComponent<SelectButton>().ID = max_hitode - 1;

        btn.transform.Find("HitodeNumber").GetComponentInChildren<Text>().text = "ヒトデちゃん" + max_hitode + "号";

        Buttons = GameObject.FindGameObjectsWithTag("HitodeButton");

        Hitodes = GameObject.FindGameObjectsWithTag("Player");

        mass = (int)Hitodes[mode_hitode].GetComponent<HitodeController>().vir_mass;
        Buttons[mode_hitode].transform.Find("HitodeHeavy").GetComponent<Text>().text =
            "おもさ " + mass.ToString() + "g";

        if(Hitodes[mode_hitode].GetComponent<HitodeController>().One_Division == true)
        {
            mass = (int)Hitodes[max_hitode - 1].GetComponent<HitodeController>().vir_mass;
            Buttons[max_hitode - 1].transform.Find("HitodeHeavy").GetComponent<Text>().text =
            "おもさ " + mass.ToString() + "g";
        }

        return btn;

    }

    public void ButtonDestroy()
    {
        Buttons = GameObject.FindGameObjectsWithTag("HitodeButton");

        Hitodes = GameObject.FindGameObjectsWithTag("Player");

        for (int id = 0; id < max_hitode; id++)
        {

            if(Hitodes[id].GetComponent<HitodeController>().destroy == true)
            {
                Image dead = Buttons[id].GetComponent<Image>();

                dead.sprite = deadBtnImage;

                Buttons[id].transform.Find("HitodeNumber").GetComponent<Text>().text = "イサマシキ";

                Buttons[id].transform.Find("HitodeHeavy").GetComponent<Text>().text = "  シースター";
            }
        }
    }
}
