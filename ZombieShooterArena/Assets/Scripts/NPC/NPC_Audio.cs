using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Audio : MonoBehaviour
{
    public AudioSource AudioSource;
    [SerializeField] private AudioClip[] AudioClips;

    private void Update()
    {
        if (AudioSource.enabled && !AudioSource.isPlaying)
        {
            AudioSource.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Length)]);
        }
    }
}