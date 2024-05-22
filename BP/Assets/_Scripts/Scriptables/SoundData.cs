using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Sound List", menuName = "Sound List")]
public class SoundData : ScriptableObject
{
    [field: SerializeField] public List<Sound> Music { get; private set; }
    [field: SerializeField] public List<Sound> Sfx { get; private set; }
}
