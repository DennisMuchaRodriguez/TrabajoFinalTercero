using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumenSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider volumeSlider;

    private static VolumenSettings instance;

    public static VolumenSettings Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            
            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (musicSlider != null)
        { 
            musicSlider.onValueChanged.AddListener(SetMusicVolume); 
        }
        if (SFXSlider != null)
        {
            SFXSlider.onValueChanged.AddListener(SetSFXVolume); 
        }

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolumeGeneral);
        }
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    public void SetVolumeGeneral(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volumeGeneral", volume);

        if (musicSlider != null) 
        { 
            musicSlider.value = volume; 
        }
        if (SFXSlider != null)
        {
            SFXSlider.value = volume; 
        }
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            if (musicSlider != null)
            {
                musicSlider.value = musicVolume;
                SetMusicVolume(musicVolume);
            }
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            {
                if (SFXSlider != null)
                {
                    SFXSlider.value = sfxVolume;
                    SetSFXVolume(sfxVolume);
                }
            }
        }

        if (PlayerPrefs.HasKey("volumeGeneral"))
        {
             float volumeGeneral = PlayerPrefs.GetFloat("volumeGeneral");
            if (volumeSlider != null)
            {
                volumeSlider.value = volumeGeneral;
                SetVolumeGeneral(volumeGeneral);
            }
        }
    }
}
