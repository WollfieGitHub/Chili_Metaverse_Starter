using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI _scoreDisplay;
    private string _defaultText;
    
    void Start()
    {
        _scoreDisplay = GetComponentInChildren<TextMeshProUGUI>();
        _defaultText = _scoreDisplay.text;
    }
    
    void Update()
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        
        _scoreDisplay.text = "Scoreboard : " +
        $"\nVolley #{scoreManager.volleyId} : " +
        $"\n\t- Number of Arrows shot : {scoreManager.nbArrowsInVolley}" +
        $"\n\t- Last arrow score : {scoreManager.lastHitScore}/10 pts" +
        $"\n\t- Volley Score : {scoreManager.volleyScore} pts" +
        $"\nScore over all volleys : {scoreManager.score} pts" +
        $"\nNumber of arrows shot : {scoreManager.nbTotalArrows}";
    }
}
