using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Planet Data", menuName = "CelestialObjectData/PlanetData")]
public class PlanetData : ScriptableObject
{
    #region Moons & Rings
    [Header("Moons & Rings Information")]
    [SerializeField] private bool hasRings;
    [SerializeField] private bool hasMoons;
    [SerializeField] private List<Moon> moons = new();
    [SerializeField] private List<Material> planetStages;


    #endregion

    #region getters setters,
    public List<Material> PlanetStages
    {
        get { return planetStages; }
        set { planetStages = value; }
    }
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
