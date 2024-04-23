using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CelestialObject))]
public class Planet : MonoBehaviour
{
    #region Moons & Rings
    [Header("Moons & Rings Information")]
    public PlanetData planetData;
    public List<Moon> moons;
    public bool hasRings;
    public List<string> ringCharacteristics;

    #endregion
    private void Start()
    {

    }

    private void Update()
    {

    }
}
