using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Allow : MonoBehaviour
{
    //public HitodeListCameraController HitodeListCameraController;

    public HitodeListCameraController listCam;
    public Transform pos;
    public int cursor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //かーそるインデックス取得
        cursor = listCam.GetComponent<HitodeListCameraController>().cursor;

        //選択されてるヒトデの座標を取得
        pos = GameObject.Find("Cnrl").GetComponent<Cnrl>().cursorTransform;

        //矢印の座標をハイライトされてるヒトデに移動
        gameObject.transform.position = new Vector3(pos.position.x, pos.position.y + 3, pos.position.z);
    }
}
