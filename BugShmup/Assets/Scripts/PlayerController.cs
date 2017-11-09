using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    float verticalInput, horizontalInput;
    Vector3 dir;
    [SerializeField] int health;
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
