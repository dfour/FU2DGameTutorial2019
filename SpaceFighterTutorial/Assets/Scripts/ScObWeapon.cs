using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Here we add CreateAssetMenu to allow us to create new Weapons from the menu
 * fileName is the initial name of the file when created
 * menuName is the name used in the menu
 */
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class ScObWeapon : ScriptableObject
{
    public GameObject bulletPrefab; // Stores out Bullet Prefab

    public string weaponName;       // weapon name e.g  plasma cannon
    public string weaponDescription;// weapon description e.g "Fires plasma ball"

    public int magazineCapacity;    // amount of bullets per magazine e.g. 5
    public float reloadSpeed;       // time to reload   e.g 2.5f (2.5 seconds)
    public float fireRate;          // bullets shot per second 1f (1 per second)
    public float damage;            // damage per bullet (100)
}
