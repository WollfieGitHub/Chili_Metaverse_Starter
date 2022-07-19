using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int Score { private set; get; } = 0;
    public int LastHitScore { private set; get; } = 0;

    public int VolleyId { private set; get; } = 0;

    public void NewVolley()
    {
        VolleyId++;
        LastHitScore = 0;
        Score = 0;
    }

    public void NewHit(int score)
    {
        Score += score;
        LastHitScore = score;
    }
}
