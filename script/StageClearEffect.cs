using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearEffect : MonoBehaviour
{
    private PlayerColliderController playerColliderController;
    float Timer = 0;
    int ImageCount = -1;
    public GameObject[] EffectImage;
    int ImageSize = 8;
    float WaitTime = 0;

    void Start()
    {
        for(int i=0;i< ImageSize;i++)
        EffectImage[i].SetActive(false);
    }


    void Update()
    {
        bool Goal_1 = GameObject.FindWithTag("Goal").GetComponent<GoalAnimationCube>().StageClear;
        bool Goal_2 = GameObject.FindWithTag("N_4Goal").GetComponent<N_4GoalAnimationCube>().N_4StageClear;
        Debug.Log("Goal = "+ Goal_1);
        if ((Goal_1 || Goal_2) && ImageCount != 7)
        {
            WaitTime += Time.deltaTime;
            if(WaitTime > 0.8f)
            Timer += Time.deltaTime;
            if(Timer > 0.2) // 0.2秒経過したら
            {
                Timer = 0;
                ImageCount++;
                EffectImage[ImageCount].SetActive(true);    //  画像の生成
            }
        }
    }
}
