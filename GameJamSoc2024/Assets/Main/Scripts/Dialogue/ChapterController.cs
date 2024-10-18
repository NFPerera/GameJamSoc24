using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterController : MonoBehaviour
{
    public Chapter[] chapters;
    ChapterDisplay display;
    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<ChapterDisplay>();

        // TEMP
        display.LoadChapter(chapters[0]);
    }

    // Update is called once per frame
    void Update()
    {

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
