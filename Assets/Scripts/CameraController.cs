using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	WebCamTexture mCamera;
	WebCamDevice[] devices = WebCamTexture.devices;
	Quaternion baseRotation;

	public GameObject plane;
	
	// Use this for initialization
	void Start ()
	{
		mCamera = new WebCamTexture ();
		baseRotation = transform.rotation;

		if (devices.Length > 0) {
			mCamera.deviceName = devices [1].name;
		} else {
			mCamera.deviceName = devices [0].name;
		}

		plane.renderer.material.mainTexture = mCamera;
		mCamera.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = baseRotation * Quaternion.AngleAxis (mCamera.videoRotationAngle, Vector3.up);
	}
}