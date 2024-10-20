using System.Collections;
using System.Collections.Generic;
using Main.Scripts.DevelopmentUtilities.Extensions;
using Main.Scripts.TowerDefenseGame.Models;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask targetMask;
    private void OnCollisionEnter(Collision collision)
    {
        if (targetMask.Includes(collision.gameObject.layer))
        {
            //print("hit " + collision.gameObject.name + " !");

            if (collision.gameObject.TryGetComponent(out TowerModel l_model))
            {
                l_model.TakeDamage(damage);
            }
            CreateBulletImpactEffect(collision);
            Destroy(collision.gameObject);
        }
    }

    void CreateBulletImpactEffect(Collision collision)
    {

        ContactPoint contact = collision.contacts[0];

        // Adjust the position slightly along the normal to avoid texture overlap
        Vector3 adjustedPosition = contact.point + contact.normal * 0.01f;

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffect,
            adjustedPosition,
            Quaternion.LookRotation(contact.normal) * Quaternion.Euler(0, 180, 0)
        );


        hole.transform.SetParent(collision.gameObject.transform);


        
        
    }
}
