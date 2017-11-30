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
        public Audios(AudioClip fly, AudioClip menu, AudioClip playershot, AudioClip superShot)
        {
            Flyehive = fly;
            MenuAmbiance = menu;
            PlayerShoot = playershot;
            SuperShot = superShot;
        }

        public static AudioClip Flyehive;
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

        Audios audios = new Audios(audioArray[0], audioArray[1], audioArray[2], audioArray[3]);
    }

    public void LoadAudio()
    {
        audioBundle = AssetBundle.LoadFromFile("AssetBundles/audio");
        audioArray = new AudioClip[audioBundle.LoadAllAssets<AudioClip>().Length];

        for(int i = 0; i < audioArray.Length; i++)
        {
            audioArray[i] = audioBundle.LoadAllAssets<AudioClip>()[i];
        }
    }
}
