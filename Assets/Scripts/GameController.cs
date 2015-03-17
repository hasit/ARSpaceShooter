using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	//spawn hazards
	public GameObject[] hazards;
	public GameObject[] items;

	public Vector3 spawnValues;
	public int hazardCount; 
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public bool gameOver;
	//private bool restart;
	private bool pauseGame = false;
	private bool openHelp = false;
	private int itemChance;

	public GameObject restartButton;
	public GameObject pauseButton;
	public GameObject quitLevelButton;
	public GameObject pauseScreen;
	public GameObject helpMenu;
	public GameObject healthUi;
	public GameObject gameOverScene;

	void Start ()
	{
		gameOver = false;
		//restart = false;
		restartButton.SetActive(false);
		quitLevelButton.SetActive (false);
		pauseButton.SetActive(true);
		pauseScreen.SetActive(false);

		if(Application.loadedLevel == 1)
			healthUi.SetActive (true);
		else
			healthUi.SetActive (false);

		gameOverScene.SetActive (false);
		Time.timeScale = 1;

		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds(startWait);

		while(true){
			for (int i = 0; i < hazardCount; i++){
				//random generate out of the hazards array
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				//Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				//random pick a item
				itemChance = Random.Range(0,20);
				if(itemChance < items.Length){
					GameObject item = items [itemChance];

					Vector3 itemPosition = new Vector3 (Random.Range(-spawnValues.x+2, spawnValues.x-2), Random.Range(-spawnValues.y+2, spawnValues.y-2), spawnValues.z);
					Quaternion itemRotation = Quaternion.identity;
					Instantiate (item, itemPosition, itemRotation);
				}

				yield return new WaitForSeconds(spawnWait);
				if(gameOver){
					break;
				}
			}
			yield return new WaitForSeconds(waveWait);

			if(gameOver)
			{
				pauseButton.SetActive(false);
				restartButton.SetActive(true);
				quitLevelButton.SetActive(true);
				healthUi.SetActive (false);
				gameOverScene.SetActive (true);
				//restart = true;
				break;
			}
		}
	}

	public void GameOver()
	{
		gameOver = true;
		/*
		pauseButton.SetActive(false);
		restartButton.SetActive(true);
		quitLevelButton.SetActive(true);
		healthUi.SetActive (false);
		*/
	}

	public void RestartGame (){
		Application.LoadLevel (Application.loadedLevel);
	}

	public void PauseGame(){
		if (pauseGame) {
			Time.timeScale = 0;
			pauseScreen.SetActive(true);
			pauseButton.SetActive(false);
			healthUi.SetActive(false);
			pauseGame = false;
		} else {
			Time.timeScale = 1;
			pauseScreen.SetActive(false);
			pauseButton.SetActive(true);
			if(Application.loadedLevel == 1)
				healthUi.SetActive(true);
			pauseGame = true;
		}
	}

	public void OpenHelp(){
		if (openHelp) {
			helpMenu.SetActive(true);
			openHelp = false;
		} else {
			helpMenu.SetActive(false);
			openHelp = true;
		}
	}

	public void LoadScene(int level)
	{
		Application.LoadLevel (level);
	}
}
