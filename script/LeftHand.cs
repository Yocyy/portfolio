using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    float Left_hand_size_x = 0.5f; // プレイヤーの手のサイズ(x)
    float Left_hand_size_y = 1.0f; // プレイヤーの手のサイズ(y)
    float Left_hand_size_z = 0.5f; // プレイヤーの手のサイズ(z)
    float Left_hand_position_x = 0.5f; // プレイヤーの場所(x)
    float Left_hand_position_y = 1.0f; // プレイヤーの場所(y)
    float Left_hand_position_z = 0.5f; // プレイヤーの場所(z)

    public GameObject hitodePrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F5))
        {
            Left_hand_size_y += 0.01f;
            Left_hand_position_y = transform.position.y + 0.0025f;
            hitodePrefab.transform.localScale = new Vector3(Left_hand_size_x, Left_hand_size_y, Left_hand_size_z);
            hitodePrefab.transform.position = new Vector3(transform.position.x, Left_hand_position_y, transform.position.z);
        }

    }
}