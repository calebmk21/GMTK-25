using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

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

        Debug.Log("Pause");
    }
    
    public void resumeGame(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        
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
}
