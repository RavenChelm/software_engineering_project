using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    // private List<GameObject> ToggleSave = new List<GameObject>();
    // private GameObject CurrentSave = null;
    private bool pause = false;
    //controll app
    public void ExitAplication()
    {
        Application.Quit();
    }
    public void MenuOn(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void MenuOff(GameObject obj)
    {
        obj.SetActive(false);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void ContinueGame()
    {
        Debug.Log("Load last saved game");
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void Save()
    {
        Debug.Log("Save last game");
    }
    public void Load()
    {
        Debug.Log("Load game");
    }
    public void Pause()
    {
        pause = !pause;
        if (pause) Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    // public void SelectToggle(GameObject toggle)
    // {
    //     foreach (var tog in ToggleSave)
    //     {
    //         if (tog.GetComponent<Toggle>().isOn == true)
    //         {
    //             CurrentSave = tog;
    //         }
    //         tog.GetComponent<Toggle>().isOn = false;
    //         tog.transform.GetChild(0).GetComponent<Image>().color = new Vector4(0, 0, 0, 255);
    //     }
    //     if (CurrentSave != null)
    //     {
    //         CurrentSave.transform.GetChild(0).GetComponent<Image>().color = new Vector4(200, 200, 200, 255);
    //     }
    //     else
    //         Debug.Log("error!");
    // }
    // public void Load()
    // {
    //     int i = 0;

    //     foreach (var tog in ToggleSave)
    //     {
    //         Debug.Log(SaveLoad.savedGames[i].Name);
    //         if (tog == CurrentSave)
    //         {
    //             Game.current = SaveLoad.savedGames[i];
    //         }
    //         i++;
    //     }
    // }
}
