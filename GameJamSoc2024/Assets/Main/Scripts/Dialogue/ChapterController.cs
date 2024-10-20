using System.Collections;
using System.Collections.Generic;
using Main.Scripts.DevelopmentUtilities.Extensions.IENumerableExtensions.DictionaryUtilities;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public SerializableDictionary<string, Chapter> allChapters;
    public Chapter[] chapters;
    ChapterDisplay display;
    void Start()
    {
        display = GetComponent<ChapterDisplay>();

        // TEMP
        //display.LoadChapter(chapters[0]);
    }

    public void StartChapter(string p_id)
    {
        display.LoadChapter(allChapters[p_id]);
        //display.LoadChapter(chapters[p_id]);
    }

    public void NextDialogue()
    {
        display.NextDialogue();
    }
}
