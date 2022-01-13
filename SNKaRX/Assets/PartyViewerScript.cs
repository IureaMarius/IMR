using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyViewerScript : MonoBehaviour
{
    private MainGameLoop game;
    private List<GameObject> units = new List<GameObject>();
    public GameObject unitPrefab;
    private void Start()
    {

        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameLoop>();
        game.boughtUnitEvent.AddListener(RefreshDisplay);
    }
    public void RefreshDisplay()
    {
        DeleteOldUnits();
        for(int i = 0; i < game.levels.Count; i++)
        {
            if (game.levels[i] <= 0)
                continue;

            GameObject x = Instantiate(unitPrefab, transform);
            x.GetComponent<PartyViewerUnitScript>().SetData(game.names[i], game.levels[i]);
            units.Add(x);
        }

    }
    private void DeleteOldUnits()
    {
        foreach (GameObject unit in units)
            Destroy(unit);
    }
}
