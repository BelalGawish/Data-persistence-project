using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text topScoreText;
    public GameObject GameOverText;
    public InputField playerInputField;
    public GameObject playerInputPanel;
    public GameObject errorText;

    private bool m_Started = false;
    public int m_Points;
    public string input;

    private bool m_GameOver = false;

    public static MainManager mainManager;

    private void Awake()
    {
        if (mainManager != null)
        {
            Destroy(mainManager);
            return;
        }
        mainManager = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        errorText.gameObject.SetActive(false);

        topScoreText.text = PlayerPrefs.GetString("TopPlayerName") + " has high Score of " + PlayerPrefs.GetInt("HighScore").ToString();

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);

            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{m_Points}";

    }

    public void GameOver()
    {
        CheckScore();

        m_GameOver = true;
        GameOverText.SetActive(true);

    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void CheckScore()
    {
        Debug.Log("Your score is: " + m_Points + " Saved score: " + PlayerPrefs.GetInt("HighScore"));

        if (m_Points > PlayerPrefs.GetInt("HighScore"))
        {
            playerInputPanel.gameObject.SetActive(true);
            SaveTopScore();
        }

    }

    public void SaveInputField(string s)
    {
        input = s;
        Debug.Log(input);
    }

    public void SaveTopScore()
    {
        if (string.IsNullOrEmpty(playerInputField.text))
        {
            errorText.gameObject.SetActive(true);
        }

        else
        {
            errorText.gameObject.SetActive(false);
            PlayerPrefs.SetInt("HighScore", m_Points);
            PlayerPrefs.SetString("TopPlayerName", input);
            topScoreText.text = PlayerPrefs.GetString("TopPlayerName") + " has high Score of " + PlayerPrefs.GetInt("HighScore").ToString();
            playerInputPanel.gameObject.SetActive(false);
        }
    }
}
