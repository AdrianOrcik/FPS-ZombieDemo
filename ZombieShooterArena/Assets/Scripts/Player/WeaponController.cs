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
                //We hit something 
                Debug.Log("Hit: " + hit.collider.name);
                NPC_Controller npcController = hit.collider.GetComponent<NPC_Controller>();

                if (npcController != null)
                {
                    npcController.SetAnimation(Constants.ANIM_TO_HIT, 0, NPC.NPC.Hit);
                    //npcController.Hit();
                }
            }

            player.InstantiateImpact(player.weaponData.ImpactShotObj, hit);
        }
    }
}