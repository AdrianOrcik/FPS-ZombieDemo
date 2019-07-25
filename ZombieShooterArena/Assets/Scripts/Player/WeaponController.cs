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
    
        public void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(player.camera.transform.position, player.camera.transform.forward, out hit, player.weaponData.Range, player.mask))
            {
                //We hit something 
                Debug.Log("Hit: " + hit.collider.name);
            }
        }
    
    }
}
