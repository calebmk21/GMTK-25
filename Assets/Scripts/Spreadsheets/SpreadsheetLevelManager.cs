using UnityEngine; 
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using System.Collections;
using TMPro;

public class SpreadsheetLevelManager : MonoBehaviour
{
    // Assign these in the editor
    [SerializeField] private float gridSize;
    [SerializeField] private float levelDuration;
    public SpreadsheetPlayerController player;
    public string[] levels;
    public TileBase hasBeenTile;
    public LayerMask wallLayer;
    public TMP_Text timerText;
    public TMP_Text scoreText;

    // internal game tracking
    private GameObject currentLevel;
    private string currentLevelName; 
    private int score;
    private int prevScore;

    [SerializeField] public GameObject canvasUI;
    [SerializeField] public GameObject minigameSelect;
    [SerializeField] public GameObject bro;
    
    void Awake()
    {
        GameManager.OnMinigameSelect += GameManagerOnOnMinigameSelect;
        bro.SetActive(true);
        //GameObject canvasUI = GameObject.FindWithTag("SpreadsheetUI");
    }
    
    void OnDestroy()
    {
        GameManager.OnMinigameSelect -= GameManagerOnOnMinigameSelect;
    }

    private void GameManagerOnOnMinigameSelect(GameManager.MinigameState minigame)
    {
        if (minigame == GameManager.MinigameState.Spreadsheet)
        {
            StartCoroutine(RunLevels());
            canvasUI.SetActive(true);
        }
    }
    
    void Start()
    {
        // StartCoroutine(RunLevels());
    }

    IEnumerator RunLevels()
    {
        UpdateScore(0);
        prevScore = 0;
        float elapsedTime = 0f;
        // ensure player is loaded
        player.EnableInput(); 
        player.gameObject.SetActive(true);
        while (elapsedTime < levelDuration)
        {
            LoadLevel(levels[Random.Range(0, levels.Length)]);
            while (elapsedTime < levelDuration && !player.GetHasWon())
            {
                // track time
                timerText.text = $"Time Left: {(int)(levelDuration - elapsedTime)}";
                elapsedTime += Time.deltaTime;
                UpdateScore(player.GetScore());
                yield return null;
            }
            UnloadLevel();
            yield return null;
        }
        // ensure player is deloaded
        player.DisableInput(); 
        player.gameObject.SetActive(false);

        SpreadsheetMinigameEnd();

    }


    public void SpreadsheetMinigameEnd()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Workday);
        StopCoroutine(RunLevels());

        int adjustedGreedScore = 0;

        if (score < 25)
        {
            adjustedGreedScore = -1;
            GameManager.Instance.slothPoints++;
        }
        else if (score < 100)
        {
            adjustedGreedScore = 0;
        }
        else if (score < 130)
        {
            adjustedGreedScore = 1;
        }
        else if (score < 170)
        {
            adjustedGreedScore = 2;
        }
        else if (score < 200)
        {
            adjustedGreedScore = 3;
        }
        else
        {
            adjustedGreedScore = 4;
        }

        GameManager.Instance.greedPoints += adjustedGreedScore;
        minigameSelect.SetActive(true);
    }
    

    public int GetScore()
    {
        return score;
    }
    public void ResetLevel()
    {
        if (currentLevelName != null)
        {
            string levelName = currentLevelName;
            UpdateScore(prevScore);
            UnloadLevel();
            LoadLevel(levelName);   
        }
    }

    void LoadLevel(string level)
    {
        GameObject levelPrefab = Resources.Load<GameObject>("Spreadsheets/Levels/" + level);
        if (levelPrefab != null)
        {
            currentLevel = Instantiate(levelPrefab);
            currentLevelName = level; 
            Tilemap floor = currentLevel.transform.Find("Floor").GetComponent<Tilemap>();
            Tilemap walls = currentLevel.transform.Find("Walls").GetComponent<Tilemap>();
            player.InitializeLevel(floor, walls, hasBeenTile, wallLayer, gridSize);
            player.SetScore(score);
            player.PositionPlayer(GameObject.FindWithTag("Start").transform.position); 
        }
        else
        {
            Debug.LogError("Level prefab not found: " + level);
        }
    }

    void UnloadLevel()
    {
        prevScore = score;
        currentLevelName = ""; 
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }
    }
    
    void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text =  $"Score: {score}";
    }
     
}
