using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TileState",menuName ="Data/TileState")]
public class TileState : ScriptableObject
{
    public int number;
    public Color textColor;
    public Color backgroundColor;
}
