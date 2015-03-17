using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchFire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	
	private bool touched;
	private int pointerID;
	private bool canFire;

	void Awake(){
		touched = false;
	}
	
	public void OnPointerDown (PointerEventData data){
		//set start point 
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			canFire = true;
		}
	}	
	
	public void OnPointerUp (PointerEventData data){
		//reset everything 
		if (data.pointerId == pointerID) {
			canFire = false;
			touched = false;
		}
	}

	public bool CanFire(){
		return canFire;
	}
}
