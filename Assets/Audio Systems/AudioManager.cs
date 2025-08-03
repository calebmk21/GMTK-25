using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    // Put new settings here
    private const string MasterVolumeKey = "MasterVolume";
    private const string MusicVolumeKey = "MusicVolume";
    private const string VoiceVolumeKey = "VoiceVolume";
    private const string SFXVolumeKey = "SFXVolume";
    
    
    public Slider masterVolume, musicVolume, voiceVolume, sfxVolume;
    
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public AudioMixer audioMixer;
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
        PlayerPrefs.SetFloat(MasterVolumeKey, masterVolume.value);
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume.value);
        PlayerPrefs.SetFloat(VoiceVolumeKey, voiceVolume.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, sfxVolume.value);
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        masterVolume.value = PlayerPrefs.GetFloat(MasterVolumeKey, masterVolume.value);;
        musicVolume.value = PlayerPrefs.GetFloat(MusicVolumeKey, musicVolume.value);
        voiceVolume.value = PlayerPrefs.GetFloat(VoiceVolumeKey, voiceVolume.value);
        sfxVolume.value = PlayerPrefs.GetFloat(SFXVolumeKey, sfxVolume.value);
    }
    public void ChangeMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", masterVolume.value);
        PlayerPrefs.SetFloat(MasterVolumeKey, masterVolume.value);
    }
    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume.value);
    }
    public void ChangeVoicesVolume()
    {
        audioMixer.SetFloat("VoiceVolume", voiceVolume.value);
        PlayerPrefs.SetFloat(VoiceVolumeKey, voiceVolume.value);
    }
    public void ChangeSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", sfxVolume.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, sfxVolume.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
