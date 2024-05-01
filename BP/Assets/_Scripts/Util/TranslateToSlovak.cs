using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateToSlovak
{
    private static TranslateToSlovak instance;

    public static TranslateToSlovak Instance
    {
        get
        {
            instance ??= new TranslateToSlovak();
            return instance;
        }
    }

    private TranslateToSlovak() { }

    public string TimeScaleToSlovak(string timeUnit)
    {
        string skUnit = "";
        switch (timeUnit)
        {
            case "sec":
                skUnit = "sek";
                break;
            case "min":
                skUnit = "min";
                break;
            case "hr":
                skUnit = "hod";
                break;
            case "day":
                skUnit = "deň";
                break;
            case "yr":
                skUnit = "rok";
                break;
            case "10 yrs":
                skUnit = "10 rokov";
                break;
            case "100 yrs":
                skUnit = "100 rokov";
                break;
            case "1000 yrs":
                skUnit = "1000 rokov";
                break;
            case "10k yrs":
                skUnit = "10k rokov";
                break;
            case "100k yrs":
                skUnit = "100k rokov";
                break;
            case "1mil yrs":
                skUnit = "1mil rokov";
                break;
        }
        return skUnit;
    }

    #region ElementTranslation
    public string ElementStateToSlovak(ElementState state)
    {
        string skState = "";
        switch (state)
        {
            case ElementState.Gas:
                skState = "plynné";
                break;
            case ElementState.Liquid:
                skState = "kvapalné";
                break;
            case ElementState.Plasma:
                skState = "plasma";
                break;
            case ElementState.Solid:
                skState = "pevné";
                break;
            case ElementState.Unknown:
                skState = "neznáme";
                break;
        }
        return skState;
    }

    public string DecayModeToSlovak(DecayMode state)
    {
        string skState = "";
        switch (state)
        {
            case DecayMode.Alpha_decay:
                skState = "Alfa rozpad";
                break;
            case DecayMode.Beta_decay:
                skState = "Beta rozpad";
                break;
            case DecayMode.Electron_capture:
                skState = "Záchyt elektrónov";
                break;
            case DecayMode.Neutron_emission:
                skState = "Neutrónová emisia";
                break;
            case DecayMode.Positron_emission:
                skState = "Emisia pozitrónov";
                break;
            case DecayMode.Proton_emission:
                skState = "Protónová emisia";
                break;
            case DecayMode.Spontaneous_fission:
                skState = "Samovolné štiepenie";
                break;
            case DecayMode.Stable:
                skState = "Stabilné";
                break;
        }
        return skState;
    }
    #endregion

    #region StarTranslation
    public string StarTypeToSlovak(StarLuminosityType luminosityType)
    {
        string skLuminosityType = "";
        switch (luminosityType)
        {
            case StarLuminosityType.Nebula:
                skLuminosityType = "Nebula";
                break;
            case StarLuminosityType.Giant:
                skLuminosityType = "Obor";
                break;
            case StarLuminosityType.HyperGiant:
                skLuminosityType = "Hyper obor";
                break;
            case StarLuminosityType.Dwarf:
                skLuminosityType = "Trpaslík";
                break;
            case StarLuminosityType.SuperGiant:
                skLuminosityType = "Super obor";
                break;
            case StarLuminosityType.TTauri:
                skLuminosityType = "T Tauri";
                break;
        }
        return skLuminosityType;
    }

    public string StarSpectralTypeToSlovak(StarSpectralType spectralType)
    {
        string skSpectralType = "";
        switch (spectralType)
        {
            case StarSpectralType.Black:
                skSpectralType = "Čierny";
                break;
            case StarSpectralType.Blue:
                skSpectralType = "Modrý";
                break;
            case StarSpectralType.None:
                skSpectralType = "Žiadny";
                break;
            case StarSpectralType.Orange:
                skSpectralType = "Oranžový";
                break;
            case StarSpectralType.Red:
                skSpectralType = "Červený";
                break;
            case StarSpectralType.White:
                skSpectralType = "Biely";
                break;
            case StarSpectralType.Yellow:
                skSpectralType = "Žltý";
                break;
        }
        return skSpectralType;
    }

    public string StarDeathTypeToSlovak(StarDeathType spectralType)
    {
        string skStarDeathType = "";
        switch (spectralType)
        {
            case StarDeathType.BlackHole:
                skStarDeathType = "Čierna diera";
                break;
            case StarDeathType.NeutronStar:
                skStarDeathType = "Neutrónová hviezda";
                break;
            case StarDeathType.WhiteDwarf:
                skStarDeathType = "Biely Trpaslík";
                break;
        }
        return skStarDeathType;
    }
    #endregion

    #region CelestialTranslation
    public string CelestialTypeToSlovak(CelestialObjectType celestialObjectType)
    {
        string skCelestialObjectType = "";
        switch (celestialObjectType)
        {
            case CelestialObjectType.Asteroid:
                skCelestialObjectType = "Asteroid";
                break;
            case CelestialObjectType.Moon:
                skCelestialObjectType = "Mesiac";
                break;
            case CelestialObjectType.Planet:
                skCelestialObjectType = "Planéta";
                break;
            case CelestialObjectType.Star:
                skCelestialObjectType = "Hviezda";
                break;
        }
        return skCelestialObjectType;
    }

    public string CelestialRegionToSlovak(CelestialRegion celestialRegion)
    {
        string skCelestialRegion = "";
        switch (celestialRegion)
        {
            case CelestialRegion.Star:
                skCelestialRegion = "Oblasť hviezdy";
                break;
            case CelestialRegion.OuterPlanets:
                skCelestialRegion = "Vonkajšie planéty";
                break;
            case CelestialRegion.OortCloud:
                skCelestialRegion = "Oortov oblak";
                break;
            case CelestialRegion.KuiperBelt:
                skCelestialRegion = "Kuiperov pás";
                break;
            case CelestialRegion.InnerPlanets:
                skCelestialRegion = "Vnútorné planéty";
                break;
            case CelestialRegion.AsteroidBelt:
                skCelestialRegion = "Pásmo astroidov";
                break;
        }
        return skCelestialRegion;
    }
    #endregion

}
