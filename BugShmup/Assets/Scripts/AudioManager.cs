using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    private List<AudioClip> audioList = new List<AudioClip>();

    private AssetBundle audioBundleSFX, audioBundleMusic;
    private AudioClip[] SFXArray, musicArray;
    private AudioSource audioSource;

    private AudioClip intro;
    private AudioClip arp;
    private AudioClip bassAndLead;
    private AudioClip bassLeadKick;
    private AudioClip solo;
    private AudioClip soloHarmony;

    private Scene scene;

    float endTime = -1;

    private void Awake()
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

        SFX audios = new SFX(SFXArray[0], SFXArray[1], SFXArray[2], SFXArray[3], SFXArray[4], SFXArray[5], SFXArray[6], SFXArray[7], SFXArray[8]);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PlayBackgroundMusic();
        Debug.Log(scene.buildIndex);
    }

    public void LoadAudio()
    {
        //SFX Load
        audioBundleSFX = AssetBundle.LoadFromFile("AssetBundles/audio.sfx");
        SFXArray = new AudioClip[audioBundleSFX.LoadAllAssets<AudioClip>().Length];

        for(int i = 0; i < SFXArray.Length; i++)
        {
            SFXArray[i] = audioBundleSFX.LoadAllAssets<AudioClip>()[i];
        }


        //Music Load
        audioBundleMusic = AssetBundle.LoadFromFile("AssetBundles/music");
        musicArray = new AudioClip[audioBundleMusic.LoadAllAssets<AudioClip>().Length];

        for (int i = 0; i < musicArray.Length; i++)
        {
            musicArray[i] = audioBundleMusic.LoadAllAssets<AudioClip>()[i];
        }

        for (int i = 0; i < musicArray.Length; i++)
        {
            if(musicArray[i].name == "SHUMP! - Intro")
                intro = musicArray[i];
            else if (musicArray[i].name == "SHUMP! - Arp")          
                arp = musicArray[i];
            else if (musicArray[i].name == "SHUMP! - Bass & Lead")
                bassAndLead = musicArray[i];
            else if (musicArray[i].name == "SHUMP! - Bass, Lead & Kick")
                bassLeadKick = musicArray[i];
            else if (musicArray[i].name == "SHUMP! - Bass, Lead & Kick")
                bassLeadKick = musicArray[i];
            else if (musicArray[i].name == "SHUMP! - Solo")
                solo = musicArray[i];
            else if (musicArray[i].name == "SHUMP! - Solo Harmony")
                soloHarmony = musicArray[i];
        }
    }

    public struct SFX
    {
        public SFX(AudioClip block, AudioClip enemyShot, AudioClip fly, AudioClip hit, AudioClip homingMissile, AudioClip laser, AudioClip menu, AudioClip playerShoot, AudioClip superShot)
        {
            Block = block;
            EnemyShot = enemyShot;
            Flyehive = fly;
            Hit = hit;
            HomingMissile = homingMissile;
            Laser = laser;
            MenuAmbiance = menu;
            PlayerShoot = playerShoot;
            SuperShot = superShot;
        }

        public static AudioClip Block;
        public static AudioClip EnemyShot;
        public static AudioClip Hit;
        public static AudioClip HomingMissile;
        public static AudioClip Flyehive;
        public static AudioClip Laser;
        public static AudioClip MenuAmbiance;
        public static AudioClip PlayerShoot;
        public static AudioClip SuperShot;
    }

    void PlayBackgroundMusic()
    {
        if(scene != SceneManager.GetActiveScene())
        {
            scene = SceneManager.GetActiveScene();
        }

        if (scene.buildIndex == 1|| 
            scene.buildIndex == 2)
        {
            // Play these only once
            if (audioSource.clip != intro && endTime <= 0)
            {
                audioSource.clip = intro;
                audioSource.Play();
                endTime = Time.time + intro.length;

            }
            else if (audioSource.clip == intro && endTime < Time.time)
            {
                audioSource.clip = bassAndLead;
                audioSource.Play();
                endTime = Time.time + bassAndLead.length;
            }
            // loop this part
            else
            {
                if ((audioSource.clip == bassAndLead || audioSource.clip == arp) && endTime < Time.time)
                {
                    audioSource.clip = bassLeadKick;
                    audioSource.Play();
                    endTime = Time.time + bassLeadKick.length;
                }

                if (audioSource.clip == bassLeadKick && endTime < Time.time)
                {
                    audioSource.clip = solo;
                    audioSource.Play();
                    endTime = Time.time + solo.length;
                }

                if (audioSource.clip == solo && endTime < Time.time)
                {
                    audioSource.clip = soloHarmony;
                    audioSource.Play();
                    endTime = Time.time + soloHarmony.length;
                }

                if (audioSource.clip == soloHarmony && endTime < Time.time)
                {
                    audioSource.clip = arp;
                    audioSource.Play();
                    endTime = Time.time + arp.length;
                }
            }


        }

        if(scene.buildIndex == 0)
        {
            if (!audioSource.clip == SFX.MenuAmbiance)
            {
                audioSource.clip = SFX.MenuAmbiance;
                audioSource.Play();
                audioSource.loop = true;
            }
        }
    }
}
