using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    
    public Color TeamColor;

    private const string FILENAME = "savefile.json";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadColor();
    }

    [Serializable]
    private class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(GetPersistantDataPath(), json);
    }

    public void LoadColor()
    {
        string path = GetPersistantDataPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }

    private string GetPersistantDataPath()
    {
        return String.Format("{0}/{1}", Application.persistentDataPath, FILENAME);
    }
}
