using System.IO;
using UnityEngine;

public static class SaveManager 
{
    public static string directory = "/SaveData/";
    public static string fileName = "MyData.txt";

    public static void Save(Data data)
    { 
        string dir = Application.persistentDataPath + directory;
        if (!Directory.Exists(dir)) 
        { 
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(data); 
        File.WriteAllText(dir + fileName, json);
    
    }

    public static Data Load()
    { 
        string path = Application.persistentDataPath + directory + fileName;
        Data data = new Data();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<Data>(json);
        }
        else
        {
            Debug.Log("save file does not exist");
        }
        
        return data;



    }
}
