using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button PositiveButton;
    public Button NegativeButton;
    private Text GameOverText;
    private GameObject GameOverCanvas;
    // Start is called before the first frame update
    void Start()
    {
        PositiveButton.onClick.AddListener(PositiveButtonEvent);
        NegativeButton.onClick.AddListener(NegativeButtonEvent);
        GameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
        GameOverCanvas = GameObject.Find("GameOverCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOverCanvas.GetComponent<Canvas>().enabled)
        {
            if (Global.currentBloodValue <= 0)
            {
                //if (Global.Debug) Debug.Log("Game Over,you lose");
                GameOverText.text = "You lose!";
            }
            if (Global.currentEnemyBloodValue <= 0)
            {
                GameOverText.text = "You win!";
                //if (Global.Debug) Debug.Log("Game Over,you win");
            }
        }
        
    }
    void PositiveButtonEvent()
    {
        GameOverCanvas.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Main");
    }

    void NegativeButtonEvent()
    {
        GameOverCanvas.GetComponent<Canvas>().enabled = false;
        SceneManager.LoadScene("Game");
    }
}
