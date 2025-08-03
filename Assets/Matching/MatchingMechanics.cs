using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;


public class MatchingMechanics : MonoBehaviour
{
    [SerializeField] private GameObject card;
    public static GameObject choice1 = null;
    public static int totalPoints;
    public static int envyNum;
    public static int prideNum;
    public static int greedNum;

    [SerializeField] public AudioSource bgm;
    [SerializeField] public GameObject background;
    
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
        if (minigame == GameManager.MinigameState.Match)
        {
            StartCoroutine("Matching");
        }
    }

    
    void Start()
    {
        //StartCoroutine("Matching");
    }

    IEnumerator Matching()
    {
        background.SetActive(true);
        bgm.Play();
        totalPoints = GameManager.Instance.greedPoints + GameManager.Instance.envyPoints + GameManager.Instance.pridePoints;
        Invoke("matchMinigameEnd", 30f);
        if(totalPoints == 0)
        {
            envyNum = 2;
            prideNum = 2;
        }
        else
        {
            envyNum = (int)10 * GameManager.Instance.envyPoints / totalPoints;
            prideNum = (int)10 * GameManager.Instance.pridePoints / totalPoints;
        }
        greedNum = 30 - envyNum - prideNum;
        for (int i = 0; i < 5; ++i)
        {
            for(int j = 0; j < 6; ++j)
            {
                Instantiate(card, new Vector3(-6.25f + 2.5f * j, 4f - i * 2, 0), Quaternion.identity);
            }
        }
        yield return null;
    }

    // Called when the minigame ends
    // Cleaup and transitions back
    public void matchMinigameEnd()
    {
        StopCoroutine("Matching");
        var Clickables = GameObject.FindGameObjectsWithTag("CardClickable");
        foreach (var item in Clickables)
        {
            if (item.name == "Card(Clone)")
            {
                DestroyImmediate(item, true);
            }
        }

        bgm.Stop();
        background.SetActive(false);
        GameManager.Instance.UpdateGameState(GameManager.GameState.Workday);
    }
}
