using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actionbutton_1 : MonoBehaviour
{
    public Button button_1;
    // Start is called before the first frame update
    void Start()
    {
        button_1.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            button_1.Select();
        }
    }
}
