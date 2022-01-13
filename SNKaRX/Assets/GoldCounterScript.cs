using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCounterScript : MonoBehaviour
{
    public MainGameLoop game;
    private int goldCounter;
    private void Start()
    {
        
    }
    void Update()
    {
        if(game == null)
        {
            game = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<MainGameLoop>();
        }
        
        goldCounter = game.currentMoney;
        GetComponent<Text>().text = "Gold: " + goldCounter;
    }
}
