using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDisable()
    {
        GameObject.Destroy(this.gameObject);
    }
}
