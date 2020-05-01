using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_4GoalCamera : MonoBehaviour
{
    float Rot_y = 0.5f;

    float Pos_x = 0.09f;
    float Pos_y = 0.05f;
    float Pos_z = 0.11f;

    float StartCameraX;
    float StartCameraY;
    float StartCameraZ;

    float StartCameraRotX;
    float Count;
    public GameObject BackGroundImage;  // MainCameraの子オブジェクトのbackgroundを継承させる。
    void Start()
    {
        //Cnrlからプレイヤーのポジションを取得
        Vector3 playerPos = GameObject.Find("Cnrl").GetComponent<Cnrl>().playerTransform.position;
        StartCameraX = playerPos.x + 4;
        StartCameraY = playerPos.y + 4;
        StartCameraZ = transform.position.z;
        transform.position = new Vector3(StartCameraX, StartCameraY, transform.position.z);
        StartCameraRotX = transform.rotation.x;
        Instantiate(BackGroundImage, BackGroundImage.transform.position, BackGroundImage.transform.rotation);
    }

    void Update()
    {
        Count += Time.deltaTime;
        Debug.Log(Count);
        // ポジション
        if (Count < 3)
        {
            StartCameraY += Pos_y;
            StartCameraX += Pos_x;
            StartCameraZ += Pos_z;
            transform.position = new Vector3(StartCameraX, StartCameraY, StartCameraZ);
            //Debug.Log("AAA");
            // 回転
            StartCameraRotX += Rot_y;
            transform.rotation = Quaternion.Euler(StartCameraRotX, transform.rotation.y, transform.rotation.z);
        }
    }
}
