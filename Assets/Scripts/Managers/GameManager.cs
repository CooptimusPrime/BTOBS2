using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

//Need to update this to follow the event design

//All health components should be linked to the game manager with their OnDeath
public class GameManager : MonoBehaviour
{
    public float SurvivalTime;
    public int Score;

    //track kills by enemy type
    //track pickups used and broken by each type

    [SerializeField] Canvas InGameUI;

    //References
    EnemyManager EM;
    
    //Game Over
    [Header("Game Over State")]
    [SerializeField] TMP_Text ScoreText;

    [SerializeField] TMP_Text MinText;
    [SerializeField] TMP_Text SecText;
    [SerializeField] TMP_Text FinalScoreText;

    [SerializeField] LineRenderer LineL;
    [SerializeField] LineRenderer LineR;

    [SerializeField] XRInteractorLineVisual XRL;
    [SerializeField] XRInteractorLineVisual XRR;

    [SerializeField] GameObject WepL;
    [SerializeField] GameObject WepR;

    [SerializeField] TMP_Text BestMin;
    [SerializeField] TMP_Text BestSec;
    [SerializeField] TMP_Text BestScore;

	[SerializeField] Canvas GameOverMenu;

	void Start()
	{
		EM=FindFirstObjectByType<EnemyManager>();
	}

	void Update()
	{
        //make it harder over time with more max enemies
        SurvivalTime += Time.deltaTime;
        float remainder = SurvivalTime%120;
        int intervals = (int)((SurvivalTime - remainder) / 120);  
        EM.Intervals= intervals;
	}

    public void AddScore(int add)
    {
        Score += add;
        ScoreText.text = Score.ToString();
    }

	public void GameOver()
	{
		Time.timeScale = 0f;
		int duration = Mathf.RoundToInt(SurvivalTime);
		int minutes = Mathf.FloorToInt(duration / 60);
		int seconds = duration % 60;

		WepL.SetActive(false);
		WepR.SetActive(false);

		LineL.enabled = true;
		LineR.enabled = true;

		XRL.enabled = true;
		XRR.enabled = true;

		//disable the background audio
		//maybe play some new audio

		InGameUI.gameObject.SetActive(false);
		GameOverMenu.gameObject.SetActive(true);

		//update minutes, seconds, and score texts
		MinText.text = minutes.ToString();

        if (seconds<10)
		    SecText.text = "0"+seconds.ToString();
        else
            SecText.text = seconds.ToString();

		FinalScoreText.text = Score.ToString();

		//check playerprefs for best score and time
		int PreviousBestMin = PlayerPrefs.GetInt("min", 0);
		int PreviousBestSec = PlayerPrefs.GetInt("sec", 0);
		int PreviousBestScore = PlayerPrefs.GetInt("score", 0);

		//compare to the current ones and overwrite if need be
		if (minutes > PreviousBestMin)
		{
			if (seconds > PreviousBestSec)
			{
				PlayerPrefs.SetInt("min", minutes);
				PlayerPrefs.SetInt("sec", seconds);
				BestMin.text = minutes.ToString();
				BestSec.text = seconds.ToString();
				//NewBestTime.SetActive(true);
			}
		}
		else
		{
			BestMin.text = PreviousBestMin.ToString();
			BestSec.text = PreviousBestSec.ToString();
		}

		if (Score > PreviousBestScore)
		{
			PlayerPrefs.SetInt("score", Score);
			BestScore.text = Score.ToString();
			//NewBestScore.SetActive(true);
		}
		else
		{
			BestScore.text = PreviousBestScore.ToString();
		}
	}

	public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
