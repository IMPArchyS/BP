using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public static SolarSystem Instance;
    [SerializeField] private Star star;
    [SerializeField] private List<Planet> planets = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        }
        Debug.Log("Creating Planet");
    }
}
