using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
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
    
    
    void Awake()
    {
        Instance = this;
    }

    // Handles logic for which route you're changing to
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

        if (dupe )
        {
            
        }
    }
    
    // Handles what happens when you change routes
    // A route is changed if you 
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
        
        
    }

    public void UpdateGameState(GameState newState)
    {
        
    }
    

    public void EndGame()
    {
        
    }
    
    
    public enum GameState
    {
        Morning,
        Workday,
        Transition,
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
