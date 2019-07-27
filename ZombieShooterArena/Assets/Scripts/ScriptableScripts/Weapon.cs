using Enums;
using UnityEngine;

namespace ScriptableScripts
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponDataObject", order = 1)]
    public class Weapon : ScriptableObject
    {
        public WeaponType WeaponType = WeaponType.None;

        public float Damage = 10f;
        public float Range = 200f;
        public GameObject MuzzleFlashObj;
        public GameObject ImpactShotObj;
        public GameObject ZombieImpactShotObj;
    }
}