using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text highScoreAndName;
    public TextMeshProUGUI currentPlayerName;
    public GameObject GameOverText;
    public GameObject newHighScore;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        //string playername = PersistantData.Instance.userName;

        if (string.IsNullOrWhiteSpace(PersistantData.Instance.userName))
        {
            currentPlayerName.SetText("Umm. you were supposed to enter a name Dopey");
        }
        else
        {
            
            currentPlayerName.SetText(PersistantData.Instance.userName);
        }

        highScoreAndName.text = "The Score to beat is:" + PersistantData.Instance.highScore + " Made by: " + PersistantData.Instance.highUserName;

         
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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
                PersistantData.Instance.LoadHighScore();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        //currentPlayerName.text = PersistantData.Instance.userName;
        


        
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (m_Points > PersistantData.Instance.highScore)
        {
        PersistantData.Instance.SaveHighScore(m_Points);
            newHighScore.SetActive(true);
            highScoreAndName.text = "The Score to beat is:" + m_Points + " Made by: " + PersistantData.Instance.userName;
        }

        

    }
}
