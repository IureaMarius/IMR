using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeeSegmentDetailsButtonScript : MonoBehaviour
{
    public int buttonIndex = -1;
    public int index;
    public Text name;
    public Text price;
    private MainGameLoop game;
    private void Start()
    {

        GetComponent<Button>().onClick.AddListener(SetDescription);
    }
    private void SetDescription()
    {
        if(game == null)
            game = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameLoop>();
        game.SelectSegment(index);
        game.SelectButton(buttonIndex);
    }
}
