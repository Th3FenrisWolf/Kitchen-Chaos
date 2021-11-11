using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
    public Text chaosScore;
    public Text winText;

    bool won = false;

    private int winThreshold = 500;

    // Update is called once per frame
    void Update() {
        int chaosScoreValue = int.Parse(chaosScore.text.Split(':')[1]);

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


        if (chaosScoreValue >= winThreshold && !won) {
            won = true;

            // pause game
            Time.timeScale = 0;
            // show win text
            winText.enabled = true;
            winText.transform.parent.GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);
        }
    }
}
