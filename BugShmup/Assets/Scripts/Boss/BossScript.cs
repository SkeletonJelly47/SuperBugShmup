using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossActions
{
    RapidFireMiddle,
    RapidFireSides,
    ShootAtPlayer,
    Homing,
    Break
}


public class BossScript : MonoBehaviour
{
    int getChildNumber = 0;
    float shotTimer;
    float shootingDirection;
    
    int shotCount;
    int cycle;
    int phase;
    bool waiting;
    bool speedUp;
    public BossActions state = BossActions.Break;
     public Transform[] eyeTransform;
    GameObject[] eyeObject;
   public Transform crosshairOne, crosshairTwo;
    public Transform weaponTarget;
    public GameObject basicBullet, fastBullet, homingBullet, bigBullet;
    [SerializeField]
    int health;

    [Header("SpreadShot")]
    public float spreadShotInterval;

    [Header("RapidFire")]
    public float rapidFireInterval;
    public int projectilesShot;
    [Header("ShootAtPlayer")]
    public float shootAtPlayerInterval;

    protected int Health
    {
        get
        {
            return health;
        }

        set
        {
            //dead
            health = value;
            if (health <= 0)
            {
                DestroySelf();
            }
        }
    }

    void Start()
    {

      /*  eyeTransform = gameObject.GetComponentsInChildren<Transform>();
        eyeObject = new GameObject[eyeTransform.Length];
        foreach (Transform child in eyeTransform)
        {
            getChildNumber++;
            eyeObject.SetValue(child.gameObject, getChildNumber - 1);
        }
        */
        BeetlePurkkaaSaatana();
        phase = 0;
        waiting = false;
        speedUp = false;
    }

