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
    public void RouteSelector()
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
            int maxCounts = 0;
            
            
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == max)
                {
                    maxCounts++;
                }
            }

            if (maxCounts > 1)
            {
                dupe = true;
            }
            
            return dupe;
        }
        int RouteToInt(RouteState route)
        {
            switch (route)
            {
                case RouteState.Greed:
                    return 0;
                case RouteState.Sloth:
                    return 1;
                case RouteState.Pride:
                    return 2;
                case RouteState.Wrath:
                    return 3;
                case RouteState.Gluttony:
                    return 4;
                case RouteState.Envy:
                    return 5;
                case RouteState.Lust:
                    return 6;
                case RouteState.Indecisive:
                    return 7;
                default:
                    throw new ArgumentOutOfRangeException(nameof(route), route, null);
            }
        }
        RouteState IntToRoute(int i)
        {
            switch (i)
            {
                case 7:
                    return RouteState.Indecisive;
                case 0:
                    return RouteState.Greed;
                case 1:
                    return RouteState.Sloth;
                case 2:
                    return RouteState.Pride;
                case 3:
                    return RouteState.Wrath;
                case 4:
                    return RouteState.Gluttony;
                case 5:
                    return RouteState.Envy;
                case 6:
                    return RouteState.Lust;
                default:
                    throw new ArgumentOutOfRangeException(nameof(i), i, null);
            }
        }
        int MaxElementIndex(int[] arr)
        {
            int ind = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > arr[ind])
                {
                    ind = i;
                }
            }
            return ind;
        }

        //Debug.Log("Route Array: " + routeArray[0] + routeArray[1] + routeArray[2] + routeArray[3] + routeArray[4] + routeArray[5] + routeArray[6]);
        
        // Gets max value from the array
        int max = MaxValue(routeArray);
        bool dupe = MaxDuplicate(routeArray, max);
        int routeInt = RouteToInt(Route);

        Debug.Log("Max Value in Array: " + max);
        Debug.Log("Is the Max Value Duplicate? " + dupe);
        Debug.Log("Route Int: " + routeInt);
        
        RouteState newRoute = Route;

        // if currently in indecision route, routeInt is out of scope for routeArray;
        // this case handles that first
        if (routeInt == 7)
        {
            Debug.Log("Entered 1");
            if (dupe)
            {
                Debug.Log("Entered 1.1");
                newRoute = RouteState.Indecisive;
            }
            else
            {
                Debug.Log("Entered 1.2");
                int ind = MaxElementIndex(routeArray);
                Debug.Log("Index: " + ind);
                newRoute = IntToRoute(ind);
            }
        }
        
        // Ties in points go to the current route
        else if (dupe && routeArray[routeInt] == max)
        {
            Debug.Log("Entered 2");
            newRoute = Route;
        }
        
        // Unless you tied two new routes at the same time
        else if (dupe && routeArray[routeInt] != max)
        {
            Debug.Log("Entered 3");
            newRoute = RouteState.Indecisive;
        }
        else
        {
            Debug.Log("Entered 4");
            int ind = MaxElementIndex(routeArray);
            newRoute = IntToRoute(ind);
        }
        
        RouteChange(newRoute);
        
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
            case GameState.Evening:
                HandleEvening();
                break;
            case GameState.Ending:
                // Selects and ending based on final route
                HandleEnding(Route);
                break;
            case GameState.Tutorial:
                HandleTutorial();
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
        RouteSelector();
        // we should add more things to the morning besides it being a transition. 
        // probably dialogue sequences
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

    public void HandleEvening()
    {
        
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

    public void HandleTutorial()
    {
        
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
            case MinigameState.Match:
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
        Evening,
        Ending,
        Tutorial
    }

    public enum MinigameState
    {
        Sleep,
        Spreadsheet,
        Match
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
