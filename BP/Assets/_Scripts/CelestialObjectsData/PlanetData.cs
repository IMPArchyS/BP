using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Data", menuName = "CelestialObjectData/PlanetData")]
public class PlanetData : ScriptableObject
{
    #region Moons & Rings
    [Header("Moons & Rings Information")]
    public bool hasRings;
    public bool hasMoons;
    public List<Moon> moons;

    #endregion

    #region getters setters
    public List<Moon> Moons
    {
        get { return moons; }
        set { moons = value; }
    }

    public bool HasRings
    {
        get { return hasRings; }
        set { hasRings = value; }
    }

    public bool HasMoons
    {
        get { return hasMoons; }
        set { hasMoons = value; }
    }

    public void CopyFrom(PlanetData other)
    {
        // Copy rings
        hasMoons = other.hasMoons;
        hasRings = other.hasRings;
        // ringCharacteristics.Clear();
        // ringCharacteristics.AddRange(other.ringCharacteristics);
    }
    #endregion
}
