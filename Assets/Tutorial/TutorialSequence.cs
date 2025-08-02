using UnityEngine;

public class TutorialSequence : MonoBehaviour
{

    void Awake()
    {
        GameManager.OnSnooze += GameManagerOnSnooze;
    }
    
    void OnDestroy()
    {
        GameManager.OnSnooze -= GameManagerOnSnooze;
    }

    public void GameManagerOnSnooze()
    {
        
    }
    
    public void SnoozeButton()
    {
        GameManager.Instance.SnoozeAlarm();
    }
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
