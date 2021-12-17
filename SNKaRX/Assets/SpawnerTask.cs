using System.Collections.Generic;
using UnityEngine;

public class SpawnerTask
{
    public int enemiesToSpawn { get; set; }
    public int minEnemiesPerWave { get; set; }
    public int maxEnemiesPerWave { get; set; }
    public int spawnDelay { get; set; }
    public Dictionary<GameObject, int> objectsToSpawn { get; set; }
}