using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponAnimation : MonoBehaviour
{

    public Weapon weapon;
    public bool isReloading = false;

    abstract public void ShootingAnimation();
    abstract public void ReloadAnimation();

}