using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodySegmentDescription
{
    public GameObject preFab { get; set; }
    public string description { get; set; }
    public Color color { get; set; }
    public int price { get; set; }
}
