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
[RequireComponent(typeof(CelestialRotation))]
[RequireComponent(typeof(OrbitalMovement))]
public class CelestialObject : MonoBehaviour
{
    #region Generic information
    [Header("General Information")]
    [SerializeField] private GenericCOData celestialObjectData;
    public GenericCOData CurrentData { get; set; }
    #endregion

    #region Unity object 
    [Header("Unity properties")]
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private UnityEvent<CelestialObject> onPlayerEnter;
    [SerializeField] private UnityEvent onPlayerExit;
    public BigInteger AgeBigInt = 0;
    private static CelestialObject currentCelestial;
    #endregion
    private void Awake()
    {
        CurrentData = ScriptableObject.CreateInstance<GenericCOData>();
        CurrentData.CopyFrom(celestialObjectData);
        AgeBigInt = BigInteger.Parse(CurrentData.Age);
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = 1.25f;
    }

    private void Start()
    {
        onPlayerEnter.AddListener(CanvasManager.Instance.CelestialObjectInfo.OnEnterRange);
        onPlayerExit.AddListener(CanvasManager.Instance.CelestialObjectInfo.OnExitRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera") && currentCelestial == null)
        {
            Debug.Log("CS-OBJ: " + gameObject.name + " -> IN RANGE");
            onPlayerEnter?.Invoke(this);
            currentCelestial = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera") && currentCelestial == this)
        {
            Debug.Log("CS-OBJ: " + gameObject.name + " -> OUT OF RANGE");
            onPlayerExit?.Invoke();
            currentCelestial = null;
        }
    }

    private void Update()
    {
    }
}
