using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{

    public static int PlayingStage;
    public static int ClearStage;

    // Start is called before the first frame update
    void Start()
    {
        // 念のための初期化
        PlayingStage = 0;
        ClearStage = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SavePlayingData(int SceneIndex)
    {

        //NOWというキーでSceneIndexの中身ををセーブ
        PlayerPrefs.SetInt("NOW", SceneIndex);
        PlayerPrefs.Save();

    }

    public static void SaveClearData(int SceneIndex)
    {

        //ClearというキーでSceneIndexの中身ををセーブ
        PlayerPrefs.SetInt("Clear", SceneIndex);
        PlayerPrefs.Save();

    }



    public static int LoadPlayingData()
    {

        // NOWのキーの文字列を代入。第二引数はデータが保存されてない場合に表示するもの
        PlayingStage = PlayerPrefs.GetInt("NOW", 0);

        return PlayingStage;
    }

    public static int LoadClearData()
    {

        // Clearのキーの文字列を代入。第二引数はデータが保存されてない場合に表示するもの
        ClearStage = PlayerPrefs.GetInt("Clear", 0);

        return ClearStage;
    }

    public static void DeleteData()
    {

        //保存データを全て削除
        PlayerPrefs.DeleteAll();

    }
}
