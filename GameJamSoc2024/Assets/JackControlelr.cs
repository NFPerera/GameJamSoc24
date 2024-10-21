using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class JackControlelr : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("asdf");
        if (other.gameObject.CompareTag("Bullet"))
            SceneManager.LoadScene("YouWin");
    }
}
