using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    public ScoreBoardManager scoreboardController = null;

    public TMPro.TextMeshPro userName = null;
    public TMPro.TextMeshPro userScore = null;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreboardController != null)
        {
            List<HighScore> scoreList = scoreboardController.LoadScores();
            for (int i = 0; i < 1; i++)
            {
                //userName = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
                //userName = GameObject.Find("Name" + i).GetComponent<Text>();
                //userScore = GameObject.Find("Score" + i).GetComponent<Text>();
                //userName = scoreList[i].getName();
            }
        }
    }
}
