using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Report : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;

        }

    }

    private static Report _instance = null;
    public static Report Instance
    {
        get
        {
            if (_instance == null)
            {
                _container = new GameObject();
                _container.name = "Report";
                _instance = _container.AddComponent(typeof(Report)) as Report;
                DontDestroyOnLoad(_container);
                Debug.LogError("cSingleton == null");

            }
            return _instance;

        }

    }

    public string GameDataFileName = "Report1.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();

            }
            return _gameData;

        }

    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("Load Clear");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);

        } else

        {
            Debug.Log("Create New Data");

            _gameData = new GameData();

        }

    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("Save Clear");

    }

    private void Awake()
    {
        _instance = this;

        

    }



}
