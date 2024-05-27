using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "CelestialEventData", menuName = "CelestialSimulation/CelestialEventData")]
[System.Serializable]
public class CelestialEventData : ScriptableObject
{
    [field: SerializeField] public List<CelestialEvent> Events { get; private set; } = new();
    [field: SerializeField] public string StartingYear { get; private set; } = "0";

    public BigInteger ConvertYearToBigInt(string year)
    {
        return BigInteger.Parse(year);
    }
}
