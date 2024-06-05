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
    private void Awake()
    {
        if (audioController == null)
        {
            audioController = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        gameMusic = GetComponent<AudioSource>();
    }

    public void ChangeAllVolume(float volume)
    {
        if (volume == -50)
        {
            audio.SetFloat("Master", -80);
        }
        else
        {
            audio.SetFloat("Master", volume);
        }
    }
    public void ChangeMusicVolume(float volume)
    {
        if (volume == -50)
        {
            audio.SetFloat("Music", -80);
        }
        else
        {
            audio.SetFloat("Music", volume);
        }

    }
    public void ChangeVFXVolume(float volume)
    {
        if (volume == -50)
        {
            audio.SetFloat("VFX", -80);
        }
        else
        {
            audio.SetFloat("VFX", volume);
        }
    }
    public void ChangeMusic(string scene)
    {
        switch (scene)
        { 
            case "Menu":
                gameMusic.clip = myAudios[0];
            break;
            case "Fase_Final":
                gameMusic.clip = myAudios[1];
                break;
            default:
                gameMusic.clip = myAudios[0];
                break;
        }
        gameMusic.Play();
    }
}