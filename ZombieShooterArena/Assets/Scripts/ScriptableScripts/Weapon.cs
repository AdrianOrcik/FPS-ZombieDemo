using System;
using Enums;
using UnityEngine;

namespace ScriptableScripts
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponDataObject", order = 1)]
    public class Weapon : ScriptableObject
    {
        public WeaponType WeaponType = WeaponType.None;

        public bool IsReloading = false;
        public int AmmoAmount = 3;
        public int StackAmount = 3;
        public float Damage = 10f;
        public float Range = 200f;
        public GameObject MuzzleFlashObj;
        public GameObject ImpactShotObj;
        public GameObject ZombieImpactShotObj;

        public void Init()
        {
            IsReloading = false;
        }
        
        public void Reload()
        {
            AmmoAmount = StackAmount;
        }

        public void Shot()
        {
            AmmoAmount -= 1;
        }

        public bool ToReload()
        {
            return AmmoAmount == 0;
        }
    }
}