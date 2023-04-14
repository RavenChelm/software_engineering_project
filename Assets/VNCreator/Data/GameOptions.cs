using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNCreator
{
    public static class GameOptions
    {
        public static float musicVolume = 0.5f;
        public static float sfxVolume = 0.5f;
        public static float readSpeed = 0.5f;
        public static bool isInstantText = false;
        public static bool isFullScreen = false;
        public static int Resolution = 0;
        public static List<string> resolutions = new List<string>();
        public static Resolution[] rsl = Screen.resolutions;

        public static void InitilizeOptions()
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
                musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            if (PlayerPrefs.HasKey("SfxVolume"))
                sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            if (PlayerPrefs.HasKey("ReadSpeed"))
                readSpeed = PlayerPrefs.GetFloat("ReadSpeed");
            if (PlayerPrefs.HasKey("InstantText"))
                isInstantText = PlayerPrefs.GetInt("InstantText") == 1 ? true : false;
            if (PlayerPrefs.HasKey("FullScreen"))
                isInstantText = PlayerPrefs.GetInt("FullScreen") == 1 ? true : false;
            if (PlayerPrefs.HasKey("Resolution"))
                Resolution = PlayerPrefs.GetInt("Resolution");

            resolutions = new List<string>();
            rsl = Screen.resolutions;
            foreach (var i in rsl)
            {
                resolutions.Add(i.width + "x" + i.height);
            }

            Resolution = resolutions.Count - 1;
        }

        public static void SetMusicVolume(float index)
        {
            musicVolume = index;
            PlayerPrefs.SetFloat("MusicVolume", index);
        }

        public static void SetSFXVolume(float index)
        {
            sfxVolume = index;
            PlayerPrefs.SetFloat("SfxVolume", index);
        }

        public static void SetReadingSpeed(float index)
        {
            readSpeed = index;
            PlayerPrefs.SetFloat("ReadSpeed", index);
        }

        public static void SetInstantText(bool index)
        {
            isInstantText = index;
            PlayerPrefs.SetInt("InstantText", index ? 1 : 0);
        }
        public static void SetFullScreen(bool index)
        {
            isFullScreen = index;
            Screen.fullScreen = isFullScreen;
            PlayerPrefs.SetInt("FullScreen", index ? 1 : 0);
        }
        public static void SetResolution(int index)
        {
            Resolution = index;
            Screen.SetResolution(rsl[Resolution].width, rsl[Resolution].height, isFullScreen);
            PlayerPrefs.SetInt("Resolution", index);
        }
    }
}
