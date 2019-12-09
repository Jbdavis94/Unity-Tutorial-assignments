using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController2 : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject gameOverUI;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text ScoreText;
    public Text CountDownText;

    private bool gameOver;
    private bool restart;
    private bool win;
    private bool titlescreen;
    private int score;
    private int time;
    private AudioSource audioSource;

    void Start()
    {
        gameOver = false;
        restart = false;
        win = false;
        titlescreen = false;
        score = 0;
        time = 30;
        UpdateScore();
        UpdateTime();
        StartCoroutine(SpawnWaves());
    }
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Main Game");
            }
        }
        if (titlescreen)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("Title Menu");
            }
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restart = true;
                titlescreen = true;
                win = false;
                break;
            }
            yield return new WaitForSeconds(waveWait);
            if (win)
            {
                restart = true;
                titlescreen = true;
                gameOver = false;
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 210)
        {
            restart = true;
            titlescreen = true;
            win = true;
        }
    }
    void UpdateTime()
    {
        CountDownText.text = "Time: " + time;
        if (time <= 0)
        {
            gameOver = true;
            restart = true;
            titlescreen = true;
            gameOverUI.SetActive(true);
        }
    }
    public void GameLost()
    {
        gameOver = true;
        win = false;
        restart = true;
        gameOverUI.SetActive(true);
    }
}
