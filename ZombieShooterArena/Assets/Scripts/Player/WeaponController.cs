using Core;
using Enums;
using NPC;
using UnityEngine;

namespace Player
{
    public class WeaponController
    {
        private PlayerController player;

        public WeaponController(PlayerController player)
        {
            this.player = player;
        }

        public void Update()
        {
            if (Input.GetButtonDown(Constants.INPUT_FIRE))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            player.animator.SetTrigger(Constants.ANIM_SHOT);
            player.AudioManager.PlaySoundFX(SoundEffectType.GunShot);
            player.InstantiateMuzzleFlash(player.weaponData.MuzzleFlashObj, player.muzzleTransform);

            RaycastHit hit;
            if (Physics.Raycast(player.camera.transform.position, player.camera.transform.forward, out hit,
                player.weaponData.Range, player.mask))
            {
                NPC_Controller npcController = hit.collider.GetComponent<NPC_Controller>();

                if (npcController != null)
                {
                    if (npcController.NPC_Behaviour != NPC_BehaviourType.ANIM_TO_HIT)
                    {
                        Debug.Log("Hit");
                        npcController.SetNpcState(NPC_BehaviourType.ANIM_TO_HIT);
                    }
                }
            }

            player.InstantiateImpact(player.weaponData.ImpactShotObj, hit);
        }
    }
}