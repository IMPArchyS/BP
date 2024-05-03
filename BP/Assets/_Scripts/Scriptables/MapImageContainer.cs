using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ImageMapping
{
    public string state;
    public Sprite image;
}
[System.Serializable]
[CreateAssetMenu(fileName = "New ImageContainer", menuName = "CelestialSimulation/ImageContainer")]
public class MapImageContainer : ScriptableObject
{
    [field: SerializeField] public List<ImageMapping> ImageMappings { get; private set; }
}
