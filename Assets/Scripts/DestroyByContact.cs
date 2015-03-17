using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private GameController gameController;
	public int attackDamage = 10;               // The amount of health taken away per attack.
	PlayerHealth playerStat; 

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} 

		if (gameController == null){
			Debug.Log ("Cannot find GameController");
		}

		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) {
			playerStat = playerObject.GetComponent <PlayerHealth> ();
		} 
		
		if (playerStat == null){
			Debug.Log ("Cannot find PlayerHealth");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary") 
		{
			return;
		}

		if (other.tag == "Player") {

			Instantiate(explosion, transform.position, transform.rotation);
			if(Application.loadedLevel == 1)
				playerStat.TakeDamage(attackDamage);

			if(Application.loadedLevel == 2){
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				gameController.GameOver();
			}
		}

		if (other.tag == "3Dbullet") {
			Instantiate(explosion, transform.position, transform.rotation);
			ScoreManager.score += scoreValue;
		}

		if(Application.loadedLevel == 2)
			Destroy (other.gameObject);

		Destroy (gameObject);//destroy object the script attached to
	}
}
