using UnityEngine;
using UnityEngine.SceneManagement;

public class NewEmptyCSharpScript: MonoBehaviour
{
    public void SwapScenes()
    {
        SceneManager.LoadScene(1);
    }
}
