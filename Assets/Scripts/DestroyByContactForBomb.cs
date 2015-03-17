using UnityEngine;
using System.Collections;

public class DestroyByContactForBomb : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;
	
	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} 
		
		if (gameController == null){
			Debug.Log ("Cannot find GameController");
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
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();

			Destroy (other.gameObject);
			Destroy (gameObject);//destroy object the script attached to
		}
	}
}
