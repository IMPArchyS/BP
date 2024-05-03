using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Atributes
    public static SoundManager Instance;
    [SerializeField] private List<Sound> music, sfx;
    [SerializeField] private AudioSource musicSrc, sfxSrc;
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
        // PlayMusic("BGMusic");
    }
    #endregion

    #region Music 
    public void PlayMusic(string name)
    {
        Sound s = music.Find(m => m.Name.Equals(name));

        if (s != null)
        {
            musicSrc.clip = s.Clip;
            musicSrc.Play();
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
        Sound s = sfx.Find(m => m.Name.Equals(name));

        if (s != null)
            sfxSrc.PlayOneShot(s.Clip);
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
