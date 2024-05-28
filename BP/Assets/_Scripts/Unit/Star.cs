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

    private bool firstTrigger = false;
    private bool redGiantGrowth = false;
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
        //redGiantGrowth = true;
    }

    public void YearlyEvent(BigInteger yearDifference)
    {
        if (yearDifference == 0 || !firstTrigger)
        {
            firstTrigger = true;
            return;
        }
        // Scale / time duration -> grow
        // init scale / scale -> shrink
        switch (CurrentData.LuminosityType)
        {
            case StarLuminosityType.ProtoStar:
                CurrentStarDurationData.ProtoStarDuration = StarShrink(CurrentStarDurationData.ProtoStarDuration, CurrentStarDurationData.TTauriStarScale,
                                                                        0.0066f * (float)yearDifference, StarLuminosityType.TTauri, (ulong)yearDifference);
                Debug.Log(CurrentStarDurationData.ProtoStarDuration);
                break;
            case StarLuminosityType.TTauri:
                CurrentStarDurationData.TTauriDuration = StarShrink(CurrentStarDurationData.TTauriDuration, CurrentStarDurationData.MainSequenceScale,
                                                                        0.00000044f * (float)yearDifference, StarLuminosityType.Dwarf, (ulong)yearDifference);
                break;
            case StarLuminosityType.Dwarf:
                if (CurrentData.SpectralType != StarSpectralType.Yellow) return;

                if (MainTimeController.Instance.Epoch == 4 && redGiantGrowth)
                    CurrentStarDurationData.MainSequenceDuration = StarGrowth(CurrentStarDurationData.MainSequenceDuration, CurrentStarDurationData.RedGiantScale,
                                                                1.04e-9f * (float)yearDifference, StarLuminosityType.Giant, (ulong)yearDifference);
                break;
            case StarLuminosityType.Giant:
                CurrentData.SpectralType = StarSpectralType.Red;
                CurrentStarDurationData.RedGiantDuration = StarShrink(CurrentStarDurationData.RedGiantDuration, CurrentStarDurationData.WhiteDwarfScale,
                                                3.6e-9f * (float)yearDifference, StarLuminosityType.Dwarf, (ulong)yearDifference);
                break;
            case StarLuminosityType.SuperGiant:
                break;
            case StarLuminosityType.HyperGiant:
                break;
            default:
                break;
        }
    }
    private string StarShrink(string duration, float endScale, float scaleDecrement, StarLuminosityType nextLST, ulong timeDiff)
    {
        ulong remainingYears = ulong.Parse(duration);

        remainingYears = remainingYears > timeDiff ? remainingYears - timeDiff : 0;
        string yearsLeft = remainingYears.ToString();

        if (remainingYears <= 0)
        {
            transform.localScale = new UnityEngine.Vector3(endScale, endScale, endScale);
            CurrentData.LuminosityType = nextLST;
            luminosityType = nextLST;
            Debug.Log("[" + remainingYears + "] -> " + luminosityType + " -> SIZE SHRINK");
            return yearsLeft;
        }
        float currentScale = transform.localScale.x;
        currentScale -= scaleDecrement;
        if (currentScale < endScale)
            currentScale = endScale;
        transform.localScale = new UnityEngine.Vector3(currentScale, currentScale, currentScale);

        //Debug.Log("{STAR} -> [" + remainingYears + "] -> " + luminosityType + " -> SIZE GROW");
        return yearsLeft;
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
            luminosityType = nextLST;
            Debug.Log("[" + remainingYears + "] -> " + luminosityType + " -> SIZE GROW");
            return yearsLeft;
        }
        float currentScale = transform.localScale.x;
        currentScale += scaleIncrement * timeDiff;
        if (currentScale > endScale)
            currentScale = endScale;

        Debug.Log(remainingYears);
        transform.localScale = new UnityEngine.Vector3(currentScale, currentScale, currentScale);

        //Debug.Log("{STAR} -> [" + remainingYears + "] -> " + luminosityType + " -> SIZE GROW");
        return yearsLeft;
    }
}
