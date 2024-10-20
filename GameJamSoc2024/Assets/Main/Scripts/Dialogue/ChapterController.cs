using System.Collections;
using System.Collections.Generic;
using Main.Scripts;
using Main.Scripts.DevelopmentUtilities.Extensions.IENumerableExtensions.DictionaryUtilities;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public SerializableDictionary<string, Chapter> allChapters;
    public string initChap;
    public bool autoLoadInitCap;
    public DialogueDisplay dialogueDisplay;
    ChapterDisplay display;
    void Start()
    {
        MasterManager.Instance.SubscribeChapterController(this);
        display = GetComponent<ChapterDisplay>();

        // TEMP
        display.LoadChapter(allChapters[initChap]);
        if(autoLoadInitCap)
            MasterManager.Instance.StartDialogue(initChap);
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
