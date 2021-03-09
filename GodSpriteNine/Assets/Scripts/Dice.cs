using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    protected Animator m_Anim;
    public int DiceValue = 0;
    public int DiceValue_Enemy = 0;
    public bool Virtual_Enemy_Animation_Parameters = false;
    private GameObject AllEvent;
    private GameObject AllEnemyEvent;
    public float EventSpeed = 5;

    private int DothingsValue = 0;

    private Vector3 tempTargetAllEventPosition;
    private Vector3 tempTargetAllEnemyEventPosition;

    private GameController GameController;

    private bool iActionCompleted = false;
    private bool EnemyActionCompleted = false;

    


    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        m_Anim = this.GetComponent<Animator>();
        AllEvent = GameObject.Find("AllEvent");
        AllEnemyEvent = GameObject.Find("AllEnemyEvent");
        m_Anim.SetFloat("DiceValue", DiceValue);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("m_Anim.GetFloat  DiceValue = " + m_Anim.GetFloat("DiceValue"));
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Dice"))
        {
            //float a = m_Anim.GetFloat("DiceValue");
            //Debug.Log("DothingsValue=" + DothingsValue);

            //Debug.Log("Global.Clicked=" + Global.Clicked);
            //Debug.Log("Global.Clicked_Enemy=" + Global.Clicked_Enemy);
            if (DothingsValue == 3)
            {
                DothingsValue = 0;
                AllEvent.transform.Translate(Vector3.left * Time.deltaTime * EventSpeed);
                if (AllEvent.transform.position.x <= tempTargetAllEventPosition.x)
                {
                    Global.Clicked = false;
                    iActionCompleted = true;
                }

                AllEnemyEvent.transform.Translate(Vector3.left * Time.deltaTime * EventSpeed);
                if (AllEnemyEvent.transform.position.x <= tempTargetAllEnemyEventPosition.x)
                {
                    Global.Clicked_Enemy = false;
                    EnemyActionCompleted = true;
                }
            }

            if (Global.Clicked)
            {
                DothingsValue = 0;
                if (m_Anim.GetFloat("DiceValue") != 0)
                {
                    AllEvent.transform.Translate(Vector3.left * Time.deltaTime * EventSpeed);
                }
                m_Anim.SetFloat("DiceValue", DiceValue);

                if (AllEvent.transform.position.x <= tempTargetAllEventPosition.x)
                {
                    Global.Clicked = false;
                    iActionCompleted = true;
                }
            }
        }

        if (Global.StartSlide && Global.Clicked_Enemy)
        {
            DothingsValue = 0;
            if (Virtual_Enemy_Animation_Parameters)
            {
                AllEnemyEvent.transform.Translate(Vector3.left * Time.deltaTime * EventSpeed);
            }
            Virtual_Enemy_Animation_Parameters = true;

            if (AllEnemyEvent.transform.position.x <= tempTargetAllEnemyEventPosition.x)
            {
                Global.Clicked_Enemy = false;
                EnemyActionCompleted = true;
            }
        }
        //Debug.Log("iActionCompleted = " + iActionCompleted);
        //Debug.Log("EnemyActionCompleted = " + EnemyActionCompleted);
        //Debug.Log("Global.CurrentRoundCompleted = " + Global.CurrentRoundCompleted);
        if (iActionCompleted && EnemyActionCompleted)
        {
            iActionCompleted = false;
            EnemyActionCompleted = false;
            Global.StartSlide = false;
            Global.CurrentRoundCompleted = true;
        }

        if (Global.StartSlide)
        {
            
        }
    }

    void OnMouseDown()
    {
        if (!Global.StartSlide)
        {
            GameController.countDownTime = 0;
            DothingsValue = 1;
            Dothings(DothingsValue);

        }
        
    }

    //value(1为玩家；2为敌方;3为自动行进一格)
    public void Dothings(int value)
    {

        if (value == 3)
        {
            m_Anim.SetFloat("DiceValue", 0);//初始化动画参数，不然会点击出现抖动
            Global.StartSlide = true;
            DiceValue = 1;

            //Global.target = Global.target + DiceValue;
            //targetEvent = Global.AllEventlist[Global.target];

            //Global.EnemyTarget = Global.EnemyTarget + DiceValue;
            //EnemyTargetEvent = Global.AllEnemyEventlist[Global.EnemyTarget];

            tempTargetAllEventPosition = new Vector3(Global.allEventVirtualPosition.x - DiceValue * Global.everyEventOffset,
                                                        Global.allEventVirtualPosition.y, Global.allEventVirtualPosition.z);
            Global.allEventVirtualPosition = tempTargetAllEventPosition;
            GameController.createEveryEvent(DiceValue);

            tempTargetAllEnemyEventPosition = new Vector3(Global.allEnemyEventVirtualPosition.x - DiceValue * Global.everyEventOffset,
                                                            Global.allEnemyEventVirtualPosition.y, Global.allEnemyEventVirtualPosition.z);
            Global.allEnemyEventVirtualPosition = tempTargetAllEnemyEventPosition;
            GameController.createEnemyEveryEvent(DiceValue);

        }
        else if (value == 1)
        {
            m_Anim.SetFloat("DiceValue", 0);//初始化动画参数，不然会点击出现抖动
            Global.StartSlide = true;
            Global.Clicked = true;
            DiceValue = Random.Range(1, 7);
            m_Anim.Play("Dice");

            //Global.target = Global.target + DiceValue;
            //targetEvent = Global.AllEventlist[Global.target];

            tempTargetAllEventPosition = new Vector3(Global.allEventVirtualPosition.x - DiceValue * Global.everyEventOffset, 
                                                        Global.allEventVirtualPosition.y, Global.allEventVirtualPosition.z);
            Global.allEventVirtualPosition = tempTargetAllEventPosition;
            GameController.createEveryEvent(DiceValue);
        }
        //2
        else  if(value == 2)
        {
            Global.Clicked_Enemy = true;
            Virtual_Enemy_Animation_Parameters = false;
            DiceValue_Enemy = Random.Range(1, 7);

            //Global.EnemyTarget = Global.EnemyTarget + DiceValue;
            //EnemyTargetEvent = Global.AllEnemyEventlist[Global.EnemyTarget];

            tempTargetAllEnemyEventPosition = new Vector3(Global.allEnemyEventVirtualPosition.x - DiceValue_Enemy * Global.everyEventOffset, 
                                                            Global.allEnemyEventVirtualPosition.y, Global.allEnemyEventVirtualPosition.z);
            Global.allEnemyEventVirtualPosition = tempTargetAllEnemyEventPosition;
            GameController.createEnemyEveryEvent(DiceValue_Enemy);

        }
        
        //Debug.Log("targetEvent:" + ", id=" + targetEvent.id + ", player=" + targetEvent.player + ", race=" + targetEvent.race + ", skill=" + targetEvent.skill);
    }

    public void Dothings(int value,int value2)
    {
        if (value == 1)
        {
            Global.StartSlide = true;
            Global.Clicked = true;

            DiceValue = value2;
            if (DiceValue != 0) 
            {
                m_Anim.Play("Dice");
            }
            

            //Global.target = Global.target + DiceValue;
            //targetEvent = Global.AllEventlist[Global.target];

            tempTargetAllEventPosition = new Vector3(Global.allEventVirtualPosition.x - DiceValue * Global.everyEventOffset,
                                                        Global.allEventVirtualPosition.y, Global.allEventVirtualPosition.z);
            Global.allEventVirtualPosition = tempTargetAllEventPosition;
            GameController.createEveryEvent(DiceValue);
        }
        else if (value == 2)
        {
            Global.Clicked_Enemy = true;
            Virtual_Enemy_Animation_Parameters = false;
            DiceValue_Enemy = value2;

            //Global.EnemyTarget = Global.EnemyTarget + DiceValue;
            //EnemyTargetEvent = Global.AllEnemyEventlist[Global.EnemyTarget];

            tempTargetAllEnemyEventPosition = new Vector3(Global.allEnemyEventVirtualPosition.x - DiceValue_Enemy * Global.everyEventOffset,
                                                            Global.allEnemyEventVirtualPosition.y, Global.allEnemyEventVirtualPosition.z);
            Global.allEnemyEventVirtualPosition = tempTargetAllEnemyEventPosition;
            GameController.createEnemyEveryEvent(DiceValue_Enemy);

        }
    }


}
