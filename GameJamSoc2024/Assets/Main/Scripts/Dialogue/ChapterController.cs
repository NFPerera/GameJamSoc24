using System.Collections;
using System.Collections.Generic;
using Main.Scripts.DevelopmentUtilities.Extensions.IENumerableExtensions.DictionaryUtilities;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public SerializableDictionary<string, Chapter> allChapters;
    public string initChap;
    public DialogueDisplay dialogueDisplay;
    ChapterDisplay display;
    void Start()
    {
        display = GetComponent<ChapterDisplay>();

        // TEMP
        display.LoadChapter(allChapters[initChap]);
    }

    public void StartChapter(string p_id)
    {
        dialogueDisplay.gameObject.SetActive(true);
        display.LoadChapter(allChapters[p_id]);
        //display.LoadChapter(chapters[p_id]);
    }

    public void NextDialogue()
    {
        display.NextDialogue();
    }
}
