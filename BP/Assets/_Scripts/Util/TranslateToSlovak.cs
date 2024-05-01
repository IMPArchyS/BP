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
}
