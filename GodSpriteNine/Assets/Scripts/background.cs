using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    public GameObject m_Background;

    //false is down;
    //true is up;
    private bool MoveDirection = false;
    public float Speed = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Background.transform.position.y <= -8)
        {
            MoveDirection = true;
        }
        else if (m_Background.transform.position.y >= 8)
        {
            MoveDirection = false;
        }

        if (MoveDirection)
        {
            m_Background.transform.Translate(Vector3.up * Time.deltaTime * Speed);
        }else{
            m_Background.transform.Translate(Vector3.down * Time.deltaTime * Speed);
        }
        
    }
}
