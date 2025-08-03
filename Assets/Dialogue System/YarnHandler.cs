using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class YarnHandler : MonoBehaviour
{

    public DialogueRunner dialogueRunner;

    public GameObject workstation;
    
    void Awake()
    {
        dialogueRunner.AddCommandHandler(
            "var_to_yarn",
            SendVariablesToYarn);
        dialogueRunner.AddCommandHandler(
            "to_workday",
            ToWorkday
            );
        dialogueRunner.AddCommandHandler(
            "to_morning",
            ToMorning
        );
        dialogueRunner.AddCommandHandler(
            "to_evening",
            ToEvening
            );
        dialogueRunner.AddCommandHandler(
            "to_game",
            ToMainGame);
        dialogueRunner.AddCommandHandler(
            "open_workstation",
            OpenWorkstation);
        dialogueRunner.AddCommandHandler(
            "close_workstation",
            CloseWorkstation);
        dialogueRunner.AddCommandHandler(
            "greed_ending",
            SetGreedEnding);
        
    }
    
    // Yarn Commands

    public void SetGreedEnding()
    {
        GameManager.Instance.SetGreedEndingStatusWithoutTheBS();
    }
    
    public void CloseWorkstation()
    {
        workstation.SetActive(false);
        Debug.Log("Closed Workstation");
    }
    public void OpenWorkstation()
    {
        workstation.SetActive(true);
    }

    public void ToMainGame()
    {
        // may change if necessary
        SceneManager.LoadScene("GameManagerTest");
    }
    
    public void SendVariablesToYarn()
    {
        GameManager.Instance.SendToYarn();
    }

    public void ToWorkday()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Workday);
    }

    public void ToMorning()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Morning);
    }

    public void ToEvening()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Evening);
    }

    public void PlayNewNode(string nodeName)
    {
        dialogueRunner.StartDialogue(nodeName);
    }
    
    
    
}
