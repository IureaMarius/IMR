using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public MainGameLoop game;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartRound()
    {
        if(game == null)
            game = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameLoop>();
        game.StartRound();
    }
}
