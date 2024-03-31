using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StarLuminosityType { Nebula, TTauri, Dwarf, Giant, SuperGiant, HyperGiant }
public enum StarSpectralType { None, Blue, White, Yellow, Orange, Red, Black }

public class Star : CelestialObject
{
    [SerializeField] private double velocity;
    [SerializeField] private double mass;
    [SerializeField] private double radius;
    [SerializeField] private double temperature;
    [SerializeField] private double age;
    [SerializeField] private StarLuminosityType luminosityType;
    [SerializeField] private StarSpectralType spectralType;
    override public void Start()
    {

    }
    override public void Update()
    {

    }
}
