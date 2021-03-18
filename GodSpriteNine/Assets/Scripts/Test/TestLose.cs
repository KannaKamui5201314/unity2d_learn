using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLose : MonoBehaviour
{
   
    float tempTime;
    private Material material;
    void Start()
    {
        tempTime = 0;
        material = GetComponent<MeshRenderer>().material;
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0);
    }
    void Update()
    {
        if (tempTime < 1)
        {
            tempTime = tempTime + Time.deltaTime;
        }
        if (material.color.a <= 1)
        {
            material.color = new Color(
          material.color.r
          , material.color.g,
            material.color.b,
            material.color.a + tempTime / 50);
        }
    }
}
