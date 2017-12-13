using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerProjectileSuperShot : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioManager.SFX.SuperShot;
        audioSource.Play();
    }
}
