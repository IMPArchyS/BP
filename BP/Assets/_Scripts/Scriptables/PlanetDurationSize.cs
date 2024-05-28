using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDurationSize : ScriptableObject
{
    [Header("Duration Properties")]
    [SerializeField] private string formationDuration;
    [SerializeField] private float formationScale;

    // getters setters
    public void CopyFrom(PlanetDurationSize other)
    {
        formationDuration = other.formationDuration;
        formationScale = other.formationScale;
    }
    public string FormationDuration
    {
        get { return formationDuration; }
        set { formationDuration = value; }
    }

    public float FormationScale
    {
        get { return formationScale; }
        set { formationScale = value; }
    }
}
