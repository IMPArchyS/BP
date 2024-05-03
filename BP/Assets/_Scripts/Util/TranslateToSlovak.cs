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
            case "100mil yrs":
                skUnit = "100mil rokov";
                break;
            case "1bil yrs":
                skUnit = "1mld rokov";
                break;
            case "1mld yrs":
                skUnit = "1bil rokov";
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

    #region ElementTranslation
    public string ElementNameToSlovak(int atomicNumber)
    {
        string skElementName = "";
        switch (atomicNumber)
        {
            case 1:
                skElementName = "Vodík";
                break;
            case 2:
                skElementName = "Hélium";
                break;
            case 3:
                skElementName = "Lítium";
                break;
            case 4:
                skElementName = "Berýlium";
                break;
            case 5:
                skElementName = "Bór";
                break;
            case 6:
                skElementName = "Uhlík";
                break;
            case 7:
                skElementName = "Dusík";
                break;
            case 8:
                skElementName = "Kyslík";
                break;
            case 9:
                skElementName = "Fluór";
                break;
            case 10:
                skElementName = "Neón";
                break;
            case 11:
                skElementName = "Sodík";
                break;
            case 12:
                skElementName = "Horčík";
                break;
            case 13:
                skElementName = "Hliník";
                break;
            case 14:
                skElementName = "Kremík";
                break;
            case 15:
                skElementName = "Fosfor";
                break;
            case 16:
                skElementName = "Síra";
                break;
            case 17:
                skElementName = "Chlór";
                break;
            case 18:
                skElementName = "Argón";
                break;
            case 19:
                skElementName = "Draslík";
                break;
            case 20:
                skElementName = "Vápnik";
                break;
            case 21:
                skElementName = "Skandium";
                break;
            case 22:
                skElementName = "Titán";
                break;
            case 23:
                skElementName = "Vanád";
                break;
            case 24:
                skElementName = "Chróm";
                break;
            case 25:
                skElementName = "Mangán";
                break;
            case 26:
                skElementName = "Železo";
                break;
            case 27:
                skElementName = "Kobalt";
                break;
            case 28:
                skElementName = "Nikel";
                break;
            case 29:
                skElementName = "Meď";
                break;
            case 30:
                skElementName = "Zinok";
                break;
            case 31:
                skElementName = "Gálium";
                break;
            case 32:
                skElementName = "Germánium";
                break;
            case 33:
                skElementName = "Arzén";
                break;
            case 34:
                skElementName = "Selén";
                break;
            case 35:
                skElementName = "Bróm";
                break;
            case 36:
                skElementName = "Kryptón";
                break;
            case 37:
                skElementName = "Rubídium";
                break;
            case 38:
                skElementName = "Stroncium";
                break;
            case 39:
                skElementName = "Yttrium";
                break;
            case 40:
                skElementName = "Zirkónium";
                break;
            case 41:
                skElementName = "Niób";
                break;
            case 42:
                skElementName = "Molybdén";
                break;
            case 43:
                skElementName = "Technécium";
                break;
            case 44:
                skElementName = "Ruténium";
                break;
            case 45:
                skElementName = "Ródium";
                break;
            case 46:
                skElementName = "Paládium";
                break;
            case 47:
                skElementName = "Striebro";
                break;
            case 48:
                skElementName = "Kadmium";
                break;
            case 49:
                skElementName = "Indium";
                break;
            case 50:
                skElementName = "Cín";
                break;
            case 51:
                skElementName = "Antimón";
                break;
            case 52:
                skElementName = "Telúr";
                break;
            case 53:
                skElementName = "Jód";
                break;
            case 54:
                skElementName = "Xenón";
                break;
            case 55:
                skElementName = "Cézium";
                break;
            case 56:
                skElementName = "Bárium";
                break;
            case 57:
                skElementName = "Lantán";
                break;
            case 58:
                skElementName = "Cér";
                break;
            case 59:
                skElementName = "Praseodým";
                break;
            case 60:
                skElementName = "Neodým";
                break;
            case 61:
                skElementName = "Prométium";
                break;
            case 62:
                skElementName = "Samárium";
                break;
            case 63:
                skElementName = "Európium";
                break;
            case 64:
                skElementName = "Gadolínium";
                break;
            case 65:
                skElementName = "Terbium";
                break;
            case 66:
                skElementName = "Dysprózium";
                break;
            case 67:
                skElementName = "Holmium";
                break;
            case 68:
                skElementName = "Erbium";
                break;
            case 69:
                skElementName = "Túlium";
                break;
            case 70:
                skElementName = "Yterbium";
                break;
            case 71:
                skElementName = "Lutécium";
                break;
            case 72:
                skElementName = "Hafnium";
                break;
            case 73:
                skElementName = "Tantal";
                break;
            case 74:
                skElementName = "Volfrám";
                break;
            case 75:
                skElementName = "Rénium";
                break;
            case 76:
                skElementName = "Osmium";
                break;
            case 77:
                skElementName = "Irídium";
                break;
            case 78:
                skElementName = "Platina";
                break;
            case 79:
                skElementName = "Zlato";
                break;
            case 80:
                skElementName = "Ortuť";
                break;
            case 81:
                skElementName = "Tálium";
                break;
            case 82:
                skElementName = "Olovo";
                break;
            case 83:
                skElementName = "Bismut";
                break;
            case 84:
                skElementName = "Polónium";
                break;
            case 85:
                skElementName = "Astat";
                break;
            case 86:
                skElementName = "Radón";
                break;
            case 87:
                skElementName = "Francium";
                break;
            case 88:
                skElementName = "Rádium";
                break;
            case 89:
                skElementName = "Aktínium";
                break;
            case 90:
                skElementName = "Tórium";
                break;
            case 91:
                skElementName = "Protaktínium";
                break;
            case 92:
                skElementName = "Urán";
                break;
            case 93:
                skElementName = "Neptúnium";
                break;
            case 94:
                skElementName = "Plutónium";
                break;
            case 95:
                skElementName = "Amerícium";
                break;
            case 96:
                skElementName = "Curium";
                break;
            case 97:
                skElementName = "Berkélium";
                break;
            case 98:
                skElementName = "Kalifornium";
                break;
            case 99:
                skElementName = "Einsteinium";
                break;
            case 100:
                skElementName = "Fermium";
                break;
            case 101:
                skElementName = "Mendelévium";
                break;
            case 102:
                skElementName = "Nobélium";
                break;
            case 103:
                skElementName = "Lawrencium";
                break;
            case 104:
                skElementName = "Rutherfordium";
                break;
            case 105:
                skElementName = "Dubnium";
                break;
            case 106:
                skElementName = "Seaborgium";
                break;
            case 107:
                skElementName = "Bohrium";
                break;
            case 108:
                skElementName = "Hásium";
                break;
            case 109:
                skElementName = "Meitnérium";
                break;
            case 110:
                skElementName = "Darmštádtium";
                break;
            case 111:
                skElementName = "Roentgénium";
                break;
            case 112:
                skElementName = "Kopernícium";
                break;
            case 113:
                skElementName = "Nihónium";
                break;
            case 114:
                skElementName = "Fleróvium";
                break;
            case 115:
                skElementName = "Moskóvium";
                break;
            case 116:
                skElementName = "Livermórium";
                break;
            case 117:
                skElementName = "Tenés";
                break;
            case 118:
                skElementName = "Oganesón";
                break;
        }
        return skElementName;
    }
    #endregion
}
