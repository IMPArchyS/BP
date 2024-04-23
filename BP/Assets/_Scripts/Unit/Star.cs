using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarLuminosityType { Nebula, TTauri, Dwarf, Giant, SuperGiant, HyperGiant }
public enum StarSpectralType { None, Blue, White, Yellow, Orange, Red, Black }
public enum StarDeathType { WhiteDwarf, BlackHole, NeutronStar }
[RequireComponent(typeof(CelestialObject))]
public class Star : MonoBehaviour
{
    #region Star Attributes

    [Header("Star Information")]
    public StarData starData;
    [SerializeField] private CelestialObject celestialObject;
    [SerializeField] private StarLuminosityType luminosityType;
    [SerializeField] private StarSpectralType spectralType;

    [Header("Star Properties")]
    [SerializeField] private float temperature; // in Kelvin
    [SerializeField] private float luminosity; // in solar luminosities
    [SerializeField] private string fusionProcess;
    [SerializeField] private float stellarWindIntensity; // in solar masses per year
    [SerializeField] private StarDeathType endOfLifeStage; // e.g., white dwarf, neutron star, black hole

    #endregion

    private void Awake()
    {

    }

    private void Start()
    {

    }
    private void Update()
    {

    }
}
