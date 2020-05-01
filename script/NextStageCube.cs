using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageCube : MonoBehaviour
{
    public int PlayingSceneIndexNum = 0;
    public int ClearSceneIndexNum = 0;
    public int NextSceneIndexNum = 0;
    public bool bNext;

    // Start is called before the first frame update
    void Start()
    {
        PlayingSceneIndexNum = SceneManager.GetActiveScene().buildIndex;  // 現在のシーンのインデックス番号を取得
        ClearSceneIndexNum = SaveLoadManager.LoadClearData();             // 最大クリアシーンを取得
        bNext = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider hit)
    {
        // 接触したオブジェクトのタグが"Player"の場合
        if (hit.gameObject.CompareTag("Player") && !bNext)
        {
            bNext = true;
            // 現在のシーンと最大クリアシーンを比較し上回っていたら更新
            if (PlayingSceneIndexNum >= ClearSceneIndexNum)
            {
                SaveLoadManager.SaveClearData(PlayingSceneIndexNum);
            }

            // 現在のシーンが最終ステージか判定する
            if (PlayingSceneIndexNum == 20)
            {
                FadeManager.FadeOut(0);                                // シーン遷移(タイトル)
            }
            else
            {
                NextSceneIndexNum = (PlayingSceneIndexNum + 1);        // 次のシーン
                SaveLoadManager.SavePlayingData(NextSceneIndexNum);    // コンティニューするシーンを更新
                FadeManager.FadeOut(NextSceneIndexNum);                // シーン遷移(次のシーン)
            }

            Debug.Log(SaveLoadManager.LoadPlayingData());
            Debug.Log(SaveLoadManager.LoadClearData());

        }
    }
        

    
}
