using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public List<SpawnerTask> Tasks = new List<SpawnerTask>();
    private SpawnerTask currentTask;
    private float currentCountdown = 0;
    public MeshRenderer gameBoardMesh;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private MainGameLoop game;
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainGameLoop>();
        
    }
    public void AddSpawnerTask(SpawnerTask task)
    {
        Tasks.Add(task);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTask == null)
        {
            if(Tasks.Count != 0)
            {
                currentTask = Tasks[0];
                Tasks.RemoveAt(0);
            }

        }
        if(currentTask != null)
        {
            if(currentCountdown < 0)
                SpawnEnemies(currentTask); 
            else
            {
                currentCountdown -= Time.deltaTime;
            }
        }
        
    }
    private void SpawnEnemies(SpawnerTask task)
    {
        int nrOfEnemies = Random.Range(currentTask.minEnemiesPerWave, currentTask.maxEnemiesPerWave);
        if (nrOfEnemies > task.enemiesToSpawn)
            nrOfEnemies = task.enemiesToSpawn;
        currentCountdown = task.spawnDelay;

        while(nrOfEnemies > 0)
        {

            var bounds = gameBoardMesh.bounds;
            GameObject objectToSpawn = GetRandomEnemy(task);
            task.objectsToSpawn[objectToSpawn]--;
            var newEnemy = Instantiate(objectToSpawn);
            newEnemy.transform.position = bounds.center + new Vector3(Random.Range(-bounds.extents.x / 2, bounds.extents.x / 2), objectToSpawn.transform.lossyScale.y / 2, Random.Range(-bounds.extents.z / 2, bounds.extents.z / 2));
            newEnemy.GetComponent<BaseEnemyScript>().enemySpawner = this;
            spawnedEnemies.Add(newEnemy);
            nrOfEnemies--;
            task.enemiesToSpawn--;
        }
        if (task.enemiesToSpawn <= 0)
            currentTask = null;

    }
    private GameObject GetRandomEnemy(SpawnerTask task)
    {
        List<GameObject> gameObjects = new List<GameObject>(task.objectsToSpawn.Keys);
        gameObjects.Where(x => task.objectsToSpawn[x] > 0);
        return gameObjects[Random.Range(0, gameObjects.Count)];
    }
    public void EnemyKilled(GameObject obj)
    {
        if (spawnedEnemies.Contains(obj))
        {
            spawnedEnemies.Remove(obj);
        }
        if (spawnedEnemies.Count == 0 && currentTask == null)
            game.SetUIElements();
            

    }
}
