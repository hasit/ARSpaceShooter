using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Slider shieldSlider;
	public int currentShield;
	public GameObject shieldOnImg;
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 1f);     // The colour the damageImage is set to, to flash.

	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.
	bool shieldOn = false;

	private GameController gameController;
	public GameObject player;
	public Collider playerPower;
	private float shieldTime = 10.0f;
	private float countDownTime;

	void Awake ()
	{
		//float time = shieldTime;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		} 
	
		if (gameController == null){
			Debug.Log ("Cannot find GameController");
		}
		GameObject playerPowerObject = GameObject.FindWithTag ("Player");
		if (playerPowerObject != null) {
			playerPower = playerPowerObject.GetComponent <Collider> ();
		} 
		
		if (playerPowerObject == null){
			Debug.Log ("Cannot find playerPowerObjects");
		}
		// Set the initial health of the player.
		currentHealth = startingHealth;
		currentShield = 0;
	}
	
	void Update ()
	{
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		
		// Reset the damaged flag.
		damaged = false;

		//
		if (shieldOn) {
			playerPower.enabled = false;
			shieldOnImg.SetActive(true);

			if(countDownTime > 0){
				countDownTime -= Time.deltaTime;
				currentShield = (int)countDownTime;
			} else {
				shieldOn = false;
				playerPower.enabled = true;
				shieldOnImg.SetActive(false);
			}
		} 
		shieldSlider.value = currentShield;
		//shieldTimeText.text = ""+ (int)countDownTime;
		//
	}
	
	public void TakeDamage (int amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;
		
		// Reduce the current health by the damage amount.
		currentHealth -= amount;
		
		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;
		
		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
			gameController.GameOver ();
			DestroyObject(player);
		}
	}

	public void ShieldSwitch(){
		shieldOn = true;
		countDownTime = shieldTime;
	}

	public void AddHealth (int amount){
		if (currentHealth < startingHealth) {
			currentHealth += amount;
			healthSlider.value = currentHealth;
		}
	}
	
	void Death ()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;
		//gameController.GameOver ();
	}       
}