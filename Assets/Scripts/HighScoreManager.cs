using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreManager : MonoBehaviour {

	string highScoreKey = "ClassicHighScore";
	string highScoreSurvivalKey = "SurvivalHighScore";
	public Text classicScoreText;
	public Text survivalScoreText;

	// Use this for initialization
	void Start () {
		classicScoreText.text = "Classic: " + PlayerPrefs.GetInt(highScoreKey);
		survivalScoreText.text = "Survival: " + PlayerPrefs.GetInt(highScoreSurvivalKey);
	}
}
