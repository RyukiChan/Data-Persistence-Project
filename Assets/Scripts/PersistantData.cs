using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PersistantData : MonoBehaviour
{

    public static PersistantData Instance;
    public string userName;
    public int highScore;
    public string highUserName;


    private void Awake()
    {
        if (Instance !=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string userName;
        public int highScore;
    }

    public void SaveHighScore(int newHigh)
    {
        SaveData data = new SaveData();
        data.userName = userName;
        data.highScore = newHigh;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("Writing Something");
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

             highScore = data.highScore;
            highUserName = data.userName;
        }
    }

}
