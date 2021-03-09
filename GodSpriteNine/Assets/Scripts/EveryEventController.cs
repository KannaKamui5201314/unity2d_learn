using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryEventController : MonoBehaviour
{
    
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
            Global.EveryEventGameObjectsList.Add(other);
        }
        if (other.gameObject.tag == "EnemyPlayer")
        {
            Global.EnemyEveryEventGameObjectsList.Add(other);
        }



        //Debug.Log(other.name);
    }
}



public class EveryEvent 
{
    public int id;
    public int player;
    public int race;
    public int skill;

    public EveryEvent(int id, int player, int race, int skill) {
        this.id = id;
        this.player = player;
        this.race = race;
        this.skill = skill;
    }
}
