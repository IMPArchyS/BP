using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Star Data", menuName = "CelestialObjectData/StarData")]
public class StarData : ScriptableObject
{
    #region Star Attributes

    [Header("Star Information")]
    [SerializeField] private StarLuminosityType luminosityType;
    [SerializeField] private StarSpectralType spectralType;

    [Header("Star Properties")]
    [SerializeField] private float luminosity; // in solar luminosities
    [SerializeField] private string fusionProcess;
    [SerializeField] private float stellarWindIntensity; // in solar masses per year
    [SerializeField] private string lifetime;
    // Additional properties specific to the end-of-life stage
    [SerializeField] private StarDeathType endOfLifeStage; // e.g., white dwarf, neutron star, black hole

    #endregion

    #region getters setters
    public void CopyFrom(StarData other)
    {
        // copy all properties from other to this
        luminosityType = other.luminosityType;
        spectralType = other.spectralType;
        luminosity = other.luminosity;
        fusionProcess = other.fusionProcess;
        stellarWindIntensity = other.stellarWindIntensity;
        lifetime = other.lifetime;
        endOfLifeStage = other.endOfLifeStage;
    }

    public StarLuminosityType LuminosityType
    {
        get { return luminosityType; }
        set { luminosityType = value; }
    }

    public StarSpectralType SpectralType
    {
        get { return spectralType; }
        set { spectralType = value; }
    }

    public float Luminosity
    {
        get { return luminosity; }
        set { luminosity = value; }
    }

    public string FusionProcess
    {
        get { return fusionProcess; }
        set { fusionProcess = value; }
    }

    public float StellarWindIntensity
    {
        get { return stellarWindIntensity; }
        set { stellarWindIntensity = value; }
    }

    public string Lifetime
    {
        get { return lifetime; }
        set { lifetime = value; }
    }

    public StarDeathType EndOfLifeStage
    {
        get { return endOfLifeStage; }
        set { endOfLifeStage = value; }
    }
    #endregion
}
