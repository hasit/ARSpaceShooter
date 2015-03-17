using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour 
{
	void OnTriggerExit (Collider other) 
	{
		//auto destroy objects that leaves the game volume
		Destroy(other.gameObject);
	}
}
