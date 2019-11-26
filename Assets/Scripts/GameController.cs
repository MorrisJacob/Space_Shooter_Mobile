using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public Text scoreText;
    //public Text restartText;
    public Text gameOverText;

    private int score;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public GameObject restartButton;

    private bool gameOver;
    private bool restart;

    private void Start()
    {
        score = 0;
        gameOver = false;
        restart = false;
        //restartText.text = "";
        restartButton.SetActive(false);
        gameOverText.text = "";
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    //private void Update()
    //{
    //    if (restart)
    //    {
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            SceneManager.LoadScene(Application.loadedLevel);
    //        }
    //    }
    //}

    IEnumerator SpawnWaves()
    {
        //wait a bit before sending the first hazard
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                //get positions from unity. random x value based on range
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                //no rotation
                Quaternion spawnRotation = Quaternion.identity;

                //instantiate our hazards
                Instantiate(hazard, spawnPosition, spawnRotation);

                //wait before we spawn the next hazard
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartButton.SetActive(true);
                //restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
