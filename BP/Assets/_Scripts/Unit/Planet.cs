using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CelestialObject))]
public class Planet : MonoBehaviour
{
    #region generic
    [Header("Generic")]
    [SerializeField] private PlanetData planetData;
    public PlanetData CurrentData { get; set; }
    #endregion

    private void Awake()
    {
        CurrentData = ScriptableObject.CreateInstance<PlanetData>();
        CurrentData.CopyFrom(planetData);
    }
    private void Start()
    {

    }

    private void Update()
    {

    }
}
