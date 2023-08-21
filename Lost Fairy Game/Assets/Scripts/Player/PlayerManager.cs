using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool gameStart;
    public SwipeManager swipeControls;
    public GameObject gameOverPanel;
    public GameObject gameStartPanel;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        gameOver = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            gameOverPanel.SetActive(false);
        }
        if (swipeControls.tap)
        {
            
            gameStart = true;
            //gameStartPanel.SetActive(false);
            Destroy(gameStartPanel);
        }


       

    }
    
}