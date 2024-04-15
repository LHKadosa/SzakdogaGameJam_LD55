using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip meleeAttackSfx, rangedAttackSfx, siegeAttackSfx, suicideAttackSfx, unitSpawnSfx, unitDeathSfx, towerHitSfx, towerDeathSfx;

    public void PlaySFX(AudioClip clip)
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(clip);
    }
}
