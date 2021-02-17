using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public static bool isMusicPlayed;
    public static SoundManager instance = null;

    public AudioClip levelMusic;
    public AudioClip mapMusic;
   
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        if(isMusicPlayed != true)
        {
            isMusicPlayed = true;
            PlayMusic(levelMusic);
        }
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.clip = sfx;
        sfxSource.Play();
    }

    public void PlayMusic(AudioClip audioClip)
    {
        if (musicSource.clip != audioClip)
        {
            musicSource.clip = audioClip;
            musicSource.Play();
            musicSource.loop = true;
        }
    }

    public static SoundManager GetInstance()
    {
        return instance;
    }
}
