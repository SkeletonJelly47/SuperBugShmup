using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int energy;
    public float Speed;
    public GameObject PlayerProjectileSuperShot;
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

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            //Health is not set or limited at start
            health = Mathf.Clamp(value, 0, maxHp);
            Debug.Log("HP: " + health);
            if (health <= 0)
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
        //Normalize for constant diagonal speeds
        //dir.Normalize();

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
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
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
}
