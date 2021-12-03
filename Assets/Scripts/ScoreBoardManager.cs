using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class ScoreBoardManager : MonoBehaviour
{
    public static ScoreBoardManager instance;
    public ScoreBoard scoreBoard;

    //Make sure the high score directory exists


    void Awake()
    {
        instance = this;
        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }
    }


    public void SaveScore(string userName, int chaosScoreValue, string gameTime)
    {
        if (scoreBoard.scoresList.Count == 0)
        {
            scoreBoard.addScore(userName, chaosScoreValue, gameTime);
        }
		else
		{
            bool foundSpot = false;
            HighScore temp = new HighScore();
            for (int i = 0; i < scoreBoard.scoresList.Count; i++)
			{
                if (foundSpot)
                {
                    //insert higher score
                    scoreBoard.scoresList[i] = temp;
                    //move lower score
                    temp = scoreBoard.scoresList[i];
                }
                if (chaosScoreValue > scoreBoard.scoresList[i].getScore())
				{
                    temp = scoreBoard.scoresList[i];
                    scoreBoard.scoresList[i] = new HighScore(userName, chaosScoreValue, gameTime);
				}
			}
            if (scoreBoard.scoresList.Count < 5)
			{
                if (foundSpot)
				{
                    scoreBoard.scoresList.Add(temp);
				}
				else
				{
                    scoreBoard.addScore(userName, chaosScoreValue, gameTime);
                }
			}
		}
        XmlSerializer serializer = new XmlSerializer(typeof(ScoreBoard));
        FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/highscores.xml", FileMode.Create);
        Debug.Log("Path: " + Application.persistentDataPath + "/HighScores/highscores.xml");
        serializer.Serialize(stream, scoreBoard);
        stream.Close();
    }

    public List<HighScore> LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/HighScores/highscores.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ScoreBoard));
            FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/highscores.xml", FileMode.Open);
            scoreBoard = serializer.Deserialize(stream) as ScoreBoard;
        }

        return scoreBoard.scoresList;
    }
}

[System.Serializable]
public class ScoreBoard
{
    public List<HighScore> scoresList = new List<HighScore>();
    //add a score to the list
    public void addScore(string userName, int chaosScoreValue, string gameTime)
	{
        scoresList.Add(new HighScore(userName, chaosScoreValue, gameTime));
	}
}

[System.Serializable]
public class HighScore
{
    public int highScore;
    public string userName;
    public string gameTime;
    public HighScore()
    {
        userName = "ANONYMOUS";
        highScore = 500;
        gameTime = "00:00:00";
    }

    public HighScore(string name, int score, string time)
    {
        userName = name;
        highScore = score;
        gameTime = time;
    }
    public int getScore()
    {
        return highScore;
    }
    public string getName()
    {
        return userName;
    }
    public string getTime()
    {
        return gameTime;
    }
}