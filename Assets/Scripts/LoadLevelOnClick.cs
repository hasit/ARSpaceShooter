using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLevelOnClick : MonoBehaviour {

	public void LoadScene(int level)
	{
		Application.LoadLevel (level);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
