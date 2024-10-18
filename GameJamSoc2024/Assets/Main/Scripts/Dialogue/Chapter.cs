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
    public Texture2D char1, char2;
    public Dialogue[] dialogues;

}
