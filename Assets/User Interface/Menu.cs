using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public static bool isPaused = false;
    
    [SerializeField] private GameObject systemSettings;
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject pauseMenuPanel;

    
    
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    
    public void pauseGame(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

        // Maybe need to include other stuff to be found in the GameManager later 
        // but also maybe this suffices 
        
        isPaused = true;
        Debug.Log("Pause");
    }
    
    public void resumeGame(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        
        isPaused = false;
        Debug.Log("Unpause");
    }

    public void panelSwap(GameObject panelA, GameObject panelB)
    {
        panelToggle(panelA);
        panelToggle(panelB);
    }

    public void panelToggle(GameObject panel)
    {
        bool isActive = panel.activeSelf;
        panel.SetActive(!isActive);
    }

    public void sceneSwap(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void playSleepMinigame(GameObject panel)
    {
        
        GameManager.Instance.MinigameSelection(GameManager.MinigameState.Sleep);
        GameManager.Instance.UpdateGameState(GameManager.GameState.Minigame);
        panelToggle(panel);
    }
    
    public void playSpreadsheetMinigame(GameObject panel)
    {
        
        GameManager.Instance.MinigameSelection(GameManager.MinigameState.Spreadsheet);
        GameManager.Instance.UpdateGameState(GameManager.GameState.Minigame);
        panelToggle(panel);
    }
    
    public void playEmailMinigame(GameObject panel)
    {
        
        GameManager.Instance.MinigameSelection(GameManager.MinigameState.Match);
        GameManager.Instance.UpdateGameState(GameManager.GameState.Minigame);
        panelToggle(panel);
    }
    
    
    // Debug Functions
    public void SkipToMorning()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Morning);
    }

    public void SkipToWorkday()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Workday);
    }
    
    public void SkipToEnding()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Ending);
    }

    public void SkipDay()
    {
        GameManager.Instance.dayNumber++;
    }

    public void GameManagerSummary()
    {
        Debug.Log("Greed Points: " + GameManager.Instance.greedPoints);
        Debug.Log("Sloth Points: " + GameManager.Instance.slothPoints);
        Debug.Log("Pride Points: " + GameManager.Instance.pridePoints);
        Debug.Log("Wrath Points: " + GameManager.Instance.wrathPoints);
        Debug.Log("Gluttony Points: " + GameManager.Instance.gluttonyPoints);
        Debug.Log("Envy Points: " + GameManager.Instance.envyPoints);
        Debug.Log("Lust Points: " + GameManager.Instance.lustPoints);
        
        
        Debug.Log("Current GameState: " + GameManager.Instance.State);
        Debug.Log("Current  Route: " + GameManager.Instance.Route);
    }

    public void AddGreedPoints()
    {
        GameManager.Instance.greedPoints++;
    }
    public void AddSlothPoints()
    {
        GameManager.Instance.slothPoints++;
    }
    public void AddPridePoints()
    {
        GameManager.Instance.pridePoints++;
    }
}
