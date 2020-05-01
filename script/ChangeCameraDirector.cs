using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraDirector : MonoBehaviour
{
    //　メインカメラ
    public GameObject mainCamera;
    //　切り替える他のカメラ
    public GameObject worldCamera;

    public GameObject hitodeListCamera;

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeCamera_HitodeList()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        worldCamera.SetActive(!worldCamera.activeSelf);
        hitodeListCamera.SetActive(!hitodeListCamera.activeSelf);
    }
}
