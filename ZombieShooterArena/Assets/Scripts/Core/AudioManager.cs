using Core.Architecture;
using Enums;
using UnityEngine;

namespace Core
{
    public class AudioManager : MainBehaviour
    {
        
        [SerializeField] private SoundBehaviour soundBehaviour;
        public void PlaySoundFX(SoundEffectType soundEffectType)
        {
           soundBehaviour.PlayFX(soundEffectType);
        }
    }
}
