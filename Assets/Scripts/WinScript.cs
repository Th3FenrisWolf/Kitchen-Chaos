using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    public Text chaosScore;
    public Text gameTime;
    public Text userName;
    public Text winText;
    public GameObject GetMoreChaosObj;
    public ScoreBoardManager scoreboardController = null;

    bool won = false;

    private int winThreshold = 500;
    private void OnTriggerEnter(Collider other)
    {
        int chaosScoreValue = int.Parse(chaosScore.text.Split(':')[1]);
        if (chaosScoreValue >= winThreshold && !won)
        {
            Debug.Log("You Won!");
            won = true;

            // pause game
            Time.timeScale = 0;
            // show win text
            winText.enabled = true;
            winText.transform.parent.GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);
            if (scoreboardController != null)
			{
                scoreboardController.SaveScore(userName.text, chaosScoreValue, gameTime.text);
            }
        } else {
            GetMoreChaosObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        GetMoreChaosObj.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        

        if (won) {
            if (Input.GetKey(KeyCode.Return)) {
                // reload scene
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }

            if (Input.GetKey(KeyCode.Escape)) {
                // go to the main screen
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
            }
        }


        
    }
}
