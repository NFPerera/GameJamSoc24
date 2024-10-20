using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private Animation animator;
    public bool isFPS;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        OpenDialogue();
    }

    private void OnDisable()
    {
        if (isFPS) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.Confined;

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
