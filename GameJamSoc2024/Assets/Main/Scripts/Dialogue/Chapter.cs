using System;
using UnityEngine;

[Serializable]
public struct Dialogue
{
    public enum Speaker { LEFT, RIGHT, NARRATOR };
    public string text;
    public Speaker speaker;
}

[System.Serializable]
public struct Chapter
{
    public Sprite leftChar, rightChar;
    public Sprite bg;
    public Dialogue[] dialogues;
    public int music;

}
