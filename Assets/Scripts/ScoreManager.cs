using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
	public static float score;
	public GameController gameOver;
	string highScoreKey = "ClassicHighScore";
	string highScoreSurvivalKey = "SurvivalHighScore";
	public GameObject highestScore;

	int oldScore;
	Text newHighScore;
	Text text;

	void Awake ()
	{
		text = GetComponent <Text> ();
		score = 0;

		newHighScore = highestScore.GetComponent <Text>(); ;
	}

	void Update()
	{
		if (!gameOver.gameOver) {
			//score by time
			score += Time.deltaTime;
			text.text = "Score: " + (int)score;
		} else {
			if(Application.loadedLevel == 1)
				oldScore = PlayerPrefs.GetInt(highScoreKey);
			if(Application.loadedLevel == 2) 
				oldScore = PlayerPrefs.GetInt(highScoreSurvivalKey);

			if((int)score > oldScore){
				if(Application.loadedLevel == 1)
					PlayerPrefs.SetInt(highScoreKey, (int)score);
				if(Application.loadedLevel == 2)
					PlayerPrefs.SetInt(highScoreSurvivalKey, (int)score);

				PlayerPrefs.Save();
			}

			if(Application.loadedLevel == 1)
				newHighScore.text = "Highest Score: " + PlayerPrefs.GetInt(highScoreKey);
			if(Application.loadedLevel == 2)
				newHighScore.text = "Highest Score: " + PlayerPrefs.GetInt(highScoreSurvivalKey);
		}
	}
}
