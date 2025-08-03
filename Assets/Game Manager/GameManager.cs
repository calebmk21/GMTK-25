using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Yarn.Unity;
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
    //[SerializeField] public float workdayLength = 120f;
    
    // Number of workdays playable
    [SerializeField] public int maxWorkdays = 8;

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

    public string routeString; 

    // Events
    public static event Action<GameState> OnGameStateChanged;
    public static event Action<RouteState> OnRouteStateChanged;
    public static event Action<MinigameState> OnMinigameSelect;
    public static event Action OnDay1;
    public static event Action OnDay2;
    public static event Action OnSnooze;
    
    void Awake()
    {
        Instance = this;
    }

    // interfacing with yarn variables
    private InMemoryVariableStorage variableStorage;
    
    void Start()
    {
        UpdateGameState(GameState.Tutorial);
        variableStorage = FindFirstObjectByType<InMemoryVariableStorage>();
        //Debug.Log("Actions Remaining: " + actionsRemaining);
    }

    // Handles logic for which route you're changing to
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

        //.Log("Max Value in Array: " + max);
        //Debug.Log("Is the Max Value Duplicate? " + dupe);
        //Debug.Log("Route Int: " + routeInt);
        
        RouteState newRoute = Route;

        // if currently in indecision route, routeInt is out of scope for routeArray;
        // this case handles that first
        if (routeInt == 7)
        {
            //Debug.Log("Entered 1");
            if (dupe)
            {
                //Debug.Log("Entered 1.1");
                newRoute = RouteState.Indecisive;
            }
            else
            {
                //Debug.Log("Entered 1.2");
                int ind = MaxElementIndex(routeArray);
                //Debug.Log("Index: " + ind);
                newRoute = IntToRoute(ind);
            }
        }
        
        // Ties in points go to the current route
        else if (dupe && routeArray[routeInt] == max)
        {
            //Debug.Log("Entered 2");
            newRoute = Route;
        }
        
        // Unless you tied two new routes at the same time
        else if (dupe && routeArray[routeInt] != max)
        {
            //Debug.Log("Entered 3");
            newRoute = RouteState.Indecisive;
        }
        else
        {
            //Debug.Log("Entered 4");
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
                // Defaults to Greed on Day 1
                if (dayNumber == 1)
                {
                    RouteChange(RouteState.Greed);
                }
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
        // Increments day counter
        ++dayNumber;
        
        // Unique events for day 1 and day 2
        // Day 1 only gets one action
        if (dayNumber == 1)
        {
            OnDay1?.Invoke();
            actionsRemaining = 1;
        }
        else if (dayNumber == 2)
        {
            OnDay2?.Invoke();
            actionsRemaining = 3;
        }
        
        // Begins ending if you reached the final day
        else if (dayNumber >= maxWorkdays)
        {
            UpdateGameState(GameState.Ending);
        }
        else
        {
            actionsRemaining = 3;
        }
        

    }

    public void HandleWorkday()
    {
        Debug.Log("Number of Actions: " + actionsRemaining);
        Debug.Log("Currently on Day: "+ dayNumber);
        
        if (dayNumber >= maxWorkdays)
        {
            UpdateGameState(GameState.Ending);
        }
        else if (actionsRemaining == 0)
        {
            // Change to evening later if need be
            UpdateGameState(GameState.Evening);
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
        // gives Yarnspinner the values at the end of the day
        variableStorage.SetValue("$greed_points", greedPoints);
        variableStorage.SetValue("$sloth_points", slothPoints);
        variableStorage.SetValue("$pride_points", pridePoints);
        variableStorage.SetValue("$envy_points", envyPoints);
        
        UpdateGameState(GameState.Morning);
        RouteSelector();
        routeString = RouteStateToString(Route);
        
        variableStorage.SetValue("$route", routeString);

    }
    
    public void HandleEnding(RouteState finalRoute)
    {
        switch (finalRoute)
        {
            case RouteState.Indecisive:
                break;
            case RouteState.Greed:
                actionsRemaining = 1000;
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

    // This should only be called if the player wishes to replay the tutorial
    public void HandleTutorial()
    {
        
    }

    public string GameStateToString(GameState state)
    {
        string s = "";

        switch (state)
        {
            case GameState.Morning:
                s = "Morning";
                break;
            case GameState.Workday:
                s = "Workday";
                break;
            case GameState.Minigame:
                s = "Minigame";
                break;
            case GameState.Evening:
                s = "Evening";
                break;
            case GameState.Ending:
                s = "Ending";
                break;
            case GameState.Tutorial:
                s = "Tutorial";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        return s;
    }
    public string RouteStateToString(RouteState route)
    {
        string s = "";
        switch (route)
        {
            case RouteState.Indecisive:
                s = "Indecisive";
                break;
            case RouteState.Greed:
                s = "Greed";
                break;
            case RouteState.Sloth:
                s = "Sloth";
                break;
            case RouteState.Pride:
                s = "Pride";
                break;
            case RouteState.Wrath:
                s = "Wrath";
                break;
            case RouteState.Gluttony:
                s = "Gluttony";
                break;
            case RouteState.Envy:
                s = "Envy";
                break;
            case RouteState.Lust:
                s = "Lust";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(route), route, null);
        }

        return s;
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
                // greedPoints++;
                Debug.Log("Greed Points: " + greedPoints);
                break;
            case MinigameState.Email:
                
                // Point gain varies based on which route you are on; defaults to greed
                // Stronger gains if you're already on the non-greed route
                if (Route == RouteState.Envy)
                {
                    envyPoints += 2;
                }
                else if (Route == RouteState.Pride)
                {
                    pridePoints += 2;
                }
                else
                {
                    greedPoints++;
                }
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
        Email
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
    
    // Tutorial Methods
    public void SnoozeAlarm()
    {
        OnSnooze?.Invoke();
    }
    
}
