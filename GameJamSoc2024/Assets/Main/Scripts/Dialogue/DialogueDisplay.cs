using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private Animation animator;


    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        OpenDialogue();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CloseDialogue();
    }

    public void OpenDialogue()
    {
        animator = GetComponent<Animation>();
        animator.Play("DialogueDisplayOnEnable");
    }

    public void CloseDialogue()
    {
        animator.Play("DialogueDisplayOnDisable");
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
