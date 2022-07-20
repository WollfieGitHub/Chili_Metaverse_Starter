using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        Instance = this;
        score = 0;
        lastHitScore = 0;
        volleyScore = 0;
        volleyId = 1;
        nbArrowsInVolley = 0;
        nbTotalArrows = 0;
    }

    public int score;
    public int nbTotalArrows;

    public int volleyId;
    public int volleyScore;
    public int lastHitScore;
    public int nbArrowsInVolley;

    public void NewVolley()
    {
        volleyId++;
        volleyScore = 0;
        lastHitScore = 0;
        score = 0;
        nbArrowsInVolley = 0;
    }

    public void NewHit(int hitScore)
    {
        score += hitScore;
        volleyScore += hitScore;
        nbArrowsInVolley++;
        nbTotalArrows++;
        lastHitScore = hitScore;
    }
}
