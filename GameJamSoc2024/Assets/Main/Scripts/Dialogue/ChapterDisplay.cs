using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterDisplay : MonoBehaviour
{
    [Header("Components")]
    public Image background;
    public Image leftchar, rightchar;
    public TextMeshProUGUI textComponent;
    public GameObject dialogueUI;
    public DialogueDisplay dialogueDisplay;

    [Header("Lighting")]
    public Color foregroundColor;
    public Color backgroundColor;

    [Header("Text")]
    public bool instantText;
    public float textSpeed;


    // Dialogue vars
    Queue<Dialogue> dialogueQueue;
    Coroutine currDialogueCoroutine;
    bool is_running;



    void Start()
    {
        dialogueQueue = new();
    }

    public void OpenChapterUI()
    {
        if (!dialogueUI.activeInHierarchy)
        {
            dialogueUI.SetActive(true);
            dialogueDisplay.OpenDialogue();
        }
    }

    public void CloseChapterUI()
    {

        if (dialogueUI.activeInHierarchy) dialogueDisplay.CloseDialogue();
    }

    public void LoadChapter(Chapter chapter)
    {
        // Pone los personajes recibidos en sus imagenes correspondientes.
        leftchar.sprite = chapter.leftChar;
        rightchar.sprite = chapter.rightChar;

        if (!rightchar.sprite) rightchar.color = new(0, 0, 0, 0);

        // Pone el background recibido.
        background.sprite = chapter.bg;

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

        // Limpia el texto anterior
        if (is_running) StopCoroutine(currDialogueCoroutine);
        textComponent.text = "";

        // Carga el texto a pantalla.
        if (instantText) textComponent.text = curr.text;
        else currDialogueCoroutine = StartCoroutine(DisplayText(curr.text));

        // Check enum con switch
        switch (curr.speaker)
        {
            case Dialogue.Speaker.LEFT:
                leftchar.color = foregroundColor;
                if (rightchar.sprite) rightchar.color = backgroundColor;
                break;
            case Dialogue.Speaker.RIGHT:
                leftchar.color = backgroundColor;
                if (rightchar.sprite) rightchar.color = foregroundColor;
                break;
            case Dialogue.Speaker.NARRATOR:
                leftchar.color = backgroundColor;
                if (rightchar.sprite) rightchar.color = backgroundColor;
                break;
        }
    }

    IEnumerator DisplayText(string text)
    {
        is_running = true;
        foreach (char c in text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(1 / textSpeed);
        }
        is_running = false;
    }

}
