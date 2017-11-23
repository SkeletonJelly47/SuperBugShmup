using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int energy;
	public float Speed;
	[SerializeField]
	float startingSpeed, maxSpeed, accelerationValue, movementGraceTimer;
	float timer;
	public GameObject PlayerProjectileSuperShot;
	float verticalInput, horizontalInput;
	Vector3 dir;
	[SerializeField]
	int health;

	public int Health
	{
		get
		{
			return health;
		}

		set
		{
			health = value;
			Debug.Log("HP: " + health);
			if (health < 0)
			{
				DestroySelf();
			}
		}
	}

	public int Energy
	{
		get
		{
			return energy;
		}
		set
		{

			energy = value;

			if (energy > 100)
			{
				energy = 100;
			}

		}
	}

	// Use this for initialization
	void Start()
	{
		dir = Vector3.forward;
	}

	// Update is called once per frame
	void Update()
	{
		//Retrieve inputs
		GetInputs();
		//Assign input values

		dir.x = horizontalInput;
		dir.y = 0;
		dir.z = verticalInput;
		if (horizontalInput != 0 || verticalInput != 0)
		{
			Delay();
		}
		else if (dir.x == 0 && dir.z == 0)
		{
			timer += Time.deltaTime;
			if (timer > movementGraceTimer)
			{
				Speed = startingSpeed;
				timer = 0;
			}

		}
		if (Speed > startingSpeed)
		{

		}
		//Normalize for constant diagonal speeds
		dir.Normalize();

		//Apply movement
		Move(dir);

		if (Input.GetButtonDown("Fire2"))
		{
			if (energy == 100)
			{
				energy = 0;
				Instantiate(PlayerProjectileSuperShot, transform.position, transform.rotation);

			}
		}
	}

	void Move(Vector3 dir)
	{
		transform.position += dir * Speed * Time.deltaTime;
	}

	void GetInputs()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");
	}

	public void TakeDamage(int damage)
	{
		Health -= damage;
	}

	void DestroySelf()
	{
		Destroy(gameObject);
		Debug.Log("Player Died");
	}
	void Delay()
	{
		Speed = Speed + (accelerationValue / 100);
		if (Speed >= maxSpeed)
		{
			Speed = maxSpeed;
		}
	}
}
