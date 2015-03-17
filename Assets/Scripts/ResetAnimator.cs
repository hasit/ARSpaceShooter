using UnityEngine;
using System.Collections;

public class ResetAnimator : MonoBehaviour {

	public Animator animator; 
	// Use this for initialization
	void Start () {
		animator.Play ("Normal");
	}
}
