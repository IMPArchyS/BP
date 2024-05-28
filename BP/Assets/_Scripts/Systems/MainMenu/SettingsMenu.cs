using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;

    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new();
        int resIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " * " + resolutions[i].height + " " + resolutions[i].refreshRate + "hz";
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                resIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void OnEnable()
    {
        volumeSlider.value = SoundManager.Instance.SfxSrc.volume;
        musicSlider.value = SoundManager.Instance.MusicSrc.volume;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        fullscreenToggle.isOn = Screen.fullScreen;

        if (volumeSlider.onValueChanged.GetPersistentEventCount() == 0)
            volumeSlider.onValueChanged.AddListener(SoundManager.Instance.AdjustSfx);

        if (musicSlider.onValueChanged.GetPersistentEventCount() == 0)
            musicSlider.onValueChanged.AddListener(SoundManager.Instance.AdjustMusic);
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
