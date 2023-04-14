using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VNCreator;
using System.Linq;
using System;
using System.IO;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string playScene;
    private bool pause = true;
    [SerializeField] private GameObject recordField;
    [SerializeField] private GameObject saveRecordField;
    [SerializeField] private GameObject textRecordPrefab;
    [SerializeField] private TMP_Text _profile;
    [SerializeField] private TMP_Text _progress;
    [SerializeField] private TMP_Text _timeGame;
    [SerializeField] private TMP_Text _timeSave;
    [SerializeField] private TMP_Text _profileSave;
    [SerializeField] private TMP_Text _progressSave;
    [SerializeField] private TMP_Text _timeGameSave;
    [SerializeField] private TMP_Text _timeSaveSave;
    private List<savedData> saveData;
    private int _currentSaveIndex = -1;
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
    public void AddTextLoading()
    {
        DeleteFieldRecord(recordField);

        saveData = GameSaveManager.Loading();
        RectTransform rectTransform = recordField.GetComponent<RectTransform>();
        Vector2 contentSize = Vector2.zero;
        foreach (savedData data in saveData)
        {
            var textOBj = Instantiate(textRecordPrefab, recordField.transform);

            var text = textOBj.GetComponentInChildren<TMP_Text>();
            text.SetText("Profile: " + data.profile + " , " + "Time Save: " + data.timeSave);

            contentSize.y += textOBj.GetComponent<RectTransform>().sizeDelta.y;
        }
        rectTransform.sizeDelta = contentSize;
    }
    public void AddTextSaving(GameObject field)
    {
        if (field == saveRecordField) DeleteFieldRecord(saveRecordField);
        else DeleteFieldRecord(recordField);



        saveData = GameSaveManager.Loading();
        if (saveData == null) return;

        RectTransform rectTransform = field.GetComponent<RectTransform>();
        Vector2 contentSize = Vector2.zero;
        foreach (savedData data in saveData)
        {
            var textOBj = Instantiate(textRecordPrefab, field.transform);

            var text = textOBj.GetComponentInChildren<TMP_Text>();
            text.SetText("Time Save: " + data.timeSave);

            contentSize.y += textOBj.GetComponent<RectTransform>().sizeDelta.y;
        }
        rectTransform.sizeDelta = contentSize;
    }
    public void ShowStats(int index)
    {
        savedData data = saveData[index];
        _profile.SetText("Profile: " + data.profile);
        _progress.SetText("Progress: " + data.progress);
        _timeGame.SetText("Time Game: " + data.timeGame);
        _timeSave.SetText("Time Save: " + data.timeSave.ToString("MM/dd/yyyy HH:mm:ss"));

        _currentSaveIndex = index;
    }
    public void ShowDefaultStats()
    {
        _profile.SetText("Profile: ");
        _progress.SetText("Progress: ");
        _timeGame.SetText("Time Game: ");
        _timeSave.SetText("Time Save: ");

    }
    public void ShowStatsSave(int index)
    {
        savedData data = saveData[index];
        _profileSave.SetText("Profile: " + data.profile);
        _progressSave.SetText("Progress: " + data.progress);
        _timeGameSave.SetText("Time Game: " + data.timeGame);
        _timeSaveSave.SetText("Time Save: " + data.timeSave.ToString("MM/dd/yyyy HH:mm:ss"));

        _currentSaveIndex = index;
    }
    public void ShowDefaultStatsSave()
    {
        _profileSave.SetText("Profile: ");
        _progressSave.SetText("Progress: ");
        _timeGameSave.SetText("Time Game: ");
        _timeSaveSave.SetText("Time Save: ");
        _currentSaveIndex = 0;
    }
    public void Load()
    {
        if (_currentSaveIndex != -1)
        {
            savedData data = saveData[_currentSaveIndex];
            var _loadList = data.loadList.Split('_').ToList();
            GameSaveManager.SetListLoad(_loadList);
            Debug.Log("Load game");

            if (Time.timeScale == 0) Time.timeScale = 1;
            GameSaveManager.firstRunTime = DateTime.Now;
            SceneManager.LoadScene(playScene, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("No load file in saveData from MenuController");
        }
    }
    public void LoadLast()
    {
        int countFiles = new DirectoryInfo(Application.persistentDataPath).GetFiles().Length;
        if (countFiles > 0)
        {
            saveData = GameSaveManager.Loading();
            savedData data = saveData[saveData.Count - 1];
            var _loadList = data.loadList.Split('_').ToList();
            GameSaveManager.SetListLoad(_loadList);
            Debug.Log("Load last game");
        }
        if (Time.timeScale == 0) Time.timeScale = 1;
        GameSaveManager.firstRunTime = DateTime.Now;
        SceneManager.LoadScene(playScene, LoadSceneMode.Single);
    }
    public void NewLoad()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        GameSaveManager.firstRunBool = true;
        GameSaveManager.firstRunTime = DateTime.Now;
        GameSaveManager.SetListLoad(null);
        SceneManager.LoadScene(playScene, LoadSceneMode.Single);
    }

    public void Save()
    {
        if (_currentSaveIndex == 0)
        {
            GameSaveManager.Save(GameSaveManager.listLoad);
        }
        else if (_currentSaveIndex > 0)
        {
            GameSaveManager.SaveInFile(GameSaveManager.listLoad, _currentSaveIndex);
        }
    }

    private void DeleteFieldRecord(GameObject field)
    {
        if (field.transform.childCount > 0)
        {
            for (int j = field.transform.childCount; j > 0; --j)
            {
                DestroyImmediate(field.transform.GetChild(0).gameObject);
            }
        }
    }
    public void Pause()
    {
        pause = !pause;
        if (pause) Time.timeScale = 1;
        else Time.timeScale = 0;

    }
}

