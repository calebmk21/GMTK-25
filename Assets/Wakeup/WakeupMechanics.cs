using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WakeupMechanics : MonoBehaviour
{
    [SerializeField] private GameObject Z;
    [SerializeField] private int maxtoSpawn = 25;

    void Awake()
    {
        GameManager.OnMinigameSelect += GameManagerOnOnMinigameSelect;
    }
    
    void OnDestroy()
    {
        GameManager.OnMinigameSelect -= GameManagerOnOnMinigameSelect;
    }

    private void GameManagerOnOnMinigameSelect(GameManager.MinigameState minigame)
    {
        if (minigame == GameManager.MinigameState.Sleep)
        {
            StartCoroutine("Testing");
            //StartCoroutine("Voices");
        }
    }


    void Start()
    {
        // StartCoroutine("Testing");
        // StartCoroutine("Voices");
    }

    IEnumerator Testing()
    {
        float startTime = Time.time;
        for(int i = 0; i < maxtoSpawn; ++i)
        {
            InstantiateZ();
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(5);

        if(Move.countImp > 0)
        {
            //code for Boss scolding or whateva, replace with event
            // Loss
            Debug.Log("WAKE UP BIH");
        }
        else
        {
            // Win
            GameManager.Instance.slothPoints += 2;
        }

        float endTime = Time.time;
        float elapsedTime = endTime - startTime;
        
        Debug.Log("Time Elapsed: " + elapsedTime);
        wakeupMinigameEnd();
    }


    // Called when the minigame ends
    // Cleaup and transitions back
    public void wakeupMinigameEnd()
    {
        StopCoroutine("Testing");
        var Clickables = GameObject.FindGameObjectsWithTag("ClickableZ");
        foreach (var item in Clickables)
        {
            if (item.name == "Clickable(Clone)")
            {
                DestroyImmediate(item, true);
            }
        }
        
        GameManager.Instance.UpdateGameState(GameManager.GameState.Workday);
    }
    
    void InstantiateZ()
    {
        Instantiate(Z, new Vector3(Random.Range(-7f, 7f), Random.Range(-4f, 4f), 0), Quaternion.Euler(new Vector3(0, 0, Random.Range(-45f, 45f))));
    }
}
