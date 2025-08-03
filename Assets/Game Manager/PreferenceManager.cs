using UnityEngine;

public class PreferenceManager : MonoBehaviour
{
    // Put new settings here
    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string VoiceVolumeKey = "VoiceVolume";
    private const string SFXVolumeKey = "SFXVolume";
    
    void Start()
    {
        LoadPrefs();
    }

    void OnApplicationQuit()
    {
        SavePrefs();
    }
    

    public void SavePrefs()
    {

    }

    public void LoadPrefs()
    {
        // fill with things later
    }
}
