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
                NPC_BodyHit npcBodyHit = hit.collider.GetComponent<NPC_BodyHit>();

                if (npcBodyHit != null)
                {
                    player.InstantiateImpact(player.weaponData.ZombieImpactShotObj, hit);
                    npcBodyHit.SetNpcState();
                }
                else
                {
                    player.InstantiateImpact(player.weaponData.ImpactShotObj, hit);
                }
            }
        }
    }
}