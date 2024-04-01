using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarLuminosityType { Nebula, TTauri, Dwarf, Giant, SuperGiant, HyperGiant }
public enum StarSpectralType { None, Blue, White, Yellow, Orange, Red, Black }

[RequireComponent(typeof(CelestialObject))]
public class Star : MonoBehaviour
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
    [SerializeField] private string endOfLifeStage; // e.g., white dwarf, neutron star, black hole
    [SerializeField] private bool willSupernova; // Indicates if the star will undergo a supernova explosion
    [SerializeField] private string supernovaTrigger; // Circumstances leading to a supernova explosion

    #endregion
    void Start()
    {

    }
    void Update()
    {

    }
}
