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
    private readonly string PPMasterVolume = "MasterVolume";
    private readonly string PPMusicVolume = "MusicVolume";
    private readonly string PPSoundEffectsVolume = "SoundEffectsVolume";

    //AudioMixer Variables
    private readonly string AAMasterVolume = "masterVolume";
    private readonly string AAMusicVolume = "musicVolume";
    private readonly string AASoundEffectsVolume = "soundEffectsVolume";

    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundEffectsSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey(PPMasterVolume))
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
    
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Deleted Save!");
        }
    }*/

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

    private void Save(string keyName, float keyValue)
    {
        PlayerPrefs.SetFloat(keyName, keyValue);
        PlayerPrefs.Save();
    }

    private void Load()
    {
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

    private void GenerateFiles()
    {
        PlayerPrefs.SetFloat(PPMasterVolume, masterSlider.maxValue);
        PlayerPrefs.SetFloat(PPMusicVolume, musicSlider.maxValue);
        PlayerPrefs.SetFloat(PPSoundEffectsVolume, soundEffectsSlider.maxValue);
    }

    private float CalculateVolume(float volume)
    {
        return Mathf.Log10(volume) * 20;
    }
}
