using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerObject;
    public GameObject segmentPrefab;
    private List<Transform> bodyParts;
    public int numberOfSegments = 5;
    public float minDistance = 0.0004f;
    public float speed = 0.02f;
    public float rotationSpeed = 10;
    public HoldableButton LeftButton, RightButton;
    public EnemySpawnerScript enemySpawner;
    public GameObject[] enemies;
    void Start()
    {
        bodyParts = new List<Transform>();
        LeftButton = GameObject.FindObjectsOfType<HoldableButton>()[1];
        RightButton = GameObject.FindObjectsOfType<HoldableButton>()[0];
        CreateSnakeSegments();
        SpawnerTask spawnerTask = new SpawnerTask()
        {
            enemiesToSpawn = 10,
            maxEnemiesPerWave = 1,
            minEnemiesPerWave = 1,
            objectsToSpawn = new Dictionary<GameObject, int> { { enemies[0], 5 }, {enemies[1], 5 } },
            spawnDelay = 1
        };
        enemySpawner.AddSpawnerTask(spawnerTask);
    }
    public void RemoveBodyPart(GameObject bodyPart)
    {
        bodyParts.Remove(bodyPart.transform);
    }

    private void CreateSnakeSegments()
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            var newObject = Instantiate(segmentPrefab, PlayerObject.transform);
            bodyParts.Add(newObject.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (bodyParts.Count > 0)
        {
            bodyParts[0].Translate(Vector3.forward * speed * Time.smoothDeltaTime, Space.Self);
            if (LeftButton.isHeld)
                bodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * -1);
            else if (RightButton.isHeld)
                bodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * 1);
            for (var i = 1; i < bodyParts.Count; i++)
            {
                var currentBodyPart = bodyParts[i];
                var prevBodyPart = bodyParts[i - 1];
                var distance = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);
                Vector3 newPosition = prevBodyPart.position;
                newPosition.y = bodyParts[0].position.y;

                float T = Time.deltaTime * distance / minDistance * speed;
                if (T > 0.5f)
                    T = 0.5f;

                currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosition, T);
                currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, T);

            }
        }
    }
}
