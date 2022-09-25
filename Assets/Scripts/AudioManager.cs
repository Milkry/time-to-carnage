using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.group;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    /// <summary>
    /// Plays a specific audio clip. To call use "FindObjectOfType<AudioManager>().Play("Audio Name");"
    /// </summary>
    /// <param name="name">Name of audio clip</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Can't play '{name}'. Audio not found!");
            return;
        }
        s.source.Play();
    }

    public void PlayOnTop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Can't play '{name}'. Audio not found!");
            return;
        }
        s.source.PlayOneShot(s.clip, s.volume);
    }

    /// <summary>
    /// Stops a specific audio clip. To call use "FindObjectOfType<AudioManager>().Stop("Audio Name");"
    /// </summary>
    /// <param name="name">Name of audio clip</param>
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Can't stop playing '{name}'. Audio not found!");
            return;
        }
        s.source.Stop();
    }

    /// <summary>
    /// Stops all playing audio clips.
    /// </summary>
    public void StopAll()
    {
        foreach (var s in sounds)
        {
            s.source.Stop();
        }
    }

    public void PlayRandom(string[] names)
    {
        Play(names[UnityEngine.Random.Range(0, names.Length)]);
    }

    public void PlayRandomOnTop(string[] names)
    {
        PlayOnTop(names[UnityEngine.Random.Range(0, names.Length)]);
    }
}
