using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

class GatlingAnimation : WeaponAnimation
{
    [SerializeField] private Transform pivot;
    public override void ShootingAnimation()
    {
      pivot.Rotate(Vector3.forward, 10000 * Time.deltaTime);
    }

    public override void ReloadAnimation()
    {

    }
}