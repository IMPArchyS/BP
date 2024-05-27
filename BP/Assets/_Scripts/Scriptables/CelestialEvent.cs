using System;
using UnityEngine;

[System.Serializable]
public enum CelestialEventType { SocietyEvent, ElementCreationEvent, PlanetEvent, AsteroidEvent, StarEvent, OuterSpaceEvent, MoonEvent, EndEpoch };
[System.Serializable]
public class CelestialEvent
{
    [field: SerializeField] public string Year { get; set; }
    [field: SerializeField] public CelestialEventType EventType { get; set; }
    [field: SerializeField] public string Keyword { get; set; }
    [field: SerializeField] public string Description { get; set; }
}
