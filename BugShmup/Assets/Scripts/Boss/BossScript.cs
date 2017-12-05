using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BossActions
{
    RapidFire,
    Pause,
    Spread
}

public class BossScript : MonoBehaviour
{
    int numbah = 0;
    public float beetleShotInterval;
    Transform[] eyeTransform;
    GameObject[] eyeObject;
    public GameObject spreadBullet;
    [SerializeField] float shootingDirection;

    List<BossActions> bossactions = new List<BossActions>();


    // Use this for initialization
    void Start()
    {
        bossactions.Add(BossActions.Pause);
        bossactions.Add(BossActions.Pause);
        bossactions.Add(BossActions.Pause);
        bossactions.Add(BossActions.Pause);

        eyeTransform = gameObject.GetComponentsInChildren<Transform>();
        eyeObject = new GameObject[eyeTransform.Length];
        foreach (Transform child in eyeTransform)
        {
            numbah++;
            eyeObject.SetValue(child.gameObject, numbah - 1);
        }
        BeetlePurkkaaSaatana();
    }

    // Update is called once per frame
    void Update()
    {
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
}
