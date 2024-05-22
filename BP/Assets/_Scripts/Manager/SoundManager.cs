using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Atributes
    public static SoundManager Instance;
    [SerializeField] private AudioSource positionSound;
    [SerializeField] private AudioSource musicSrc, sfxSrc;
    [SerializeField] private SoundData sd;
    #endregion

    #region Startup
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

    }
    #endregion

    #region Music 
    public void PlayMusic(string name)
    {
        Sound s = sd.Music.Find(m => m.Name.Equals(name));

        if (s != null)
        {
            musicSrc.clip = s.Clip;
            musicSrc.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound s = sd.Music.Find(m => m.Name.Equals(name));

        if (s != null)
        {
            musicSrc.clip = s.Clip;
            musicSrc.Stop();
        }
    }

    public void AdjustMusic(float value)
    {
        musicSrc.volume = value;
        if (musicSrc.volume == 0)
            musicSrc.mute = true;
        else
            musicSrc.mute = false;
    }
    #endregion

    #region SFX
    public void PlaySfx(string name)
    {
        Sound s = sd.Sfx.Find(m => m.Name.Equals(name));

        if (s != null)
            sfxSrc.PlayOneShot(s.Clip);
    }
    public void PlaySfx(string name, Vector3 pos)
    {
        Sound s = sd.Sfx.Find(m => m.Name.Equals(name));

        if (s != null)
        {
            positionSound.transform.position = pos;
            positionSound.volume = sfxSrc.volume;
            positionSound.PlayOneShot(s.Clip);
        }
    }

    public void AdjustSfx(float value)
    {
        sfxSrc.volume = value;
        if (musicSrc.volume == 0)
            musicSrc.mute = true;
        else
            musicSrc.mute = false;
    }
    #endregion
}
