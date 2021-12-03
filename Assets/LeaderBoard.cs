using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    public ScoreBoardManager scoreboardController = null;

    public TMPro.TextMeshProUGUI userName = null;
    public TMPro.TextMeshProUGUI userScore = null;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreboardController != null)
        {
            List<HighScore> scoreList = scoreboardController.LoadScores();
            for (int i = 0; i < scoreList.Count; i++)
            {
                userName = GameObject.Find("Name" + i).GetComponent<TMPro.TextMeshProUGUI>();
                userScore = GameObject.Find("Score" + i).GetComponent<TMPro.TextMeshProUGUI>();
                userName.text = scoreList[i].getName();
                userScore.text = scoreList[i].getScore().ToString();

            }
        }
    }
}
