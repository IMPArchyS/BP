using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CelestialEventData", menuName = "CelestialSimulation/CelestialEventData")]
[System.Serializable]
public class CelestialEventData : ScriptableObject
{
    [field: SerializeField] public List<CelestialEvent> Events { get; private set; } = new();
}
