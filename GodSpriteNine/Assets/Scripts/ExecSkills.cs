using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ExecSkills : MonoBehaviour
{
    private GameController GameController;
    private EveryEvents everyEvents;

    public Text testText;
    private string strJson = "";
    // Start is called before the first frame update
    void Start()
    {
        
        GameController = GameObject.Find("GameController").GetComponent<GameController>();
        everyEvents = GetEveryEvents();
        Debug.Log("dataPath=" + Application.dataPath);
        Debug.Log("persistentDataPath=" + Application.persistentDataPath);
        Debug.Log("streamingAssetsPath=" + Application.streamingAssetsPath);
        Debug.Log("temporaryCachePath=" + Application.temporaryCachePath);
        Debug.Log("consoleLogPath=" + Application.consoleLogPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ExecSkill()
    {
        if (Global.EveryEventGameObjectsList.Count >= 1)
        {

            EveryEvent everyEvent = Global.EveryEventGameObjectsList[Global.EveryEventGameObjectsList.Count - 1]
                                    .gameObject.GetComponent<EveryEventController>().getEveryEvent();
            //Debug.Log("everyEvent = " + everyEvent);
            Debug.Log("\neveryEvent.race=" + everyEvent.race + "\neveryEvent.skill=" + everyEvent.skill);
            if (!everyEvent.race.Equals("0"))
            {
                int injuryValue = everyEvents.everyEvents[(int.Parse(everyEvent.race) - 1) * 7 + int.Parse(everyEvent.skill)].injuryValue;
                Debug.Log("injuryValue = " + injuryValue);
                switch (everyEvent.skill)
                {
                    case "4":break;
                    case "6":
                        GameController.setBlood((Global.currentBloodValue + injuryValue) >= 100 ? 100 : Global.currentBloodValue + injuryValue);
                        break;
                    default:
                        GameController.setBloodEnemy((Global.currentEnemyBloodValue + injuryValue) >= 100 ? 100 : Global.currentEnemyBloodValue + injuryValue);
                        break;
                }
                
            }
            
        }
        if (Global.EnemyEveryEventGameObjectsList.Count >= 1)
        {

            EveryEvent everyEvent = Global.EnemyEveryEventGameObjectsList[Global.EnemyEveryEventGameObjectsList.Count - 1]
                                    .gameObject.GetComponent<EveryEventController>().getEveryEvent();
            //Debug.Log("everyEvent = " + everyEvent);
            Debug.Log("\neveryEvent.race=" + everyEvent.race + "\neveryEvent.skill=" + everyEvent.skill);
            if (!everyEvent.race.Equals("0"))
            {
                int injuryValue = everyEvents.everyEvents[(int.Parse(everyEvent.race) - 1) * 7 + int.Parse(everyEvent.skill)].injuryValue;
                Debug.Log("injuryValue = " + injuryValue);
                switch (everyEvent.skill)
                {
                    case "4": break;
                    case "6":
                        GameController.setBloodEnemy((Global.currentEnemyBloodValue + injuryValue)>=100? 100: Global.currentEnemyBloodValue + injuryValue);
                        break;
                    default:
                        GameController.setBlood((Global.currentBloodValue + injuryValue) >= 100 ? 100 : Global.currentBloodValue + injuryValue);
                        break;
                }
            }

        }
        return true;
    }

    public static string LoadFile(string filePath)
    {
        string url = Application.streamingAssetsPath + "/" + filePath;

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            return File.ReadAllText(url, System.Text.Encoding.UTF8);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            WWW www = new WWW(url);
            while (!www.isDone) { }
            return Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3); 
        }
        return "";
    }

    EveryEvents GetEveryEvents()
    {
        string filepath = "json/Skills.json";
        strJson = LoadFile(filepath);
        //Debug.Log(strJson);
        testText.text = Application.platform.ToString();
        return JsonUtility.FromJson<EveryEvents>(strJson);
        

        //EveryEvents everyEvents = JsonUtility.FromJson<EveryEvents>(str);
        //Debug.Log(everyEvents.everyEvents);
        //Debug.Log("injuryValue = " + everyEvents.everyEvents[0].injuryValue);
    }
}
