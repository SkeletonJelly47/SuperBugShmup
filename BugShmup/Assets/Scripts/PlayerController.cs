﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public static int energy;
	public float Speed;
	[SerializeField]
	float startingSpeed, maxSpeed, accelerationValue, movementGraceTimer;
	float timer;
	public GameObject PlayerProjectileSuperShot;
    public ParticleSystem HitParticle;
	float verticalInput, horizontalInput;
	Vector3 dir;
	[SerializeField]
	int health;
    [SerializeField]
    int maxHp;
    float invincibilityTimer;
    bool invincibility;
    [SerializeField]
    float invincibilityAmount;
    [SerializeField]
    int collisionDamage;
    float xMax, zMax, xMin, zMin;
    public List<GameObject> HPBarPieces;
    public Image SuperShot;

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
            HitParticle.Play();
            for (int i = 0; i < HPBarPieces.Count; i++)
            {
                if (i * 10 < health)
                {
                    HPBarPieces[i].SetActive(true);
                }
                else
                {
                    HPBarPieces[i].SetActive(false);
                }
            }
			if (health <= 0)
			{
				DestroySelf();
                GameLogic.GL.LoseGame();
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
            if (energy == 0)
            {
                SuperShot.fillAmount = 0f;
            }
            else
            {
                SuperShot.fillAmount = energy / 100f;
                Debug.Log("Fill amount: " + energy / 100f);
                Debug.Log("Energy actual: " + energy);
            }
		}
	}

	// Use this for initialization
	void Start()
	{
        HitParticle.Pause();
        SuperShot.fillAmount = 0;
        invincibility = false;
        invincibilityTimer = invincibilityAmount;
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
		//Normalize for constant diagonal speeds
		dir.Normalize();

		//Apply movement
		Move(dir);

		if (Input.GetButtonDown("Fire2"))
		{
			if (Energy == 100)
			{
				Energy = 0;
				Instantiate(PlayerProjectileSuperShot, transform.position, transform.rotation);

			}
		}

        //Apply timer
        if (invincibility == true && invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else
        {

            invincibilityTimer = invincibilityAmount;
            invincibility = false;
        }
	}

	void Move(Vector3 dir)
	{
        //Calculate new position
        Vector3 newPos = transform.position + dir * Speed * Time.deltaTime;
        
        //Confine within boundaries
        if(newPos.x > xMax)
        {
            newPos.x = xMax;
        }
        if (newPos.x < xMin)
        {
            newPos.x = xMin;
        }
        if (newPos.z > zMax)
        {
            newPos.z = zMax;
        }
        if (newPos.z < zMin)
        {
            newPos.z = zMin;
        }
        //Apply new position
        transform.position = newPos;
	}

	void GetInputs()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");
	}

    //Player takes damage
	public void TakeDamage(int damage)
	{
        if (invincibility == false)
        {
            Health -= damage;
        }
        invincibility = true;
    }

    //When enemy touches the player
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TakeDamage(collisionDamage);
            Debug.Log("TakeDamage damageeeeeee");
        }
    }

    //When enemy keeps touching the player
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            TakeDamage(collisionDamage);
            Debug.Log("Take damage ontriggerstay");
        }
    }
	public void ReceiveBoundaries(float width, float height, Vector3 origin)
    {
        xMax = origin.x + width / 2;
        zMax = origin.z + height / 2;
        xMin = origin.x - width / 2;
        zMin = origin.z - height / 2;
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
