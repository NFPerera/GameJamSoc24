using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public Chapter[] chapters;
    ChapterDisplay display;
    void Start()
    {
        display = GetComponent<ChapterDisplay>();

        // TEMP
        //display.LoadChapter(chapters[0]);
    }

    public void StartChapter(int index)
    {
        display.LoadChapter(chapters[index]);
    }

    public void NextDialogue()
    {
        display.NextDialogue();
    }
}
