using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    [SerializeField] GameObject scorePanel;

    private int score;
    public TMP_Text scoreText;

    private int highscore;
    public TMP_Text highscoreText;

    //just for disable them
    public TMP_Text gameScoreText;
    public TMP_Text gameSpeedText;
    public TMP_Text gameSizeText;


    public void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);
        score = 0;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //sub
    public void OnEnable()
    {
        Player.playerDied += PlayerDied;
    }

    //unsub
    public void OnDisable()
   {
        Player.playerDied -= PlayerDied;
   }


    public void PlayerDied(int score)
    {
        this.score = score;
        highscore = Mathf.Max(score, highscore);
        PlayerPrefs.SetInt("HighScore", highscore);

        Invoke("ScorePanel", 1.5f);
    }
    private void ScorePanel()
    {
        //disable 
        gameSpeedText.enabled = false;
        gameScoreText.enabled = false;
        gameSizeText.enabled = false;

        // set scores
        highscoreText.text = highscore + "";
        scoreText.text = score + "";

        scorePanel.SetActive(true);
    }

}
