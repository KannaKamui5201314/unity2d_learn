using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMainScene : MonoBehaviour
{
    //KeyCode currentKey;
    private GameObject QuitGameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        //currentKey = KeyCode.Space;
        QuitGameCanvas = GameObject.Find("QuitGameCanvas");
        QuitGameCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            showQuitGameDialog();
        }
    }

    //void OnGUI()
    //{
    //    if (Input.anyKeyDown)
    //    {
    //        Event e = Event.current;
    //        if (e.isKey)
    //        {
    //            currentKey = e.keyCode;
    //            Debug.Log("Current Key is : " + currentKey.ToString());
    //        }
    //    }
    //}

    void showQuitGameDialog()
    {
        QuitGameCanvas.GetComponent<Canvas>().enabled = true;
    }
}
