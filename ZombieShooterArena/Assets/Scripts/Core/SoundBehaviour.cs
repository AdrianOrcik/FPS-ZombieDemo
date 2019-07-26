using Enums;
using UnityEngine;

namespace Core
{
    public class SoundBehaviour : MonoBehaviour
    {
        public AudioSource AudioSource;
        public AudioClip[] AudioClips;

        public void PlayFX(SoundEffectType soundEffectType)
        {
            AudioSource.PlayOneShot(AudioClips[(int)soundEffectType]);
        }
    }
}
