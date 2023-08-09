using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public GameObject pauseScreen;
    private bool paused;

    public bool isGameActive;

    public int lives;
    private int score;
    private float spawnRate = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
    }
    public void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }    
    // Update is called once per frame
    void Update()
    {
        // Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }
    // If the game is active, Game Manager spawns targets
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    // Update the score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Update the lives
    public void UpdateLives(int livesToExtract)
    {
        lives -= livesToExtract;
        livesText.text = "Lives: " + lives;
    }
    // Game Over method
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }
    // Restart method using the name of the current scene
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        // Start game and set difficulty
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        // Start spawning and set score 0 and lives 3
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        // Get rid of title screen
        titleScreen.SetActive(false);
    }
}
