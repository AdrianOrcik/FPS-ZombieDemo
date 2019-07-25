using UnityEngine;

public enum WeaponType
{
    None = -1,
    Eagle = 0
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponDataObject", order = 1)]
public class Weapon : ScriptableObject
{
    public WeaponType WeaponType = WeaponType.None;

    public float Damage = 10f;
    public float Range = 200f;
}