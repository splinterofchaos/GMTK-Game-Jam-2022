using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "weapon", menuName = "ScriptableObjects/WeaponManager", order = 1)]
public class WeaponsManager : ScriptableObject
{
    [SerializeField]
    public ProjectileScript prefab;
    [SerializeField]
    public bool fireRight = true;
    [SerializeField]
    public bool fireLeft = true;
    [SerializeField]
    public bool fireBack = false;

    internal void Fire(Transform weaponLeft, Transform weaponRight, Transform weaponBack)
    {
        if(fireLeft)
            Instantiate(prefab, weaponLeft).transform.parent = null;
        if(fireRight)
            Instantiate(prefab, weaponRight).transform.parent = null;
        if(fireBack)
            Instantiate(prefab, weaponBack).transform.parent = null;
    }
}