    // Update is called once per frame
    void Update()
    {   
        switch (state)
        {
            case BossActions.Break:
                {
                    StopAllCoroutines();

                    if (phase == 0)
                    {
                        if(speedUp == true)
                        {
                            SpreadShotSpeed();
                        }
                        StartCoroutine(PauseBetweenShots(2));
                        state = BossActions.RapidFireSides;
                    }
                    if (phase == 1)
                    {
                        StartCoroutine(PauseBetweenShots(1));
                        state = BossActions.RapidFireMiddle;
                    }
                    if (phase == 2)
                    {
                        StartCoroutine(PauseBetweenShots(3));
                        state = BossActions.ShootAtPlayer;
                    }
                    if (phase == 3)
                    {
                        cycle = 0;
                        StartCoroutine(PauseBetweenShots(1));
                        state = BossActions.Homing;
                        speedUp = true;
                    }
                    if (phase == 4)
                    {

                    }

                    break;
                }
            case BossActions.RapidFireSides:
                {
                    if (waiting == false && shotCount < projectilesShot)
                    {

                        RapidFire(eyeTransform[5].transform.position);
                        RapidFire(eyeTransform[8].transform.position);
                        StartCoroutine(PauseBetweenShots(rapidFireInterval));
                        shotCount += 1;
                    }
                    if (shotCount >= projectilesShot)
                    {
                        shotCount = 0;
                        phase += 1;
                        state = BossActions.Break;
                    }
                    break;
                }
            case BossActions.RapidFireMiddle:
                {
                    if (waiting == false && shotCount < projectilesShot)
                    {

                        RapidFire(eyeTransform[6].transform.position);
                        RapidFire(eyeTransform[7].transform.position);
                        StartCoroutine(PauseBetweenShots(rapidFireInterval));
                        shotCount += 1;
                    }
                    if (shotCount >= projectilesShot)
                    {
                        shotCount = 0;
                        phase += 1;
                        state = BossActions.Break;
                    }
                    break;
                }
            case BossActions.ShootAtPlayer:
                {
                    if (waiting == false && shotCount < 5)
                    {
                        crosshairOne.transform.LookAt(weaponTarget);
                        crosshairTwo.transform.LookAt(weaponTarget);
                        ShootAtPlayer(eyeTransform[2].transform.position, crosshairOne.rotation);
                        ShootAtPlayer(eyeTransform[3].transform.position, crosshairTwo.rotation);
                        StartCoroutine(PauseBetweenShots(0.02f));
                        shotCount += 1;
                    }
                    if (waiting == false && shotCount >= 5)
                    {
                        StartCoroutine(PauseBetweenShots(shootAtPlayerInterval));
                        shotCount = 0;
                        cycle += 1;
                    }
                    if (cycle == 6)
                    {
                        phase += 1;
                        state = BossActions.Break;
                    }
                    break;
                }
            case BossActions.Homing:
                {
                    if (waiting == false && shotCount < 3)
                    {
                        HomingFire(eyeTransform[1].transform.position, 315);
                        HomingFire(eyeTransform[4].transform.position, 45);
                        HomingFire(eyeTransform[5].transform.position, 225);
                        HomingFire(eyeTransform[6].transform.position, 180f);
                        HomingFire(eyeTransform[7].transform.position, 180f);
                        HomingFire(eyeTransform[8].transform.position, 135f);
                        StartCoroutine(PauseBetweenShots(1f));
                        shotCount += 1;

                    }
                    if (waiting == false && shotCount >= 3)
                    {
                        shotCount = 0;
                        phase = 0;
                        state = BossActions.Break;
                    }
                    break;
                }
        }
    }
    void BeetlePewPewEyeOne()
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 22.5f, transform.eulerAngles.z);

        Instantiate(basicBullet, eyeTransform[1].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y - 11.25f, transform.eulerAngles.z);

        Instantiate(basicBullet, eyeTransform[1].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y - 22.5f, transform.eulerAngles.z);

        Instantiate(basicBullet, eyeTransform[1].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y - 11.25f, transform.eulerAngles.z);

        Instantiate(basicBullet, eyeTransform[1].transform.position, Quaternion.Euler(rot));
    }
    void BeetlePewPewEyeFour()
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 22.5f, transform.eulerAngles.z);

        Instantiate(basicBullet, eyeTransform[4].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y - 11.25f, transform.eulerAngles.z);

        Instantiate(basicBullet, eyeTransform[4].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y - 22.5f, transform.eulerAngles.z);

        Instantiate(basicBullet, eyeTransform[4].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y - 11.25f, transform.eulerAngles.z);

        Instantiate(basicBullet, eyeTransform[4].transform.position, Quaternion.Euler(rot));
    }
    void BeetlePurkkaaSaatana()
    {
        InvokeRepeating("BeetlePewPewEyeOne", 1.0f, spreadShotInterval);
        InvokeRepeating("BeetlePewPewEyeFour", 1.0f, spreadShotInterval);
    }
    void SpreadShotSpeed()
    {
        CancelInvoke("BeetlePewPewEyeOne");
        CancelInvoke("BeetlePewPewEyeFour");
        spreadShotInterval -= 0.5f;
        if (spreadShotInterval <= 0.25)
        {
            spreadShotInterval = 0.25f;
        }
        InvokeRepeating("BeetlePewPewEyeOne", 1.0f, spreadShotInterval);
        InvokeRepeating("BeetlePewPewEyeFour", 1.0f, spreadShotInterval);
    }
    void RapidFire(Vector3 eyePosition)
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        Instantiate(basicBullet, eyePosition, Quaternion.Euler(rot));
    }
    void ShootAtPlayer(Vector3 eyePosition, Quaternion crossHair)
    {
        Instantiate(fastBullet, eyePosition, crossHair);
    }
    void HomingFire(Vector3 eyePosition, float yRotation)
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        Instantiate(homingBullet, eyePosition, Quaternion.Euler(rot));
    }
    IEnumerator PauseBetweenShots(float pauseDuration)
    {
        waiting = true;
        yield return new WaitForSeconds(pauseDuration);
        waiting = false;
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}

