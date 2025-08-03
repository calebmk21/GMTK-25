using UnityEngine;
using Random = UnityEngine.Random;

public class Card : MonoBehaviour
{
    private bool locked = false;
    public static bool stopMultMatch = false;
    private string sinType;
    private float rand;

    private int greedConversion;
    
    void Start()
    {
        //set card sin based on sin points
        //Debug.Log("envyNum: " + MatchingMechanics.envyNum + " prideNum: " + MatchingMechanics.prideNum + " greedNum: " + MatchingMechanics.greedNum);
        if (MatchingMechanics.envyNum != 0 && MatchingMechanics.prideNum != 0 && MatchingMechanics.greedNum != 0)
        {
            rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0: sinType = "greed"; MatchingMechanics.greedNum--; break;
                case 1: sinType = "pride"; MatchingMechanics.prideNum--; break;
                case 2: sinType = "envy"; MatchingMechanics.envyNum--; break;

            }
        }
        else
        {
            if (MatchingMechanics.prideNum == 0)
            {
                if (MatchingMechanics.envyNum == 0) { sinType = "greed"; MatchingMechanics.greedNum--; }
                else if (MatchingMechanics.greedNum == 0) { sinType = "envy"; MatchingMechanics.envyNum--; }
                else
                {
                    rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0: sinType = "greed"; MatchingMechanics.greedNum--; break;
                        case 1: sinType = "envy"; MatchingMechanics.envyNum--; break;
                    }
                }
            }
            else if (MatchingMechanics.envyNum == 0)
            {
                if (MatchingMechanics.greedNum == 0) { sinType = "pride"; MatchingMechanics.prideNum--; }
                else if (MatchingMechanics.prideNum == 0) { sinType = "greed"; MatchingMechanics.greedNum--; }
                else
                {
                    rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0: sinType = "pride"; MatchingMechanics.prideNum--; break;
                        case 1: sinType = "greed"; MatchingMechanics.greedNum--; break;
                    }
                }
            }
            else
            {
                sinType = "greed"; MatchingMechanics.greedNum--;
            }
        }
    }
        
    
    //locks in card choices
    void OnMouseDown()
    {
        if (!stopMultMatch)
        {
            if (!locked)
            {
                //locks card from being chosen again
                locked = true;
                switch (sinType)
                {
                    case "greed": GetComponent<SpriteRenderer>().color = Color.yellow; break;
                    case "envy": GetComponent<SpriteRenderer>().color = Color.green; break;
                    case "pride": GetComponent<SpriteRenderer>().color = Color.blue; break;
                }
                if (MatchingMechanics.choice1 == null)
                {
                    MatchingMechanics.choice1 = gameObject;
                }
                else
                {
                    //check if matching
                    stopMultMatch = true;
                    if (MatchingMechanics.choice1.GetComponent<Card>().sinType == this.sinType)
                    {
                        MatchingMechanics.choice1.transform.localScale = new Vector3(1.0f, 0.5f, 1f);
                        transform.localScale = new Vector3(1.0f, 0.5f, 1f);
                        switch (this.sinType)
                        {
                            case "greed":
                                greedConversion += 1;
                                if (greedConversion % 6 == 0)
                                {
                                    greedConversion = 0;
                                    GameManager.Instance.greedPoints += 1;
                                }
                                break;
                            case "envy":
                                GameManager.Instance.envyPoints += 1;
                                break;
                            case "pride":
                                GameManager.Instance.pridePoints += 1;
                                break;
                        }
                        MatchingMechanics.choice1 = null;
                        stopMultMatch = false;
                    }
                    else
                    {
                        //empties out cards for next match
                        Invoke("invalidMatch", 1);
                    }
                }
            }
        }
        
    }

    //cleans up variables for next match if invalid
    void invalidMatch()
    {
        MatchingMechanics.choice1.GetComponent<Card>().locked = false;
        MatchingMechanics.choice1.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().color = Color.white;
        locked = false;
        MatchingMechanics.choice1 = null;
        stopMultMatch = false;
    }
}
