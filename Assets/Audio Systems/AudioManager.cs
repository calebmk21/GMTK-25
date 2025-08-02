using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;

    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioSource sfxSource;
    
    //public AudioClip bgmClip;
    //public AudioClip sfxClip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeSlider.value = 0.5f;
    }

    public void OnChangeSlider()
    {
        audioSource.volume = volumeSlider.value;
        //PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value);
        //PlayerPrefs.Save();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
