using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnerTask
{
    public int enemiesToSpawn { get; set; }
    public int minEnemiesPerWave { get; set; }
    public int maxEnemiesPerWave { get; set; }
    public float spawnDelay { get; set; }
    public Dictionary<GameObject, int> objectsToSpawn { get; set; }
}