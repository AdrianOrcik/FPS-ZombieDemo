using Core;
using Enums;
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
            player.animator.SetTrigger("ShotTrigger");
            player.AudioManager.PlaySoundFX(SoundEffectType.GunShot);
            player.InstantiateMuzzleFlash(player.weaponData.MuzzleFlashObj, player.muzzleTransform);

            RaycastHit hit;
            if (Physics.Raycast(player.camera.transform.position, player.camera.transform.forward, out hit,
                player.weaponData.Range, player.mask))
            {
                //We hit something 
                Debug.Log("Hit: " + hit.collider.name);
            }

            player.InstantiateImpact(player.weaponData.ImpactShotObj, hit);
        }
    }
}