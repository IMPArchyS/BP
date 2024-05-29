using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
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
    #region star utils
    [Header("Star Utils")]
    [SerializeField] private ParticleSystem insideProtoDisk;
    [SerializeField] private ParticleSystem outsideProtoDisk;
    [SerializeField] private ParticleSystem outerProtoDisk;
    [SerializeField] private ParticleSystem nebulaEmmision;
    [SerializeField] private List<Material> starStageMaterials;
    [SerializeField] private List<ParticleSystem> starStageFX;
    [SerializeField] private List<ParticleSystem> starStageGlow;

    #endregion
    #region private stuff
    private bool firstTrigger = false;
    private bool redGiantGrowth = false;
    private bool redGiantGrowth2 = false;
    private bool whiteDwarfTrigger = false;
    #endregion
    private void Awake()
    {
        CurrentData = ScriptableObject.CreateInstance<StarData>();
        CurrentData.CopyFrom(starData);

        CurrentStarDurationData = ScriptableObject.CreateInstance<StarDurationSizes>();
        CurrentStarDurationData.CopyFrom(starDurationData);
    }

    private void LateUpdate()
    {
        SpinPlanetaryDisk();
    }

    private void Start()
    {
        // debug log child 0 to 10
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i));
        }
    }

    public void SpinPlanetaryDisk()
    {
        if (insideProtoDisk == null || outsideProtoDisk == null || outerProtoDisk == null)
            return;

        insideProtoDisk.transform.Rotate(UnityEngine.Vector3.up, 10 * Time.deltaTime, Space.World);
        outsideProtoDisk.transform.Rotate(UnityEngine.Vector3.up, 5 * Time.deltaTime, Space.World);
        outerProtoDisk.transform.Rotate(UnityEngine.Vector3.up, 2 * Time.deltaTime, Space.World);
    }

    public void MajorEvent(string keyword)
    {
        switch (keyword)
        {
            case "RedGiantGrowth":
                redGiantGrowth = true;
                redGiantGrowth2 = true;
                break;

            case "WhiteDwarf":
                CurrentData.SpectralType = StarSpectralType.White;
                spectralType = StarSpectralType.White;
                CurrentData.LuminosityType = StarLuminosityType.Dwarf;
                luminosityType = StarLuminosityType.Dwarf;
                transform.GetChild(6).GetComponent<MeshRenderer>().material = starStageMaterials[2];
                break;

            case "WhiteDwarfTrigger":
                whiteDwarfTrigger = true;
                break;
            case "TTauri Star":
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(6).GetComponent<MeshRenderer>().material = starStageMaterials[1];
                break;

            case "EarlyMainSequence":
                StartCoroutine(InnerProtoDiskExplode());
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(true);
                transform.GetChild(5).gameObject.SetActive(true);
                transform.GetChild(6).GetComponent<MeshRenderer>().material = starStageMaterials[2];
                break;

            case "MainPlanetsFormed":
                StartCoroutine(ProtoDiskExplode(insideProtoDisk));
                break;

            case "OuterDiskFaids":
                StartCoroutine(ProtoDiskExplode(outerProtoDisk));
                break;

            default:
                break;
        }
    }

    private IEnumerator ProtoDiskExplode(ParticleSystem disk)
    {
        int stellarTimeScale;
        if (MainTimeController.Instance.StellarTimeScale > MainTimeController.Instance.StellarYear)
            stellarTimeScale = MainTimeController.Instance.StellarYear;
        else
            stellarTimeScale = (int)MainTimeController.Instance.StellarTimeScale;

        while (disk.transform.localScale.x > 0)
        {
            float currentScale = disk.transform.localScale.x;
            currentScale -= 0.05f * Time.deltaTime * stellarTimeScale;
            if (currentScale < 0)
                currentScale = 0;
            disk.transform.localScale = new UnityEngine.Vector3(currentScale, currentScale, currentScale);
            yield return null;
        }
        disk.gameObject.SetActive(false);
    }

    private IEnumerator InnerProtoDiskExplode()
    {
        int stellarTimeScale;
        if (MainTimeController.Instance.StellarTimeScale > MainTimeController.Instance.StellarYear)
            stellarTimeScale = MainTimeController.Instance.StellarYear;
        else
            stellarTimeScale = (int)MainTimeController.Instance.StellarTimeScale;

        while (outsideProtoDisk.transform.localScale.x > 0)
        {
            float currentScale = outsideProtoDisk.transform.localScale.x;
            var main = insideProtoDisk.main;
            currentScale -= 0.05f * Time.deltaTime * stellarTimeScale;
            if (currentScale < 0)
                currentScale = 0;
            outsideProtoDisk.transform.localScale = new UnityEngine.Vector3(currentScale, currentScale, currentScale);

            Color currentColor = main.startColor.Evaluate(0); // Use the Evaluate method to get the color value
            float h, s, v;
            Color.RGBToHSV(currentColor, out h, out s, out v);
            v -= 0.0001f * Time.deltaTime * stellarTimeScale; // Adjust this value as needed
            if (v < 0.7f) // 50% of the original brightness
                v = 0.7f;
            main.startColor = Color.HSVToRGB(h, s, v);

            yield return null;
        }
        outsideProtoDisk.gameObject.SetActive(false);
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
                break;
            case StarLuminosityType.TTauri:
                CurrentStarDurationData.TTauriDuration = StarShrink(CurrentStarDurationData.TTauriDuration, CurrentStarDurationData.MainSequenceScale,
                                                                        0.00000044f * (float)yearDifference, StarLuminosityType.Dwarf, (ulong)yearDifference);
                break;
            case StarLuminosityType.Dwarf:
                if (CurrentData.SpectralType != StarSpectralType.Yellow) return;
                if (CurrentStarDurationData.RedGiantDuration == "0") return;

                if (CurrentData.SpectralType == StarSpectralType.White)
                {

                }

                if (MainTimeController.Instance.Epoch == 4 && redGiantGrowth)
                    CurrentStarDurationData.MainSequenceDuration = StarGrowth(CurrentStarDurationData.MainSequenceDuration, 75,
                                                                0.0000001875f * (float)yearDifference, StarLuminosityType.Giant, (ulong)yearDifference);
                break;
            case StarLuminosityType.Giant:
                CurrentData.SpectralType = StarSpectralType.Red;
                spectralType = StarSpectralType.Red;

                if (MainTimeController.Instance.Epoch == 4 && redGiantGrowth2 && CurrentStarDurationData.RedGiantDuration != "0")
                    CurrentStarDurationData.RedGiantDuration = StarGrowth(CurrentStarDurationData.RedGiantDuration, CurrentStarDurationData.RedGiantScale,
                                                                        0.00000037f * (float)yearDifference, StarLuminosityType.Giant, (ulong)yearDifference);

                if (MainTimeController.Instance.Epoch == 4 && redGiantGrowth2 && whiteDwarfTrigger && CurrentStarDurationData.WhiteDwarfDuration != "0")
                {
                    CurrentStarDurationData.WhiteDwarfDuration = StarShrink(CurrentStarDurationData.WhiteDwarfDuration, CurrentStarDurationData.WhiteDwarfScale,
                                                                        0.00008f * (float)yearDifference, StarLuminosityType.Dwarf, (ulong)yearDifference);
                }
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
        currentScale += scaleIncrement;
        if (currentScale > endScale)
            currentScale = endScale;

        transform.localScale = new UnityEngine.Vector3(currentScale, currentScale, currentScale);
        return yearsLeft;
    }
}
