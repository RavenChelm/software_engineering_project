using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VNCreator;
using TMPro;


public class VNCreator_OptionsMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider readSpeedSlider;
    public Toggle instantTextToggle;
    public Toggle fullScreenToggle;
    public TMP_Dropdown dropdownResolition;

    void Start()
    {
        GameOptions.InitilizeOptions();

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = GameOptions.musicVolume;
            musicVolumeSlider.onValueChanged.AddListener(GameOptions.SetMusicVolume);
        }
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = GameOptions.sfxVolume;
            sfxVolumeSlider.onValueChanged.AddListener(GameOptions.SetSFXVolume);
        }
        if (readSpeedSlider != null)
        {
            readSpeedSlider.value = GameOptions.readSpeed;
            readSpeedSlider.onValueChanged.AddListener(GameOptions.SetReadingSpeed);
        }
        if (instantTextToggle != null)
        {
            instantTextToggle.isOn = GameOptions.isInstantText;
            instantTextToggle.onValueChanged.AddListener(GameOptions.SetInstantText);
        }
        if (fullScreenToggle != null)
        {
            fullScreenToggle.isOn = GameOptions.isFullScreen;
            fullScreenToggle.onValueChanged.AddListener(GameOptions.SetFullScreen);
        }
        if (dropdownResolition != null)
        {
            List<string> resolutions = new List<string>();
            Resolution[] rsl = Screen.resolutions;
            foreach (var i in rsl)
            {
                resolutions.Add(i.width + "x" + i.height);
            }
            dropdownResolition.ClearOptions();
            dropdownResolition.AddOptions(resolutions);

            dropdownResolition.value = GameOptions.Resolution;
            dropdownResolition.onValueChanged.AddListener(GameOptions.SetResolution);
        }

    }
}
