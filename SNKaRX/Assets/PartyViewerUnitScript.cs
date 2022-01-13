using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyViewerUnitScript : MonoBehaviour
{
    public string name;
    public int level;
    public float fillLevel;
    public Slider slider;
    public Text nameText;
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetData(string name, int setLevel)
    {
        name = name;
        level = Utils.GetLevel(setLevel);
        switch(level)
        {
            case 1:
                fillLevel = (float)setLevel / 3;
                break;
            case 2:
                fillLevel = (float)(setLevel - 3) / 6;
                break;
            case 3:
                fillLevel = 1;
                break;

        }
        slider.value = fillLevel;
        nameText.text = name;
        levelText.text = "Lvl. " + level;
    }
}
