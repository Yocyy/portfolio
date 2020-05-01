using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HitodeNum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HitodeNumDraw();
    }

    public void HitodeNumDraw()
    {
        GameObject.Find("HitodeNum").gameObject.GetComponent<Text>().text = GameObject.Find("Cnrl").gameObject.GetComponent<Cnrl>().max_hitode.ToString();
    }
}
