using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Text topPlayerNameText;
    public Text topScore;
    public InputField playerInputField;

    private void Update()
    {
        topPlayerNameText.text = "Player Name: " + PlayerPrefs.GetString("TopPlayerName");
        topScore.text = "Top Score: " + PlayerPrefs.GetInt("HighScore").ToString();
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

    public void ResetTopScore()
    {
        PlayerPrefs.DeleteAll();
    }
}


