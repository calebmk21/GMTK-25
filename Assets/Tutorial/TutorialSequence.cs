using UnityEngine;
using Yarn.Unity;

public class TutorialSequence : MonoBehaviour
{
    public GameObject tutorialWorkstation;
    public DialogueRunner dialogueRunner;
    public DialogueRunner dialogueRunnerMain;
    public void OpenWorkstation()
    {
        tutorialWorkstation.SetActive(true);
    }

    public void AdvanceTutorial()
    {
        dialogueRunner.StartDialogue("Orientation");
    }

    public void AdvanceTutorialAgain()
    {
        dialogueRunnerMain.StartDialogue("PostOrientation");
    }
}
