using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary; 

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	private Quaternion calibrationQuaternion;
	public SimpleTouchFire touchFire;

	void Start(){
		CalibrateAccelerometer ();//could start by pressing button
	}

	void Update ()
	{
		/*/
		#if UNITY_EDITOR
		//regular input

		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);// as GameObject;
			audio.Play();
		}
		#endif
		/*/
		#if UNITY_WP_8_1
		//use with touch input
		if (touchFire.CanFire() && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);// as GameObject;
			audio.Play();
		}	
		#endif

		#if UNITY_ANDROID
		//use with touch input
		if (touchFire.CanFire() && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);// as GameObject;
			audio.Play();
		}	
		#endif

		//
	}

	void FixedUpdate ()//physic update step function
	{
		//
		#if UNITY_EDITOR
			//regular keyboard input
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		#endif
		//
		#if UNITY_WP_8_1
			////////////////////
			//use with accelerometer
			Vector3 accelerationRaw = Input.acceleration;
			Vector3 acceleration = FixedAcceleration (accelerationRaw);
			//Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
			Vector3 movementPhone = new Vector3 (acceleration.x, -acceleration.y, 0.0f);
		#endif

		#if UNITY_ANDROID
		////////////////////
		//use with accelerometer
		Vector3 accelerationRaw = Input.acceleration;
		Vector3 acceleration = FixedAcceleration (accelerationRaw);
		//Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);
		Vector3 movementPhone = new Vector3 (acceleration.x, -acceleration.y*3, 0.0f);
		#endif
		//
		#if UNITY_EDITOR
			rigidbody.velocity = movement * speed; //control speed of game object
		#endif
		//
		#if UNITY_WP_8_1
			rigidbody.velocity = movementPhone * speed; //control speed of game object
		#endif

		#if UNITY_ANDROID
		rigidbody.velocity = movementPhone * speed; //control speed of game object
		#endif
		//
		//keep player in bound
		rigidbody.position = new Vector3 
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				//0.0f, 
				Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax),
				Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)//,
				//0.0f
			);

		//rigidbody.rotation = Quaternion.Euler (rigidbody.velocity.z * tilt, 0.0f, rigidbody.velocity.x * -tilt);
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

	//calibrate the acceleration input
	void CalibrateAccelerometer(){
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}
	//get the calibrated value from input
	Vector3 FixedAcceleration(Vector3 acceleration){
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

}
