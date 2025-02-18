using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlide;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       if(!PlayerPrefs.HasKey("musicVolume"))
       {
        PlayerPrefs.SetFloat("musicVolume", 1);
        Load();
       }

       else
       {
            Load();
       } 
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlide.value;
        Save();
    }

    private void Load()
    {
        volumeSlide.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlide.value);
    }

}
