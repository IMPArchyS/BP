using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CelestialObjectType { Planet, Moon, Star, Asteroid }
public enum CelestialRegion { NearStar, InnerPlanets, OuterPlanets, AsteroidBelt, KuiperBelt, OortCloud }
[System.Serializable]
public class CelestialObject : MonoBehaviour
{
    #region Generic information
    [Header("General Information")]

    public string objectName;
    public CelestialObjectType type;
    public CelestialRegion region;
    public float mass;
    public float diameter;
    public float radius;
    public float velocity;
    public long age; // years

    #endregion

    #region Atmosphere
    [Header("Atmosphere Information")]

    public bool hasAtmosphere;
    public string atmosphereComposition;

    #endregion

    #region Orbit
    [Header("Orbital Information")]

    public float orbitalRadius; // in kilometers
    public float orbitalPeriod; // in Earth days
    public float orbitalEccentricity;
    public float inclination; // in degrees

    #endregion

    #region Surface
    [Header("Surface Information")]

    public string[] surfaceFeatures;
    public float minTemperature; // in Celsius
    public float averageTemperature; // in Celsius
    public float maxTemperature; // in Celsius

    #endregion

    #region Gravity & Magnetic field
    [Header("Magentic fields & Gravitational Information")]

    public float gravity; // in m/s^2
    public float rotationPeriod; // in Earth days
    public bool hasMagneticField;
    public float magneticFieldStrength; // in Tesla

    #endregion

    virtual public void Start()
    {

    }

    virtual public void Update()
    {

    }
}
