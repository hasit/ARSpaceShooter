using UnityEngine;
using System.Collections;

public class DestroyByCollecting : MonoBehaviour {

	public GameObject explosion;
	private int healthValue = 10;
	private GameController gameController;
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

		if (other.tag == "3Dbullet") 
		{
			return;
		}

		if (other.tag == "Player" && gameObject.tag == "HealthOrb") {
			Instantiate(explosion, transform.position, transform.rotation);
			playerStat.AddHealth(healthValue);
			//audio.Play();
		}

		if (other.tag == "Player" && gameObject.tag == "ShieldOrb") {
			Instantiate(explosion, transform.position, transform.rotation);
			playerStat.ShieldSwitch();
			//audio.Play();
		}
		Destroy (gameObject);//destroy object the script attached to
	}
}
