using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuitGameDialog : MonoBehaviour
{

    public Button PositiveButton;
    public Button NegativeButton;
    private GameObject QuitGameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        

        PositiveButton.GetComponent<Button>().onClick.AddListener(PositiveButtonEvent);
        NegativeButton.GetComponent<Button>().onClick.AddListener(NegativeButtonEvent);
        QuitGameCanvas = GameObject.Find("QuitGameCanvas");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PositiveButtonEvent()
    {
        if (SceneManager.GetActiveScene().name.Equals("Game"))
        {
            SceneManager.LoadScene("Main");
        }
        if (SceneManager.GetActiveScene().name.Equals("Main"))
        {
            Application.Quit();
        }
    }

    void NegativeButtonEvent()
    {
        QuitGameCanvas.GetComponent<Canvas>().enabled = false;
    }
}
