using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] name,sentence;
    public Sprite[] speaker;

}
