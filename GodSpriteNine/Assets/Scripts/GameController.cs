using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    public Scrollbar BloodScrollbar;

    public Text BloodValueText;
    public Scrollbar BloodScrollbarEnemy;
    public Text BloodValueTextEnemy;
    public Text RoundCount;
    public Text RoundText;
    public Text CountDownText;

    public GameObject EveryEventPrefab;
    public GameObject AllEvent;
    public GameObject AllEnemyEvent;

    public Sprite NullEventIcon;
    public Sprite[] EventIcons_1;
    public Sprite[] EventIcons_2;
    public Sprite[] EventIcons_3;
    public Sprite[] EventIcons_4;
    public Sprite[] EventIcons_5;
    public Sprite[] EventIcons_6;
    public Sprite[] EventIcons_7;
    

    private Dice Dice;

    public float countDownTime = 0;

    private static float EveryRoundTime = 15;

    private bool BeginDialogCompleted = false;

    private bool startCountDownTime = false;

    private bool LoadDataCompleted = false;

    private GameObject QuitGameCanvas;
    private GameObject TransitionCanvas;
    private GameObject BeginDialog;
    private GameObject GameOverCanvas;

    private AudioSource BackgroundAudio;

    void OnDisable() 
    {
        
        //if(Global.Debug) Debug.Log("OnDisable");
    }

    void OnDestroy()
    {
        //if (Global.Debug) Debug.Log("OnDestroy");
    }

    void Start()
    {
        //修改帧率
        //Application.targetFrameRate = 30;
        //Ctrl + K + C全选注释
        //Ctrl + K + U 全选去掉注释
        

        QuitGameCanvas = GameObject.Find("QuitGameCanvas");
        TransitionCanvas = GameObject.Find("TransitionCanvas");
        BeginDialog = GameObject.Find("BeginDialog");
        GameOverCanvas = GameObject.Find("GameOverCanvas");
        QuitGameCanvas.GetComponent<Canvas>().enabled = false;
        TransitionCanvas.GetComponent<Canvas>().enabled = true;
        BeginDialog.GetComponent<Canvas>().enabled = false;
        GameOverCanvas.GetComponent<Canvas>().enabled = false;

        InitData();

        LoadDataCompleted = LoadData();

        Dice = GameObject.Find("Dice").GetComponent<Dice>();
        BackgroundAudio = GameObject.Find("background").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameOver();
        if (Global.CurrentRoundCompleted)
        {
            Global.CurrentRoundCompleted = false;
            Global.NextRound = true;

            Global.RoundCount = Global.RoundCount + 1;
            RoundCount.GetComponent<Text>().text = Global.RoundCount.ToString();

            //Debug.Log("EveryEventGameObjectsList.count = " + Global.EveryEventGameObjectsList.Count);
            //Debug.Log("EnemyEveryEventGameObjectsList.count = " + Global.EnemyEveryEventGameObjectsList.Count);
            Global.EveryEventGameObjectsList.Clear();
            Global.EnemyEveryEventGameObjectsList.Clear();
        }

        //游戏开始入口
        if (LoadDataCompleted)//①
        {
            TransitionCanvas.GetComponent<Canvas>().enabled = false;
            BeginDialog.GetComponent<Canvas>().enabled = true;
            LoadDataCompleted = false;
        }

        if (BeginDialog.GetComponent<Canvas>().enabled)//②
        {
            countDownTime += Time.deltaTime;
            if (countDownTime > 2.0f)
            {
                BeginDialog.GetComponent<Canvas>().enabled = false;
                BeginDialogCompleted = true;
                countDownTime = 0;
                
                Global.RoundCount = Global.RoundCount + 1;
                RoundCount.GetComponent<Text>().text = Global.RoundCount.ToString();
                Global.NextRound = true;
                if (!Global.Debug)
                {
                    BackgroundAudio.Play();
                }
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            showQuitGameDialog();
        }
        //Debug.Log("Global.NextRound = " + Global.NextRound);
        if (Global.NextRound && BeginDialogCompleted) //③
        {
            Global.EveryEventGameObjectsList.Clear();
            Global.EnemyEveryEventGameObjectsList.Clear();
            Global.NextRound = false;
            startCountDownTime = true;
            countDownTime = 0;
            //if (Global.Clicked)
            {
                Dice.Dothings(2);
            }
        }
        if (startCountDownTime)//④
        {
            if (!Global.StartSlide)
            {
                countDownTime += Time.deltaTime;
                CountDownText.text = (EveryRoundTime - Mathf.Floor(countDownTime)).ToString();
            }
                
            if (countDownTime > EveryRoundTime)
            {
                Dice.Dothings(1,0);
                countDownTime = 0;
                startCountDownTime = false;

            }
        }
    }

    public void createEveryEvent(int count) 
    {
        for (int i = 0; i < count; i++)
        {
            //Debug.Log(AllEventlist.Count);
            int race;
            if (Random.Range(1, 11) > 7)
            {
                race = Random.Range(1, 8);
            }
            else
            {
                race = 0;
            }
            if (Global.AllEventlist.Count == 0) 
            {
                //Debug.Log("AllEventlist.Count = " + AllEventlist.Count);
                race = 0;
            }
            EveryEvent newEveryEvent = new EveryEvent(race.ToString(), Random.Range(0, 7).ToString());
            Global.AllEventlist.Add(newEveryEvent);
            Vector3 tempEveryEventPosition = new Vector3(Global.lastEveryEventPosition.x + Global.everyEventOffset, -1, 0);
            Vector3 newEveryEventPosition = new Vector3(Global.lastEveryEventPosition.x + Global.everyEventOffset + AllEvent.transform.position.x, -1, 0);
            Global.lastEveryEventPosition = tempEveryEventPosition;
            GameObject newEveryEventPrefab = Instantiate(EveryEventPrefab, newEveryEventPosition, Quaternion.identity);
            //Debug.Log(newEveryEventPrefab.transform.position.x);
            newEveryEventPrefab.transform.parent = AllEvent.transform;
            //Debug.Log(newEveryEventPrefab.transform.position.x);
            setSprite(newEveryEventPrefab, newEveryEvent);
            setEveryEvent(newEveryEventPrefab, newEveryEvent);
        }
    }

    public void createEnemyEveryEvent(int count)
    {
        for (int i = 0; i < count; i++)
        {
            //Debug.Log(AllEventlist.Count);
            int race;
            if (Random.Range(1, 11) > 7)
            {
                race = Random.Range(1, 8);
            }
            else
            {
                race = 0;
            }
            if (Global.AllEnemyEventlist.Count == 0)
            {
                //Debug.Log("AllEnemyEventlist.Count = " + AllEnemyEventlist.Count);
                race = 0;
            }
            EveryEvent newEveryEvent = new EveryEvent(race.ToString(), Random.Range(0, 7).ToString());
            Global.AllEnemyEventlist.Add(newEveryEvent);
            Vector3 tempEveryEnemyEventPosition = new Vector3(Global.lastEveryEnemyEventPosition.x + Global.everyEventOffset, 1, 0);
            Vector3 newEveryEventPosition = new Vector3(Global.lastEveryEnemyEventPosition.x + Global.everyEventOffset + AllEnemyEvent.transform.position.x, 1, 0);
            Global.lastEveryEnemyEventPosition = tempEveryEnemyEventPosition;
            GameObject newEveryEventPrefab = Instantiate(EveryEventPrefab, newEveryEventPosition, Quaternion.identity);
            //Debug.Log(newEveryEventPrefab.transform.position.x);
            newEveryEventPrefab.transform.parent = AllEnemyEvent.transform;
            //Debug.Log(newEveryEventPrefab.transform.position.x);
            setSprite(newEveryEventPrefab, newEveryEvent);
            setEveryEvent(newEveryEventPrefab, newEveryEvent);
        }
    }

    public void getSpriteName()
    {
        Sprite sprite = null;
        for (int i = 0; i < 7; i++) 
        {
            sprite = EventIcons_7[i];
            Debug.Log("sprite" + i + " =" + sprite.name);
        }
        
    }

    void setSprite(GameObject m_GameObject, EveryEvent newEveryEvent)
    {
        Sprite sprite = null;
        //Debug.Log("newEveryEvent.race=" + newEveryEvent.race + "; newEveryEvent.skill=" + newEveryEvent.skill) ;
        switch (newEveryEvent.race) {
            case "0":
                sprite = NullEventIcon;
                break;
            case "1":
                sprite = EventIcons_1[int.Parse(newEveryEvent.skill)];
                break;
            case "2":
                sprite = EventIcons_2[int.Parse(newEveryEvent.skill)];
                break;
            case "3":
                sprite = EventIcons_3[int.Parse(newEveryEvent.skill)];
                break;
            case "4":
                sprite = EventIcons_4[int.Parse(newEveryEvent.skill)];
                break;
            case "5":
                sprite = EventIcons_5[int.Parse(newEveryEvent.skill)];
                break;
            case "6":
                sprite = EventIcons_6[int.Parse(newEveryEvent.skill)];
                break;
            case "7":
                sprite = EventIcons_7[int.Parse(newEveryEvent.skill)];
                break;

        }
        m_GameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        if (newEveryEvent.race.Equals("0")) {
            //设置颜色
            m_GameObject.GetComponent<SpriteRenderer>().color = new Color32(112, 200, 181, 255);
        }
    }

    void setEveryEvent(GameObject m_GameObject, EveryEvent newEveryEvent)
    {
        m_GameObject.GetComponent<EveryEventController>().setEveryEvent(newEveryEvent);
    }

    public void setBlood(int value)
    {
        Global.currentBloodValue = value;
        BloodScrollbar.size = value * 0.01f;
        BloodValueText.text = value.ToString();
    }

    public void setBloodEnemy(int value)
    {
        Global.currentEnemyBloodValue = value;
        BloodScrollbarEnemy.size = value * 0.01f;
        BloodValueTextEnemy.text = value.ToString();
    }

    void InitData()
    {
        Global.RoundCount = 0;
        Global.currentBloodValue = Global.DefaultBloodValue;
        Global.currentEnemyBloodValue = Global.DefaultBloodValue;
        Global.target = 0;
        Global.AllEventlist.Clear();
        Global.AllEnemyEventlist.Clear();
        Global.lastEveryEventPosition = new Vector3(-1.28f, -1, 0);
        Global.lastEveryEnemyEventPosition = new Vector3(-1.28f, 1, 0);
        Global.allEventVirtualPosition = new Vector3(0, 0, 0);
        Global.allEnemyEventVirtualPosition = new Vector3(0, 0, 0);
    }

    bool LoadData()
    {
        AllEvent.transform.position = Global.allEventVirtualPosition;
        AllEnemyEvent.transform.position = Global.allEnemyEventVirtualPosition;
        setBlood(Global.DefaultBloodValue);
        setBloodEnemy(Global.DefaultBloodValue);
        createEveryEvent(20);
        createEnemyEveryEvent(20);
        RoundCount.GetComponent<Text>().text = Global.RoundCount.ToString();
        return true;
    }

    void showQuitGameDialog()
    {
        QuitGameCanvas.GetComponent<Canvas>().enabled = true;
    }


    void gameOver()
    {
        if (Global.currentBloodValue <= 0 || Global.currentEnemyBloodValue <= 0)
        {
            GameOverCanvas.GetComponent<Canvas>().enabled = true;
        } 
    }
}
