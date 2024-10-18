using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct Dialogue
{
    public string text;
    public bool is_char1;
}

[Serializable]
public struct Chapter
{
    public Sprite char1, char2;
    public Sprite bg;
    public Dialogue[] dialogues;

}
