using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Demo_GM : MonoBehaviour {

    Color myUpColor = new Color32(255, 255, 255, 255);
    Color myDownColor = new Color32(180, 180, 180, 255);

    public static Demo_GM Gm;

    public Image[] UIImage;

    // Use this for initialization
    void Awake () {
        Screen.fullScreen = false;

        Gm = this;
    }
	
	// Update is called once per frame
	void Update () {

        KeyUPDownchange();


    }


    void InitColor()
    {

        for (int i = 0; i < UIImage.Length; i++)
        {
            UIImage[i].color = new Color(255, 255, 255);


        }

    }

    public void KeyUPDownchange()
    {
        // wsad
        if (Input.GetKeyUp(KeyCode.A))
        {
            Demo_GM.Gm.UIImage[2].color = myUpColor;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            Demo_GM.Gm.UIImage[3].color = myUpColor;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Demo_GM.Gm.UIImage[0].color = myUpColor;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Demo_GM.Gm.UIImage[1].color = myUpColor;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Demo_GM.Gm.UIImage[2].color = myUpColor;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Demo_GM.Gm.UIImage[3].color = myUpColor;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Demo_GM.Gm.UIImage[0].color = myUpColor;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Demo_GM.Gm.UIImage[1].color = myUpColor;
        }
        ///
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Demo_GM.Gm.UIImage[4].color = myUpColor;
        }
        
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Demo_GM.Gm.UIImage[5].color = myUpColor;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Demo_GM.Gm.UIImage[4].color = myDownColor;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Demo_GM.Gm.UIImage[5].color = myDownColor;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Demo_GM.Gm.UIImage[6].color = myDownColor;
        }
    
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Demo_GM.Gm.UIImage[6].color = myUpColor;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Demo_GM.Gm.UIImage[7].color = myDownColor;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Demo_GM.Gm.UIImage[7].color = myUpColor;
        }

    }

}
