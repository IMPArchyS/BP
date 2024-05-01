using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private List<Sound> music, sfx;
    [SerializeField] private AudioSource musicSrc, sfxSrc;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // PlayMusic("BGMusic");
    }

    public void PlayMusic(string name)
    {
        Sound s = music.Find(m => m.Name.Equals(name));

        if (s != null)
        {
            musicSrc.clip = s.Clip;
            musicSrc.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = sfx.Find(m => m.Name.Equals(name));

        if (s != null)
            sfxSrc.PlayOneShot(s.Clip);
    }

    public void AdjustMusic(float value)
    {
        musicSrc.volume = value;
        if (musicSrc.volume == 0)
            musicSrc.mute = true;
        else
            musicSrc.mute = false;
    }

    public void AdjustSfx(float value)
    {
        sfxSrc.volume = value;
        if (musicSrc.volume == 0)
            musicSrc.mute = true;
        else
            musicSrc.mute = false;
    }
}
