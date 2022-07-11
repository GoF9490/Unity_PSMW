using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public enum saveSlot
{
    none,
    save1,
    save2,
    save3,
    save4,
    save5
}

public class DataController : MonoBehaviour
{
    public PlayerData playerData;

    public static DataController instance = null;

    public saveSlot ss = saveSlot.none;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else
            if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "Test")
        {
            playerData.map = 1;
        }

        if (SceneManager.GetActiveScene().name == "Test 1")
        {
            playerData.map = 2;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Test")
        {
            playerData.map = 1;
        }

        if (SceneManager.GetActiveScene().name == "Test 1")
        {
            playerData.map = 2;
        }
    }


    public void SaveGameData()
    {
        if (ss == saveSlot.save1)
        {
            string jsonData = JsonUtility.ToJson(playerData, true);
            string path = Path.Combine(Application.dataPath, "playerData1.json");
            File.WriteAllText(path, jsonData);
            Debug.Log("Save");
        }
        else

        if (ss == saveSlot.save2)
        {
            string jsonData = JsonUtility.ToJson(playerData, true);
            string path = Path.Combine(Application.dataPath, "playerData2.json");
            File.WriteAllText(path, jsonData);
            Debug.Log("Save");
        }
        else

        if (ss == saveSlot.save3)
        {
            string jsonData = JsonUtility.ToJson(playerData, true);
            string path = Path.Combine(Application.dataPath, "playerData3.json");
            File.WriteAllText(path, jsonData);
            Debug.Log("Save");
        }
        else

        if (ss == saveSlot.save4)
        {
            string jsonData = JsonUtility.ToJson(playerData, true);
            string path = Path.Combine(Application.dataPath, "playerData4.json");
            File.WriteAllText(path, jsonData);
            Debug.Log("Save");
        }
        else

        if (ss == saveSlot.save5)
        {
            string jsonData = JsonUtility.ToJson(playerData, true);
            string path = Path.Combine(Application.dataPath, "playerData5.json");
            File.WriteAllText(path, jsonData);
            Debug.Log("Save");
        }
    }

    public void LoadGameData()
    {
        if (ss == saveSlot.save1)
        {
            string path = Path.Combine(Application.dataPath, "playerData1.json");
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log("Load");
            }
            else

            {
                NewGame();
            }
        }
        else

        if (ss == saveSlot.save2)
        {
            string path = Path.Combine(Application.dataPath, "playerData2.json");
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log("Load");
            }
            else

            {
                NewGame();
            }
        }
        else

        if (ss == saveSlot.save3)
        {
            string path = Path.Combine(Application.dataPath, "playerData3.json");
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log("Load");
            }
            else

            {
                NewGame();
            }
        }
        else

        if (ss == saveSlot.save4)
        {
            string path = Path.Combine(Application.dataPath, "playerData4.json");
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log("Load");
            }
            else

            {
                NewGame();
            }
        }
        else

        if (ss == saveSlot.save5)
        {
            string path = Path.Combine(Application.dataPath, "playerData5.json");
            if (File.Exists(path))
            {
                string jsonData = File.ReadAllText(path);
                playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log("Load");
            }
            else

            {
                NewGame();
            }
        }
    }

    public void NewGame()
    {
        Debug.Log("New Game");
        playerData.map = 1;
        playerData.soul = 30;
        SaveGameData();
        LoadGameData();
    }

    

}

[System.Serializable]
public class PlayerData
{
    public int soul = 32;
    public int HP = 100;
    public String andf;
    public int map;
    
}
