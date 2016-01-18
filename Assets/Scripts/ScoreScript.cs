using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
    public static ScoreScript instance = null;
    private Text scoreText;
    public int actualScore;
	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        actualScore = GameManager.instance.score;
	}

    public void IncreaseScore(int score) {
        int value = actualScore + score;
        scoreText.text = "" + value;
    }

    public void SetScore(int score) {
        scoreText.text = "" + score;
    }
	// Update is called once per frame
	void Update () {
        actualScore = GameManager.instance.score;
        SetScore(actualScore);
	}
}
