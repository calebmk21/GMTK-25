using UnityEngine;

public class TutorialSequence : MonoBehaviour
{
    public GameObject tutorialWorkstation;

    public void OpenWorkstation()
    {
        tutorialWorkstation.SetActive(true);
    }
}
