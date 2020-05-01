using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
   //bool flag = false;
    void Start()
    {

    }

    void Update()
    {
       // float range = 0.5f;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Vector3[] VertexData = mesh.vertices;

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    flag ^= true;
        //}
        //if (flag)
        //{
        //    range = 1.0f;
        //}

        //for (int i = 0; i < 24; i++)
        //{
        //    if (flag && VertexData[i].y == 0.5f)
        //    {
        //        VertexData[i].y = range;
        //    }
        //    else if (!flag && VertexData[i].y == 1.0f)
        //    {
        //        VertexData[i].y = range;
        //    }
        //}

       // mesh.vertices = VertexData;
        //mesh.RecalculateNormals();
    }
}