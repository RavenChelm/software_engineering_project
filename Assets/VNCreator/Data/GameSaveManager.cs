using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace VNCreator
{
    [Serializable]
    public class savedData
    {
        public string profile;
        public string progress;
        public string timeGame;
        public DateTime timeSave;
        public string loadList;
        public string pathSave;
        public savedData()
        {
            profile = "test";
            progress = "test";
            timeGame = string.Empty;
            timeSave = DateTime.MinValue;
            loadList = null;
        }
        public savedData(string _profile, string _progress, string _timeGame, DateTime _timeSave, string _loadList, string _pathSave)
        {
            profile = _profile;
            progress = _progress;
            timeGame = _timeGame;
            timeSave = _timeSave;
            loadList = _loadList;
            pathSave = _pathSave;
        }
    }
    public static class GameSaveManager
    {
        public static string currentLoadName = string.Empty;
        public static List<string> listLoad = new List<string>();
        public static DateTime firstRunTime = DateTime.MinValue;
        public static bool firstRunBool = false;
        public static void SetListLoad(List<string> _listLoad)
        {
            listLoad = _listLoad;
        }
        public static List<string> Load(string loadName)
        {
            if (loadName == string.Empty)
            {
                currentLoadName = loadName;
                return null;
            }

            if (!PlayerPrefs.HasKey(currentLoadName))
            {
                Debug.LogError("You have not saved anything with the name " + currentLoadName);
                return null;
            }

            string _loadString = PlayerPrefs.GetString(currentLoadName);
            List<string> _loadList = _loadString.Split('_').ToList();
            _loadList.RemoveAt(_loadList.Count - 1);
            currentLoadName = loadName;
            return _loadList;
        }

        public static List<string> Load()
        {
            if (currentLoadName == string.Empty)
            {
                return null;
            }

            if (!PlayerPrefs.HasKey(currentLoadName))
            {
                Debug.LogError("You have not saved anything with the name " + currentLoadName);
                return null;
            }

            string _loadString = PlayerPrefs.GetString(currentLoadName);
            List<string> _loadList = _loadString.Split('_').ToList();

            // string[] files = Directory.GetFiles(Application.persistentDataPath);
            // XmlSerializer xmlSerializer = new XmlSerializer(typeof(savedData));
            // FileStream file = File.Open(files[files.Length-1], FileMode.Open);
            // savedData saveData = (savedData)xmlSerializer.Deserialize(file);
            // file.Close();
            // _loadList = saveData.loadList.Split('_').ToList();
            return _loadList;
        }

        public static List<string> LoadNode()
        {
            return listLoad;
        }
        public static void Save(List<string> storyPath)
        {
            Debug.Log("Hello there!");
            string _save = string.Join("_", storyPath.ToArray());
            // PlayerPrefs.SetString(currentLoadName, _save);
            if (!Directory.Exists(Application.persistentDataPath + "/Save/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Save/");
            }
            int countFiles = new DirectoryInfo(Application.persistentDataPath + "/Save").GetFiles().Length;
            string path = Application.persistentDataPath + "/Save" + "/savedGames" + (countFiles + 1) + ".txt";
            savedData newData;
            if (firstRunBool == true)
            {
                TimeSpan q = DateTime.Now - firstRunTime;
                Debug.Log(q.ToString(@"hh\:mm\:ss"));
                newData = new savedData("test", "test", _timeGame: q.ToString(@"hh\:mm\:ss"), System.DateTime.Now, _save, path);
            }
            else
            {
                var w = Loading();
                TimeSpan q = TimeSpan.Parse(w[w.Count - 1].timeGame) + (DateTime.Now - firstRunTime);
                Debug.Log(q.ToString(@"hh\:mm\:ss"));
                newData = new savedData("test", "test", q.ToString(@"hh\:mm\:ss"), System.DateTime.Now, _save, path);
            }


            XmlSerializer xmlSerializer = new XmlSerializer(typeof(savedData));
            FileStream file = File.Create(path);
            xmlSerializer.Serialize(file, newData);
            Debug.Log(path);
            file.Close();

        }
        public static void SaveInFile(List<string> storyPath, int index)
        {
            Debug.Log("Hello there");
            string _save = string.Join("_", storyPath.ToArray());

            savedData newData;
            var w = Loading();
            TimeSpan q = TimeSpan.Parse(w[index].timeGame) + (DateTime.Now - firstRunTime);
            Debug.Log(q.ToString(@"hh\:mm\:ss"));
            newData = new savedData("test", "test", q.ToString(@"hh\:mm\:ss"), System.DateTime.Now, _save, w[index].pathSave);

            if (!Directory.Exists(Application.persistentDataPath + "/Save/"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Save/");
            }
            int countFiles = new DirectoryInfo(Application.persistentDataPath + "/Save").GetFiles().Length;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(savedData));
            FileStream file = File.OpenWrite(w[index].pathSave);
            xmlSerializer.Serialize(file, newData);
            Debug.Log(w[index].pathSave);
            file.Close();

        }
        public static List<savedData> Loading()
        {
            Debug.Log("General Kenobi...");
            Debug.Log(Application.persistentDataPath + "/Save/");

            int countFiles = new DirectoryInfo(Application.persistentDataPath + "/Save/").GetFiles().Length;
            if (countFiles != 0)
            {
                List<savedData> data = new List<savedData>();
                string[] files = Directory.GetFiles(Application.persistentDataPath + "/Save/");

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(savedData));
                for (int i = 0; i < countFiles; i++)
                {
                    FileStream file = File.Open(files[i], FileMode.Open);
                    savedData saveData = (savedData)xmlSerializer.Deserialize(file);
                    file.Close();

                    data.Add(saveData);
                }
                return data;
            }
            else
            {
                return null;
            }
        }
        public static void NewLoad(string saveName)
        {
            currentLoadName = saveName;
            PlayerPrefs.SetString(saveName, string.Empty);
        }
    }
}
