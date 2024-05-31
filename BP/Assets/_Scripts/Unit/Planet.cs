using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

[RequireComponent(typeof(CelestialObject))]
public class Planet : MonoBehaviour
{
    #region generic
    [Header("Generic")]
    [SerializeField] private PlanetData planetData;
    public PlanetData CurrentData { get; set; }
    public PlanetData TodayData { get; set; }
    #endregion

    public void ChangePlanet(string keyword)
    {
        (string, int) res = ExtractParts(keyword);

        Debug.Log(res);

        if (res.Item1 == transform.name)
        {
            ChangePlanetMaterial(res.Item2);
            Debug.Log(transform.name + " EVENT");
        }
    }
    private void ChangePlanetMaterial(int index)
    {
        GetComponent<MeshRenderer>().material = planetData.PlanetStages[index];
    }

    private (string, int) ExtractParts(string input)
    {
        string pattern = @"^([A-Za-z]+)(\d+)$";

        Match match = Regex.Match(input, pattern);

        if (match.Success)
        {
            string alphabeticPart = match.Groups[1].Value;
            int numericPart = int.Parse(match.Groups[2].Value);
            return (alphabeticPart, numericPart);
        }
        return (string.Empty, 0);
    }

    private void Awake()
    {
        CurrentData = ScriptableObject.CreateInstance<PlanetData>();
        CurrentData.CopyFrom(planetData);
        TodayData.CopyFrom(planetData);
    }
    private void Start()
    {

    }

    private void Update()
    {

    }
}
