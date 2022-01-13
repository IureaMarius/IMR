using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerObject;
    public List<GameObject> segmentPrefabs;
    public List<string> descriptions;
    public List<Color> UIColors;
    public List<int> prices;
    public List<string> names;
    public int currentMoney;
    public Text unitName;
    private int currentRound = -1;
    public List<SpawnerTask> tasks = new List<SpawnerTask>();
    public List<Material> boardMaterials;
    public bool playerWon = false;
    private PartyViewerScript party;



    public List<int> fundsPerRound = new List<int>();
    public List<int> levels = new List<int>();
    public List<int> counts = new List<int>();
    private List<Transform> bodyParts = new List<Transform>();
    public float minDistance = 0.0004f;
    public float speed = 0.02f;
    public float rotationSpeed = 20;
    public HoldableButton LeftButton, RightButton;
    public EnemySpawnerScript enemySpawner;
    public GameObject[] enemies;
    public GameObject menu;
    public Text gameOverTitle;
    public GameObject gameOverButton;
    public GameObject gameOverScreen;
    public List<Button> seeSegmentButtons = new List<Button>();
    private int selectedSegment = -1;
    public Text description;
    public List<GameObject> selectedPrefabs = new List<GameObject>();
    private List<int> selectedIndexes = new List<int>();
    private int selectedButton = -1;
    public UnityEvent boughtUnitEvent = new UnityEvent();
    private GameObject canvas;
    private MeshRenderer meshRenderer;
    void Start()
    {
        unitName = GameObject.Find("UnitName").GetComponent<Text>();
        bodyParts = new List<Transform>();
        party = GameObject.Find("PartyViewerContent").GetComponent<PartyViewerScript>();
        boughtUnitEvent.AddListener(party.RefreshDisplay);
        menu = GameObject.Find("Menu");
        description = GameObject.Find("UnitDescription").GetComponent<Text>();
        canvas = GameObject.Find("Canvas");
        gameOverButton = GameObject.Find("GameOverButton");
        gameOverButton.GetComponent<Button>().onClick.AddListener(RestartScene);
        gameOverScreen = GameObject.Find("GameOver");
        seeSegmentButtons = new List<Button>();
        seeSegmentButtons.Add(GameObject.Find("BuyUnitButton1").GetComponent<Button>());
        seeSegmentButtons.Add(GameObject.Find("BuyUnitButton2").GetComponent<Button>());
        seeSegmentButtons.Add(GameObject.Find("BuyUnitButton3").GetComponent<Button>());
        gameOverScreen.SetActive(false);
        canvas.SetActive(true);
        meshRenderer = GetComponent<MeshRenderer>();
        InitializeSpawnerTasks();
        LeftButton = GameObject.FindObjectsOfType<HoldableButton>()[1];
        RightButton = GameObject.FindObjectsOfType<HoldableButton>()[0];
        menu.SetActive(true);
        SetUIElements();
    }
    private void InitializeSpawnerTasks()
    {
        tasks.Add(new SpawnerTask()
        {
            enemiesToSpawn = 10,
            maxEnemiesPerWave = 2,
            minEnemiesPerWave = 1,
            objectsToSpawn = new Dictionary<GameObject, int> { { enemies[0], 200 }},
            spawnDelay = 2.5f
        });
        tasks.Add(new SpawnerTask()
        {
            enemiesToSpawn = 20,
            maxEnemiesPerWave = 3,
            minEnemiesPerWave = 2,
            objectsToSpawn = new Dictionary<GameObject, int> { { enemies[0], 200 }, { enemies[1], 50 }},
            spawnDelay = 2.5f
        });
        tasks.Add(new SpawnerTask()
        {
            enemiesToSpawn = 40,
            maxEnemiesPerWave = 5,
            minEnemiesPerWave = 3,
            objectsToSpawn = new Dictionary<GameObject, int> { { enemies[0], 200 }, { enemies[1], 50 }, { enemies[2], 50 } },
            spawnDelay = 2.5f
        });
        tasks.Add(new SpawnerTask()
        {
            enemiesToSpawn = 50,
            maxEnemiesPerWave = 5,
            minEnemiesPerWave = 3,
            objectsToSpawn = new Dictionary<GameObject, int> { { enemies[0], 100 }, { enemies[1], 50 }, { enemies[2], 50 } },
            spawnDelay = 2.5f
        });
        tasks.Add(new SpawnerTask()
        {
            enemiesToSpawn = 70,
            maxEnemiesPerWave = 7,
            minEnemiesPerWave = 4,
            objectsToSpawn = new Dictionary<GameObject, int> { { enemies[0], 50 }, { enemies[1], 50 }, { enemies[2], 50 } },
            spawnDelay = 2.5f
        });
        tasks.Add(new SpawnerTask()
        {
            enemiesToSpawn = 100,
            maxEnemiesPerWave = 10,
            minEnemiesPerWave = 3,
            objectsToSpawn = new Dictionary<GameObject, int> { { enemies[0], 50 }, { enemies[1], 50 }, { enemies[2], 50 } },
            spawnDelay = 2.5f
        });
        tasks.Add(new SpawnerTask()
        {
            enemiesToSpawn = 120,
            maxEnemiesPerWave = 15,
            minEnemiesPerWave = 3,
            objectsToSpawn = new Dictionary<GameObject, int> {{ enemies[1], 100 }, { enemies[2], 50 } },
            spawnDelay = 2.5f
        });
        tasks.Add(new SpawnerTask()
        {
            enemiesToSpawn = 220,
            maxEnemiesPerWave = 20,
            minEnemiesPerWave = 3,
            objectsToSpawn = new Dictionary<GameObject, int> { { enemies[0], 200 }, { enemies[1], 50 }, { enemies[2], 50 } },
            spawnDelay = 2.5f
        });
    }
    public void SetUIElements()
    {
        currentRound++;
        if (currentRound > fundsPerRound.Count)
        {
            playerWon = true;
            GameOver();
        }
        DeleteAllSegments();
        LeftButton.gameObject.SetActive(false);
        RightButton.gameObject.SetActive(false);
        menu.SetActive(true);
        currentMoney = fundsPerRound[currentRound];
        Roll();
    }
    public void Roll()
    {
        if (currentMoney < 2)
            return;
        foreach(Button button in seeSegmentButtons)
        {
            button.gameObject.SetActive(true);
        }
        currentMoney -= 2;
        int weightSum = 0;
        foreach (int count in counts)
            weightSum += count;

        foreach(var button in seeSegmentButtons)
        {
            int result = Random.Range(0, weightSum);
            int tempSum = 0;
            int random;
            for(random = 0; random < counts.Count; random++)
            {
                tempSum += counts[random];
                if (result < tempSum)
                    break;

            }
            button.image.color = UIColors[random];
            button.GetComponent<SeeSegmentDetailsButtonScript>().name.text = names[random];
            button.GetComponent<SeeSegmentDetailsButtonScript>().price.text = prices[random] + "G";
            button.GetComponent<SeeSegmentDetailsButtonScript>().index = random;
        }
    }
    public void SelectSegment(int index)
    {
        selectedSegment = index;
        description.text = descriptions[index];
        unitName.text = names[index];
    }
    public void SelectButton(int buttonIndex)
    {
        selectedButton = buttonIndex;
    }
    public void BuySegment()
    {
        if(selectedSegment != -1 && prices[selectedSegment] <= currentMoney)
        {
            selectedIndexes.Add(selectedSegment);
            currentMoney -= prices[selectedSegment];
            counts[selectedSegment]--;
            selectedSegment = -1;
            seeSegmentButtons[selectedButton].gameObject.SetActive(false);
            selectedButton = -1;
            CalculateLevels();
            boughtUnitEvent.Invoke();
        }
    }

    public void CalculateLevels()
    {

        for(int i = 0; i < levels.Count; i++)
            levels[i] = 0;
        foreach(int index in selectedIndexes)
        {
            levels[index]++;
        }
    }
    private void SelectSegments()
    {
        selectedPrefabs = new List<GameObject>();
        for(int i = 0; i < levels.Count; i++)
        {
            if (levels[i] > 0)
                selectedPrefabs.Add(segmentPrefabs[i]);
        }

    }
    private void DeleteAllSegments()
    {
        foreach(var bodyPart in bodyParts)
        {
            Destroy(bodyPart.gameObject);
        }
        bodyParts = new List<Transform>();
    }
    public void StartRound()
    {
        LeftButton.gameObject.SetActive(true);
        RightButton.gameObject.SetActive(true);
        SelectSegments();
        CreateSnakeSegments();
        meshRenderer.material = boardMaterials[currentRound];
        enemySpawner.AddSpawnerTask(tasks[currentRound]);
        menu.SetActive(false);
    }
   
    public void RemoveBodyPart(GameObject bodyPart)
    {
        bodyParts.Remove(bodyPart.transform);
    }

    private void CreateSnakeSegments()
    {
        for (int i = 0; i < selectedPrefabs.Count; i++)
        {
            var newObject = Instantiate(selectedPrefabs[i], PlayerObject.transform);
            newObject.GetComponent<BodySegmentScript>().SetLevel(Utils.GetLevel(levels[segmentPrefabs.IndexOf(selectedPrefabs[i])]));
            bodyParts.Add(newObject.transform);
        }
    }
    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        if (playerWon == true)
        {
            gameOverTitle.text = "You Won!";
            gameOverButton.GetComponentInChildren<Text>().text = "Play Again";
        }else
        {
            gameOverTitle.text = "Game Over!";
            gameOverButton.GetComponentInChildren<Text>().text = "Try Again";
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (bodyParts?.Count <= 0 && menu?.activeInHierarchy == false)
            GameOver();
        if (bodyParts?.Count > 0)
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
    public List<Transform> GetBodyParts()
    {
        return bodyParts;
    }
    private void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
