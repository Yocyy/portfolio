using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCameraController : MonoBehaviour
{
    float Rot_y = 0;
    float Pos_x = 0.0005f;
    public GameObject BackGroundImage;  // MainCameraの子オブジェクトのbackgroundを継承させる。
    void Start()
    {
        //Cnrlからプレイヤーのポジションを取得
        Vector3 playerPos = GameObject.Find("Cnrl").GetComponent<Cnrl>().playerTransform.position;
        transform.position = new Vector3(playerPos.x + 4, playerPos.y + 4, transform.position.z);
        Instantiate(BackGroundImage, BackGroundImage.transform.position, BackGroundImage.transform.rotation);
    }

    void Update()
    {
        if (Pos_x < 3)
        {
            transform.position = new Vector3(transform.position.x + Pos_x, transform.position.y, transform.position.z);
        }
        if (Rot_y < 20)
        {
            Rot_y += 0.1f;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + Rot_y, transform.rotation.z);
        }
 
    }
}
