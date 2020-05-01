using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        //Cnrlからプレイヤーのポジションを取得
        Vector3 playerPos = GameObject.Find("Cnrl").GetComponent<Cnrl>().playerTransform.position;

        // カメラ座標生成
        transform.position = new Vector3(playerPos.x + 4, playerPos.y + 4, transform.position.z);

        if(transform.position.y < 5)
        {
            transform.position = new Vector3(playerPos.x + 4, 5.0f, transform.position.z);
        }
    }
}
