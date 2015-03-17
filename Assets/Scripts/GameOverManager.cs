using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public GameController gc;
	Animator anim;

	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gc.gameOver) {
			anim.SetTrigger("GameOver");
		}
	}
}
