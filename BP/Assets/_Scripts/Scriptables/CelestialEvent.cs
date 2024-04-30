using System;
using UnityEngine;

[System.Serializable]
public class CelestialEvent
{
    [field: SerializeField] public long Year { get; set; }
    [field: SerializeField] public string Keyword { get; set; }
    [field: SerializeField] public string Description { get; set; }
}
