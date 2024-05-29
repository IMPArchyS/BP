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
                planets[4].gameObject.SetActive(true);
                break;

            case "SaturnBorn":
                planets[5].gameObject.SetActive(true);
                break;

            case "UranusBorn":
                planets[6].gameObject.SetActive(true);
                break;

            case "NeptuneBorn":
                planets[7].gameObject.SetActive(true);
                break;

            case "EarthBorn":
                planets[2].gameObject.SetActive(true);
                break;

            case "VenusBorn":
                planets[1].gameObject.SetActive(true);
                break;

            case "MarsBorn":
                planets[3].gameObject.SetActive(true);
                break;

            case "MercuryBorn":
                planets[0].gameObject.SetActive(true);
                break;

            case "TheiaBorn":
                planets[8].gameObject.SetActive(true);
                break;
        }
        Debug.Log("Creating Planet");
    }
}
