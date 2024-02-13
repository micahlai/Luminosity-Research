using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreManagerSkyRise : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;

    public int scoreNum;
    public int livesNum;

    float scoreNumDisplay;

    public void updateScore(int value)
    {
        scoreNum += value;
        
    }
    public void updateLives()
    {
        livesNum -= 1;
        lives.text = livesNum.ToString() + " lives left";
    }
    private void Update()
    {
        scoreNumDisplay = Mathf.Lerp(scoreNumDisplay, scoreNum, Time.deltaTime * 5);
        score.text = Mathf.RoundToInt(scoreNumDisplay).ToString() + " ft.";
    }
    private void Start()
    {
        livesNum = 3;
        lives.text = livesNum.ToString() + " lives left";
        scoreNum = 0;
        scoreNumDisplay = 0;
    }
}
