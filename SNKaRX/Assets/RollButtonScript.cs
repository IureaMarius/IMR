using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollButtonScript : MonoBehaviour
{
    public MainGameLoop game;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Roll()
    {
        if(game == null)
            game = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameLoop>();
        game.Roll();
    }

}
