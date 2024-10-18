using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterDisplay : MonoBehaviour
{
    public Image leftchar, rightchar;
    public TextMeshProUGUI textComponent;
    public GameObject dialogueUI;

    Queue<Dialogue> dialogueQueue;

    public Color foregroundColor, backgroundColor;

    void Start()
    {
        dialogueQueue = new();
    }

    public void OpenChapterUI()
    {
        if (!dialogueUI.activeInHierarchy) dialogueUI.SetActive(true);
    }

    public void CloseChapterUI()
    {

        if (dialogueUI.activeInHierarchy) dialogueUI.GetComponent<DialogueDisplay>().OnDisable_();
    }

    public void LoadChapter(Chapter chapter)
    {
        // Pone los personajes recibidos en sus imagenes correspondientes.
        leftchar.sprite = chapter.char1;
        rightchar.sprite = chapter.char2; // Can be null. mirar por si acaso

        // Pone en cola todos los dialogos del chapter.
        dialogueQueue.Clear();

        foreach (Dialogue d in chapter.dialogues)
        {
            dialogueQueue.Enqueue(d);
        }

        // Cargar primer texto.
        NextDialogue();
    }

    public void NextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            CloseChapterUI();
            return;
        }


        Dialogue curr = dialogueQueue.Dequeue();
        // Carga el texto a pantalla.
        textComponent.text = curr.text;

        // Check is_char1, si es asi hacer opuesto mas oscuro.
        if (curr.is_char1)
        {
            leftchar.color = foregroundColor;
            rightchar.color = backgroundColor;
        }
        else
        {
            leftchar.color = backgroundColor;
            rightchar.color = foregroundColor;
        }
    }

}
