using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //Saves settings in registry
    //For PC -> "Computer\HKEY_CURRENT_USER\SOFTWARE\Unity\UnityEditor\{Company}\{Project}"
    //For Android -> "/data/data/pkg-name/shared_prefs/pkg-name.v2.playerprefs.xml"

    //Save Preferences
    private readonly string PPFilesGenerated = "FilesGenerated";
    private readonly string PPMasterVolume = "MasterVolume";
    private readonly string PPMusicVolume = "MusicVolume";
    private readonly string PPSoundEffectsVolume = "SoundEffectsVolume";
    public readonly string PPTutorial = "TutorialDone"; //Tutorial Finished = 1 | Tutorial Unfinished = 0

    //AudioMixer Variables
    private readonly string AAMasterVolume = "masterVolume";
    private readonly string AAMusicVolume = "musicVolume";
    private readonly string AASoundEffectsVolume = "soundEffectsVolume";

    //References
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundEffectsSlider;
    public Button resetTutorial;

    private void Start()
    {
        if (PlayerPrefs.HasKey(PPFilesGenerated))
        {
            //Returning player
            Load();
        }
        else
        {
            //New player
            GenerateFiles();
        }
    }
    
    private void Update()
    {
        if (PlayerPrefs.GetInt(PPTutorial) == 1)
        {
            resetTutorial.interactable = true;
        }
        else if (PlayerPrefs.GetInt(PPTutorial) == 0)
        {
            resetTutorial.interactable = false;
        }
    }

    private void GenerateFiles()
    {
        PlayerPrefs.SetInt(PPFilesGenerated, 1);
        PlayerPrefs.SetFloat(PPMasterVolume, masterSlider.maxValue);
        PlayerPrefs.SetFloat(PPMusicVolume, musicSlider.maxValue);
        PlayerPrefs.SetFloat(PPSoundEffectsVolume, soundEffectsSlider.maxValue);
        PlayerPrefs.SetInt(PPTutorial, 0);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat(AAMasterVolume, CalculateVolume(volume));
        Save(PPMasterVolume, volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(AAMusicVolume, CalculateVolume(volume));
        Save(PPMusicVolume, volume);
    }

    public void SetSoundEffectsVolume(float volume)
    {
        audioMixer.SetFloat(AASoundEffectsVolume, CalculateVolume(volume));
        Save(PPSoundEffectsVolume, volume);
    }
    private float CalculateVolume(float volume)
    {
        return Mathf.Log10(volume) * 20;
    }

    private void Save(string keyName, float keyValue)
    {
        PlayerPrefs.SetFloat(keyName, keyValue);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        //Get values from save file
        float masterVol = PlayerPrefs.GetFloat(PPMasterVolume);
        float musicVol = PlayerPrefs.GetFloat(PPMusicVolume);
        float soundEffectsVol = PlayerPrefs.GetFloat(PPSoundEffectsVolume);

        //Set the sliders
        masterSlider.value = masterVol;
        musicSlider.value = musicVol;
        soundEffectsSlider.value = soundEffectsVol;

        //Set the audio level
        audioMixer.SetFloat(AAMasterVolume, CalculateVolume(masterVol));
        audioMixer.SetFloat(AAMusicVolume, CalculateVolume(musicVol));
        audioMixer.SetFloat(AASoundEffectsVolume, CalculateVolume(soundEffectsVol));
    }

    public void ResetTutorial()
    {
        PlayerPrefs.SetInt(PPTutorial, 0);
        PlayerPrefs.Save();
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
