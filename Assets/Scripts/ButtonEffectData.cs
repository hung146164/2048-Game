using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonEffect",menuName = "Data/ButtonEffect")]
public class ButtonEffectData : ScriptableObject
{
    [Header("Button Colors")]
    //red default
    public Color color = new Color(1f, 0f, 0f, 1f);
    
}
