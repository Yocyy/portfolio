using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catapult : MonoBehaviour
{
    public AudioClip se_catapult;
    public GameObject catapult_obj;
    private new AudioSource audio;
    private int catapult_con;
    private bool once;
    // Start is called before the first frame update
    void Start()
    {
        catapult_con = 0;
        once = false;
        // 音関連
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("catapult") && !once)
        { 
            catapult_obj.SetActive(true);
            audio.PlayOneShot(se_catapult);
            once = true;
        }

        if(once)
        {
            catapult_con++;
            if(catapult_con > 100)
            {
                catapult_obj.SetActive(false);
            }
        }
    }
}
