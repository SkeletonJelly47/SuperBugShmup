using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    private List<AudioClip> audioList = new List<AudioClip>();

    private AssetBundle audioBundle;
    private AudioClip[] audioArray;

    public struct Audios
    {
        public Audios(AudioClip block, AudioClip enemyShot, AudioClip fly, AudioClip hit, AudioClip laser, AudioClip menu, AudioClip playerShoot, AudioClip superShot)
        {
            Block = block;
            EnemyShot = enemyShot;
            Flyehive = fly;
            Hit = hit;
            Laser = laser;
            MenuAmbiance = menu;
            PlayerShoot = playerShoot;
            SuperShot = superShot;
        }

        public static AudioClip Block;
        public static AudioClip EnemyShot;
        public static AudioClip Hit;
        public static AudioClip Flyehive;
        public static AudioClip Laser;
        public static AudioClip MenuAmbiance;
        public static AudioClip PlayerShoot;
        public static AudioClip SuperShot;
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LoadAudio();

        Audios audios = new Audios(audioArray[0], audioArray[1], audioArray[2], audioArray[3], audioArray[4], audioArray[5], audioArray[6], audioArray[7]);
    }

    public void LoadAudio()
    {
        audioBundle = AssetBundle.LoadFromFile("AssetBundles/audio.sfx");
        audioArray = new AudioClip[audioBundle.LoadAllAssets<AudioClip>().Length];

        for(int i = 0; i < audioArray.Length; i++)
        {
            audioArray[i] = audioBundle.LoadAllAssets<AudioClip>()[i];
            Debug.Log(audioArray[i].name);
        }
    }
}
