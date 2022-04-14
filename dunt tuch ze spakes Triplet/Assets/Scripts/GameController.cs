 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Score Field")]
    public Text deathScoreText;
    public Text scoreText;


    [Header("Canvases")]
    public GameObject GameScreen;
    public GameObject MenuScreen;

    [Header(" ")]
    public int score = 0;

    public GameObject playerPrefab;

    public void IncreaseScore(int n = 1) {
        score += n;
        ChangeText();
    }
    private void ChangeText() {
        if (score < 10) {
            scoreText.text = "0" + score.ToString();
        }
        else scoreText.text = score.ToString();
    }

    public void Replay() {
        GameScreen.SetActive(true);
        MenuScreen.SetActive(false);
        GameObject newplayer = Instantiate(playerPrefab, transform);
        newplayer.transform.position = new Vector2 (0.0f, 1.0f);
        score = 0;
    }
}

