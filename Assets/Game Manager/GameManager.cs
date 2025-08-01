using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Allows this to be referenced elsewhere
    public static GameManager Instance;

    // Length in seconds of the work day
    /*
     QUESTION: Should this be the timer to make a decision or the total timed workday
     i.e. you have workdayLength seconds to complete your task OR the minigame and you can
     flip between tasks 
     ALTERNATIVELY we can have this be the timer to make a decision and you get locked into said
     decision and must finish it (to success or fail depending on performance), then it moves to the next segment
     */
    [SerializeField] public float workdayLength = 120f;
    
    // Number of workdays playable
    [SerializeField] public int maxWorkdays = 10;

    // Day number
    [SerializeField] int dayNumber;
    
    // Action counter
    [SerializeField] int actionsRemaining;

    [SerializeField] public GameObject minigamePanel; 
    
    // Route Points
    public int greedPoints;
    public int slothPoints;
    public int pridePoints;
    public int wrathPoints;
    public int gluttonyPoints;
    public int envyPoints;
    public int lustPoints;
    
    
    // Game State variables
    public GameState State;
    public RouteState Route;
    public MinigameState Minigame;


    public static event Action<GameState> OnGameStateChanged;
    public static event Action<RouteState> OnRouteStateChanged;
    public static event Action<MinigameState> OnMinigameSelect;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.Morning);
        Debug.Log("Actions Remaining: " + actionsRemaining);
    }

    // Handles logic for which route you're changing to
    // TODO
    public void RouteSelector(int previousRoute)
    {
        int[] routeArray = new int[7]
            { greedPoints, slothPoints, pridePoints, wrathPoints, gluttonyPoints, envyPoints, lustPoints };
        
        // Quick Helper functions
        int MaxValue(int[] arr)
        {
            int max = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }

            return max;
        }
        bool MaxDuplicate(int[] arr, int max)
        {
            bool dupe = false;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == max)
                {
                    dupe = true;
                }
            }

            return dupe;
        }
        
        // Gets max value from the array
        int max = MaxValue(routeArray);
        bool dupe = MaxDuplicate(routeArray, max);

        if (dupe)
        {
            
        }
    }
    
    // Handles what happens when you change routes

    public void RouteChange(RouteState newRoute)
    {
        Route = newRoute;
        switch (newRoute)
        {
            case RouteState.Indecisive:
                break;
            case RouteState.Greed:
                break;
            case RouteState.Sloth:
                break;
            case RouteState.Pride:
                break;
            case RouteState.Wrath:
                break;
            case RouteState.Gluttony:
                break;
            case RouteState.Envy:
                break;
            case RouteState.Lust:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newRoute), newRoute, null);
        }
        
        OnRouteStateChanged?.Invoke(newRoute);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Morning:
                // Actions resets only when morning is called
                HandleMorning();
                break;
            case GameState.Workday:
                HandleWorkday();
                break;
            case GameState.Minigame:
                // Selecting a minigame decrements the action counter
                HandleMinigame();
                break;
            case GameState.Ending:
                // Selects and ending based on final route
                HandleEnding(Route);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnGameStateChanged?.Invoke(newState);
    }
    
    // Game State Functions

    public void HandleMorning()
    {
        actionsRemaining = 3;
        dayNumber++;
    }

    public void HandleWorkday()
    {
        Debug.Log("Number of Actions: " + actionsRemaining);
        Debug.Log("Currently on Day: "+ dayNumber);
        
        if (dayNumber == maxWorkdays)
        {
            UpdateGameState(GameState.Ending);
        }
        else if (actionsRemaining == 0)
        {
            UpdateGameState(GameState.Morning);
        }
        else
        {
            minigamePanel.SetActive(true);
        }
        
    }

    public void HandleMinigame()
    {
        actionsRemaining--;
    }

    public void HandleEnding(RouteState finalRoute)
    {
        switch (finalRoute)
        {
            case RouteState.Indecisive:
                break;
            case RouteState.Greed:
                break;
            case RouteState.Sloth:
                break;
            case RouteState.Pride:
                break;
            case RouteState.Wrath:
                break;
            case RouteState.Gluttony:
                break;
            case RouteState.Envy:
                break;
            case RouteState.Lust:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(finalRoute), finalRoute, null);
        }
    }

    public void MinigameSelection(MinigameState minigame)
    {
        Minigame = minigame;
        switch (minigame)
        {
            case MinigameState.Sleep:
                slothPoints++;
                Debug.Log("Sloth Points: " + slothPoints);
                break;
            case MinigameState.Spreadsheet:
                greedPoints++;
                Debug.Log("Greed Points: " + greedPoints);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(minigame), minigame, null);
        }
        
        OnMinigameSelect?.Invoke(minigame);
    }
    
    public enum GameState
    {
        Morning,
        Workday,
        Minigame,
        Ending
    }

    public enum MinigameState
    {
        Sleep,
        Spreadsheet
    }

    public enum RouteState
    {
        Indecisive,
        Greed,
        Sloth,
        Pride,
        Wrath,
        Gluttony,
        Envy,
        Lust
    }
    
}
