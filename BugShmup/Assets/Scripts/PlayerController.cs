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
    [SerializeField] int maxHp;

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
    }

    void Move(Vector3 dir)
    {
        transform.position += dir * Speed * Time.deltaTime;
    }

    void GetInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
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
}
