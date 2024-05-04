using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public enum CelestialObjectType { Planet, Moon, Star, Asteroid }
public enum CelestialRegion { Star, InnerPlanets, OuterPlanets, AsteroidBelt, KuiperBelt, OortCloud }
[System.Serializable]
[RequireComponent(typeof(LifeSpanController))]
[RequireComponent(typeof(SphereCollider))]
public class CelestialObject : MonoBehaviour
{
    #region Generic information
    [Header("General Information")]
    public GenericCOData celestialObjectData;
    public string objectName;
    public CelestialObjectType type;
    public CelestialRegion region;
    public float mass;
    public float diameter;
    public float radius;
    public float velocity;
    public BigInteger age; // years

    #endregion

    #region Atmosphere
    [Header("Atmosphere Information")]

    public bool hasAtmosphere;
    public float atmospherePressure; // in Pascals
    public List<Element> atmosphereComposition;

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

    public List<string> surfaceFeatures;
    public List<Element> groundElements;
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

    #region Unity object 
    [Header("Unity properties")]
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private UnityEvent<CelestialObject> onPlayerEnter;
    #endregion
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 1f;
    }

    private void Start()
    {
        onPlayerEnter.AddListener(CanvasManager.Instance.CelestialObjectInfo.OnEnterRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            Debug.Log("CS-OBJ: " + gameObject.name + " -> IN RANGE");
            onPlayerEnter?.Invoke(this);
        }
    }

    private void Update()
    {

    }




}
