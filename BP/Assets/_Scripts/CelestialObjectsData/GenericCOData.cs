using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGenericData", menuName = "CelestialObjectData/GenericData")]
public class GenericCOData : ScriptableObject
{
    #region Generic information
    [Header("General Information")]

    [SerializeField] private string objectName = "generic_celestial";
    [SerializeField] private CelestialObjectType type;
    [SerializeField] private CelestialRegion region;
    [SerializeField] private float mass;
    [SerializeField] private float radius;
    [SerializeField] private float velocity; // km/s
    [SerializeField] private string age = "0"; // years

    #endregion

    #region Atmosphere
    [Header("Atmosphere Information")]

    [SerializeField] private bool hasAtmosphere;
    [SerializeField] private float atmospherePressure; // in Pascals
    [SerializeField] private List<Element> atmosphereComposition;

    #endregion

    #region Orbit
    [Header("Orbital Information")]

    [SerializeField] private float orbitalRadius; // in kilometers
    [SerializeField] private float orbitalPeriod; // in Earth days
    [SerializeField] private float orbitalEccentricity;
    [SerializeField] private float inclination; // in degrees

    #endregion

    #region Surface
    [Header("Surface Information")]

    [SerializeField] private string surface;
    [SerializeField] private List<Element> groundElements;
    [SerializeField] private float minTemperature; // in Celsius
    [SerializeField] private float averageTemperature; // in Celsius
    [SerializeField] private float maxTemperature; // in Celsius

    #endregion

    #region Gravity & Magnetic field
    [Header("Magentic fields & Gravitational Information")]

    [SerializeField] private float gravity; // in m/s^2
    [SerializeField] private bool hasMagneticField;
    [SerializeField] private float magneticFieldStrength; // in gauss

    #endregion

    #region getters setters
    public void CopyFrom(GenericCOData other)
    {
        objectName = other.objectName;
        type = other.type;
        region = other.region;
        mass = other.mass;
        radius = other.radius;
        velocity = other.velocity;
        age = other.age;
        hasAtmosphere = other.hasAtmosphere;
        atmospherePressure = other.atmospherePressure;
        atmosphereComposition = new List<Element>(other.atmosphereComposition);
        orbitalRadius = other.orbitalRadius;
        orbitalPeriod = other.orbitalPeriod;
        orbitalEccentricity = other.orbitalEccentricity;
        inclination = other.inclination;
        surface = other.surface;
        groundElements = new List<Element>(other.groundElements);
        minTemperature = other.minTemperature;
        averageTemperature = other.averageTemperature;
        maxTemperature = other.maxTemperature;
        gravity = other.gravity;
        hasMagneticField = other.hasMagneticField;
        magneticFieldStrength = other.magneticFieldStrength;
    }

    public string ObjectName
    {
        get { return objectName; }
        set { objectName = value; }
    }

    public CelestialObjectType Type
    {
        get { return type; }
        set { type = value; }
    }

    public CelestialRegion Region
    {
        get { return region; }
        set { region = value; }
    }

    public float Mass
    {
        get { return mass; }
        set { mass = value; }
    }

    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    public float Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    public string Age
    {
        get { return age; }
        set { age = value; }
    }
    public bool HasAtmosphere
    {
        get { return hasAtmosphere; }
        set { hasAtmosphere = value; }
    }

    public float AtmospherePressure
    {
        get { return atmospherePressure; }
        set { atmospherePressure = value; }
    }

    public List<Element> AtmosphereComposition
    {
        get { return atmosphereComposition; }
        set { atmosphereComposition = value; }
    }

    public float OrbitalRadius
    {
        get { return orbitalRadius; }
        set { orbitalRadius = value; }
    }

    public float OrbitalPeriod
    {
        get { return orbitalPeriod; }
        set { orbitalPeriod = value; }
    }

    public float OrbitalEccentricity
    {
        get { return orbitalEccentricity; }
        set { orbitalEccentricity = value; }
    }

    public float Inclination
    {
        get { return inclination; }
        set { inclination = value; }
    }

    public string Surface
    {
        get { return surface; }
        set { surface = value; }
    }

    public List<Element> GroundElements
    {
        get { return groundElements; }
        set { groundElements = value; }
    }

    public float MinTemperature
    {
        get { return minTemperature; }
        set { minTemperature = value; }
    }

    public float AverageTemperature
    {
        get { return averageTemperature; }
        set { averageTemperature = value; }
    }

    public float MaxTemperature
    {
        get { return maxTemperature; }
        set { maxTemperature = value; }
    }

    public float Gravity
    {
        get { return gravity; }
        set { gravity = value; }
    }

    public bool HasMagneticField
    {
        get { return hasMagneticField; }
        set { hasMagneticField = value; }
    }

    public float MagneticFieldStrength
    {
        get { return magneticFieldStrength; }
        set { magneticFieldStrength = value; }
    }

    #endregion
}
