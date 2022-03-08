using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MenuManager : MonoBehaviour
{
    /*
     TODO:
    1- save score
   
     */

    public MenuManager Instance;
    public MainManager MainInstance;

    public Text topPlayerNameText;
    public Text topScore;
    public InputField playerInputField;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    public void SaveData()
    { 
        Data save = new Data();
        save.Name += playerInputField.text;
        //save.Score = MainInstance.m_Points;

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/SavedData.json" , json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/SavedData.json";
        if (File.Exists(path))
        { 
        string json = File.ReadAllText(path);
            Data load = JsonUtility.FromJson<Data>(json);

            topPlayerNameText.text = "TPlayer Name: " + load.Name;
            topScore.text = "TTop Score: " + load.Score;
        }


    }

    public void SaveName()
    {
        SaveData();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else

        Application.Quit();
#endif
    }
}


