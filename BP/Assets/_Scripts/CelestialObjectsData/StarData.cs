using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Star Data", menuName = "CelestialObjectData/StarData")]
public class StarData : ScriptableObject
{
    #region Star Attributes

    [Header("Star Information")]
    [SerializeField] private CelestialObject celestialObject;
    [SerializeField] private StarLuminosityType luminosityType;
    [SerializeField] private StarSpectralType spectralType;

    [Header("Star Properties")]
    [SerializeField] private float temperature; // in Kelvin
    [SerializeField] private float luminosity; // in solar luminosities
    [SerializeField] private string fusionProcess;
    [SerializeField] private float stellarWindIntensity; // in solar masses per year

    // Additional properties specific to the end-of-life stage
    [SerializeField] private StarDeathType endOfLifeStage; // e.g., white dwarf, neutron star, black hole

    #endregion
}
