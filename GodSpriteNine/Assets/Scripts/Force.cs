using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Force : MonoBehaviour
{
    private float force_x = 3;
    private float force_y = 25;
    private float time;
    private float limitTime = 0.15f;
    private Text blood;

    // Start is called before the first frame update
    void Start()
    {
        blood = GetComponentInChildren<Text>();
        blood.color = new Color(blood.color.r, blood.color.g, blood.color.b, 0);
    }
    void FixedUpdate() 
    {
        if (time < limitTime)
        {
            time += Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(force_x, force_y));
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(blood.color.a);
        
        if (blood.color.a <= 1)
        {
            blood.color = new Color(blood.color.r, blood.color.g, blood.color.b, blood.color.a + Time.deltaTime);
        }
        if (blood.color.a >= 1)
        {
            Destroy(this.gameObject);
        }
    }
}
