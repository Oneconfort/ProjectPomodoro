using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController audioController;
    public AudioClip[] myAudios;
    private AudioSource gameMusic;
    public AudioMixer audio;
    private float masterVolume;
    private float musicVolume ;
    private float vfxVolume ;



    private void Awake()
    {
        if (audioController == null)
        {
            audioController = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        gameMusic = GetComponent<AudioSource>();
        if (gameMusic == null)
        {
           // Debug.LogError("AudioSource component missing on AudioController GameObject.");
            return;
        }
    }

    public void ChangeAllVolume(float volume)
    {
        masterVolume = volume == -50 ? -80 : volume;
        audio.SetFloat("Master", masterVolume);
    }

    public void ChangeMusicVolume(float volume)
    {
        musicVolume = volume == -50 ? -80 : volume;
        audio.SetFloat("Music", musicVolume);
    }

    public void ChangeVFXVolume(float volume)
    {
        vfxVolume = volume == -50 ? -80 : volume;
        audio.SetFloat("VFX", vfxVolume);
    }

    public void ChangeMusic(string scene)
    {
        if (gameMusic == null)
        {
            Debug.LogError("AudioSource component missing on AudioController GameObject.");
            return;
        }

        switch (scene)
        {
            case "Menu":
                gameMusic.clip = myAudios.Length > 0 ? myAudios[0] : null;
                break;
            case "Fase_Final":
                gameMusic.clip = myAudios.Length > 1 ? myAudios[1] : null;
                break;
            default:
                gameMusic.clip = myAudios.Length > 0 ? myAudios[0] : null;
                break;
        }

        if (gameMusic.clip != null)
        {
            gameMusic.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned for the specified scene.");
        }
    }

    private void ApplyStoredVolumes()
    {
        audio.SetFloat("Master", masterVolume);
        audio.SetFloat("Music", musicVolume);
        audio.SetFloat("VFX", vfxVolume);
    }
}