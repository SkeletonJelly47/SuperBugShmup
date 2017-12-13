using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerProjectile : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioManager.SFX.Laser;
        audioSource.Play();
    }
}
