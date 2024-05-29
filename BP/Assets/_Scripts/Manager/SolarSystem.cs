using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SolarSystem : MonoBehaviour
{
    public static SolarSystem Instance;
    [SerializeField] private Star star;
    [SerializeField] private List<Planet> planets = new();

    private bool planetMig1 = false;
    private bool planetMig2 = false;
    private string planetMig1Dur = "2000000";
    private string planetMig2Dur = "15000000";
    private List<Planet> selectedPlanets = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void ThrowPlanets()
    {
        foreach (Planet planet in planets)
        {
            planet.gameObject.SetActive(false);
        }
    }
    public void CreatePlanet(string keyword)
    {

        switch (keyword)
        {
            case "JupiterBorn":
                planets[4].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[4].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;

            case "SaturnBorn":
                planets[5].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[5].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;

            case "UranusBorn":
                planets[6].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[6].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;

            case "NeptuneBorn":
                planets[7].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[7].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;

            case "EarthBorn":
                planets[2].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[2].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;

            case "VenusBorn":
                planets[1].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[1].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;

            case "MarsBorn":
                planets[3].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[3].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;

            case "MercuryBorn":
                planets[0].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[0].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;

            case "TheiaBorn":
                planets[8].transform.GetComponent<MeshRenderer>().enabled = true;
                planets[8].transform.GetComponent<OrbitalMovement>().showOrbit = true;
                break;
            case "TheiaDestroyed":
                planets[8].transform.GetComponent<OrbitalMovement>().orbitSpeed = planets[8].transform.GetComponent<OrbitalMovement>().orbitSpeed / 4;
                break;
        }
        Debug.Log("Creating Planet");
    }

    public void MoonFormation(Planet planet)
    {
        if (planet.gameObject.name != "Earth")
        {
            planet.gameObject.SetActive(false);
        }
    }
    public void HandleSolarSystemEvent(string keyword)
    {
        switch (keyword)
        {
            case "PlanetMigration1":
                planetMig1 = true;
                selectedPlanets = planets.Where(p => p.GetComponent<CelestialObject>().CurrentData.ObjectName.ToLower() == "jupiter" ||
                                                            p.GetComponent<CelestialObject>().CurrentData.ObjectName.ToLower() == "saturn" ||
                                                            p.GetComponent<CelestialObject>().CurrentData.ObjectName.ToLower() == "urán" ||
                                                            p.GetComponent<CelestialObject>().CurrentData.ObjectName.ToLower() == "neptún").ToList();
                break;
            case "PlanetMigration2":
                planetMig2 = true;
                selectedPlanets = planets.Where(p => p.GetComponent<CelestialObject>().CurrentData.ObjectName.ToLower() == "urán" ||
                                                    p.GetComponent<CelestialObject>().CurrentData.ObjectName.ToLower() == "neptún").ToList();
                break;
        }
    }

    public void YearlyEvent(BigInteger yearDifference)
    {
        if (planetMig1)
        {
            planetMig1Dur = PlanetMigrate(planetMig1Dur, (ulong)yearDifference, selectedPlanets);
            if (planetMig1Dur == "0")
            {
                planetMig1 = false;
            }
        }
        if (planetMig2)
        {
            planetMig2Dur = PlanetMigrate(planetMig2Dur, (ulong)yearDifference, selectedPlanets);
            if (planetMig2Dur == "0")
            {
                planetMig2 = false;
            }
        }
    }

    private string PlanetMigrate(string duration, ulong timeDiff, List<Planet> planets)
    {
        ulong remainingYears = ulong.Parse(duration);
        remainingYears = remainingYears > timeDiff ? remainingYears - timeDiff : 0;
        string yearsLeft = remainingYears.ToString();
        foreach (Planet planet in planets)
        {
            CelestialObject csPlanet = planet.GetComponent<CelestialObject>();
            switch (csPlanet.CurrentData.ObjectName.ToLower())
            {
                case "jupiter":
                    if (planetMig1)
                        MovePlanet(planet, 720.3f, 520.3f, 520.3f, 0.0001f, 0.00005f, 0.00005f, remainingYears);
                    break;
                case "saturn":
                    if (planetMig1)
                        MovePlanet(planet, 1353.7f, 953.7f, 953.7f, 0.0002f, 0.000115f, 0.000115f, remainingYears);
                    break;
                case "urán":
                    if (planetMig1)
                        MovePlanet(planet, 1919.1f, 1419.1f, 1419.1f, 0.0003f, 0.0002075f, 0.000265f, remainingYears);

                    if (planetMig2)
                        MovePlanet(planet, 2419.1f, 1919.1f, 1919.1f, 3.3e-5f, 3.3e-5f, 3.3e-5f, remainingYears);
                    break;
                case "neptún":
                    if (planetMig1)
                        MovePlanet(planet, 3026.8f, 2206.8f, 2206.8f, 0.000692f, 0.0004435f, 0.00054205f, remainingYears);

                    if (planetMig2)
                        MovePlanet(planet, 3606.8f, 3006.8f, 3006.8f, 3.86e-5f, 5.3e-5f, 5.3e-5f, remainingYears);
                    break;
            }
        }
        return yearsLeft;
    }

    private void MovePlanet(Planet planet, float endPosX, float endPosY, float endPosZ, float stepX, float stepY, float stepZ, ulong remainingYears)
    {
        OrbitalMovement csPlanetOrbit = planet.GetComponent<OrbitalMovement>();

        if (remainingYears <= 0)
        {
            csPlanetOrbit.xRadius = endPosX;
            csPlanetOrbit.yRadius = endPosY;
            csPlanetOrbit.zRadius = endPosZ;
        }
        else
        {
            float currentRadiusX = csPlanetOrbit.xRadius;
            float currentRadiusY = csPlanetOrbit.yRadius;
            float currentRadiusZ = csPlanetOrbit.zRadius;
            currentRadiusX += stepX;
            currentRadiusY += stepY;
            currentRadiusZ += stepZ;

            csPlanetOrbit.xRadius += currentRadiusX;
            csPlanetOrbit.yRadius += currentRadiusY;
            csPlanetOrbit.zRadius += currentRadiusZ;

            if (csPlanetOrbit.xRadius > endPosX)
                csPlanetOrbit.xRadius = endPosX;

            if (csPlanetOrbit.yRadius > endPosY)
                csPlanetOrbit.yRadius = endPosY;

            if (csPlanetOrbit.zRadius > endPosZ)
                csPlanetOrbit.zRadius = endPosZ;
        }
    }
}
