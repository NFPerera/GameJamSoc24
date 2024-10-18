using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("hit " + collision.gameObject.name + " !");
            CreateBulletImpactEffect(collision);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("hit wall");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
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
