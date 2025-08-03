using System;
using UnityEngine;

public class GreedEnding : MonoBehaviour
{

    [SerializeField] public GameObject greedPanel;
    
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Ending && GameManager.Instance.Route == GameManager.RouteState.Greed)
        {
            greedPanel.SetActive(true);
        }
    }
    
    public void PanelToggle(GameObject panel)
    {
        bool isActive = panel.activeSelf;
        panel.SetActive(!isActive);
    }
    
    
    public void OnlySpreadsheets()
    {
        GameManager.Instance.MinigameSelection(GameManager.MinigameState.Spreadsheet);
        PanelToggle(greedPanel);
    }
    
}
