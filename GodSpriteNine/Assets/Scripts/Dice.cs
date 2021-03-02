using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    protected Animator m_Anim;
    public int DiceValue = 0;
    private bool mouseDown = false;
    private GameObject AllEvent;
    public float AllEventSpeed = 5;

    private Vector3 tempAllEventPosition;
    private Vector3 tempTargetAllEventPosition;
    // Start is called before the first frame update
    void Start()
    {
        m_Anim = this.GetComponent<Animator>();
        AllEvent = GameObject.FindGameObjectWithTag("AllEvent");
        m_Anim.SetFloat("DiceValue", DiceValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseDown)
        {
            if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Dice"))
            {
                //float a = m_Anim.GetFloat("DiceValue");
                //Debug.Log("a=" + a);
                
                if (m_Anim.GetFloat("DiceValue") != 0) 
                {
                    AllEvent.transform.Translate(Vector3.left * Time.deltaTime * AllEventSpeed);
                }
                m_Anim.SetFloat("DiceValue", DiceValue);
                //Debug.Log("AllEvent.transform.localPosition.x = " + AllEvent.transform.localPosition.x);
                //Debug.Log("tempTargetAllEventPosition.x = " + tempTargetAllEventPosition.x);
                if (AllEvent.transform.position.x <= tempTargetAllEventPosition.x)
                {
                    mouseDown = false;
                    
                }
            }
        }
    }

    void OnMouseDown()
    {
        mouseDown = true;
        m_Anim.SetFloat("DiceValue", 0);//初始化动画参数，不然会点击出现抖动
        DiceValue = Random.Range(1, 7);
        m_Anim.Play("Dice");
        Global.target = Global.target + DiceValue;
        EveryEvent targetEvent = Global.AllEventlist[Global.target];
        //Debug.Log("targetEvent:" + ", id=" + targetEvent.id + ", player=" + targetEvent.player + ", race=" + targetEvent.race + ", skill=" + targetEvent.skill);
        tempAllEventPosition = AllEvent.transform.position;
        tempTargetAllEventPosition = new Vector3(Global.allEventVirtualPosition.x - DiceValue * Global.everyEventOffset, Global.allEventVirtualPosition.y, Global.allEventVirtualPosition.z);
        Global.allEventVirtualPosition = tempTargetAllEventPosition;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().createEveryEvent(DiceValue);
    }


}
