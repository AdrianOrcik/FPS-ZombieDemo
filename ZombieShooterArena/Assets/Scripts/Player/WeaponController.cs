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
            player.weaponData.Init();
        }

        public void Update()
        {
            //TODO: Osetrenie uplnym zaciatkom hry
            if (Input.GetButtonDown(Constants.INPUT_FIRE))
            {
                if (!player.weaponData.IsReloading)
                {
                    Shoot();
                }
            }
        }

        private void Shoot()
        {
            player.animator.SetTrigger(Constants.ANIM_SHOT);
            player.AudioManager.PlaySoundFX(SoundEffectType.GunShot);
            player.InstantiateMuzzleFlash(player.weaponData.MuzzleFlashObj, player.muzzleTransform);

            player.weaponData.Shot();
            player.EventManager.OnTriggerPlayerShot();

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