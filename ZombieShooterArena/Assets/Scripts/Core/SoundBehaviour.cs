using Enums;
using UnityEngine;

namespace Core
{
    public class SoundBehaviour : MonoBehaviour
    {
        public AudioSource AudioSource;
        public AudioClip[] AudioClips;
        private AudioClip nextShotClip = null;

        public void PlayFX(SoundEffectType soundEffectType)
        {
            if (AudioSource.isPlaying && soundEffectType == SoundEffectType.ReloadGun)
            {
                nextShotClip = AudioClips[(int) soundEffectType];
                return;
            }

            AudioSource.PlayOneShot(AudioClips[(int) soundEffectType]);
        }

        private void Update()
        {
            //TODO: refactor
            if (nextShotClip != null && !AudioSource.isPlaying)
            {
                AudioSource.PlayOneShot(nextShotClip);
                nextShotClip = null;
            }
        }
    }
}