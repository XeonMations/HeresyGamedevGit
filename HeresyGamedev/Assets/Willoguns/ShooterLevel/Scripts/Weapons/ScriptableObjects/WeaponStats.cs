using UnityEngine;

[CreateAssetMenu(fileName = "weaponStats", menuName = "Willoguns/Weapons/WeaponStats")]
public class WeaponStats : ScriptableObject
{
    [Header("Misc")]
    public string weaponName;
    [Header("Shooting")]
    public bool fullAuto;
    public float RPM;
    [Header("Ammo")]
    public float maximumReserve;
    public float maximumMagazine;
    public float maximumMagazineExtended;
    [Header("Reloading")]
    public float reloadTime;
}
