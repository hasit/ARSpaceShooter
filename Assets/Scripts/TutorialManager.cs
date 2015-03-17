using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public GameObject[] tutorialImgs;
	public GameObject helpMenu;

	private bool openHelp = false;
	private GameObject currentObject;
	private int currentIndex;
	private GameObject nextObject;


	void Start() {

		//turn off everything but first picture
		for(int i = 0; i < tutorialImgs.Length; i++){
			if(i == 0)
				tutorialImgs [i].SetActive (true);
			else
				tutorialImgs [i].SetActive (false);
		}
		currentObject = tutorialImgs [0];
		currentIndex = 0;
	}
	// Use this for initialization
	public void NextTutorial () {
		currentObject.SetActive (false);

		if (currentIndex == tutorialImgs.Length - 1) {
			currentIndex = 0;
		} else {
			currentIndex++;
		}

		nextObject = tutorialImgs [currentIndex];
		nextObject.SetActive (true);
		currentObject = nextObject;
	}

	public void PreviousTutorial () {
		currentObject.SetActive (false);
		
		if (currentIndex == 0) {
			currentIndex = tutorialImgs.Length - 1;
		} else {
			currentIndex--;
		}
		
		nextObject = tutorialImgs [currentIndex];
		nextObject.SetActive (true);
		currentObject = nextObject;
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
}
