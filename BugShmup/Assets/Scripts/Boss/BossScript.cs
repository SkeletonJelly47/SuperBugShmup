using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossActions
{
    RapidFire,
    Spread,
    SpawnFlies,
    Idle,
    Pause
}


public class BossScript : MonoBehaviour
{
    int numbah = 0;
    public float pauseTimer = 0;
    float shotTimer;
    public float shotDuration;
    public float beetleShotInterval;
    float shootingDirection;
    float shotCount;
    Transform[] eyeTransform;
    GameObject[] eyeObject;
    public GameObject spreadBullet;
    [SerializeField]

    bool waiting;
    public BossActions state = BossActions.Pause;


    //  List<BossActions> bossactions = new List<BossActions>();




    // Use this for initialization
    void Start()
    {
        /*   bossactions.Add(BossActions.Pause);
           bossactions.Add(BossActions.Pause);
           bossactions.Add(BossActions.Pause);
           bossactions.Add(BossActions.Pause); */

        eyeTransform = gameObject.GetComponentsInChildren<Transform>();
        eyeObject = new GameObject[eyeTransform.Length];
        foreach (Transform child in eyeTransform)
        {
            numbah++;
            eyeObject.SetValue(child.gameObject, numbah - 1);
        }
        BeetlePurkkaaSaatana();
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case BossActions.Pause:
                {
                    StopAllCoroutines();
                    StartCoroutine(PauseBetweenShots(3));
                    state = BossActions.RapidFire;
                    break;
                }
            case BossActions.RapidFire:
                {
                    if (waiting == false && shotCount < 10)
                    {
                        RapidFire(eyeTransform[5].transform.position);
                        RapidFire(eyeTransform[8].transform.position);
                        StartCoroutine(PauseBetweenShots(0.1f));
                        shotCount += 1;
                    }
                    if (waiting == false && shotCount >= 10)
                    {
                        RapidFire(eyeTransform[6].transform.position);
                        RapidFire(eyeTransform[7].transform.position);
                        StartCoroutine(PauseBetweenShots(0.1f));
                        shotCount += 1;
                    }
                    if (shotCount >= 20)
                    {
                        shotCount = 0;
                        state = BossActions.Pause;
                        Debug.Log("Fuck me sideways");
                    }
                    break;
                }
        }
    }
    void BeetlePewPewEyeOne()
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 22.5f + 140, transform.eulerAngles.z);

        Instantiate(spreadBullet, eyeTransform[1].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y + 11.25f, transform.eulerAngles.z);

        Instantiate(spreadBullet, eyeTransform[1].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y + 11.25f, transform.eulerAngles.z);

        Instantiate(spreadBullet, eyeTransform[1].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y + 11.25f, transform.eulerAngles.z);

        Instantiate(spreadBullet, eyeTransform[1].transform.position, Quaternion.Euler(rot));
    }
    void BeetlePewPewEyeFour()
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 22.5f + 140, transform.eulerAngles.z);

        Instantiate(spreadBullet, eyeTransform[4].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y + 11.25f, transform.eulerAngles.z);

        Instantiate(spreadBullet, eyeTransform[4].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y + 11.25f, transform.eulerAngles.z);

        Instantiate(spreadBullet, eyeTransform[4].transform.position, Quaternion.Euler(rot));

        rot = new Vector3(transform.eulerAngles.x, Quaternion.Euler(rot).eulerAngles.y + 11.25f, transform.eulerAngles.z);

        Instantiate(spreadBullet, eyeTransform[4].transform.position, Quaternion.Euler(rot));
    }
    void BeetlePurkkaaSaatana()
    {
        InvokeRepeating("BeetlePewPewEyeOne", 1.0f, beetleShotInterval);
        InvokeRepeating("BeetlePewPewEyeFour", 1.0f, beetleShotInterval);
    }
    void RapidFire(Vector3 eyeLocation)
    {
        Vector3 rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        Instantiate(spreadBullet, eyeLocation, Quaternion.Euler(rot));
    }
    void Pause(float pauseDuration)
    {
        // FUGG DIS :DD
        pauseTimer += Time.deltaTime;

        if (pauseTimer >= pauseDuration)
        {
            state = BossActions.RapidFire;
            pauseTimer = 0;
        }
    }
    IEnumerator PauseBetweenShots(float pauseDuration)
    {
        waiting = true;
        yield return new WaitForSeconds(pauseDuration);
        waiting = false;
    }
}

