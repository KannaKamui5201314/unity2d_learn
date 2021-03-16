using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EveryEventController : MonoBehaviour
{
    private EveryEvent everyEvent;
    // Start is called before the first frame update
    void Start()
    {
        string name = GetComponent<SpriteRenderer>().sprite.name;
        //Debug.Log("name = " + name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DestroyEvent") 
        {
            GameObject.Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Global.EveryEventGameObjectsList.Add(this.GetComponent<Collider2D>());
        }
        if (other.gameObject.tag == "EnemyPlayer")
        {
            Global.EnemyEveryEventGameObjectsList.Add(this.GetComponent<Collider2D>());
        }

        //Debug.Log(other.name);
    }

    public void setEveryEvent(EveryEvent tempEveryEvent)
    {
        everyEvent = tempEveryEvent;
    }

    public EveryEvent getEveryEvent()
    {
        return everyEvent;
    }
}


[System.Serializable]
public class EveryEvent 
{
    public string id;
    public string skillname;
    public string race;
    public string skill;
    public int injuryValue;

    public EveryEvent(string race, string skill) {
        this.race = race;
        this.skill = skill;
    }

}

public class EveryEvents
{
    public EveryEvent[] everyEvents;
}
