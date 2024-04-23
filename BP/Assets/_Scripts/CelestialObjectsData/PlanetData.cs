using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Data", menuName = "CelestialObjectData/PlanetData")]
public class PlanetData : ScriptableObject
{
    #region Moons & Rings
    [Header("Moons & Rings Information")]
    public List<Moon> moons;
    public bool hasRings;
    public List<string> ringCharacteristics;

    #endregion
}
