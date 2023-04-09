using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;

[System.Serializable]
public class AudioSetting
{
    public Slider slider;
    public GameObject mutePrefabs;
    public string exposedParam;

    public void Initialize()
    {
        slider.value = PlayerPrefs.GetFloat(exposedParam);
    }

    public void SetExposedParam(float value)
    {
        mutePrefabs.SetActive(value <= slider.minValue);
        AudioManager.Instance.mixer.SetFloat(exposedParam, value);        
    }
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioMixer mixer;
    public AudioSetting[] audioSettings;
    public Sound[] musicSounds, sfxSounds;
    private enum AudioGroups { Music, SFX };
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < audioSettings.Length; i++)
        {
            audioSettings[i].Initialize();
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, musicSound => musicSound.name == name);
        if (sound == null)
        {
            Debug.Log("Sound not found" + name);
        } else
        {
            sound.audioSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, sfxSound => sfxSound.name == name);
        if (sound == null)
        {
            Debug.Log("Sound not found" + name);
        }
        else
        {
            sound.audioSource.Play();
        }
    }

    public void ToggleMusic()
    {
        float value = PlayerPrefs.GetFloat(audioSettings[(int)AudioGroups.Music].exposedParam) != audioSettings[(int)AudioGroups.Music].slider.minValue ? audioSettings[(int)AudioGroups.Music].slider.minValue : PlayerPrefs.GetFloat(audioSettings[(int)AudioGroups.Music].exposedParam);
        audioSettings[(int)AudioGroups.Music].SetExposedParam(value);
    }

    public void ToggleSFX()
    {
        float value = PlayerPrefs.GetFloat(audioSettings[(int)AudioGroups.SFX].exposedParam) != audioSettings[(int)AudioGroups.SFX].slider.minValue ? audioSettings[(int)AudioGroups.SFX].slider.minValue : PlayerPrefs.GetFloat(audioSettings[(int)AudioGroups.SFX].exposedParam);
        audioSettings[(int)AudioGroups.SFX].SetExposedParam(value);
    }

    public void SetMusicVolume(float value)
    {
        Debug.Log("set volume: " + value);
        audioSettings[(int)AudioGroups.Music].SetExposedParam(value);
        PlayerPrefs.SetFloat(audioSettings[(int)AudioGroups.Music].exposedParam, value);
    }

    public void SetSFXVolume(float value)
    {
        audioSettings[(int)AudioGroups.SFX].SetExposedParam(value);
        PlayerPrefs.SetFloat(audioSettings[(int)AudioGroups.SFX].exposedParam, value);
    }

    public void PlayButtonInSFX()
    {
        PlaySFX("ButtonIn");
    }

    public void PlayButtonOutSFX()
    {
        PlaySFX("ButtonOut");
    }
        
}
