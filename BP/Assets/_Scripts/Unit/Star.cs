using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum StarLuminosityType { Nebula, ProtoStar, TTauri, Dwarf, Giant, SuperGiant, HyperGiant }
public enum StarSpectralType { None, Blue, White, Yellow, Orange, Red, Black }
public enum StarDeathType { WhiteDwarf, BlackHole, NeutronStar }
[RequireComponent(typeof(CelestialObject))]
public class Star : MonoBehaviour
{
    #region Star Attributes

    [Header("Star Information")]
    [SerializeField] private StarData starData;
    public StarData CurrentData { get; set; }

    #endregion

    private void Awake()
    {
        CurrentData = ScriptableObject.CreateInstance<StarData>();
        CurrentData.CopyFrom(starData);
    }


    private void Start()
    {
    }
    private void Update()
    {

    }
    public void MajorEvent()
    {
        Debug.Log("Major Event");
    }
}
