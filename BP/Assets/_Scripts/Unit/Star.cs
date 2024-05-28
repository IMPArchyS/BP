using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Animations;

public enum StarLuminosityType { ProtoStar, TTauri, Dwarf, Giant, SuperGiant, HyperGiant }
public enum StarSpectralType { None, Blue, White, Yellow, Orange, Red, Black }
public enum StarDeathType { WhiteDwarf, BlackHole, NeutronStar }
[RequireComponent(typeof(CelestialObject))]
public class Star : MonoBehaviour
{
    #region Star Attributes

    [Header("Star Information")]
    [SerializeField] private StarLuminosityType luminosityType;
    [SerializeField] private StarSpectralType spectralType;
    [SerializeField] private StarData starData;
    [SerializeField] private StarDurationSizes starDurationData;
    public StarData CurrentData { get; set; }
    public StarDurationSizes CurrentStarDurationData { get; set; }

    #endregion

    private void Awake()
    {
        CurrentData = ScriptableObject.CreateInstance<StarData>();
        CurrentData.CopyFrom(starData);

        CurrentStarDurationData = ScriptableObject.CreateInstance<StarDurationSizes>();
        CurrentStarDurationData.CopyFrom(starDurationData);
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

    public void YearlyEvent(BigInteger yearDifference)
    {
        // Scale / time duration
        switch (luminosityType)
        {
            case StarLuminosityType.ProtoStar:
                CurrentStarDurationData.ProtoStarDuration = StarGrowth(CurrentStarDurationData.ProtoStarDuration, CurrentStarDurationData.TTauriStarScale,
                                                                        0.00003f * (float)yearDifference, StarLuminosityType.TTauri, (ulong)yearDifference);
                Debug.Log(CurrentStarDurationData.ProtoStarDuration);
                break;
            case StarLuminosityType.TTauri:
                CurrentStarDurationData.TTauriDuration = StarGrowth(CurrentStarDurationData.TTauriDuration, CurrentStarDurationData.MainSequenceScale,
                                                                        0.0000004f * (float)yearDifference, StarLuminosityType.Dwarf, (ulong)yearDifference);
                break;
            case StarLuminosityType.Dwarf:
                break;
            case StarLuminosityType.Giant:
                break;
            case StarLuminosityType.SuperGiant:
                break;
            case StarLuminosityType.HyperGiant:
                break;
            default:
                break;
        }
    }

    private string StarGrowth(string duration, float endScale, float scaleIncrement, StarLuminosityType nextLST, ulong timeDiff)
    {
        ulong remainingYears = ulong.Parse(duration);

        remainingYears = remainingYears > timeDiff ? remainingYears - timeDiff : 0;
        string yearsLeft = remainingYears.ToString();

        if (remainingYears <= 0)
        {
            transform.localScale = new UnityEngine.Vector3(endScale, endScale, endScale);
            CurrentData.LuminosityType = nextLST;
            Debug.Log("[" + remainingYears + "] -> " + luminosityType + " -> SIZE GROW");
            return yearsLeft;
        }
        float currentScale = transform.localScale.x;
        currentScale += scaleIncrement;
        if (currentScale > endScale)
            currentScale = endScale;
        transform.localScale = new UnityEngine.Vector3(currentScale, currentScale, currentScale);

        //Debug.Log("{STAR} -> [" + remainingYears + "] -> " + luminosityType + " -> SIZE GROW");
        return yearsLeft;
    }
}
