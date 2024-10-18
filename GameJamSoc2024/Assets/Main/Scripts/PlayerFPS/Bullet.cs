using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target")){
            print("hit "+collision.gameObject.name+ " !");
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Wall")){
            print("hit wall");
            Destroy(gameObject);
        }
    } 
}
