using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    Animation animator;
    
    public void PlayAnim()
    {
        animator = GetComponent<Animation>();
        animator.Play("DialogueDisplayOnEnable");
    }

    public void OnDisable_()
    {
        animator.Play("DialogueDisplayOnDisable");
        Debug.Log("This has been disabled");
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
